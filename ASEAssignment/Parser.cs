// ***********************************************************************
// Assembly         : ASEScriptParser
// Author           : Kevin Brewster
// Created          : 12-29-2022
//
// Last Modified By : Kevin Brewster
// Last Modified On : 01-16-2023
// ***********************************************************************
// <copyright file="Parser.cs" company="ASEScriptParser">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************


using System.Security.Cryptography.X509Certificates;

/// <summary>
/// Class ASTNode.
/// </summary>
abstract class ASTNode
{
}

/// <summary>
/// Class BlockNode.
/// Implements the <see cref="ASTNode" />
/// </summary>
/// <seealso cref="ASTNode" />
class BlockNode : ASTNode
{
    /// <summary>
    /// The children
    /// </summary>
    public List<ASTNode> children;

    /// <summary>
    /// Initializes a new instance of the <see cref="BlockNode"/> class.
    /// </summary>
    /// <param name="children">The children.</param>
    public BlockNode(List<ASTNode> children)
    {
        this.children = children;
    }

}

/// <summary>
/// Class IfNode.
/// Implements the <see cref="ASTNode" />
/// </summary>
/// <seealso cref="ASTNode" />
class IfNode : ASTNode
{
    /// <summary>
    /// The block
    /// </summary>
    public ASTNode block;

    /// <summary>
    /// The condition
    /// </summary>
    public ASTNode condition;

    /// <summary>
    /// Initializes a new instance of the <see cref="IfNode"/> class.
    /// </summary>
    /// <param name="condition">The condition.</param>
    /// <param name="block">The block.</param>
    public IfNode(ASTNode condition, ASTNode block)
    {
        this.block = block;
        this.condition = condition;
    }
}

/// <summary>
/// Class WhileNode.
/// Implements the <see cref="ASTNode" />
/// </summary>
/// <seealso cref="ASTNode" />
class WhileNode : ASTNode
{
    /// <summary>
    /// The condition
    /// </summary>
    public ASTNode condition;

    /// <summary>
    /// The block
    /// </summary>
    public ASTNode block;

    /// <summary>
    /// Initializes a new instance of the <see cref="WhileNode"/> class.
    /// </summary>
    /// <param name="condition">The condition.</param>
    /// <param name="block">The block.</param>
    public WhileNode(ASTNode condition, ASTNode block)
    {
        this.condition = condition;
        this.block = block;
    }
}

/// <summary>
/// Class IntegerNode.
/// Implements the <see cref="ASTNode" />
/// </summary>
/// <seealso cref="ASTNode" />
class IntegerNode : ASTNode
{
    /// <summary>
    /// The value
    /// </summary>
    public int value;

    /// <summary>
    /// Initializes a new instance of the <see cref="IntegerNode"/> class.
    /// </summary>
    /// <param name="value">The value.</param>
    public IntegerNode(int value)
    {
        this.value = value;
    }
}

/// <summary>
/// Class IdentifierNode.
/// Implements the <see cref="ASTNode" />
/// </summary>
/// <seealso cref="ASTNode" />
class IdentifierNode : ASTNode
{
    /// <summary>
    /// The identifier
    /// </summary>
    public string identifier;

    /// <summary>
    /// Initializes a new instance of the <see cref="IdentifierNode"/> class.
    /// </summary>
    /// <param name="identifier">The identifier.</param>
    public IdentifierNode(string identifier)
    {
        this.identifier = identifier;
    }
}

/// <summary>
/// Class OperatorNode.
/// Implements the <see cref="ASTNode" />
/// </summary>
/// <seealso cref="ASTNode" />
class OperatorNode : ASTNode
{
    /// <summary>
    /// The left
    /// </summary>
    public ASTNode left;
    /// <summary>
    /// The op
    /// </summary>
    public string op;
    /// <summary>
    /// The right
    /// </summary>
    public ASTNode right;

