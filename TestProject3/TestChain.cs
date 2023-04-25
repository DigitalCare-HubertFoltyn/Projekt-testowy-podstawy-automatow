using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject3
{
    [TestClass]
    internal class TestChain
    {
        private int a;
        public TestContext TestContext { get; set; }
        private static TestContext _testContext;




        [TestInitialize]
        public void RunBeforeEveryTest()
        {
            //this.TestContext = TestContext;
            a = 1;
          

        }
        [TestCleanup]
        public void RunAfterEveryTestMethod()
        {
            Trace.Write("RunAfterEveryTestMethod will execute after every single test method in a class");
        }
        [ClassInitialize]
        public static void RunBeforeAllOfTheTestMethodsStarted(TestContext testContext)
        {
            _testContext = testContext;
            Trace.Write("I will run one time before all the tests in the class started");
        }
        [ClassCleanup]
        public void RunAfterEveryTestClass()
        {
            Trace.Write("I will run one time after all test in the class finished");
        }

        //use the test context like this, not through the static property as I showed in my videos
        [TestMethod]
        public void TestMethod2()
        {
            //Use it using TestContext instead of _testContext
            Trace.Write(TestContext.TestName);
            Assert.IsTrue(a == 1);
        }




    }
}
