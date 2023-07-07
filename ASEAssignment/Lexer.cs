// ***********************************************************************
// Assembly         : ASEScriptParser
// Author           : Kevin Brewster
// Created          : 12-10-2022
//
// Last Modified By : Kevin Brewster
// Last Modified On : 01-16-2023
// ***********************************************************************
// <copyright file="Lexer.cs" company="ASEScriptParser">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************


using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;

/// <summary>
/// Data class storing token <see cref="type" /> and <see cref="value" />
/// </summary>
public struct Token
{
    /// <summary>
    /// Type of token
    /// </summary>
    public TokenType type;
    /// <summary>
    /// identifier value of token
    /// </summary>
    public string value;

    /// <summary>
    /// Initializes a new instance of the <see cref="Token"/> struct.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="value">The value.</param>
    public Token(TokenType type, string value) : this()
    {
        this.type = type;
        this.value = value;
    }
}

/// <summary>
/// Builder which processes each character individually to create a string lexeme
/// </summary>
class LexemeMemory
{
    /// <summary>
    /// Stores word data
    /// </summary>
    private StringBuilder data = new StringBuilder();

    /// <summary>
    /// Add character to <see cref="data" />
    /// </summary>
    /// <param name="chr">character to add</param>
    public void AddChar(char chr)
    {
        this.data.Append(chr);
    }

    /// <summary>
    /// Build lexeme from <see cref="data" />
    /// </summary>
    /// <returns>string compiled lexeme</returns>
    public string Build()
    {
        string compiledLexeme = data.ToString();
        Clear();
        return compiledLexeme;
    }

    /// <summary>
    /// Clears lexeme data
    /// </summary>
    public void Clear()
    {
        this.data.Clear();
    }

    /// <summary>
    /// Checks if current lexeme data is empty
    /// </summary>
    /// <returns><c>true</c> if this instance is empty; otherwise, <c>false</c>.</returns>
    public bool IsEmpty()
    {
        return this.data.Length == 0;
    }
}

/// <summary>
/// The type of Token the lexeme is
/// </summary>
public enum TokenType
{
    /// <summary>
    /// The operator
    /// </summary>
    OPERATOR,
    /// <summary>
    /// The keyword
    /// </summary>
    KEYWORD,
    /// <summary>
    /// The identifier
    /// </summary>
    IDENTIFIER,
    /// <summary>
    /// The new line
    /// </summary>
    NEW_LINE,
    /// <summary>
    /// The number
    /// </summary>
    NUMBER,
    /// <summary>
    /// The comment
    /// </summary>
    COMMENT
}

/// <summary>
/// The state of <see cref="LexicalStateMachine" />
/// </summary>
public enum State
{
    /// <summary>
    /// The idle
    /// </summary>
    IDLE,
    /// <summary>
    /// The identifier
    /// </summary>
    IDENTIFIER,
    /// <summary>
    /// The number
    /// </summary>
    NUMBER,
    /// <summary>
    /// The operator
    /// </summary>
    OPERATOR
}

/// <summary>
/// State machine that compiles lexemes into tokens with <see cref="TokenType" />.
/// </summary>
class LexicalStateMachine
{
    /// <summary>
    /// The state
    /// </summary>
    public State state = State.IDLE;

    /// <summary>
    /// stores current data of the unmade token
    /// </summary>
    public LexemeMemory lexemeMemory = new LexemeMemory();

    /// <summary>
    /// stores list of completed tokens
    /// </summary>
    public List<Token> tokens = new List<Token>();

    /// <summary>
    /// Reads characters into lexemeMemory
    /// </summary>
    /// <param name="currentChar">current character</param>
    public void Read(char currentChar)
    {
        switch (state)
        {
            case State.IDLE:
                IdleRead(currentChar);
                break;
            case State.IDENTIFIER:
                IdentifierRead(currentChar);
                break;
            case State.NUMBER:
                NumberRead(currentChar);
                break;
            case State.OPERATOR:
                OperatorRead(currentChar);
                break;
        }
    }

    /// <summary>
    /// Builds lexeme when state is <see cref="State.IDLE" />
    /// </summary>
    /// <param name="currentChar">current character</param>
    private void IdleRead(char currentChar)
    {
        if (IsWhitespace(currentChar))
        {
        }
        else if (IsLetter(currentChar))
        {
            lexemeMemory.AddChar(currentChar);
            state = State.IDENTIFIER;
        }
        else if (IsDigit(currentChar))
        {
            lexemeMemory.AddChar(currentChar);
            state = State.NUMBER;
        }
        else if (IsOperator(currentChar))
        {
            lexemeMemory.AddChar(currentChar);
            state = State.OPERATOR;
        }
    }

