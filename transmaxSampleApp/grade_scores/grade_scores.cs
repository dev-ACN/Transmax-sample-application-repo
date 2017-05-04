using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grade_scores {
    public class grade_scores {

        //Normally would declare many of these methods private, but making it simple to call them for testing.
        //A class to store relevant information a line in a read txt file
        public class LineInfo {
            public int grade;
            public String firstName;
            public String lastName;
        }

        //Takes a directory path and returns the name of the output text file with '-graded.txt' on end in place of assumed '.txt'.
        private void exceptionThrown(String error) {
            Console.WriteLine(error);
            throw new System.ArgumentException(error);
        }

        public String getOutputLocation(String readlocation) {
            String outputFileLocation = readlocation;
            outputFileLocation = outputFileLocation.Remove(outputFileLocation.Length - 4) + "-graded.txt";
            return outputFileLocation;
        }

        //Splits a directory path into each folder and returns the last file, to get that file by itself.
        public String getLastFileName(String fileLocation) {
            String[] outputFolders = fileLocation.Split('\\');
            return outputFolders[outputFolders.Length - 1];
        }


        //Takes 2 lineInfo arrays one containing values, and one to contain the sorted values
        //Sorts info by grade (desc), lastname, firstname
        public void sortArray(ref LineInfo[] sortedValues, ref LineInfo[] unsortedValues) {
            sortedValues = new LineInfo[unsortedValues.Length];
            sortedValues = unsortedValues.OrderByDescending(infograde => infograde.grade).ThenBy(infoLastName => infoLastName.lastName).ThenBy(infoFirstName => infoFirstName.firstName).ToArray();
        }


        //Reads information from a array of strings each with expected format  "lastname, firstname, grade" Into lineInfo array
        public void stringArrayToLineInfoArray(ref String[] inputText, ref LineInfo[] unsortedInfo) {
            unsortedInfo = new LineInfo[inputText.Length];
            int insertedLines = 0;
            foreach (string line in inputText) {//For each line in file, fill the sorted info array with values
                //Split the user info
                String[] linesInfo = line.Split(' ');
                if (linesInfo.Length < 3) {//Must be 3, somtimes 4 if space after last line, for now just enforcing no less than 3
                    exceptionThrown("Sorry there is somthing wrong with the text files format.");
                }
                //Note format in linesInfo is Lastname, Firstname, Grade so 0 is lastname, 1 is firstname and 2 is grade in seperateUserInfo[].
                try {
                    int thisGrade = Convert.ToInt32(linesInfo[2]);
                } catch {
                    exceptionThrown("Sorry there is somthing wrong with the text files format.");
                }
                unsortedInfo[insertedLines] = new LineInfo();
                unsortedInfo[insertedLines].firstName = linesInfo[1];
                unsortedInfo[insertedLines].lastName = linesInfo[0];
                unsortedInfo[insertedLines].grade = Convert.ToInt32(linesInfo[2]);
                insertedLines++;
            }
        }

        //Takes a array of LineInfo and a file location to create a text file containing the information in the order of the LineInfo array. Also Writes this to console.
        //For testing also returns a String
        public String writeOut(ref LineInfo[] givenInfo, string fileLocation) {
            System.IO.StreamWriter file = new System.IO.StreamWriter(fileLocation);
            String allLines = "";
            String writtenLine;
            foreach (LineInfo line in givenInfo) {//Write each line
                writtenLine = line.lastName + ' ' + line.firstName + ' ' + line.grade;
                allLines += writtenLine + '\n';
                Console.WriteLine(writtenLine);
                file.WriteLine(writtenLine);
            }
            file.Close();
            return allLines;
        }


        //The main method that can also be called in unit tests.
        public void mainGradeScoresMethod(string[] args) {

            String inputFileLocation;
            String outputFileLocation;
            String[] inputText;
            LineInfo[] unsortedInfo = new LineInfo[1];
            LineInfo[] sortedInfo = new LineInfo[1];

            //Determine input and if valid file
            if (args.Length == 0) {//No input given request file location
                Console.Write("Please enter the location of the input file:\n");
                inputFileLocation = Console.ReadLine();
            } else {//File location already given
                inputFileLocation = args[0];
            }

            if (".txt" != Path.GetExtension(inputFileLocation)) {//Going to assume for sake of testing this wont need to be checked in other public methods.
                exceptionThrown("Only supposed to be for .txt files");
            }

            if (File.Exists(inputFileLocation)) {//Check given file exists
                string text = System.IO.File.ReadAllText(inputFileLocation);
            } else {
                exceptionThrown("Sorry, cannot find that file");
            }
            //Ressolve output text file
            outputFileLocation = getOutputLocation(inputFileLocation);





            //Read in file
            inputText = System.IO.File.ReadAllLines(inputFileLocation);
            //moves read string array into lineInfo array
            stringArrayToLineInfoArray(ref inputText, ref unsortedInfo);
            //Sorts lineInfo array by grade (desc), lastname, firstname
            sortArray(ref sortedInfo, ref unsortedInfo);

            //Now to write out the Info to both console and text file
            writeOut(ref sortedInfo, outputFileLocation);

            //Closing statement
            Console.Write("Finished: created " + getLastFileName(outputFileLocation));
            Console.ReadLine();
        }

        //Main method runs gradeScores.
        static void Main(string[] args) {
            (new grade_scores()).mainGradeScoresMethod(args);
        }
    }
}
