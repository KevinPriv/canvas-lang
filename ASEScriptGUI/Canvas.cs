// ***********************************************************************
// Assembly         : ASEScriptGUI
// Author           : Kevin Brewster
// Created          : 11-09-2022
//
// Last Modified By : Kevin Brewster
// Last Modified On : 01-16-2023
// ***********************************************************************
// <copyright file="Canvas.cs" company="ASEScriptGUI">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.CodeDom;

/// <summary>
/// The canvas within the UI, drew ontop of the PictureBox
/// </summary>
public class Canvas
 {

    /// <summary>
    /// The width
    /// </summary>
    public readonly int WIDTH = 442;
    /// <summary>
    /// The height
    /// </summary>
    public readonly int HEIGHT = 325;

    /// <summary>
    /// The current pen
    /// </summary>
    private Pen currentPen = new Pen(Brushes.Black);

    /// <summary>
    /// The graphics
    /// </summary>
    private Graphics graphics;

    /// <summary>
    /// The pen x
    /// </summary>
    public int penX = 0;
    /// <summary>
    /// The pen y
    /// </summary>
    public int penY = 0;

    /// <summary>
    /// The fill
    /// </summary>
    private bool fill = false;

    /// <summary>
    /// The update frames
    /// </summary>
    public List<Action> updateFrames = new List<Action>();
    /// <summary>
    /// Sets graphics context
    /// </summary>
    /// <param name="graphics">graphics context</param>
    public Canvas(Graphics graphics)
    {
        this.graphics = graphics;
    }
    /// <summary>
    /// Clears canvas
    /// </summary>
    public void Clear()
    {
        graphics.Clear(Color.White);
    }

    /// <summary>
    /// Resets pen position
    /// </summary>
    public void Reset()
    {
        this.penX = 0;
        this.penY = 0;
    }

    /// <summary>
    /// Turn shape filling on/off
    /// </summary>
    /// <param name="fill">turn fill on/off</param>
    public void Fill(bool fill)
    {
        this.fill = fill;
    }

    /// <summary>
    /// Draw Rectangle
    /// </summary>
    /// <param name="width">width of rect</param>
    /// <param name="length">length of rect</param>
    public void DrawRect(int width, int length)
    {
        if (fill)
        {
            graphics.FillRectangle(currentPen.Brush, penX, penY, width, length);
        } else
        {
            graphics.DrawRectangle(currentPen, penX, penY, width, length);
        }
    }
    /// <summary>
    /// Draw circle
    /// </summary>
    /// <param name="radius">radius of circle</param>
    public void DrawCircle(int radius)
    {
        if (fill)
        {
            graphics.FillEllipse(currentPen.Brush, penX, penY, radius, radius);
        }
        else
        {
            graphics.DrawEllipse(currentPen, penX, penY, radius, radius);
        }
    }

    /// <summary>
    /// Draws triangle from origin to (x2, y2) to (x3, y3)
    /// </summary>
    /// <param name="x2">x2</param>
    /// <param name="y2">y2</param>
    /// <param name="x3">x3</param>
    /// <param name="y3">y3</param>
    public void DrawTriangle(int x2, int y2, int x3, int y3) 
    {

        PointF[] points = new PointF[3]
        {
            new Point(penX, penY),
            new Point(x2, y2),
            new Point(x3, y3)
        };
        if (fill)
        {
            graphics.FillPolygon(currentPen.Brush, points);
        }
        else
        {
            graphics.DrawPolygon(currentPen,points);
        }
    }
    /// <summary>
    /// Moves pen position to (x,y)
    /// </summary>
    /// <param name="newX">new x position</param>
    /// <param name="newY">new y position</param>
    public void MoveTo(int newX, int newY)
    {
        this.penX = newX;
        this.penY = newY;
    }

    /// <summary>
    /// Draws line from origin to (newX, newY)
    /// </summary>
    /// <param name="newX">position to draw line to</param>
    /// <param name="newY">postion to draw line to</param>
    public void Draw(int newX, int newY)
    {
        graphics.DrawLine(currentPen, penX, penY, newX, newY);
        MoveTo(newX, newY);
    }

    /// <summary>
    /// Changes brush colour
    /// </summary>
    /// <param name="brush">The brush colour</param>
    public void ChangeBrush(Brush brush)
    {
        currentPen.Brush = brush;
    }

}
