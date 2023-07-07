// ***********************************************************************
// Assembly         : ASEScriptParser
// Author           : Kevin Brewster
// Created          : 01-16-2023
//
// Last Modified By : Kevin Brewster
// Last Modified On : 01-16-2023
// ***********************************************************************
// <copyright file="ScriptEnviornment.cs" company="ASEScriptParser">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************


using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;


/// <summary>
/// Used to execute a language script.
/// </summary>
public class ScriptEnvironment
{
    /// <summary>
    /// Predefined commands inside of the language
    /// </summary>
    private CommandDispatcher commandDispatcher;

    /// <summary>
    /// Initializes a new instance of the <see cref="ScriptEnvironment"/> class.
    /// </summary>
    /// <param name="commandDispatcher">The command dispatcher.</param>
    public ScriptEnvironment(CommandDispatcher commandDispatcher)
    {
        this.commandDispatcher = commandDispatcher;
    }

    /// <summary>
    /// Executes the script and then pipelines the process (Lexing -&gt; Parsing AST -&gt; Interpreting)
    /// </summary>
    /// <param name="script">Script to be evaluated</param>
    public void Execute(string script)
    {
        List<Token> tokens = new Lexer().Lex(script);
        BlockNode ast = new Parser(tokens).Parse();
        Interpreter interpreter = new Interpreter(commandDispatcher, ast);
        interpreter.Execute();
    }
}
