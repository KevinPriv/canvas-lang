// ***********************************************************************
// Assembly         : ASEScriptParser
// Author           : Kevin Brewster
// Created          : 12-29-2022
//
// Last Modified By : Kevin Brewster
// Last Modified On : 01-16-2023
// ***********************************************************************
// <copyright file="Interpreter.cs" company="ASEScriptParser">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Security.AccessControl;

/// <summary>
/// Executes and invokes AST tree
/// To get AST <see cref="Parser" />
/// </summary>
class Interpreter
{
    /// <summary>
    /// Stores script commands and their dispatcher
    /// </summary>
    private CommandDispatcher dispatcher;

    /// <summary>
    /// Stores AST being evaluated
    /// </summary>
    private BlockNode programAST;

    /// <summary>
    /// Stores global Variables
    /// </summary>
    private Dictionary<string, int> globalVars;
    /// <summary>
    /// Stores script Method
    /// </summary>
    private Dictionary<MethodSig, BlockNode> methods;

    /// <summary>
    /// Sets environment up ready to execute AST
    /// </summary>
    /// <param name="dispatcher">Command Dispatcher</param>
    /// <param name="programAST">Main program AST</param>
    public Interpreter(CommandDispatcher dispatcher, BlockNode programAST)
    {
        this.dispatcher = dispatcher;
        this.programAST = programAST;
        this.globalVars = new Dictionary<string, int>();
        this.methods = new Dictionary<MethodSig, BlockNode>();
    }

    /// <summary>
    /// Executes the main program AST
    /// </summary>
    public void Execute()
    {
        foreach (ASTNode statement in programAST.children)
        {
            Execute(statement, globalVars);
        }
    }

    /// <summary>
    /// Executes a branch node
    /// </summary>
    /// <param name="node">Node being executed</param>
    /// <param name="variableScope">Variable scope of execution</param>
    public void Execute(ASTNode node, Dictionary<string, int> variableScope)
    {
        if (node is AssignmentNode variable)
        {
            variableScope[variable.varName] = (int)Evaluate(variable.value, globalVars);
        }
        else if (node is IfNode ifNode)
        {
            if ((bool)Evaluate(ifNode.condition, variableScope))
            {
                Execute(ifNode.block, variableScope);
            }
        }
        else if (node is WhileNode whileNode)
        {
            while ((bool)Evaluate(whileNode.condition, variableScope))
            {
                Execute(whileNode.block, variableScope);
            }
        }
        else if (node is MethodNode methodNode)
        {
            MethodSig signature = new MethodSig(methodNode.methodName, methodNode.parameters);
            this.methods[signature] = (BlockNode) methodNode.block;
        }
        else if (node is MethodInvokeNode methodInvokeNode)
        {
            // list of arguments we are passing though
            List<ASTNode> arguments = methodInvokeNode.parameters;
            // gets the methodsig and blocknode we are wanting to invoke
            KeyValuePair<MethodSig, BlockNode> methodInvocation = this.methods.
                First(method => method.Key.args.Count == arguments.Count);

            for(int i = 0; i < arguments.Count; i++)
            {
                string argName = ((IdentifierNode) methodInvocation.Key.args[i]).identifier;
                int value = (int)Evaluate(arguments[i], variableScope);
                // sets params to local variables in scope
                variableScope[argName] = value;
            }
            Execute(methodInvocation.Value, variableScope);
        }
        else if(node is CommandInvokeNode commandInvokeNode)
        {
            string cmdName = commandInvokeNode.commandName;
            List<string> cmdArgs = new List<string>();
            foreach(ASTNode cmdArg in commandInvokeNode.parameters) {  
                int value = (int)Evaluate(cmdArg, variableScope);
                cmdArgs.Add(value.ToString());
            }
            dispatcher.Dispatch(cmdName, cmdArgs.ToArray());
        }
        else if (node is BlockNode block)
        {
            foreach (ASTNode statement in block.children) { 
                Execute(statement, variableScope);
            }
        }
    }

