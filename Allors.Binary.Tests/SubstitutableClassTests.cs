// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SubstitutableClassTests.cs" company="allors bvba">
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
    using System;
    using System.IO;
    using System.Reflection;

    using Allors.Binary.Tests.SubstitutableAssembly;

    using NUnit.Framework;

    [TestFixture]
    public class SubstitutableClassTests
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
        public void SubstituteClassRefelection()
        {
            Assembly assembly = Assembly.LoadFile(_substitutableAssemblyFileInfo.FullName);

            Type testFormType = assembly.GetType("Allors.Binary.Tests.SubstitutableAssembly.TestForm");

            FieldInfo constructorCalledFieldInfo = testFormType.GetField("constructorCalled", BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetField);
            FieldInfo baseConstructorCalledFieldInfo = testFormType.GetField("baseConstructorCalled", BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetField);
            FieldInfo assemblyConstructorCalledFieldInfo = testFormType.GetField("assemblyConstructorCalled", BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetField);

            FieldInfo button1FieldInfo = testFormType.GetField("button1", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetField);
            FieldInfo textBox1FieldInfo = testFormType.GetField("textBox1", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetField);
            FieldInfo nadaFieldInfo = testFormType.GetField("nada", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetField);
            FieldInfo sealedSingleFieldInfo = testFormType.GetField("sealedSingle", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetField);
            FieldInfo sealedHierarchyFieldInfo = testFormType.GetField("sealedHierarchy", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetField);

            object testForm = Activator.CreateInstance(testFormType);
            Assert.AreEqual("Allors.Binary.Tests.SubstituteAssembly.Form", testForm.GetType().BaseType.FullName);

            bool constructorCalled = (bool) constructorCalledFieldInfo.GetValue(testForm);
            Assert.IsTrue(constructorCalled);

            bool baseConstructorCalled = (bool) baseConstructorCalledFieldInfo.GetValue(testForm);
            Assert.IsTrue(baseConstructorCalled);

            bool assemblyConstructorCalled = (bool) assemblyConstructorCalledFieldInfo.GetValue(testForm);
            Assert.IsTrue(assemblyConstructorCalled);

            Assert.AreEqual("Allors.Binary.Tests.ReferencedAssembly.Button", button1FieldInfo.FieldType.FullName);
            object button1 = button1FieldInfo.GetValue(testForm);
            Assert.AreEqual("Allors.Binary.Tests.SubstituteAssembly.Button", button1.GetType().FullName);

            Assert.AreEqual("Allors.Binary.Tests.ReferencedAssembly.TextBox", textBox1FieldInfo.FieldType.FullName);
            object textBox1 = textBox1FieldInfo.GetValue(testForm);
            Assert.AreEqual("Allors.Binary.Tests.ReferencedAssembly.TextBox", textBox1.GetType().FullName);

            Assert.AreEqual("Allors.Binary.Tests.SubstitutableAssembly.Nada", nadaFieldInfo.FieldType.FullName);
            object nada = nadaFieldInfo.GetValue(testForm);
            Assert.AreEqual("Allors.Binary.Tests.SubstitutableAssembly.Nada", nada.GetType().FullName);
            
            Assert.AreEqual("Allors.Binary.Tests.SubstituteAssembly.SealedSingle", sealedSingleFieldInfo.FieldType.FullName);
            object sealedSingle = sealedSingleFieldInfo.GetValue(testForm);
            Assert.AreEqual("Allors.Binary.Tests.SubstituteAssembly.SealedSingle", sealedSingle.GetType().FullName);

            Assert.AreEqual("Allors.Binary.Tests.SubstituteAssembly.SealedHierarchy", sealedHierarchyFieldInfo.FieldType.FullName);
            object sealedHierarchy = sealedHierarchyFieldInfo.GetValue(testForm);
            Assert.AreEqual("Allors.Binary.Tests.SubstituteAssembly.SealedHierarchy", sealedHierarchy.GetType().FullName);
        }

        [Test]
        public void SubstituteClass()
        {
            TestForm testForm = new TestForm();

            Assert.AreEqual("Allors.Binary.Tests.SubstituteAssembly.Form", testForm.GetType().BaseType.FullName);

            Assert.AreEqual("Allors.Binary.Tests.SubstituteAssembly.Button", testForm.Button1.GetType().FullName);
            Assert.AreEqual("Allors.Binary.Tests.ReferencedAssembly.TextBox", testForm.TextBox1.GetType().FullName);
            Assert.AreEqual("Allors.Binary.Tests.SubstitutableAssembly.Nada", testForm.Nada.GetType().FullName);
            Assert.AreEqual("Allors.Binary.Tests.SubstituteAssembly.SealedSingle", testForm.SealedSingle.GetType().FullName);
            Assert.AreEqual("Allors.Binary.Tests.SubstituteAssembly.SealedHierarchy", testForm.SealedHierarchy.GetType().FullName);

            Nada nada = new Nada();

            Assert.AreEqual("1", nada.TestSealedSingle("1"));
            Assert.AreEqual("2", nada.TestSealedHierarchy("2"));
            Assert.AreEqual("3", nada.TestSealedHierarchyAbstract("3"));
        }
    }
}