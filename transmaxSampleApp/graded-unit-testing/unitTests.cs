using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using grade_scores;

namespace graded_unit_testing {
    [TestClass]
    public class unitTests {
        

        //Helper method fills a simple file given a location and a array of strings.
        private void writeSampleFile(string fileLocation, ref string[] lines) {
            System.IO.StreamWriter file = new System.IO.StreamWriter(fileLocation);
            foreach (string line in lines) {//Writes out each line to file
                file.WriteLine(line);
            }
            file.Close();
        }


        private String usingDirectory() {//Helper method ethod to easily standardise the directory used for files made across tests
            String path = "C:\\sampleGradeFolder";
            System.IO.Directory.CreateDirectory(path);
            return path;
        }


        //Test sorting by grades
        //Note for this test case sorting by firstname or lastname would give different orders.
        [TestMethod, TestCategory("grade_scores")]
        public void simpleSortGrades() {
            //Setup given text file for test
            string[] newArgs = { usingDirectory() + "\\grade.txt" };
            string[] fileLines = { "Luther, Laura, 32", "Jones, Jayden, 85", "Anderson, Alex, 3" };
            writeSampleFile(newArgs[0], ref fileLines);

            //Determine created text files contents
            grade_scores.grade_scores myProgram = new grade_scores.grade_scores();
            myProgram.mainGradeScoresMethod(newArgs);
            string[] wrote = System.IO.File.ReadAllLines(usingDirectory()+"\\grade-graded.txt");
            
            //Assertions that should be true. fileLines should be rearranged into written text file in this order
            Assert.AreEqual(fileLines[1], wrote[0]);
            Assert.AreEqual(fileLines[0], wrote[1]);
            Assert.AreEqual(fileLines[2], wrote[2]);
        }


        //Test sorting by lastname when grades are equal
        //note for this test case sorting by Firstname at a higher proity would instead give a result in a different order
        [TestMethod, TestCategory("grade_scores_sorting")]
        public void simpleSortByLastName() {
            //Setup given text file for test
            string[] newArgs = { usingDirectory() + "\\grade.txt" };
            string[] fileLines = { "Anderson, John, 32", "Spencer, Joel, 32", "Burness, Cody, 32" };
            writeSampleFile(newArgs[0], ref fileLines);

            //Determine created text files contents
            grade_scores.grade_scores myProgram = new grade_scores.grade_scores();
            myProgram.mainGradeScoresMethod(newArgs);
            string[] wrote = System.IO.File.ReadAllLines(usingDirectory() + "\\grade-graded.txt");

            //Assertions that should be true. fileLines should be rearranged into written text file in this order
            Assert.AreEqual(fileLines[0], wrote[0]);
            Assert.AreEqual(fileLines[2], wrote[1]);
            Assert.AreEqual(fileLines[1], wrote[2]);
        }


        //Test sorting by firstname with all other values the same.
        [TestMethod, TestCategory("grade_scores_sorting")]
        public void simpleSortByFirstName() {
            //Setup given text file for test
            string[] newArgs = { usingDirectory() + "\\grade.txt" };
            string[] fileLines = { "Spencer, Bob, 32", "Spencer, Amy, 32", "Spencer, Cody, 32" };
            writeSampleFile(newArgs[0], ref fileLines);

            //Determine created text files contents
            grade_scores.grade_scores myProgram = new grade_scores.grade_scores();
            myProgram.mainGradeScoresMethod(newArgs);
            string[] wrote = System.IO.File.ReadAllLines(usingDirectory() + "\\grade-graded.txt");

            //Assertions that should be true. fileLines should be rearranged into written text file in this order
            Assert.AreEqual(fileLines[1], wrote[0]);
            Assert.AreEqual(fileLines[0], wrote[1]);
            Assert.AreEqual(fileLines[2], wrote[2]);
        }

        //This test runs as above but extra white space between words, does not change outcome
        [TestMethod, TestCategory("grade_scores_sorting")]
        public void sortExtraWhiteSpace() {
            //Setup given text file for test
            string[] newArgs = { usingDirectory() + "\\grade.txt" };
            string[] fileLines = { "Spencer,   Bob, 32", "Spencer,    Amy, 32", "Spencer,      Cody, 32" };
            writeSampleFile(newArgs[0], ref fileLines);

            //Determine created text files contents
            grade_scores.grade_scores myProgram = new grade_scores.grade_scores();
            myProgram.mainGradeScoresMethod(newArgs);
            string[] wrote = System.IO.File.ReadAllLines(usingDirectory() + "\\grade-graded.txt");

            //Assertions that should be true. fileLines should be rearranged into written text file in this order
            //Note dont use values in fileLines as whitespace removed in output
            Assert.AreEqual("Spencer, Amy, 32", wrote[0]);
            Assert.AreEqual("Spencer, Bob, 32", wrote[1]);
            Assert.AreEqual("Spencer, Cody, 32", wrote[2]);
        }