    /// <summary>
    /// Initializes a new instance of the <see cref="OperatorNode"/> class.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="op">The op.</param>
    /// <param name="right">The right.</param>
    public OperatorNode(ASTNode left, string op, ASTNode right)
    {
        this.left = left;
        this.op = op;
        this.right = right;
    }
}

/// <summary>
/// Class ComparisonNode.
/// Implements the <see cref="ASTNode" />
/// </summary>
/// <seealso cref="ASTNode" />
class ComparisonNode : ASTNode
{
    /// <summary>
    /// The left
    /// </summary>
    public ASTNode left;
    /// <summary>
    /// The op
    /// </summary>
    public string op;
    /// <summary>
    /// The right
    /// </summary>
    public ASTNode right;

    /// <summary>
    /// Initializes a new instance of the <see cref="ComparisonNode"/> class.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="op">The op.</param>
    /// <param name="right">The right.</param>
    public ComparisonNode(ASTNode left, string op, ASTNode right)
    {
        this.left = left;
        this.op = op;
        this.right = right;
    }
}

/// <summary>
/// Class AssignmentNode.
/// Implements the <see cref="ASTNode" />
/// </summary>
/// <seealso cref="ASTNode" />
class AssignmentNode : ASTNode
{
    /// <summary>
    /// The variable name
    /// </summary>
    public string varName;
    /// <summary>
    /// The value
    /// </summary>
    public ASTNode value;

    /// <summary>
    /// Initializes a new instance of the <see cref="AssignmentNode"/> class.
    /// </summary>
    /// <param name="varName">Name of the variable.</param>
    /// <param name="value">The value.</param>
    public AssignmentNode(string varName, ASTNode value)
    {
        this.varName = varName;
        this.value = value;
    }
}

/// <summary>
/// Class MethodNode.
/// Implements the <see cref="ASTNode" />
/// </summary>
/// <seealso cref="ASTNode" />
class MethodNode : ASTNode
{
    /// <summary>
    /// The method name
    /// </summary>
    public string methodName;
    /// <summary>
    /// The parameters
    /// </summary>
    public List<ASTNode> parameters;
    /// <summary>
    /// The block
    /// </summary>
    public ASTNode block;

    /// <summary>
    /// Initializes a new instance of the <see cref="MethodNode"/> class.
    /// </summary>
    /// <param name="methodName">Name of the method.</param>
    /// <param name="parameters">The parameters.</param>
    /// <param name="block">The block.</param>
    public MethodNode(string methodName, List<ASTNode> parameters, ASTNode block)
    {
        this.methodName = methodName;
        this.parameters = parameters;
        this.block = block;
    }
}


/// <summary>
/// Class MethodInvokeNode.
/// Implements the <see cref="ASTNode" />
/// </summary>
/// <seealso cref="ASTNode" />
class MethodInvokeNode : ASTNode
{
    /// <summary>
    /// The method name
    /// </summary>
    public string methodName;
    /// <summary>
    /// The parameters
    /// </summary>
    public List<ASTNode> parameters;

    /// <summary>
    /// Initializes a new instance of the <see cref="MethodInvokeNode"/> class.
    /// </summary>
    /// <param name="methodName">Name of the method.</param>
    /// <param name="parameters">The parameters.</param>
    public MethodInvokeNode(string methodName, List<ASTNode> parameters)
    {
        this.methodName = methodName;
        this.parameters = parameters;
    }
}


/// <summary>
/// Class CommandInvokeNode.
/// Implements the <see cref="ASTNode" />
/// </summary>
/// <seealso cref="ASTNode" />
class CommandInvokeNode : ASTNode
{
    /// <summary>
    /// The command name
    /// </summary>
    public string commandName;
    /// <summary>
    /// The parameters
    /// </summary>
    public List<ASTNode> parameters;

    /// <summary>
    /// Initializes a new instance of the <see cref="CommandInvokeNode"/> class.
    /// </summary>
    /// <param name="commandName">Name of the command.</param>
    /// <param name="parameters">The parameters.</param>
    public CommandInvokeNode(string commandName, List<ASTNode> parameters)
    {
        this.commandName = commandName;
        this.parameters = parameters;
    }
}


