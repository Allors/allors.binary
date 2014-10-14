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
namespace Allors.Binary.Tests.ReferencedAssembly
{
    using System;

    public abstract class SealedAbstractClass
    {
        public string abstractField;
        
        public string AbstractProperty
        {
            get { throw new Exception("This property should be sustituted!"); }
            set { throw new Exception("This property should be sustituted!"); }
        }

        public SealedAbstractClass()
        {
            abstractField = "Referenced";
        }
    }
        
    public sealed class SealedHierarchy : SealedAbstractClass
    {
        public string field;

       
        public string Property
        {
            get { throw new Exception("This property should be sustituted!"); }
            set { throw new Exception("This property should be sustituted!"); }
        }

        public SealedHierarchy()
        {
            field = "Referenced";
        }
    }
}