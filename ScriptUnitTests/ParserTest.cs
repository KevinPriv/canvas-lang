namespace ScriptUnitTests
{
    
    /// <summary>
    /// Unit tests for <see cref="ScriptParser"/>
    /// </summary>
    [TestClass]
    public class ParserTest
    {
        ScriptEnvironment env;


        /// <summary>
        /// Sets up <see cref="ScriptEnvironment"/>
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            CommandDispatcher dispatcher = new CommandDispatcher();
            this.env = new ScriptEnvironment(dispatcher);
            dispatcher.Registry.Register(new CircleCommandMock());
        }

        /// <summary>
        /// Tests Parse Method
        /// Input: No Matching Commands
        /// Expects: InvalidCommandException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidCommandException))]
        public void Parse_NoMatchingCommands_ThrowsInvalidCommandExeption()
        {
            // arrange
            string command = "cicle 10";
            // act
            env.Execute(command);
        }


        /// <summary>
        /// Tests Parse Method
        /// Input: Valid Command
        /// </summary>
        [TestMethod]
        public void Parse_ValidCommand()
        {
            string command = "circle 10";
            // act
            env.Execute(command);
        }

    }


    /// <summary>
    /// Command Mock to be added to parser
    /// </summary>
    public class CircleCommandMock : AbstractCommand
    {
        public override string Name => "circle";

        public override string[] Alias => new string[0];

        public override void Execute(string[] args)
        {
            Console.WriteLine("Drawing Circle");
        }
    }
}
   