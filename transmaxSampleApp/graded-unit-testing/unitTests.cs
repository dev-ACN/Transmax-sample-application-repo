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
            grade_scores.grade_scores myProgram = new grade_scores.grade_scores();
            string output = myProgram.getOutputLocation("classA-grades.txt");
            Assert.AreEqual("classA-grades-graded.txt", output);
            //Ok what should I test.
            //There are like 6 methods. The true main, and writeOut, sortArray, stringArrayToLineInfoArray, getLastFileName, getOutputLocation, also LineInfo

            //grade_scores.;
            //Assert.AreEqual();
        }

        [TestMethod, TestCategory("grade_scores")]
        public void TestMethodFail() {
            string[] newArgs = new string[1];
            grade_scores.grade_scores myProgram = new grade_scores.grade_scores();
            string output = myProgram.getOutputLocation("classA-grades.txt");
            Assert.AreEqual("", output);
            //There are like 6 methods. The true main, and writeOut, sortArray, stringArrayToLineInfoArray, getLastFileName, getOutputLocation, also LineInfo
        }

        [TestMethod, TestCategory("grade_scores")]
        public void TestMethodReadWrite() {
            string[] newArgs = new string[1];
            newArgs[0] = "C:\\grade.txt";
            System.IO.StreamWriter file = new System.IO.StreamWriter(newArgs[0]);
            file.WriteLine("Patterson, Bill, 98");
            file.Close();
            grade_scores.grade_scores myProgram = new grade_scores.grade_scores();
            string[] wrote =  System.IO.File.ReadAllLines(myProgram.getOutputLocation(newArgs[0]));;
            Assert.AreEqual("Patterson, Bill, 98", wrote[0]);
            //There are like 6 methods. The true main, and writeOut, sortArray, stringArrayToLineInfoArray, getLastFileName, getOutputLocation, also LineInfo
        }

        [TestMethod, TestCategory("grade_scores")]
        public void autoFail() {
            Assert.AreEqual("Failed", "");
            //There are like 6 methods. The true main, and writeOut, sortArray, stringArrayToLineInfoArray, getLastFileName, getOutputLocation, also LineInfo
        }
    }
}
