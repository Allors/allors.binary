// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageBox.cs" company="allors bvba">
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
    public class MessageBox
    {
        public static string Show(bool boolean)
        {
            return "Referenced: Show(" + boolean + ")";
        }

        public static string Show(string text)
        {
            return "Referenced: Show(" + text + ")";
        }

        public static string Show(int count)
        {
            return "Referenced: Show(" + count + ")";
        }

        public static string Show(string text, int count)
        {
            return "Referenced: Show(" + text + " " + count + ")";
        }


        public static string Show2(bool boolean)
        {
            return "Referenced: Show2(" + boolean + ")";
        }

        public static string Show2(string text)
        {
            return "Referenced: Show2(" + text + ")";
        }

        public static string Show2(int count)
        {
            return "Referenced: Show2(" + count + ")";
        }

        public static string Show2(string text, int count)
        {
            return "Referenced: Show2(" + text + " " + count + ")";
        }

    }
}