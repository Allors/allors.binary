// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SubstituteClassTests.cs" company="allors bvba">
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
    public class SubstituteClassTests
    {
        private FileInfo _substitutesAssemblyFileInfo;
        private Substitutes _substitutes;

        [SetUp]
        public void SetUp()
        {
            _substitutesAssemblyFileInfo = new FileInfo(Constants.Substitutes);
            _substitutes = new Substitutes(_substitutesAssemblyFileInfo.FullName);
        }

        [Test]
        public void SubstitutableFullName()
        {
            SubstituteClass substitute = _substitutes.SubstituteClasses["Allors.Binary.Tests.SubstituteAssembly.Button"];

            Assert.AreEqual("Allors.Binary.Tests.ReferencedAssembly.Button", substitute.SubstitutableFullName);

            Assert.AreNotEqual("Allors.Binary.Tests.SubstituteAssembly.Button", substitute.SubstitutableFullName);
            Assert.AreNotEqual("Button", substitute.SubstitutableFullName);
        }
    }
}