// ***********************************************************************
// Assembly         : ASEScriptGUI
// Author           : Kevin Brewster
// Created          : 11-09-2022
//
// Last Modified By : Kevin Brewster
// Last Modified On : 01-16-2023
// ***********************************************************************
// <copyright file="ASECommands.cs" company="ASEScriptGUI">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Windows.Forms;
using ASEScriptGUI;
using Timer = System.Windows.Forms.Timer;

/// <summary>
/// Auxillary base class for commands executed on the command line
/// </summary>
public abstract class CmdLineCommand : AbstractCommand
{
    /// <summary>
    /// Gets the window.
    /// </summary>
    /// <value>The window.</value>
    public ScriptingWindow Window
    {
        /// <summary>
        /// Getter for the programs window
        /// </summary>
        get
        {
            return Program.window;
        }
    }
}

/// <summary>
/// Invalid arugment size exception
/// </summary>
public class InvalidArgumentSizeException : Exception {
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidArgumentSizeException"/> class.
    /// </summary>
    /// <param name="expectedSize">The expected size.</param>
    /// <param name="actualSize">The actual size.</param>
    public InvalidArgumentSizeException(int expectedSize, int actualSize):
        base(String.Format("Expected {0} arguments instead recieved {1}.", expectedSize, actualSize)) { }
}

/// <summary>
/// Run command, executes the script within the scripting window
/// </summary>
public class RunCommand : CmdLineCommand
{
    /// <summary>
    /// Gets the name.
    /// </summary>
    /// <value>The name.</value>
    public override string Name => "run";

    /// <summary>
    /// Gets the alias.
    /// </summary>
    /// <value>The alias.</value>
    public override string[] Alias => new string[] {};

    /// <summary>
    /// Executes the script within the scripting window
    /// </summary>
    /// <param name="args">None</param>
    /// <exception cref="global.InvalidArgumentSizeException">0</exception>
    public override void Execute(string[] args)
    {
        // Expects zero arguments
        if(args.Length != 0)
        {
            throw new InvalidArgumentSizeException(0, args.Length);
        }
       
        ScriptEnvironment interpreter = new ScriptEnvironment(Window.commandDispatcher);
        interpreter.Execute(Window.ScriptTextBox.Text);
    }

}

/// <summary>
/// Auxillary base class for drawing commands and other commands which need access to the canvas
/// </summary>
public abstract class CanvasCommand: AbstractCommand
{
    /// <summary>
    /// Gets the canvas.
    /// </summary>
    /// <value>The canvas.</value>
    public Canvas Canvas
    {
        /// <summary>
        /// Getter for the Canvas
        /// </summary>
        get
        {
            return Program.window.canvas;
        }
    }
}
/// <summary>
/// Circle command, Draws a circle to canvas
/// </summary>
public class CircleCommand : CanvasCommand
{
    /// <summary>
    /// Gets the name.
    /// </summary>
    /// <value>The name.</value>
    public override string Name => "circle";

    /// <summary>
    /// Gets the alias.
    /// </summary>
    /// <value>The alias.</value>
    public override string[] Alias => new string[] { };

    /// <summary>
    /// Draws a circle
    /// </summary>
    /// <param name="args">Radius</param>
    /// <exception cref="global.InvalidArgumentSizeException">1</exception>
    public override void Execute(string[] args)
    {
        // Expects one arguments
        if (args.Length != 1)
        {
            throw new InvalidArgumentSizeException(1, args.Length);
        }
        int radius = new IntegerArgumentPredicate(0, Canvas.WIDTH).Validate(args[0]);
      
        Canvas.DrawCircle(radius);
        Program.window.updateTimer.Tick += delegate
        {
            Canvas.DrawCircle(radius);
            Program.window.GetPictureBox().Refresh();
        };
        Program.window.updateTimer.Start();
    }
}
/// <summary>
/// Moveto command, moves pen to coordinates
/// </summary>
public class MovetoCommand : CanvasCommand
{
    /// <summary>
    /// Gets the name.
    /// </summary>
    /// <value>The name.</value>
    public override string Name => "moveto";

    /// <summary>
    /// Gets the alias.
    /// </summary>
    /// <value>The alias.</value>
    public override string[] Alias => new string[] { };

    /// <summary>
    /// Moves pen to coordinates
    /// </summary>
    /// <param name="args">x, y</param>
    /// <exception cref="global.InvalidArgumentSizeException">2</exception>
    public override void Execute(string[] args)
    {
        // Expects two arguments
        if (args.Length != 2)
        {
            throw new InvalidArgumentSizeException(2, args.Length);
        }
        int x = new IntegerArgumentPredicate(0, Canvas.WIDTH).Validate(args[0]);
        int y = new IntegerArgumentPredicate(0, Canvas.HEIGHT).Validate(args[1]);

        Canvas.MoveTo(x, y);
    }
}
/// <summary>
/// Drawto command, Draws line from current pen positon to a new pen position
/// </summary>
public class DrawtoCommand : CanvasCommand
{
    /// <summary>
    /// Gets the name.
    /// </summary>
    /// <value>The name.</value>
    public override string Name => "drawto";

