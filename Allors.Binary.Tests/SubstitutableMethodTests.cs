// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SubstitutableMethodTests.cs" company="allors bvba">
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

    using Allors.Binary.Tests.SubstitutableAssembly;

    using NUnit.Framework;

    [TestFixture]
    public class SubstitutableMethodTests  
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
        public void SubstituteWithType()
        {
            Assert.AreEqual("Referenced: Show(False)", TestForm.ShowMessageBox(false));

            Assert.AreEqual("Substitute: Referenced: Show(Test)", TestForm.ShowMessageBox("Test"));
            Assert.AreEqual("Substitute: Referenced: Show(0)", TestForm.ShowMessageBox(0));
            Assert.AreEqual("Substitute: Referenced: Show(Test 0)", TestForm.ShowMessageBox("Test", 0));

            Assert.AreEqual("Substitute: Referenced: Show2(Test)", TestForm.ShowMessageBox2("Test"));
            Assert.AreEqual("Substitute: Referenced: Show2(0)", TestForm.ShowMessageBox2(0));
            Assert.AreEqual("Substitute: Referenced: Show2(Test 0)", TestForm.ShowMessageBox2("Test", 0));
        }

        [Test]
        public void SubstituteWithTypeAndMethodName()
        {
            TestForm testForm = new TestForm();
            Assert.AreEqual("Substitute: Referenced: ShowDialog()", testForm.CallShowDialog());
        }

    }
}