    /// <summary>
    /// Evaluates ASTNode
    /// if `IntegerNode` returns integer value
    /// if `IdentifierNode` returns variable value
    /// if `OperatorNode` returns Evaluated Operator
    /// if `ComparisonNode` returns Evaluated Comparison
    /// </summary>
    /// <param name="value">value to evaluate</param>
    /// <param name="variableScope">scope of variables</param>
    /// <returns>object depending on <see cref="value" /> node</returns>
    /// <exception cref="Exception">Couldn't evaluate" + value</exception>
    private object Evaluate(ASTNode value, Dictionary<string, int> variableScope)
    {
        if(value is IntegerNode integer)
        {
            return integer.value;
        } else if(value is IdentifierNode varName)
        {
            return variableScope[varName.identifier];
        } else if (value is OperatorNode op)
        {
            return EvaluateOperation(op, variableScope);
        }else if (value is ComparisonNode comp)
        {
            return EvaluateComparison(comp, variableScope);
        }
        throw new Exception("Couldn't evaluate" + value);
    }

    /// <summary>
    /// Evaluates Operations
    /// </summary>
    /// <param name="op">Operator Node to evaluate</param>
    /// <param name="variableScope">scope of variables</param>
    /// <returns>Integer when OperatorNode is evaluated</returns>
    private int EvaluateOperation(OperatorNode op, Dictionary<string, int> variableScope)
    {
        int left = (int) Evaluate(op.left, variableScope);
        int right = (int) Evaluate(op.right, variableScope);
        return new OperationFactory().GetOperation(left, right, op.op);
    }

    /// <summary>
    /// Evaluates different operations
    /// </summary>
    public class OperationFactory
    {
        /// <summary>
        /// Evaluates operation on two integers
        /// </summary>
        /// <param name="left">Term on the left</param>
        /// <param name="right">Term on the right</param>
        /// <param name="op">Operator to apply to terms</param>
        /// <returns>Evaluated integer</returns>
        /// <exception cref="Exception">Invalid operation: " + op</exception>
        public int GetOperation(int left, int right, string op)
        {
            switch (op)
            {
                case "+":
                    return left + right;
                case "-":
                    return left - right;
                case "*":
                    return left * right;
                case "/":
                    return left / right;
            }
            throw new Exception("Invalid operation: " + op);
        }
    }

    /// <summary>
    /// Evaluates comparisons
    /// </summary>
    /// <param name="op">Comparison Node to evaluate</param>
    /// <param name="variableScope">scope of variables</param>
    /// <returns>Evaluated boolean</returns>
    private bool EvaluateComparison(ComparisonNode op, Dictionary<string, int> variableScope)
    {
        object left = Evaluate(op.left, variableScope);
        object right = Evaluate(op.right, variableScope);
        return new ComparisonFactory().GetComparison(left, right, op.op);
    }

    /// <summary>
    /// Evaluates comparisons
    /// </summary>
    public class ComparisonFactory
    {
        /// <summary>
        /// Evaluates comparison of two integers
        /// </summary>
        /// <param name="left">Term on the left of operator</param>
        /// <param name="right">Term on right of operator</param>
        /// <param name="comparison">Comparison operator</param>
        /// <returns>Evaluated boolean</returns>
        /// <exception cref="Exception">Invalid comparison: " + comparison</exception>
        public bool GetComparison(object left, object right, string comparison)
        {
            switch (comparison)
            {
                case "==":
                    return (int)left == (int)right;
                case "!=":
                    return (int)left != (int)right;
                case "<=":
                    return (int)left <= (int)right;
                case ">=":
                    return (int)left >= (int)right;
                case ">":
                    return (int)left > (int)right;
                case "<":
                    return (int)left < (int)right;
                case "&&":
                    return (bool)left && (bool)right;
                case "||":
                    return ((bool)left) || (bool)right;
            }
            throw new Exception("Invalid comparison: " + comparison);
        }
    }
}

/// <summary>
/// Stores method signature as dataclass
/// </summary>
/// <param name="name">Name of method</param>
/// <param name="args">Arguments of the method</param>
record struct MethodSig(string name, List<ASTNode> args);