/// <summary>
/// Class SyntaxException.
/// Implements the <see cref="System.Exception" />
/// </summary>
/// <seealso cref="System.Exception" />
public class SyntaxException : Exception {
    /// <summary>
    /// Initializes a new instance of the <see cref="SyntaxException"/> class.
    /// </summary>
    /// <param name="line">The line.</param>
    /// <param name="message">The message.</param>
    public SyntaxException(int line, string message): base("line" + line + ":" +  message) { }
    
}

/// <summary>
/// Class Parser.
/// </summary>
class Parser
{
    /// <summary>
    /// Gets the tokens.
    /// </summary>
    /// <value>The tokens.</value>
    public List<Token> Tokens { get; }

    /// <summary>
    /// The current position
    /// </summary>
    private int currentPos = 0;

    /// <summary>
    /// The current line
    /// </summary>
    private int currentLine = 1;

    /// <summary>
    /// Initializes a new instance of the <see cref="Parser"/> class.
    /// </summary>
    /// <param name="tokens">The tokens.</param>
    public Parser(List<Token> tokens)
    {
        Tokens = tokens;
    }


    /// <summary>
    /// Enterance parsing method
    /// Starts parsing of <see cref="Tokens" />
    /// </summary>
    /// <returns>Main ASTNode</returns>
    public BlockNode Parse()
    {
        List<ASTNode> nodes = new List<ASTNode>();
        while(isEndOfTokens())
        {
            if (Peak().type == TokenType.NEW_LINE)
            {
                ConsumeNewline();
            }
            else
            {
                nodes.Add(ParseToken());
            }
        }
        return new BlockNode(nodes);

    }

    /// <summary>
    /// Parses Token depending on its type
    /// </summary>
    /// <returns>ASTNode.</returns>
    /// <exception cref="global.SyntaxException">Couldn't parse token: " + token.value</exception>
    private ASTNode ParseToken()
    {
        Token token = Peak();
        switch(token.type)
        {
            case TokenType.IDENTIFIER:
                return ParseTokenIdentifier();
            case TokenType.NEW_LINE:
                Read();
                return ParseToken();
            default:  
                throw new SyntaxException(currentLine, "Couldn't parse token: " + token.value);
        }
    }

    /// <summary>
    /// Parses Tokens classified as identifiers into ASTNodes
    /// </summary>
    /// <returns>ASTNode</returns>
    private ASTNode ParseTokenIdentifier()
    {
        Token token = Read();
        switch (token.value)
        {
            case "If":
                return ParseIfStatement();
            case "While":
                return ParseWhileStatement();
            case "Method":
                return ParseMethod();
            default:
                if(Reads("="))
                {
                    return ParseVariableAssign(token.value);
                } else if(Reads("("))
                {
                    return ParseMethodInvoke(token.value);
                }
                return ParseCommand(token.value);
        }
    }

    /// <summary>
    /// Parses Variable Assign into <see cref="AssignMentNode" />
    /// </summary>
    /// <param name="name">Name of variable</param>
    /// <returns>Assignment Node</returns>
    private ASTNode ParseVariableAssign(string name)
    { 
        ASTNode assignNode = new AssignmentNode(name, ParseSumDifference());
        ConsumeNewline();
        return assignNode;
    }

    /// <summary>
    /// Parses Method Invokes into <see cref="MethodInvokeNode" />
    /// </summary>
    /// <param name="name">Name of method</param>
    /// <returns>Method Invoke Node</returns>
    private ASTNode ParseMethodInvoke(string name)
    {
        return new MethodInvokeNode(name, ParseArguments());
    }

    /// <summary>
    /// Parses arguments into a List of ASTNodes
    /// e.g. (x, y, 10, 40+3)
    /// </summary>
    /// <returns>List of ASTNodes</returns>
    private List<ASTNode> ParseArguments()
    {
        List<ASTNode> args = new List<ASTNode>();
        while(!Reads(")"))
        {
            args.Add(ParseTerm());
            Reads(",");
        }
        return args;
    }

