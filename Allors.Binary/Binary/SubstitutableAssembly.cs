// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SubstitutableAssembly.cs" company="allors bvba">
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
    using System;
    using System.Collections;

    using Mono.Cecil;

    public class SubstitutableAssembly : CollectionBase
    {
        private readonly string assemblyFileName;
        private readonly AssemblyDefinition assemblyDefinition;

        public SubstitutableAssembly(string assemblyFileName)
        {
            this.assemblyFileName = assemblyFileName;

            this.assemblyDefinition = AssemblyDefinition.ReadAssembly(assemblyFileName);

            ////_assemblyDefinition.MainModule.LoadSymbols();
            foreach (TypeDefinition typeDefinition in Helper.GetAllTypes(this.assemblyDefinition.MainModule))
            {
                if (!typeDefinition.IsInterface)
                {
                    var substitutableClass = new SubstitutableClass(this.assemblyDefinition, typeDefinition);
                    Console.Out.WriteLine(substitutableClass.ToString());
                    List.Add(substitutableClass);
                }
            }
        }

        public AssemblyDefinition AssemblyDefinition
        {
            get { return this.assemblyDefinition; }
        }

        public override string ToString()
        {
            return this.assemblyDefinition.ToString();
        }

        public SubstitutableClass this[int index]
        {
            get { return (SubstitutableClass)List[index]; }
        }

        public void Substitute(Substitutes substitutes)
        {
            foreach (SubstitutableClass binaryType in this)
            {
                binaryType.Substitute(substitutes);
            }
        }

        public void Save()
        {
            this.assemblyDefinition.Write(this.assemblyFileName);
        }

        public void Save(string newFileName)
        {
            this.assemblyDefinition.Write(newFileName);
        }
    }
}