    /// <summary>
    /// Gets the alias.
    /// </summary>
    /// <value>The alias.</value>
    public override string[] Alias => new string[] { };

    /// <summary>
    /// Draws line from current pen position to a new pen position
    /// </summary>
    /// <param name="args">newX, newY</param>
    /// <exception cref="global.InvalidArgumentSizeException">2</exception>
    public override void Execute(string[] args)
    {
        // Expects two arguments
        if (args.Length != 2)
        {
            throw new InvalidArgumentSizeException(2, args.Length);
        }
        int x = new IntegerArgumentPredicate(0, Canvas.WIDTH).Validate(args[0]);
        int y = new IntegerArgumentPredicate(0, Canvas.HEIGHT).Validate(args[1]);

        Canvas.Draw(x, y);
    }
}

/// <summary>
/// Clear command, clears canvas
/// </summary>
public class ClearCommand : CanvasCommand
{
    /// <summary>
    /// Gets the name.
    /// </summary>
    /// <value>The name.</value>
    public override string Name => "clear";

    /// <summary>
    /// Gets the alias.
    /// </summary>
    /// <value>The alias.</value>
    public override string[] Alias => new string[] { };

    /// <summary>
    /// Clears canvas
    /// </summary>
    /// <param name="args">None</param>
    /// <exception cref="global.InvalidArgumentSizeException">0</exception>
    public override void Execute(string[] args)
    {
        // Expects zero arguments
        if (args.Length != 0)
        {
            throw new InvalidArgumentSizeException(0, args.Length);
        }
        Canvas.Clear();
    }
}

/// <summary>
/// Reset command, resets pen position
/// </summary>
public class ResetCommand : CanvasCommand
{
    /// <summary>
    /// Gets the name.
    /// </summary>
    /// <value>The name.</value>
    public override string Name => "reset";

    /// <summary>
    /// Gets the alias.
    /// </summary>
    /// <value>The alias.</value>
    public override string[] Alias => new string[] { };

    /// <summary>
    /// Resets pen position
    /// </summary>
    /// <param name="args">None</param>
    /// <exception cref="global.InvalidArgumentSizeException">0</exception>
    public override void Execute(string[] args)
    {
        // Expects zero arguments
        if (args.Length != 0)
        {
            throw new InvalidArgumentSizeException(0, args.Length);
        }
        Canvas.Reset();
    }
}

/// <summary>
/// Rectangle command, draws rectangle to canvas
/// </summary>
public class RectangleCommand : CanvasCommand
{
    /// <summary>
    /// Gets the name.
    /// </summary>
    /// <value>The name.</value>
    public override string Name => "rectangle";

    /// <summary>
    /// Gets the alias.
    /// </summary>
    /// <value>The alias.</value>
    public override string[] Alias => new string[] { "rect" };

    /// <summary>
    /// Draws rectangle
    /// </summary>
    /// <param name="args">width,length</param>
    /// <exception cref="global.InvalidArgumentSizeException">2</exception>
    public override void Execute(string[] args)
    {
        // Expects two arguments
        if (args.Length != 2)
        {
            throw new InvalidArgumentSizeException(2, args.Length);
        }
        int width = new IntegerArgumentPredicate(0, Canvas.WIDTH - Canvas.penX).Validate(args[0]);
        int length = new IntegerArgumentPredicate(0, Canvas.HEIGHT - Canvas.penY).Validate(args[1]);
        Canvas.DrawRect(width, length);
    }
}

/// <summary>
/// Traingle command, draws triangle to canvas
/// </summary>
public class TriangleCommand : CanvasCommand
{
    /// <summary>
    /// Gets the name.
    /// </summary>
    /// <value>The name.</value>
    public override string Name => "triangle";

    /// <summary>
    /// Gets the alias.
    /// </summary>
    /// <value>The alias.</value>
    public override string[] Alias => new string[] { };

    /// <summary>
    /// Draws triangle
    /// </summary>
    /// <param name="args">x1, y1, x2, y2</param>
    /// <exception cref="global.InvalidArgumentSizeException">4</exception>
    public override void Execute(string[] args)
    {
        // Expects four arguments
        if (args.Length != 4)
        {
            throw new InvalidArgumentSizeException(4, args.Length);
        }
        int x1 = new IntegerArgumentPredicate(0, Canvas.WIDTH - Canvas.penX).Validate(args[0]);
        int y1 = new IntegerArgumentPredicate(0, Canvas.HEIGHT - Canvas.penY).Validate(args[1]);
        int x2 = new IntegerArgumentPredicate(0, Canvas.WIDTH - Canvas.penX).Validate(args[2]);
        int y2 = new IntegerArgumentPredicate(0, Canvas.HEIGHT - Canvas.penY).Validate(args[3]);
        Canvas.DrawTriangle(x1, y1, x2, y2);
        Program.window.updateTimer.Tick += delegate
        {
            Canvas.DrawTriangle(x1, y1, x2, y2);
            Program.window.GetPictureBox().Refresh();
        };

    }
}

