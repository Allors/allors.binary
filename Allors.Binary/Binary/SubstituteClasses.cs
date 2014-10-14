// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SubstituteClasses.cs" company="allors bvba">
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
    using System.Reflection;
    using System.Text;

    using Allors.Binary.Attributes;

    using Mono.Cecil;

    public class SubstituteClasses : CollectionBase
    {
        private readonly Assembly assembly;
        private readonly AssemblyDefinition assemblyDefinition;

        internal SubstituteClasses(string assemblyFileName)
        {
            this.assembly = Assembly.LoadFrom(assemblyFileName);
            this.assemblyDefinition = AssemblyDefinition.ReadAssembly(assemblyFileName);

            foreach (Type type in Assembly.GetTypes())
            {
                object[] attributes = type.GetCustomAttributes(typeof(SubstituteClassAttribute), true);
                if (attributes.Length > 0)
                {
                    SubstituteClass substitute = new SubstituteClass(this.assemblyDefinition, type);
                    InnerList.Add(substitute);
                }
            }
        }

        public Assembly Assembly
        {
            get { return this.assembly; }
        }

        public AssemblyDefinition AssemblyDefinition
        {
            get { return this.assemblyDefinition; }
        }

        public override string ToString()
        {
            StringBuilder toString = new StringBuilder();
            foreach (SubstituteClass substituteClass in this)
            {
                toString.Append(substituteClass.ToString());
                toString.Append("\n");
            }

            return toString.ToString();
        }

        public SubstituteClass this[int index]
        {
            get { return (SubstituteClass)List[index]; }
        }

        public SubstituteClass this[string typeFullName]
        {
            get
            {
                foreach (SubstituteClass substitute in this)
                {
                    if (string.Equals(typeFullName, substitute.Type.FullName))
                    {
                        return substitute;
                    }
                }

                return null;
            }
        }

        public SubstituteClass LookupBySubstitutableFullName(string substitutableFullName)
        {
            foreach (SubstituteClass substitute in this)
            {
                if (substitutableFullName != null && substitutableFullName.Equals(substitute.SubstitutableFullName))
                {
                    return substitute;
                }
            }

            return null;
        }
    }
}