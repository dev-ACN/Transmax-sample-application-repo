using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using grade_scores;

namespace graded_unit_testing {
    [TestClass]
    public class unitTests {
        //As it stands, I may have to do unitTests Without really making a file for Appveyor...
        //Oddly the unit-tests need better permision.
        //Creates a simple file given a location and a array of strings.
        private void writeSampleFile(string fileLocation, ref string[] lines) {
            System.IO.StreamWriter file = new System.IO.StreamWriter(fileLocation);
            foreach (string line in lines) {
                file.WriteLine(line);
            }
            file.Close();
        }


        private String usingDirectory() {//Method to easily standardise the directory used for files made 
            String path = "C:\\sampleGradeFolder";
            System.IO.Directory.CreateDirectory(path);
            return path;
        }

        [TestMethod, TestCategory("grade_scores_methods")]

        public void getValidOutputFileLocation() {
            string[] newArgs = new string[1];
            grade_scores.grade_scores myProgram = new grade_scores.grade_scores();
            string output = myProgram.getOutputLocation("classA-grades.txt");
            Assert.AreEqual("classA-grades-graded.txt", output);
        }

        [TestMethod, TestCategory("grade_scores_True_Method_AppveyorIssues")]
        public void TestMethodReadWrite() {
            //Setup given text file for test
            string[] newArgs = new string[1];
            string[] fileLines = { "Smith, John, 32", "Spencer, Joel, 85", "Spencer, Cody, 85" };

            newArgs[0] = usingDirectory()+"\\grade.txt";
            //newArgs[0] = "C:\\Sector\\General Content\\Git_Folder\\transmaxRepo\\sampleOutput\\grade2.txt";
            writeSampleFile(newArgs[0], ref fileLines);

            //Determine created text files contents
            grade_scores.grade_scores myProgram = new grade_scores.grade_scores();
            myProgram.mainGradeScoresMethod(newArgs);
            string[] wrote = System.IO.File.ReadAllLines(usingDirectory()+"\\grade-graded.txt");
            //Console.WriteLine("");
            Assert.AreEqual(fileLines[2], wrote[0]);
            Assert.AreEqual(fileLines[1], wrote[1]);
            Assert.AreEqual(fileLines[0], wrote[2]);
        }
    }
}
