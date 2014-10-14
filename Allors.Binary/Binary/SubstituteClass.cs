// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SubstituteClass.cs" company="allors bvba">
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

    using Allors.Binary.Attributes;

    using Mono.Cecil;

    public class SubstituteClass
    {
        private readonly Type type;
        private readonly TypeDefinition typeDefinition;
        private readonly string substitutableFullName;
        private readonly bool isBaseSubsitution;

        public SubstituteClass(AssemblyDefinition assemblyDefinition, Type type)
        {
            this.type = type;
            this.typeDefinition = assemblyDefinition.MainModule.GetType(type.FullName);

            object[] attributes = type.GetCustomAttributes(typeof(SubstituteClassAttribute), true);
            SubstituteClassAttribute attribute = (SubstituteClassAttribute)attributes[0];
            if (attribute.SubstitutableType != null)
            {
                this.isBaseSubsitution = false;
                this.substitutableFullName = attribute.SubstitutableType.FullName;
            }
            else
            {
                if (type.BaseType == null)
                {
                    throw new Exception("Base type is required");
                }

                this.isBaseSubsitution = true;
                this.substitutableFullName = type.BaseType.FullName;
            }
        }

        public Type Type
        {
            get { return this.type; }
        }

        public TypeDefinition TypeDefinition
        {
            get { return this.typeDefinition; }
        }

        public string SubstitutableFullName
        {
            get
            {
                return this.substitutableFullName;
            }
        }

        public MethodDefinition Contructor
        {
            get { return Helper.GetFirstContructor(this.typeDefinition); }
        }

        public bool IsBaseSubsitution
        {
            get { return this.isBaseSubsitution; }
        }

        public override string ToString()
        {
            return this.typeDefinition.ToString();
        }
    }
}