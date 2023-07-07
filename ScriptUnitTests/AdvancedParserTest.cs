using System.Collections;
using System.Data;
using System.Diagnostics.Contracts;
using System.Transactions;


namespace ScriptUnitTests
{
    /// <summary>
    /// Tests for advanced parsing, includes variables, if statements and methods
    /// </summary>
    [TestClass]
    public class AdvancedParserTest
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
        /// Tests Execution of while statement but has invalid ending keyword
        /// Input: While loop with EndIf grammar instead of Endloop
        /// Expects: SyntaxException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SyntaxException))]
        public void While_InvalidEndingKeyWord_ThrowsSyntaxException()
        {
            // arrange
            string command = "While 10!=10\n"
                + "circle 20\n"
                + "Endif\n";
            // act
            env.Execute(command);
        }

        /// <summary>
        /// Tests Execution of while statement but has invalid ending keyword
        /// Input: While loop with EndIf grammar instead of Endloop
        /// Expects: SyntaxException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void While_NoEnding_ThrowsSyntaxException()
        {
            // arrange
            string command = "While 10!=10\n"
                + "circle 20\n"
                + "circle 15\n";
            // act
            env.Execute(command);
        }

        /// <summary>
        /// Tests Execution of while statement but has invalid ending keyword
        /// Input: If statement with EndWhile grammar instead of EndIf
        /// Expects: SyntaxException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void If_NoEnding_ThrowsSyntaxException()
        {
            // arrange
            string command = "If 10!=10\n"
                + "circle 20\n"
                + "circle 15\n";
            // act
            env.Execute(command);
        }

        /// <summary>
        /// Tests assigning of variables
        /// Input: Variable assign
        /// Expects: Success
        /// </summary>
        [TestMethod]
        public void Variable_Assign_Success()
        {
            // arrange
            string command = "var1 = 11";
            // act
            env.Execute(command);
        }

        /// <summary>
        /// Tests sending variables into command parameters
        /// Input: Passing variable into Command Parameters
        /// Expects: Success
        /// </summary>
        [TestMethod]
        public void Variable_CommandParameter_Success()
        {
            // arrange
            string command = "radius = 11\n" +
                "circle radius";
            // act
            env.Execute(command);
        }

        /// <summary>
        /// Tests sending variables into a method call
        /// Input: Passing variables into a method call
        /// Expects: Success
        /// </summary>
        [TestMethod]
        public void Variable_MethodPass_Success()
        {
            // arrange
            string command = "var1 = 11\n" +
                "Method DrawCircle(radius)\n" +
                "circle radius\n" +
                "Endmethod\n" +
                "DrawCircle(var1)";
            // act
            env.Execute(command);
        }

        /// <summary>
        /// Tests nested if statements inside of a method
        /// Input: Variable assign
        /// Expects: Success
        /// </summary>
        [TestMethod]
        public void NestedMethodAndIfStatemenet_Success()
        {
            // arrange
            string command = "Method DrawCircle(radius)\n" +
                "If var1 == radius\n" +
                "circle radius\n" +
                "Endif\n" + 
                "Endmethod\n";
            // act
            env.Execute(command);
        }

    }
}
