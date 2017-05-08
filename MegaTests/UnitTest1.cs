using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AI_Assignment_2;

namespace MegaTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            BuildKB KB = new BuildKB("apple.txt");
            TruthTable TT = new TruthTable(KB.Implies, KB.Vars, KB.TrueVars, "apple");
            
            Assert.IsTrue(TT.BuildTT() == "Yes 1", "Returns yes with 1 level for apple");
        }
    }
}