        //This test demonstrates how additional informatoin can be added to end of each line without throwing an exception
        [TestMethod, TestCategory("grade_scores_sorting")]
        public void sortedBadInfo() {
            //Setup given text file for test
            string[] newArgs = { usingDirectory() + "\\grade.txt" };
            string[] fileLines = { "Spencer, Bob, 32, Physics", "Spencer, Amy, 32, Math", "Spencer, Cody, 32, English" };
            writeSampleFile(newArgs[0], ref fileLines);

            //Determine created text files contents
            grade_scores.grade_scores myProgram = new grade_scores.grade_scores();
            myProgram.mainGradeScoresMethod(newArgs);
            string[] wrote = System.IO.File.ReadAllLines(usingDirectory() + "\\grade-graded.txt");

            //Assertions that should be true. fileLines should be rearranged into written text file in this order
            //Note dont use values in fileLines as whitespace removed in output
            Assert.AreEqual("Spencer, Amy, 32", wrote[0]);
            Assert.AreEqual("Spencer, Bob, 32", wrote[1]);
            Assert.AreEqual("Spencer, Cody, 32", wrote[2]);
        }

        //This Test Removes the grade from amy in the written file
        [TestMethod, TestCategory("grade_scores_exceptions")]
        [ExpectedException(typeof(ArgumentException), "")]
        public void exceptionMissingValues() {
            //Setup given text file for test
            string[] newArgs = { usingDirectory() + "\\grade.txt" };
            string[] fileLines = { "Spencer, Bob, 32", "Spencer, Amy, ", "Spencer, Cody, 32" };
            writeSampleFile(newArgs[0], ref fileLines);

            //Attempt created text files contents
            grade_scores.grade_scores myProgram = new grade_scores.grade_scores();
            myProgram.mainGradeScoresMethod(newArgs);
        }

        //This Test like the proir one has a missing grade, but also no ', '  for amy
        [TestMethod, TestCategory("grade_scores_exceptions")]
        [ExpectedException(typeof(ArgumentException), "")]
        public void exceptionMissingValues2() {
            //Setup given text file for test
            string[] newArgs = { usingDirectory() + "\\grade.txt" };
            string[] fileLines = { "Spencer, Bob, 32", "Spencer, Amy", "Spencer, Cody, 32" };
            writeSampleFile(newArgs[0], ref fileLines);

            //Attempt created text files contents
            grade_scores.grade_scores myProgram = new grade_scores.grade_scores();
            myProgram.mainGradeScoresMethod(newArgs);
        }

        //This test has amy removed from the line, so far throws exception, but could be adjusted to resolve with a missing line
        [TestMethod, TestCategory("grade_scores_exceptions")]
        [ExpectedException(typeof(ArgumentException), "")]
        public void exceptionMissingLine() {
            //Setup given text file for test
            string[] newArgs = { usingDirectory() + "\\grade.txt" };
            string[] fileLines = { "Spencer, Bob, 32", "", "Spencer, Cody, 32" };
            writeSampleFile(newArgs[0], ref fileLines);

            //Attempt created text files contents
            grade_scores.grade_scores myProgram = new grade_scores.grade_scores();
            myProgram.mainGradeScoresMethod(newArgs);
        }

        //This test has no spacing between Amy and the grade
        [TestMethod, TestCategory("grade_scores_exceptions")]
        [ExpectedException(typeof(ArgumentException), "")]
        public void exceptionNoSpacing() {
            //Setup given text file for test
            string[] newArgs = { usingDirectory() + "\\grade.txt" };
            string[] fileLines = { "Spencer, Bob, 32", "Spencer, Amy32", "Spencer, Cody, 32" };
            writeSampleFile(newArgs[0], ref fileLines);

            //Attempt created text files contents
            grade_scores.grade_scores myProgram = new grade_scores.grade_scores();
            myProgram.mainGradeScoresMethod(newArgs);
        }


        //This test checks the file is a .txt file
        [TestMethod, TestCategory("grade_scores_exceptions")]
        [ExpectedException(typeof(ArgumentException), "")]
        public void excetpionTxtFile() {
            //Setup given text file for test
            string[] newArgs = { usingDirectory() + "\\grade" };
            string[] fileLines = { "Spencer, Bob, 32", "Spencer, Amy, 32", "Spencer, Cody, 32" };
            writeSampleFile(newArgs[0], ref fileLines);

            //Attempt created text files contents
            grade_scores.grade_scores myProgram = new grade_scores.grade_scores();
            myProgram.mainGradeScoresMethod(newArgs);
        }

        //Tests 3rd Value in a line is indeed a Int
        [TestMethod, TestCategory("grade_scores_exceptions")]
        [ExpectedException(typeof(ArgumentException), "")]
        public void exceptionNotInt() {
            //Setup given text file for test
            string[] newArgs = { usingDirectory() + "\\grade" };
            string[] fileLines = { "Spencer, Bob, forty", "Spencer, Amy, 32", "Spencer, Cody, 32" };
            writeSampleFile(newArgs[0], ref fileLines);

            //Attempt created text files contents
            grade_scores.grade_scores myProgram = new grade_scores.grade_scores();
            myProgram.mainGradeScoresMethod(newArgs);
        }

        //Tests missing File
        [TestMethod, TestCategory("grade_scores_exceptions")]
        [ExpectedException(typeof(ArgumentException), "")]
        public void exceptionNoFile() {
            //Setup given text file for test
            string[] newArgs = { usingDirectory() + "\\grade2" };
            string[] fileLines = { "Spencer, Bob, forty", "Spencer, Amy, 32", "Spencer, Cody, 32" };

            //Attempt created text files contents
            grade_scores.grade_scores myProgram = new grade_scores.grade_scores();
            myProgram.mainGradeScoresMethod(newArgs);
        }
    }
}
