// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SealedSingle.cs" company="allors bvba">
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
    using System;

    using Allors.Binary.Attributes;

    [SubstituteClass(typeof(Allors.Binary.Tests.ReferencedAssembly.SealedSingle))]
    public class SealedSingle
    {

        public string field;

        private int intProperty;
        private string property;

        public int IntProperty
        {
            get
            {
                return intProperty;
            }

            set
            {
                intProperty = value;
                property.ToString();
            }
        }

        public string Property
        {
            get
            {
                return property;
            }

            set
            {
                property = value;
                intProperty = int.Parse(value);
            }
        }

        public SealedSingle()
        {
            field = "Substitute";
            property = "Substitute";
        }
    }
}