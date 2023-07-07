// ***********************************************************************
// Assembly         : ASEScriptGUI
// Author           : Kevin Brewster
// Created          : 11-07-2022
//
// Last Modified By : Kevin Brewster
// Last Modified On : 01-16-2023
// ***********************************************************************
// <copyright file="ScriptingWindow.cs" company="ASEScriptGUI">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Diagnostics;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace ASEScriptGUI
{
    /// <summary>
    /// Contains ScriptingWindow Events alongside access to the ScriptParser and CommandParser
    /// </summary>
    public partial class ScriptingWindow : Form
    {

        /// <summary>
        /// The canvas
        /// </summary>
        public Canvas? canvas;

        /// <summary>
        /// Multi-line commands with script support
        /// </summary>
        public CommandDispatcher scriptCommandDispatcher = new CommandDispatcher();

        /// <summary>
        /// Single line commands, no script support
        /// </summary>
        public CommandDispatcher commandDispatcher = new CommandDispatcher();


        /// <summary>
        /// The update timer
        /// </summary>
        public System.Windows.Forms.Timer updateTimer = new System.Windows.Forms.Timer();

        /// <summary>
        /// Gets the script text box.
        /// </summary>
        /// <value>The script text box.</value>
        public RichTextBox ScriptTextBox
        {
            get { return scriptWindow; }
        }

        /// <summary>
        /// Adds commands to commandRegistry and scriptRegistry
        /// </summary>
        public ScriptingWindow()
        {
            InitializeComponent();
            CommandRegistry scriptRegistry = scriptCommandDispatcher.Registry;
            CommandRegistry commandRegistry = commandDispatcher.Registry;

            // Canvas Commands
            scriptRegistry.Register(new MovetoCommand());
            scriptRegistry.Register(new DrawtoCommand());
            scriptRegistry.Register(new ClearCommand());
            scriptRegistry.Register(new ResetCommand());

            // Shape commands
            scriptRegistry.Register(new RectangleCommand());
            scriptRegistry.Register(new TriangleCommand());
            scriptRegistry.Register(new CircleCommand());

            // Pen commands
            scriptRegistry.Register(new PenCommand());
            scriptRegistry.Register(new FillCommand());

            // Command Line commands
            commandRegistry.Register(new RunCommand());

            commandRegistry.Commands.AddRange(scriptRegistry.Commands);
        }

        /// <summary>
        /// Sets up bitmap ontop of the PictureBox.
        /// Also creates instance of Canvas which can draw ontop of the bitmap.
        /// </summary>
        public void SetupPictureBox()
        {
            Bitmap bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics graphic = Graphics.FromImage(bitmap);
            canvas = new Canvas(graphic);
            pictureBox1.Image = bitmap;
            updateTimer.Stop();
            updateTimer = new System.Windows.Forms.Timer();
            updateTimer.Interval = 500; // specify interval time as you want
            updateTimer.Start();
        }

        /// <summary>
        /// Gets picturebox from form
        /// </summary>
        /// <returns>Picturebox</returns>
        public PictureBox GetPictureBox()
        {
            return pictureBox1;
        }

        /// <summary>
        /// Handles the Click event of the loadToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Title = "Browse ASEScript Files",

                    CheckFileExists = true,
                    CheckPathExists = true,

                    DefaultExt = "txt",
                    Filter = "txt files (*.txt)|*.txt",
                    RestoreDirectory = true,

                    ReadOnlyChecked = true,
                    ShowReadOnly = true
                };

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (StreamReader reader = new StreamReader(openFileDialog.FileName))
                    {
                        string[] lines =  ScriptFile.Read(reader);
                        ScriptTextBox.Clear();
                        foreach(string line in lines)
                        {
                            ScriptTextBox.AppendText(line + "\n");
                        }
                    }
                }
            
        }

        /// <summary>
        /// Handles the TextChanged event of the richTextBox1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Click event of the pictureBox1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Load event of the ScriptingWindow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ScriptingWindow_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Handles the Click event of the label2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void label2_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the 1 event of the label1_Click control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the KeyDown event of the textBox1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                SetupPictureBox();
                canvas.Clear();
                try
               {
                    ScriptEnvironment interpreter = new ScriptEnvironment(commandDispatcher);
                    interpreter.Execute(commandLine.Text);
                } catch(Exception exception)
                {
                   // MessageBox.Show(exception.Message, "An exception occured!");
                    DateTime now = DateTime.Now;
                    this.outputBox.SelectionColor = Color.Red;
                    this.outputBox.AppendText("----Exception----\n");
                    this.outputBox.AppendText(now.ToString("F") + "\n");
                    this.outputBox.AppendText(exception.Message + "\n");
                    this.outputBox.SelectionColor = Color.Red;
                    this.outputBox.AppendText("-----------------\n");
 
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        /// <summary>
        /// Handles the Click event of the fileToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Click event of the saveToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Title = "Save ASEScript File",

                CheckPathExists = true,

                DefaultExt = "txt",
                Filter = "txt files (*.txt)|*.txt",
                RestoreDirectory = true,

            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                
                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                {
                    ScriptFile.Write(writer, ScriptTextBox.Text.Split("\n"));
                }
            }
        }

        /// <summary>
        /// Handles the ItemClicked event of the menuStrip1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ToolStripItemClickedEventArgs"/> instance containing the event data.</param>
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}