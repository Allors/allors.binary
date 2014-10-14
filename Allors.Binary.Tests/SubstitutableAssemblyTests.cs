// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SubstitutableAssemblyTests.cs" company="allors bvba">
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
namespace Allors.Binary.Tests
{
    using System.IO;

    using NUnit.Framework;

    [TestFixture]
    public class SubstitutableAssemblyTests
    {
        private FileInfo _substitutesAssemblyFileInfo;
        private Substitutes _substitutes;

        private FileInfo _substitutableAssemblyFileInfo;
        private Binary.SubstitutableAssembly _substitutableAssembly;

        [SetUp]
        public void SetUp()
        {
            _substitutesAssemblyFileInfo = new FileInfo(Constants.Substitutes);
            _substitutes = new Substitutes(_substitutesAssemblyFileInfo.FullName);

            _substitutableAssemblyFileInfo = new FileInfo(Constants.Substitutables);
            _substitutableAssembly = new Binary.SubstitutableAssembly(_substitutableAssemblyFileInfo.FullName);
        }

        [Test]
        public void Default()
        {
            Assert.AreEqual(5, _substitutes.SubstituteClasses.Count);

            SubstituteClass formSubstitute = _substitutes.SubstituteClasses["Allors.Binary.Tests.SubstituteAssembly.Form"];
            SubstituteClass buttonSubstitute = _substitutes.SubstituteClasses["Allors.Binary.Tests.SubstituteAssembly.Button"];
            SubstituteClass sealedSingleSubstitute = _substitutes.SubstituteClasses["Allors.Binary.Tests.SubstituteAssembly.SealedSingle"];
            SubstituteClass sealedHierarchySubstitute = _substitutes.SubstituteClasses["Allors.Binary.Tests.SubstituteAssembly.SealedHierarchy"];
            SubstituteClass sealedAbstractClassSubstitute = _substitutes.SubstituteClasses["Allors.Binary.Tests.SubstituteAssembly.SealedAbstractClass"];

            Assert.IsNotNull(formSubstitute);
            Assert.IsNotNull(buttonSubstitute);
            Assert.IsNotNull(sealedSingleSubstitute);
            Assert.IsNotNull(sealedHierarchySubstitute);
            Assert.IsNotNull(sealedAbstractClassSubstitute);
        }

    }
}