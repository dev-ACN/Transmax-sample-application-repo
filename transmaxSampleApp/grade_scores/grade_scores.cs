using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

//Grade scores program 
//By Aaron Newton
//Accepts a '\path\file.txt' to file
//file should contain on each line "lastname, firstname, grade" in this order to work otherwise throws exception for only the firstline it reads that does not meet assumptions.
//creates a new '\path\file-graded.txt'sorts by grade, lastname, firstname

namespace grade_scores {
    public class grade_scores {

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

        //returns string+ -graded.txt in place of assumed .txt of a given string.
        private String getOutputLocation(String readlocation) {
            String outputFileLocation = readlocation;
            outputFileLocation = outputFileLocation.Remove(outputFileLocation.Length - 4) + "-graded.txt";
            return outputFileLocation;
        }

        //Splits a directory path into each folder and returns the last file, to get that file by itself.
        private String getLastFileName(String fileLocation) {
            String[] outputFolders = fileLocation.Split('\\');
            return outputFolders[outputFolders.Length - 1];
        }


        //Takes 2 lineInfo arrays one containing values, and one to contain the sorted values
        //Sorts info by grade (desc), lastname, firstname
        private void sortArray(ref LineInfo[] sortedValues, ref LineInfo[] unsortedValues) {
            sortedValues = new LineInfo[unsortedValues.Length];
            sortedValues = unsortedValues.OrderByDescending(infograde => infograde.grade).ThenBy(infoLastName => infoLastName.lastName).ThenBy(infoFirstName => infoFirstName.firstName).ToArray();
        }


        //Reads information from a array of strings each with expected format  "lastname, firstname, grade" Into lineInfo array
        private void stringArrayToLineInfoArray(ref String[] inputText, ref LineInfo[] unsortedInfo) {
            unsortedInfo = new LineInfo[inputText.Length];
            int insertedLines = 0;
            foreach (string line in inputText) {//For each line in file, fill the sorted info array with values
                //Remove whitespace
                string usedLine = Regex.Replace(line, @"\s+", "");
                String[] linesInfo = usedLine.Split(',');
                if (linesInfo.Length < 3) {//Should be 3, but extra spaces at last line may interfere, for now just enforcing no less than 3
                    exceptionThrown("Sorry there is somthing wrong with the text files format. Needs at least 3 values per line to read. Check line:" + (insertedLines+1)+" of the given text file.");
                }
                //Note format in linesInfo is Lastname, Firstname, Grade so 0 is lastname, 1 is firstname and 2 is grade in seperateUserInfo[].
               
                //Check if last character of the grade parameter is a ',' and remove it so that a potential int can still successfully be retrieved.
                
                if (linesInfo[2].Length > 1) {//a grade of 1 would throw exception otherwise without this check
                    char c = linesInfo[2][linesInfo[2].Length - 1];
                    if (c == ',') {
                        linesInfo[2] = linesInfo[2].Remove(linesInfo[2].Length - 1);
                    }
                }

                //Attempt to change grade to a int
                try {
                    int thisGrade = Convert.ToInt32(linesInfo[2]);
                } catch {
                    exceptionThrown("Sorry there is somthing wrong with the text files format. Third value in text file should be a int. Check line "+(insertedLines + 1)+" of the given file");
                }
                //Put information from lineInfo into unsorted Info
                unsortedInfo[insertedLines] = new LineInfo();
                unsortedInfo[insertedLines].firstName = linesInfo[1];
                unsortedInfo[insertedLines].lastName = linesInfo[0];
                unsortedInfo[insertedLines].grade = Convert.ToInt32(linesInfo[2]);
                insertedLines++;
            }
        }

        //Takes a array of LineInfo and a file location to create a text file containing the information in the order of the LineInfo array. Also Writes this to console.
        private void writeOut(ref LineInfo[] givenInfo, string fileLocation) {
            System.IO.StreamWriter file = new System.IO.StreamWriter(fileLocation);
            String allLines = "";
            String writtenLine;
            foreach (LineInfo line in givenInfo) {//Write each line
                writtenLine = line.lastName + ", " + line.firstName + ", " + line.grade;
                allLines += writtenLine + '\n';
                Console.WriteLine(writtenLine);
                file.WriteLine(writtenLine);
            }
            file.Close();
        }

        //Method that takes the user input if it exists, or requests locations then determine if given path that file exists, and if its .txt, 
        private string determineReadFile (string[] args){
            string readFile; 
            if (args.Length == 0) {//No input given request file location
                Console.Write("Please enter the location of the input file:\n");
                readFile = Console.ReadLine();
            } else {//File location already given
                readFile = args[0];
            }

            if (".txt" != Path.GetExtension(readFile)) {//Going to assume for sake of testing this wont need to be checked in other public methods.
                exceptionThrown("Only supposed to be for .txt files");
            }
            if (!File.Exists(readFile)) {//Check given file exists
                exceptionThrown("Sorry, cannot find that file");
            }
            return readFile; 
        }


        //The main method that can also be called in unit tests.
        public void mainGradeScoresMethod(string[] args) {
            //Determine path for input and output
            String inputFileLocation = determineReadFile(args);
            String outputFileLocation = getOutputLocation(inputFileLocation);
            //Read to String[] from input path.
            String[] inputText = System.IO.File.ReadAllLines(inputFileLocation);
            //Declare LineInfo class.
            LineInfo[] unsortedInfo = new LineInfo[1];
            LineInfo[] sortedInfo = new LineInfo[1];
            //moves read string array into lineInfo array
            stringArrayToLineInfoArray(ref inputText, ref unsortedInfo);
            //Sorts lineInfo array by grade (desc), lastname, firstname
            sortArray(ref sortedInfo, ref unsortedInfo);

            //Now to write out the sorted information to both console and text file
            writeOut(ref sortedInfo, outputFileLocation);

            //Closing statement
            Console.Write("Finished: created " + getLastFileName(outputFileLocation));
        }

        //Main method runs gradeScores. Moved readline here to not interfere with tests.
        static void Main(string[] args) {
            (new grade_scores()).mainGradeScoresMethod(args);
            Console.ReadLine();
        }
    }
}
