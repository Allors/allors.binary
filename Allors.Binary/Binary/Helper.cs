// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Helper.cs" company="allors bvba">
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
    using System.Collections.Generic;

    using Mono.Cecil;

    /// <summary>
    /// Because we can not use the Mono.Cecil.Rocks project because
    /// of the .Net 2.0 compatibility, we will add helper methods to this class.
    /// </summary>
    public static class Helper
    {
        public static List<TypeDefinition> GetAllTypes(ModuleDefinition moduleDefinition)
        {
            List<TypeDefinition> typeDefinitions = new List<TypeDefinition>();
            foreach (TypeDefinition typeDefinition in moduleDefinition.Types)
            {
                if (!"<Module>".Equals(typeDefinition.Name))
                {
                    typeDefinitions.Add(typeDefinition);
                    GetAllTypes(typeDefinitions, typeDefinition);
                }
            }

            return typeDefinitions;
        }

        private static void GetAllTypes(List<TypeDefinition> typeDefinitions, TypeDefinition parentTypeDefinition)
        {
            foreach (TypeDefinition typeDefinition in parentTypeDefinition.NestedTypes)
            {
                if (!"<Module>".Equals(typeDefinition.Name))
                {
                    typeDefinitions.Add(typeDefinition);
                    GetAllTypes(typeDefinitions, typeDefinition);
                }
            }
        }

        /// <summary>
        /// Gets all the contstructors for this type definition.
        /// </summary>
        /// <param name="typeDefinition">
        /// The type definition.
        /// </param>
        /// <returns>
        /// The constructors.
        /// </returns>
        public static List<MethodDefinition> GetContructors(TypeDefinition typeDefinition)
        {
            List<MethodDefinition> methodDefinitions = new List<MethodDefinition>();
            foreach (MethodDefinition methodDefinition in typeDefinition.Methods)
            {
                if (methodDefinition.IsConstructor)
                {
                    methodDefinitions.Add(methodDefinition);
                }
            }

            return methodDefinitions;
        }

        /// <summary>
        /// Gets the first contstructor for this type definition.
        /// </summary>
        /// <param name="typeDefinition">
        /// The type definition.
        /// </param>
        /// <returns>
        /// The first constructor.
        /// </returns>
        public static MethodDefinition GetFirstContructor(TypeDefinition typeDefinition)
        {
            List<MethodDefinition> constructors = GetContructors(typeDefinition);
            if (constructors.Count > 0)
            {
                return constructors[0];
            }

            return null;
        }
    }
}