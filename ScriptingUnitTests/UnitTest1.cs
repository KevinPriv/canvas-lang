using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScriptingLanguage;

namespace ScriptingUnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            new ScriptParser().parse();
        }
    }
}
