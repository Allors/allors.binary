// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestForm.cs" company="allors bvba">
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
namespace Allors.Binary.Tests.SubstitutableAssembly
{
    using Allors.Binary.Tests.ReferencedAssembly;

    public partial class TestForm : Form
    {
        public bool constructorCalled = false;

        private Button button1;
        private TextBox textBox1;
        private Nada nada;
        private SealedSingle sealedSingle;
        private SealedHierarchy sealedHierarchy;

        public TestForm()
        {
            constructorCalled = true;

            button1 = new Button();
            textBox1 = new TextBox();
            nada = new Nada();
            sealedSingle = new SealedSingle();
            sealedHierarchy = new SealedHierarchy();
        }

        public static string ShowMessageBox(bool boolean)
        {
            return MessageBox.Show(boolean);
        }

        public static string ShowMessageBox(string text)
        {
            return MessageBox.Show(text);
        }

        public static string ShowMessageBox(int integer)
        {
            return MessageBox.Show(integer);
        }

        public static string ShowMessageBox(string text, int integer)
        {
            return MessageBox.Show(text, integer);
        }


        public static string ShowMessageBox2(bool boolean)
        {
            return MessageBox.Show2(boolean);
        }

        public static string ShowMessageBox2(string text)
        {
            return MessageBox.Show2(text);
        }

        public static string ShowMessageBox2(int integer)
        {
            return MessageBox.Show2(integer);
        }

        public static string ShowMessageBox2(string text, int integer)
        {
            return MessageBox.Show2(text, integer);
        }

        #region Properties
        public Button Button1
        {
            get { return button1; }
        }
        public TextBox TextBox1
        {
            get { return textBox1; }
        }
        public Nada Nada
        {
            get { return nada; }
        }
        public SealedSingle SealedSingle
        {
            get { return sealedSingle; }
        }
        public SealedHierarchy SealedHierarchy
        {
            get { return sealedHierarchy; }
        }
        #endregion

        public string CallShowDialog()
        {
            return ShowDialog();
        }
    }
}