    /// <summary>
    /// Builds lexeme when state is <see cref="State.IDENTIFIER" />
    /// </summary>
    /// <param name="currentChar">current character</param>
    private void IdentifierRead(char currentChar)
    {
        if (IsNewline(currentChar))
        {
            PushNewIdentifier();
            PushNewLine();
            this.state = State.IDLE;
        }
        else if (IsWhitespace(currentChar))
        {
            PushNewIdentifier();
            this.state = State.IDLE;
        }
        else if (IsLetter(currentChar) || IsDigit(currentChar))
        {
            lexemeMemory.AddChar(currentChar);
            this.state = State.IDENTIFIER;
        }
        
        else if (IsOperator(currentChar))
        {
            PushNewIdentifier();
            lexemeMemory.AddChar(currentChar);
            this.state = State.OPERATOR;
        }
    }
    /// <summary>
    /// Builds lexeme when state is <see cref="State.NUMBER" />
    /// </summary>
    /// <param name="currentChar">current character</param>
    private void NumberRead(char currentChar)
    {
        if (IsNewline(currentChar))
        {
            PushNewNumber();
            PushNewLine();
            this.state = State.IDLE;
        }
        else if (IsWhitespace(currentChar))
        {
            PushNewNumber();
            this.state = State.IDLE;
        }
        else if (IsLetter(currentChar))
        {
            lexemeMemory.AddChar(currentChar);
            this.state = State.IDENTIFIER;
        }
        else if (IsDigit(currentChar))
        {
            lexemeMemory.AddChar(currentChar);
            this.state = State.NUMBER;
        }
        else if (IsOperator(currentChar))
        {
            PushNewNumber();
            lexemeMemory.AddChar(currentChar);
            this.state = State.OPERATOR;
        }
    }
    /// <summary>
    /// Builds lexeme when state is <see cref="State.OPERATOR" />
    /// </summary>
    /// <param name="currentChar">current character</param>
    private void OperatorRead(char currentChar)
    {
        if (IsNewline(currentChar))
        {
            PushNewOperator();
            PushNewLine();
            this.state = State.IDLE;
        }
        else if (IsWhitespace(currentChar))
        {
            PushNewOperator();
            this.state = State.IDLE;
        }
        else if (IsLetter(currentChar))
        {
            PushNewOperator();
            lexemeMemory.AddChar(currentChar);
            this.state = State.IDENTIFIER;
        }
        else if (IsDigit(currentChar))
        {
            PushNewOperator();
            lexemeMemory.AddChar(currentChar);
            this.state = State.NUMBER;
        }
        else if (IsOperator(currentChar))
        {
            lexemeMemory.AddChar(currentChar);
        }
    }

    /// <summary>
    /// Pushes new identifer to <see cref="tokens" /> list
    /// </summary>
    private void PushNewIdentifier()
    {
        string value = lexemeMemory.Build();
        if (!String.IsNullOrEmpty(value))
        {
            tokens.Add(new Token(TokenType.IDENTIFIER, value));
        }
    }
    /// <summary>
    /// Pushes new line to <see cref="tokens" /> list
    /// </summary>
    private void PushNewLine()
    {
        tokens.Add(new Token(TokenType.NEW_LINE, "\n"));
    }

    /// <summary>
    /// Pushes new number to <see cref="tokens" /> list
    /// </summary>
    private void PushNewNumber()
    {
        string value = lexemeMemory.Build();
        if (!String.IsNullOrEmpty(value))
        {
            tokens.Add(new Token(TokenType.NUMBER, value));
        }
    }

    /// <summary>
    /// Pushes new operator to <see cref="tokens" /> list
    /// </summary>
    private void PushNewOperator()
    {
        string value = lexemeMemory.Build();
        if (!String.IsNullOrEmpty(value))
        {
            tokens.Add(new Token(TokenType.OPERATOR, value));
        }
    }
    /// <summary>
    /// If character param is the new line char returns true
    /// </summary>
    /// <param name="c">character</param>
    /// <returns>is c is a new line character</returns>
    private bool IsNewline(char c)
    {
        return c == '\n';
    }

    /// <summary>
    /// If character param is a whitespace returns true
    /// </summary>
    /// <param name="c">character</param>
    /// <returns>is c is a whitespace</returns>
    private bool IsWhitespace(char c)
    {
        return Char.IsWhiteSpace(c);
    }

    /// <summary>
    /// If character param is a letter returns true
    /// </summary>
    /// <param name="c">character</param>
    /// <returns>is c a letter</returns>
    private bool IsLetter(char c)
    {
        return Char.IsLetter(c);
    }
    /// <summary>
    /// If character param is a digit returns true
    /// </summary>
    /// <param name="c">character</param>
    /// <returns>is c a digit</returns>
    private bool IsDigit(char c)
    {
        return Char.IsDigit(c);
    }

    /// <summary>
    /// If character param is an operator returns true
    /// </summary>
    /// <param name="c">character</param>
    /// <returns>is c a operator</returns>
    private bool IsOperator(char c)
    {
        return true;
    }

}

/// <summary>
/// Performs lexical analysis on string script
/// </summary>
class Lexer
{
    /// <summary>
    /// State machine to generate tokens
    /// </summary>
    readonly LexicalStateMachine stateMachine = new LexicalStateMachine();

    /// <summary>
    /// Performs the lexical analysis on script
    /// </summary>
    /// <param name="script">Script being lex'ed</param>
    /// <returns>List of tokens</returns>
    public List<Token> Lex(string script)
    {
        // sends each character off to state machine
        foreach (char ch in script)
        {
            stateMachine.Read(ch);
        }
        // end of file
        stateMachine.Read('\n');
        return stateMachine.tokens;
    }
}
