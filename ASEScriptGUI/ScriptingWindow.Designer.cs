// ***********************************************************************
// Assembly         : ASEScriptGUI
// Author           : Kevin Brewster
// Created          : 11-07-2022
//
// Last Modified By : Kevin Brewster
// Last Modified On : 11-21-2022
// ***********************************************************************
// <copyright file="ScriptingWindow.Designer.cs" company="ASEScriptGUI">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace ASEScriptGUI
{
    /// <summary>
    /// Class ScriptingWindow.
    /// Implements the <see cref="System.Windows.Forms.Form" />
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    partial class ScriptingWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scriptWindow = new System.Windows.Forms.RichTextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.commandLine = new System.Windows.Forms.TextBox();
            this.commandLineLabel = new System.Windows.Forms.Label();
            this.scriptWindowLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.outputBox = new System.Windows.Forms.RichTextBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // scriptWindow
            // 
            this.scriptWindow.Location = new System.Drawing.Point(12, 45);
            this.scriptWindow.Name = "scriptWindow";
            this.scriptWindow.Size = new System.Drawing.Size(328, 263);
            this.scriptWindow.TabIndex = 4;
            this.scriptWindow.Text = "";
            this.scriptWindow.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(346, 27);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(442, 325);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // commandLine
            // 
            this.commandLine.Location = new System.Drawing.Point(12, 329);
            this.commandLine.Name = "commandLine";
            this.commandLine.Size = new System.Drawing.Size(328, 23);
            this.commandLine.TabIndex = 6;
            this.commandLine.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // commandLineLabel
            // 
            this.commandLineLabel.AutoSize = true;
            this.commandLineLabel.Location = new System.Drawing.Point(12, 311);
            this.commandLineLabel.Name = "commandLineLabel";
            this.commandLineLabel.Size = new System.Drawing.Size(89, 15);
            this.commandLineLabel.TabIndex = 8;
            this.commandLineLabel.Text = "Command Line";
            this.commandLineLabel.Click += new System.EventHandler(this.label2_Click);
            // 
            // scriptWindowLabel
            // 
            this.scriptWindowLabel.AutoSize = true;
            this.scriptWindowLabel.Location = new System.Drawing.Point(12, 27);
            this.scriptWindowLabel.Name = "scriptWindowLabel";
            this.scriptWindowLabel.Size = new System.Drawing.Size(101, 15);
            this.scriptWindowLabel.TabIndex = 9;
            this.scriptWindowLabel.Text = "Scripting Window";
            this.scriptWindowLabel.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 355);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 15);
            this.label3.TabIndex = 11;
            this.label3.Text = "Output";
            // 
            // outputBox
            // 
            this.outputBox.Location = new System.Drawing.Point(12, 373);
            this.outputBox.Name = "outputBox";
            this.outputBox.ReadOnly = true;
            this.outputBox.Size = new System.Drawing.Size(776, 65);
            this.outputBox.TabIndex = 10;
            this.outputBox.Text = "";
            // 
            // ScriptingWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.outputBox);
            this.Controls.Add(this.scriptWindowLabel);
            this.Controls.Add(this.commandLineLabel);
            this.Controls.Add(this.commandLine);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.scriptWindow);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ScriptingWindow";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.ScriptingWindow_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        /// <summary>
        /// The menu strip1
        /// </summary>
        private MenuStrip menuStrip1;
        /// <summary>
        /// The file tool strip menu item
        /// </summary>
        private ToolStripMenuItem fileToolStripMenuItem;
        /// <summary>
        /// The load tool strip menu item
        /// </summary>
        private ToolStripMenuItem loadToolStripMenuItem;
        /// <summary>
        /// The save tool strip menu item
        /// </summary>
        private ToolStripMenuItem saveToolStripMenuItem;
        /// <summary>
        /// The script window
        /// </summary>
        private RichTextBox scriptWindow;
        /// <summary>
        /// The picture box1
        /// </summary>
        private PictureBox pictureBox1;
        /// <summary>
        /// The command line
        /// </summary>
        private TextBox commandLine;
        /// <summary>
        /// The command line label
        /// </summary>
        private Label commandLineLabel;
        /// <summary>
        /// The script window label
        /// </summary>
        private Label scriptWindowLabel;
        /// <summary>
        /// The label3
        /// </summary>
        private Label label3;
        /// <summary>
        /// The output box
        /// </summary>
        private RichTextBox outputBox;
    }
}