using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using grade_scores;

namespace graded_unit_testing {
    [TestClass]
    public class unitTests {

        //Creates a simple file given a location and a array of strings.
        private void writeSampleFile(string fileLocation, ref string[] lines) {
            System.IO.StreamWriter file = new System.IO.StreamWriter(fileLocation);
            foreach (string line in lines) {
                file.WriteLine(line);
            }
            file.Close();
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
        public void TestMethodReadWrite() {
            //Setup given text file for test
            string[] newArgs = new string[1];
            string[] fileLines = { "Smith, John, 32", "Spencer, Joel, 85", "Spencer, Cody, 85" };
            newArgs[0] = "C:\\grade.txt";
            writeSampleFile(newArgs[0], ref fileLines);

            //Determine created text files contents
            grade_scores.grade_scores myProgram = new grade_scores.grade_scores();
            myProgram.mainGradeScoresMethod(newArgs);
            string[] wrote = System.IO.File.ReadAllLines("C:\\grade-graded.txt"); ;
            Assert.AreEqual(fileLines[2], wrote[0]);
            Assert.AreEqual(fileLines[1], wrote[1]);
            Assert.AreEqual(fileLines[0], wrote[2]);
        }
    }
}
