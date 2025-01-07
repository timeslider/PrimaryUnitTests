using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Primary_Puzzle_Solver;

namespace PrimaryUnitTests
{
    [TestClass]
    public class UtilTests
    {
        [TestMethod]
        public void TestPrintPuzzleRange()
        {
            // Arrange
            StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act
            Util.PrintBitboardRange(@"C:\Users\rober\Documents\Polyomino List\2x2_count_24.bin", 0, 100);

            // Reset the console output to its original state
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });

            // Capture the actual output
            string actualOutput = stringWriter.ToString();

            // Print the acual output for debugging
            Console.WriteLine($"Actual>{actualOutput}<");

            // Assert
            string expectedOutput = "34356504510271\r\nW W W W W W - - \r\nW W W W W W - - \r\nW W W W W W - - \r\nW W W W W W - - \r\nW W W W W W - - \r\nW W W W W - - - \r\n- - - - - - - - \r\n- - - - - - - - \r\n\r\n";
            //Assert.AreEqual(expectedOutput, stringWriter.ToString(), $"Uh-oh! Expected {expectedOutput} but got {stringWriter} instead.");
            Assert.AreEqual(expectedOutput, actualOutput);

        }


    }
}

