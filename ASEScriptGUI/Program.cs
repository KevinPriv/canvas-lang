// ***********************************************************************
// Assembly         : ASEScriptGUI
// Author           : Kevin Brewster
// Created          : 11-07-2022
//
// Last Modified By : Kevin Brewster
// Last Modified On : 11-21-2022
// ***********************************************************************
// <copyright file="Program.cs" company="ASEScriptGUI">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace ASEScriptGUI
{
    /// <summary>
    /// The main class of the entry point for the application.
    /// </summary>
    public static class Program
    {

        /// <summary>
        /// The window
        /// </summary>
        public static ScriptingWindow window = new ScriptingWindow();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(window);
        }
    }
}