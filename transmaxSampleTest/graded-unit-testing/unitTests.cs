using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using grade_scores;

namespace graded_unit_testing {
    [TestClass]
    public class unitTests {
        private void helperTestMethod1() {

        }

        [TestMethod, TestCategory("grade_scores")]
        public void TestMethod1() {
            string[] newArgs = new string[1];
            grade_scores.Program myProgram = new grade_scores.Program();
            string output = myProgram.getOutputLocation("classA-grades.txt");
            Assert.AreEqual("classA-grades-graded.txt", output);
            //Ok what should I test.
            //There are like 6 methods. The true main, and writeOut, sortArray, stringArrayToLineInfoArray, getLastFileName, getOutputLocation, also LineInfo

            //grade_scores.;
            //Assert.AreEqual();
        }

        public void TestMethod2() {
            string[] newArgs = new string[1];
            grade_scores.Program myProgram = new grade_scores.Program();
            string output = myProgram.getOutputLocation("classA-grades.txt");
            Assert.AreEqual("", output);
            //There are like 6 methods. The true main, and writeOut, sortArray, stringArrayToLineInfoArray, getLastFileName, getOutputLocation, also LineInfo
        }
    }
}
