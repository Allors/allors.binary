// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Substitute.cs" company="allors bvba">
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
namespace Allors.Binary
{
    using System;
    using System.IO;

    using Microsoft.Build.Framework;

    public class Substitute : ITask
    {
        public IBuildEngine BuildEngine { get; set; }

        public ITaskHost HostObject { get; set; }

        [Required]
        public string Input { get; set; }

        public string Output { get; set; }

        [Required]
        public string Substitutes { get; set; }

        public bool Execute()
        {
            try
            {
                var inputFileInfo = new FileInfo(this.Input);
                var substitutesFileInfo = new FileInfo(this.Substitutes);
                FileInfo outputFileInfo = null;
                if (!string.IsNullOrWhiteSpace(this.Output))
                {
                    outputFileInfo = new FileInfo(this.Output);
                }

                var makeSurePdbDllIsBound = new Mono.Cecil.Pdb.PdbReaderProvider().ToString();
                var makeSureMdbDllIsBound = new Mono.Cecil.Mdb.MdbReaderProvider().ToString();

                var substitutableAssembly = new SubstitutableAssembly(inputFileInfo.FullName);
                var substitutes = new Substitutes(substitutesFileInfo.FullName);

                substitutableAssembly.Substitute(substitutes);

                if (outputFileInfo != null)
                {
                    substitutableAssembly.Save(outputFileInfo.FullName);
                }
                else
                {
                    substitutableAssembly.Save();
                }

                return true;
            }
            catch(Exception e)
            {
                Console.Out.WriteLine(e.Message + "\n" + e.StackTrace);
                return false;
            }
        }
    }
}