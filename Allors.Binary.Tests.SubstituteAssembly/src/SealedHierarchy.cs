// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SealedHierarchy.cs" company="allors bvba">
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
namespace Allors.Binary.Tests.SubstituteAssembly
{
    using Allors.Binary.Attributes;

    [SubstituteClass(typeof(Allors.Binary.Tests.ReferencedAssembly.SealedAbstractClass))]
    public class SealedAbstractClass
    {
        public string abstractField;

        private string abstractProperty;

        public string AbstractProperty
        {
            get { return abstractProperty; }
            set { abstractProperty = value; }
        }

        public SealedAbstractClass()
        {
            abstractField = "Substitute";
            abstractProperty = "Substitute";
        }
    }
    
    [SubstituteClass(typeof(Allors.Binary.Tests.ReferencedAssembly.SealedHierarchy))]
    public class SealedHierarchy : SealedAbstractClass
    {
        public string field;

        private string property;

        public string Property
        {
            get { return property; }
            set { property = value; }
        }

        public SealedHierarchy()
        {
            field = "Substitute";
            property = "Substitute";
        }
    }
}