    /// <summary>
    /// Parses if statements into <see cref="IfNode" />
    /// </summary>
    /// <returns>If Node</returns>
    /// <exception cref="global.SyntaxException">Expected Endif instead recieved: " + Peak().value</exception>
    private ASTNode ParseIfStatement()
    {
        ASTNode condition = ParseComparison();
        ConsumeNewline();
        ASTNode block = ParseBlock();
        if(!Reads("Endif"))
        {
            throw new SyntaxException(currentLine, "Expected Endif instead recieved: " + Peak().value);
        }
        return new IfNode(condition, block);
    }

    /// <summary>
    /// Parses Method into <see cref="MethodNode" />
    /// Example of method grammar:
    /// <code>
    /// Method method1(x, y)
    /// rectangle 20,20
    /// Endmethod
    /// </code>
    /// </summary>
    /// <returns>Method Node</returns>
    /// <exception cref="global.SyntaxException">Expecting param list in method decleration</exception>
    /// <exception cref="global.SyntaxException">Expected Endmethod instead recieved: " + Peak().value</exception>
    private ASTNode ParseMethod()
    {
        string name = Read().value;
        if (!Reads("("))
        {
            throw new SyntaxException(currentLine, "Expecting param list in method decleration");
        }
        List<ASTNode> args = ParseArguments();
        ConsumeNewline();
        ASTNode block = ParseBlock();
        if (!Reads("Endmethod"))
        {
            throw new SyntaxException(currentLine, "Expected Endmethod instead recieved: " + Peak().value);
        }
        return new MethodNode(name, args, block);
    }

    /// <summary>
    /// Parses comparisons into <see cref="ComparisonNode" />
    /// </summary>
    /// <returns>Comparison Node</returns>
    private ASTNode ParseComparison()
    {
        ASTNode left = ParseSumDifference();
        string op = Read().value;
        ASTNode right = ParseSumDifference();
        return new ComparisonNode(left, op, right);
    }

    /// <summary>
    /// Parses while grammar into <see cref="WhileNode" />
    /// </summary>
    /// <returns>While Node</returns>
    /// <exception cref="global.SyntaxException">Expected Endloop instead recieved: " + Peak().value</exception>
    private ASTNode ParseWhileStatement()
    {
        ASTNode condition = ParseComparison();
        ConsumeNewline();
        ASTNode block = ParseBlock();
        if (!Reads("Endloop"))
        {
            throw new SyntaxException(currentLine, "Expected Endloop instead recieved: " + Peak().value);
        }
        return new WhileNode(condition,block);
    }

    /// <summary>
    /// Parses Sum and Difference operators into <see cref="OperatorNode" />
    /// </summary>
    /// <returns>Operator Node</returns>
    private ASTNode ParseSumDifference()
    {
        ASTNode left = ParseProductQuotient();
        while(Peak().value == "+" || Peak().value == "-")
        {
            string op = Read().value;
            ASTNode right = ParseProductQuotient();
            left = new OperatorNode(left, op, right);
        }
        return left;
    }

    /// <summary>
    /// Parses Product and Quotient operators into <see cref="OperatorNode" />
    /// </summary>
    /// <returns>OperatorNode</returns>
    private ASTNode ParseProductQuotient()
    {
        ASTNode left = ParseTerm();
        while (Peak().value == "*" || Peak().value == "/")
        {
            string op = Read().value;
            ASTNode right = ParseTerm();
            left = new OperatorNode(left, op, right);
        }
        return left;
    }

