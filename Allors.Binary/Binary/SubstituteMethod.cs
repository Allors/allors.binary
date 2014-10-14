// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SubstituteMethod.cs" company="allors bvba">
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
    using Mono.Cecil;

    public class SubstituteMethod
    {
        private readonly MethodDefinition methodDefinition;
        private readonly string fullTypeName;
        private readonly string methodName;

        public SubstituteMethod(MethodDefinition methodDefinition, CustomAttribute customAttribute)
        {
            this.methodDefinition = methodDefinition;
            TypeReference typeDefinition = (TypeReference)customAttribute.ConstructorArguments[0].Value;
            this.fullTypeName = typeDefinition.FullName;
            
            this.methodName = methodDefinition.Name;
            if (customAttribute.ConstructorArguments.Count > 1)
            {
                this.methodName = (string)customAttribute.ConstructorArguments[1].Value;
            }
        }

        public MethodDefinition MethodDefinition
        {
            get { return this.methodDefinition; }
        }

        public override string ToString()
        {
            return this.methodDefinition.ToString();
        }

        internal bool Matches(MethodReference operand)
        {
            if (this.methodName.Equals(operand.Name))
            {
                if (this.fullTypeName.Equals(operand.DeclaringType.FullName))
                {
                    if (this.methodDefinition.Parameters.Count == operand.Parameters.Count)
                    {
                        for (int i = 0; i < this.methodDefinition.Parameters.Count; i++)
                        {
                            if (!this.methodDefinition.Parameters[i].ParameterType.FullName.Equals(operand.Parameters[i].ParameterType.FullName))
                            {
                                return false;
                            }
                        }

                        return true;
                    }
                }
            }

            return false;
        }
    }
}