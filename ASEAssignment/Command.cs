// ***********************************************************************
// Assembly         : ASEScriptParser
// Author           : Kevin Brewster
// Created          : 11-05-2022
//
// Last Modified By : Kevin Brewster
// Last Modified On : 11-21-2022
// ***********************************************************************
// <copyright file="Command.cs" company="ASEScriptParser">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

/// <summary>
/// Base command structure
/// </summary>
public abstract class AbstractCommand
{
    /// <summary>
    /// Gets the name.
    /// </summary>
    /// <value>The name.</value>
    public abstract string Name {
        /// <summary>
        /// Name of command
        /// </summary>
        get;
    }
    /// <summary>
    /// Gets the alias.
    /// </summary>
    /// <value>The alias.</value>
    public abstract string[] Alias {
        /// <summary>
        /// Alias(es) of command
        /// </summary>
        get; 
    }
    /// <summary>
    /// Executes the command
    /// </summary>
    /// <param name="args">The arguments.</param>
    public abstract void Execute(string[] args);

    /// <summary>
    /// Is <paramref name="name" /> the name of the command
    /// </summary>
    /// <param name="name">The name.</param>
    /// <returns><c>true</c> if the specified name is name; otherwise, <c>false</c>.</returns>
    public bool IsName(string name)
    {
        return Name == name;
    }
    /// <summary>
    /// Is <paramref name="name" /> an alias of the command
    /// </summary>
    /// <param name="name">The name.</param>
    /// <returns><c>true</c> if the specified name is alias; otherwise, <c>false</c>.</returns>
    public bool IsAlias(string name)
    {
        return Alias.Contains(name);
    }

    /// <summary>
    /// Is <paramref name="name" /> the alias or name of the command
    /// </summary>
    /// <param name="name">The name.</param>
    /// <returns><c>true</c> if [is name or alias] [the specified name]; otherwise, <c>false</c>.</returns>
    public bool IsNameOrAlias(string name)
    {
        return IsName(name) || IsAlias(name);
    }


}

/// <summary>
/// Argument predicate which can be used to validate if an argument is correct
/// <see cref="T" /> is the type the argument will be after validation if complete
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class ArgumentPredicate<T>
{
    /// <summary>
    /// Validates and parses <paramref name="argument" /> to <see cref="T" />
    /// </summary>
    /// <param name="argument">The argument you want validating</param>
    /// <returns>Returns <paramref name="argument" /> as <see cref="T" /> type</returns>
    public abstract T Validate(string argument);

}

/// <summary>
/// Invalid Argument Exception
/// </summary>
public class InvalidArgumentException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidArgumentException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public InvalidArgumentException(string message) : base(message) { }
}
/// <summary>
/// Integer out of bounds Exception
/// </summary>
public class IntegerOutOfBoundsException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IntegerOutOfBoundsException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public IntegerOutOfBoundsException(string message) : base(message) { }
}
/// <summary>
/// Predicate for validating string argument into an integer with a lower and max bound.
/// </summary>
public class IntegerArgumentPredicate : ArgumentPredicate<int>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IntegerArgumentPredicate"/> class.
    /// </summary>
    /// <param name="lowestRange">The lowest range.</param>
    /// <param name="highestRange">The highest range.</param>
    public IntegerArgumentPredicate(int lowestRange, int highestRange)
    {
        LowestRange = lowestRange;
        HighestRange = highestRange;
    }

    /// <summary>
    /// Gets the lowest range.
    /// </summary>
    /// <value>The lowest range.</value>
    public int LowestRange {
        /// <summary>
        /// Gets Lower Bound Integer can be
        /// </summary>
        get;
    }

    /// <summary>
    /// Gets the highest range.
    /// </summary>
    /// <value>The highest range.</value>
    public int HighestRange {
        /// <summary>
        /// Gets Highest Bound Integer can be
        /// </summary>
        get;
    }

    /// <summary>
    /// Parses <see cref="string" /> argument to <see cref="int" /> and returns it as long as its within the lower and highest bounds.
    /// </summary>
    /// <param name="argument">Argument you would like validating</param>
    /// <returns><paramref name="argument" /> as <see cref="int" /></returns>
    /// <exception cref="global.InvalidArgumentException"></exception>
    /// <exception cref="global.IntegerOutOfBoundsException"></exception>
    public override int Validate(string argument)
    {
        bool validInt = int.TryParse(argument, out int num);
        // check if string-> int parse failed or not
        if (!validInt)
        {
            throw new InvalidArgumentException(string.Format("Could not parse {0} into a valid integer.", argument));
        }
        if(num < LowestRange || num > HighestRange)
        {
            throw new IntegerOutOfBoundsException(string.Format("Integer, {0}, was not within the bounds of {1} and {2}", num, LowestRange, HighestRange));
        }

        return num;
    }
}
/// <summary>
/// Stores an array of commands
/// </summary>
public class CommandRegistry
{
    /// <summary>
    /// The commands
    /// </summary>
    private List<AbstractCommand> commands = new List<AbstractCommand>();

    /// <summary>
    /// Gets the commands.
    /// </summary>
    /// <value>The commands.</value>
    public List<AbstractCommand> Commands
    {
        get
        {
            return commands;
        }

        private set
        {
            commands = value;
        }
    }
    /// <summary>
    /// Register command
    /// </summary>
    /// <param name="command">Command you would like registered to the registry</param>
    public void Register(AbstractCommand command)
    {
        commands.Add(command);
    }

}


/// <summary>
/// Invalid Command Exception
/// </summary>
public class InvalidCommandException: Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidCommandException"/> class.
    /// </summary>
    public InvalidCommandException(): base("Invalid command was entered") { }
}

/// <summary>
/// Dispatches commands stored in <see cref="CommandRegistry" />
/// </summary>
public class CommandDispatcher
{
    /// <summary>
    /// The registry
    /// </summary>
    private CommandRegistry registry = new CommandRegistry();

    /// <summary>
    /// Gets the registry.
    /// </summary>
    /// <value>The registry.</value>
    public CommandRegistry Registry
    {
        get
        {
            return registry;
        }
        private set
        {
            registry = value;
        }
    }
    /// <summary>
    /// Dispatches and executes command based on commandname
    /// </summary>
    /// <param name="commandName">Name of command to execute</param>
    /// <param name="args">Arguments the command needs to execute</param>
    /// <exception cref="global.InvalidCommandException"></exception>
    public void Dispatch(string commandName, string[] args)
    {
        foreach (AbstractCommand command in registry.Commands)
        {
            if (command.IsNameOrAlias(commandName))
            {
                command.Execute(args);
                return;
            }
        }
        throw new InvalidCommandException();
    }
}