/// <summary>
/// Pen command, changes pen colour
/// </summary>
public class PenCommand : CanvasCommand
{
    /// <summary>
    /// Gets the name.
    /// </summary>
    /// <value>The name.</value>
    public override string Name => "pen";

    /// <summary>
    /// Gets the alias.
    /// </summary>
    /// <value>The alias.</value>
    public override string[] Alias => new string[] { };

    /// <summary>
    /// if using 2 colours then returns true if currently on color1
    /// </summary>
    private bool isColor1 = true;

    /// <summary>
    /// Changes pen colour
    /// </summary>
    /// <param name="args">Pen colour of the following: black, red, green, blue</param>
    /// <exception cref="global.InvalidArgumentSizeException">1</exception>
    /// <exception cref="global.InvalidArgumentException">Unsupported brush colour. Supported colours: 1:RED, 2:BLACK, 3:GREEN, 4:BLUE</exception>
    public override void Execute(string[] args)
    {
        // Expects one arguments
        
        if (args.Length != 1)
        {
            throw new InvalidArgumentSizeException(1, args.Length);
        }
        int colour = new IntegerArgumentPredicate(0, 6).Validate(args[0].ToLower());
        
        switch (colour)
        {
            case 0:
                break;
            case 1:
                Canvas.ChangeBrush(Brushes.Red);
                break;
            case 2:
                Canvas.ChangeBrush(Brushes.Green);
                break;
            case 3:
                Canvas.ChangeBrush(Brushes.Blue);
                break;
            case 4:
                Program.window.updateTimer.Tag = new ColourEventArgs()
                {
                    Color1 = Brushes.Green,
                    Color2 = Brushes.Blue
                };
                Program.window.updateTimer.Tick += new EventHandler(UpdateColor);
                break;
            case 5:
                Program.window.updateTimer.Tag = new ColourEventArgs()
                {
                    Color1 = Brushes.Blue,
                    Color2 = Brushes.Yellow
                };
                Program.window.updateTimer.Tick += new EventHandler(UpdateColor);
                break;
            case 6:
                Program.window.updateTimer.Tag = new ColourEventArgs()
                {
                    Color1 = Brushes.Black,
                    Color2 = Brushes.White
                };
                Program.window.updateTimer.Tick += new EventHandler(UpdateColor);
                break;
            default:
                throw new InvalidArgumentException("Unsupported brush colour. Supported colours: 1:RED, 2:BLACK, 3:GREEN, 4:BLUE");
        }

    }


    /// <summary>
    /// Updates the color.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    public void UpdateColor(object sender, EventArgs e)
    {
       ColourEventArgs args = ((ColourEventArgs)((Timer)sender).Tag);
       if(isColor1)
        {
            Canvas.ChangeBrush(args.Color1);
        } else
        {
            Canvas.ChangeBrush(args.Color2);
        }
        isColor1 = !isColor1;
    }

    /// <summary>
    /// Class ColourEventArgs.
    /// Implements the <see cref="System.EventArgs" />
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    class ColourEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the color1.
        /// </summary>
        /// <value>The color1.</value>
        public Brush Color1 { get; set; }
        /// <summary>
        /// Gets or sets the color2.
        /// </summary>
        /// <value>The color2.</value>
        public Brush Color2 { get; set; }
    }
}


/// <summary>
/// Predicate validating the parse between string to boolean
/// </summary>
public class BooleanPredicate : ArgumentPredicate<bool>
{
    /// <summary>
    /// Validates if <paramref name="argument" /> is valid boolean
    /// </summary>
    /// <param name="argument">the string boolean OR on/off</param>
    /// <returns><paramref name="argument" /> as boolean</returns>
    /// <exception cref="global.InvalidArgumentException">Could not parse argument to boolean. (on/off) or (true/false)</exception>
    public override bool Validate(string argument)
    {

        if(argument == "on" || argument == "true")
        {
            return true;
        } 
        if(argument == "off" || argument == "false")
        {
            return false;
        }
        throw new InvalidArgumentException("Could not parse argument to boolean. (on/off) or (true/false)");
    }
}

/// <summary>
/// Fill command, turns on/off shape filling
/// </summary>
public class FillCommand : CanvasCommand
{
    /// <summary>
    /// Gets the name.
    /// </summary>
    /// <value>The name.</value>
    public override string Name => "fill";

    /// <summary>
    /// Gets the alias.
    /// </summary>
    /// <value>The alias.</value>
    public override string[] Alias => new string[] { };
    /// <summary>
    /// Turns on/off shape filling
    /// </summary>
    /// <param name="args">on/off or true/false</param>
    /// <exception cref="global.InvalidArgumentSizeException">1</exception>
    public override void Execute(string[] args)
    {
        // Expects one arguments
        if (args.Length != 1)
        {
            throw new InvalidArgumentSizeException(1, args.Length);
        }
        bool fill = new BooleanPredicate().Validate(args[0]);

        Canvas.Fill(fill);
    }
}