    /// <summary>
    /// Parses terms into <see cref="IntegerNode" /> and <see cref="IdentifierNode" />
    /// </summary>
    /// <returns>Integer Node or Identifier Node</returns>
    /// <exception cref="global.SyntaxException">Expected ) instead recieved: " + Peak().value</exception>
    /// <exception cref="global.SyntaxException">Could not parse term</exception>
    private ASTNode ParseTerm()
    {
        Token token = Read();
        if(token.type == TokenType.NUMBER)
        {
            return new IntegerNode(int.Parse(token.value));
        } else if(token.type == TokenType.IDENTIFIER)
        {
            return new IdentifierNode(token.value);
        } else if(token.value == "(")
        {
            ASTNode expr = ParseTerm();
            if(!Reads(")")) {
                throw new SyntaxException(currentLine, "Expected ) instead recieved: " + Peak().value);
            }
            return expr;
        }
        throw new SyntaxException(currentLine, "Could not parse term");

    }

    /// <summary>
    /// Parses commands into <see cref="CommandInvokeNode" />
    /// </summary>
    /// <param name="commandName">Command Name</param>
    /// <returns>Command Invoke Node</returns>
    private ASTNode ParseCommand(string commandName)
    {
        List<ASTNode> args = new List<ASTNode>();
        while (!Reads(TokenType.NEW_LINE))
        {
            args.Add(ParseTerm());
            Reads(",");
        }
        return new CommandInvokeNode(commandName, args);
    }

    /// <summary>
    /// Parses a block of statements into <see cref="BlockNode" />
    /// </summary>
    /// <returns>Block Node</returns>
    private ASTNode ParseBlock()
    {
        List<ASTNode> nodes = new List<ASTNode>();
        Token token = Peak();
        while (token.value != "Endif" && token.value != "Endloop" && token.value != "Endmethod")
        {
            if (Peak().type == TokenType.NEW_LINE)
            {
                ConsumeNewline();
            }
            else
            {
                nodes.Add(ParseToken());
            }
            if (isEndOfTokens())
            {
                token = Peak();
            }
        }
   
        return new BlockNode(nodes);
    }

    /// <summary>
    /// Reads new line and consumes it
    /// </summary>
    /// <exception cref="global.SyntaxException">Expected newline instead recieved: " + Peak().value</exception>
    private void ConsumeNewline()
    {
        if (!Reads(TokenType.NEW_LINE))
        {
            throw new SyntaxException(currentLine, "Expected newline instead recieved: " + Peak().value);
        }
    }

    /// <summary>
    /// Reads next token in <see cref="Tokens" />
    /// </summary>
    /// <returns>Next token</returns>
    public Token Read()
    {
        Token read = Tokens[currentPos++];
        if (read.type == TokenType.NEW_LINE)
        {
            currentLine++;
        }
        return read;
    }

    /// <summary>
    /// Reads next token in <see cref="Tokens" /> and asserts it with <see cref="value" />
    /// </summary>
    /// <param name="value">value to be asserted with next token</param>
    /// <returns>If <see cref="value" /> is equal to the value of the next token</returns>
    private bool Reads(String value)
    {
        Token read = Peak();
        if (read.type == TokenType.NEW_LINE)
        {
            currentLine++;
        }
        if (read.value == value)
        {
            currentPos++;
            return true;
        }

        return false;
    }
    /// <summary>
    /// Reads next token in <see cref="Tokens" /> and asserts it with <see cref="value" />
    /// </summary>
    /// <param name="value">value to be asserted with next token</param>
    /// <returns>If <see cref="value" /> is equal to the type of the next token</returns>
    private bool Reads(TokenType value)
    {
        Token read = Peak();
        if (read.type == TokenType.NEW_LINE)
        {
            currentLine++;
        }
        if (read.type == value)
        {
            currentPos++;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Peaks at the next token to be read in <see cref="Tokens" />
    /// </summary>
    /// <returns>Next token to be read</returns>
    public Token Peak()
    {
        return Tokens[currentPos];
    }

    /// <summary>
    /// If all the tokens have been read
    /// </summary>
    /// <returns>If the end of <see cref="Tokens" /> has been reached.</returns>
    private bool isEndOfTokens()
    {
        return currentPos < Tokens.Count - 1;
    }

}