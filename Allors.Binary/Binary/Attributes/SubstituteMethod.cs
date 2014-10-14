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

namespace Allors.Binary.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Method)]
    public class SubstituteMethodAttribute : Attribute
    {
        private readonly Type _substitutableType;
        private readonly string _substituableMethodName;

        public SubstituteMethodAttribute(Type type)
        {
            _substitutableType = type;
        }

        public SubstituteMethodAttribute(Type type, string methodName) : this(type)
        {
            _substituableMethodName = methodName;
        }

        public Type SubstitutableType
        {
            get { return _substitutableType; }
        }

        public string SubstituableMethodName
        {
            get { return _substituableMethodName; }
        }
    }
}