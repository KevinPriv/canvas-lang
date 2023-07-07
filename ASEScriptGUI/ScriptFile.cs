// ***********************************************************************
// Assembly         : ASEScriptGUI
// Author           : Kevin Brewster
// Created          : 11-20-2022
//
// Last Modified By : Kevin Brewster
// Last Modified On : 11-21-2022
// ***********************************************************************
// <copyright file="ScriptFile.cs" company="ASEScriptGUI">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Adds reading and writing functionality
/// </summary>
public class ScriptFile {
    /// <summary>
    /// Reads file from stream reader
    /// </summary>
    /// <param name="reader">reader</param>
    /// <returns>array of lines read from file</returns>
    public static string[] Read(StreamReader reader)
    {
        List<string> lines = new List<string>();

        string line = reader.ReadLine();
        //Continue to read until you reach end of file
        while (line != null)
        {
            lines.Add(line);
            //Read the next line
            line = reader.ReadLine();
        }
        return lines.ToArray();
    }

    /// <summary>
    /// Writes file from streamwriter
    /// </summary>
    /// <param name="writer">writer</param>
    /// <param name="script">the array of lines being written</param>
    public static void Write(StreamWriter writer, string[] script)
    {
        foreach(string line in script)
        {
            writer.WriteLine(line);
        }
    }

}

