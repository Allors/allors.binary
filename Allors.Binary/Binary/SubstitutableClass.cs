// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SubstitutableClass.cs" company="allors bvba">
//   Copyright 2008-2014 Allors bvba.
//   
//   This program is free software: you can redistribute it and/or modify
//   it under the terms of the GNU Lesser General Public License as published by
//   the Free Software Foundation, either version 3 of the License, or
//   (at your option) any later version.
//   
//   This program is distributed in the hope that it will be useful,
//   but WITHOUT ANY WARRANTY; without even the implied warranty of
//   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//   GNU Lesser General Public License for more details.
//   
//   You should have received a copy of the GNU Lesser General Public License
//   along with this program.  If not, see http://www.gnu.org/licenses.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Binary
{
    using System.Collections;

    using Mono.Cecil;
    using Mono.Cecil.Cil;

    public class SubstitutableClass
    {
        private readonly AssemblyDefinition assemblyDefinition;
        private readonly TypeDefinition typeDefinition;

        public SubstitutableClass(AssemblyDefinition assemblyDefinition, TypeDefinition typeDefinition)
        {
            this.assemblyDefinition = assemblyDefinition;
            this.typeDefinition = typeDefinition;
        }

        public TypeDefinition TypeDefinition
        {
            get { return this.typeDefinition; }
        }

        public override string ToString()
        {
            return this.typeDefinition.ToString();
        }

        internal void Substitute(Substitutes substitutes)
        {
            if (this.typeDefinition.Name != "<Module>")
            {
                this.SubstituteConstructors(substitutes);
                this.SubstituteBaseType(substitutes);
                this.SubstituteMethods(substitutes);
                this.SubstituteFields(substitutes);
                this.SubstituteLocalVariables(substitutes);
            }
        }

        private void SubstituteConstructors(Substitutes substitutes)
        {
            foreach (MethodDefinition constructor in Helper.GetContructors(this.typeDefinition))
            {
                if (constructor.HasBody)
                {
                    foreach (Instruction instruction in constructor.Body.Instructions)
                    {
                        if (instruction.OpCode.Equals(OpCodes.Call))
                        {
                            MethodReference operand = (MethodReference)instruction.Operand;
                            TypeReference operandDeclaringType = operand.DeclaringType;

                            SubstituteClass substitute = substitutes.SubstituteClasses.LookupBySubstitutableFullName(operandDeclaringType.FullName);

                            if (substitute != null)
                            {
                                // TODO: Migration check
                                if (operand.Name.Equals(".ctor"))
                                {
                                    MethodDefinition substituteConstructorDefinition = substitute.Contructor;
                                    MethodReference substituteConstructorReference = this.assemblyDefinition.MainModule.Import(substituteConstructorDefinition);
                                    instruction.Operand = substituteConstructorReference;
                                }
                            }
                        }
                    }
                }
            }

            ArrayList constructorsAndMethods = new ArrayList(Helper.GetContructors(this.typeDefinition));
            constructorsAndMethods.AddRange(this.typeDefinition.Methods);

            foreach (MethodDefinition method in constructorsAndMethods)
            {
                if (method.HasBody)
                {
                    foreach (Instruction instruction in method.Body.Instructions)
                    {
                        if (instruction.OpCode.Equals(OpCodes.Newobj))
                        {
                            MethodReference operand = (MethodReference)instruction.Operand;
                            TypeReference operandDeclaringType = operand.DeclaringType;

                            SubstituteClass substitute = substitutes.SubstituteClasses.LookupBySubstitutableFullName(operandDeclaringType.FullName);

                            if (substitute != null)
                            {
                                MethodDefinition substituteConstructorDefinition = substitute.Contructor;
                                MethodReference substituteConstructorReference = this.assemblyDefinition.MainModule.Import(substituteConstructorDefinition);
                                instruction.Operand = substituteConstructorReference;
                            }
                        }
                    }
                }
            }
        }

        private void SubstituteBaseType(Substitutes substitutes)
        {
            TypeReference baseTypeReference = this.typeDefinition.BaseType;
            SubstituteClass substitute = substitutes.SubstituteClasses.LookupBySubstitutableFullName(baseTypeReference.FullName);

            if (substitute != null)
            {
                TypeReference substituteTypeReference = this.assemblyDefinition.MainModule.Import(substitute.Type);
                this.typeDefinition.BaseType = substituteTypeReference;
            }
        }

        private void SubstituteMethods(Substitutes substitutes)
        {
            ArrayList constructorsAndMethods = new ArrayList(Helper.GetContructors(this.typeDefinition));
            constructorsAndMethods.AddRange(this.typeDefinition.Methods);

            foreach (MethodDefinition method in constructorsAndMethods)
            {
                if (method.HasBody)
                {
                    foreach (Instruction instruction in method.Body.Instructions)
                    {
                        if (instruction.OpCode.Equals(OpCodes.Call) ||
                            instruction.OpCode.Equals(OpCodes.Calli) ||
                            instruction.OpCode.Equals(OpCodes.Callvirt))
                        {
                            MethodReference methodReference = (MethodReference)instruction.Operand;

                            SubstituteMethod substitute = substitutes.SubstituteMethods.Lookup(methodReference);
                            if (substitute != null)
                            {
                                MethodReference substituteMethodReference = this.assemblyDefinition.MainModule.Import(substitute.MethodDefinition);
                                instruction.Operand = substituteMethodReference;
                            }
                            else
                            {
                                SubstituteClass declaringClassSubstitute = substitutes.SubstituteClasses.LookupBySubstitutableFullName(methodReference.DeclaringType.FullName);
                                if (declaringClassSubstitute != null && !declaringClassSubstitute.IsBaseSubsitution)
                                {
                                    TypeReference classSubstitueReference = this.assemblyDefinition.MainModule.Import(declaringClassSubstitute.Type);
                                    methodReference.DeclaringType = classSubstitueReference;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void SubstituteFields(Substitutes substitutes)
        {
            foreach (FieldDefinition field in this.typeDefinition.Fields)
            {
                TypeReference fieldTypeReference = field.FieldType;
                SubstituteClass substitute = substitutes.SubstituteClasses.LookupBySubstitutableFullName(fieldTypeReference.FullName);

                if (substitute != null && !substitute.IsBaseSubsitution)
                {
                    TypeReference substituteTypeReference = this.assemblyDefinition.MainModule.Import(substitute.Type);
                    field.FieldType = substituteTypeReference;
                }
            }
        }

        private void SubstituteLocalVariables(Substitutes substitutes)
        {
            ArrayList constructorsAndMethods = new ArrayList(Helper.GetContructors(this.typeDefinition));
            constructorsAndMethods.AddRange(this.typeDefinition.Methods);

            foreach (MethodDefinition method in constructorsAndMethods)
            {
                if (method.HasBody)
                {
                    foreach (VariableDefinition variableDefinition in method.Body.Variables)
                    {
                        SubstituteClass substitute = substitutes.SubstituteClasses.LookupBySubstitutableFullName(variableDefinition.VariableType.FullName);

                        if (substitute != null && !substitute.IsBaseSubsitution)
                        {
                            TypeReference substituteTypeReference = this.assemblyDefinition.MainModule.Import(substitute.Type);
                            variableDefinition.VariableType = substituteTypeReference;
                        }
                    }
                }
            }
        }
    }
}