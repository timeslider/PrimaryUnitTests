using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Primary_Puzzle_Solver;


//Bitboard Constructor
// Make sure it can't accept a size less than 1 or larger than sizeX/Y  ✅
// Make sure the sizeX is getting set correctly   --------------------  ✅
// Make sure the sizeY is getting set correctly   --------------------  ✅
//bb.SetBitboardCell();
// Make sure it can't accept a size less than 1 or larger than sizeX/Y  ✅
// Make sure it sets the correct bit ---------------------------------- ✅
//bb.GetBitboardCell;
// Make sure it can't accept a size less than 1 or larger than sizeX/Y  ✅
// Make sure it gets the right value from the right cell -------------- ✅
//bb.PrintBitboard();
// Make sure it prints the right bitboard ----------------------------- ❎
// Make sure it prints the right bitboard in the right orientation ---- ✅

// Arrange
// Act
// Assert

namespace PrimaryUnitTests
{

    [TestClass]
    public sealed class BitboardTests
    {
        private StringBuilder? ConsoleOutput { get; set; }

        /// <summary>
        /// Tests if the constructors throws an execption if x or y is either too small (less than 1) or too large (greater than 8)
        /// </summary>
        [DataTestMethod]
        public void Constructor_Size()
        {
            // Arrange, Act, & Assert
            // First constructor
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Bitboard(0UL, 0), "sizeX was too large.");
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Bitboard(0UL, 9), "sizeY was too large.");

            // Second constructor
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Bitboard(0UL, 0, 4), "sizeX was too small.");
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Bitboard(0UL, 4, 0), "sizeY was too small.");
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Bitboard(0UL, 9, 4), "sizeX was too large.");
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Bitboard(0UL, 4, 9), "sizeY was too large.");
        }

        /// <summary>
        /// Tests that the SizeY is getting set properly.
        /// </summary>
        [TestMethod]
        public void Constructor_SizeX()
        {
            // Arrange & Act
            Bitboard bb = new Bitboard(0UL, 5, 2);


            // Assert
            Assert.AreEqual(5, bb.SizeX, $"Expected {5}, but got {bb.SizeX} instead");

        }

        /// <summary>
        /// Tests that the SizeY is getting set properly.
        /// </summary>
        [TestMethod]
        public void Constructor_SizeY()
        {
            // Arrange & Act
            Bitboard bb = new Bitboard(0UL, 5);


            // Assert
            Assert.AreEqual(5, bb.SizeY, $"Expected {5}, but got {bb.SizeY} instead");

        }

        [TestMethod]
        public void SetBitboardCell_BoundsCheck()
        {
            // Arrange
            Bitboard bitboard = new Bitboard(0UL, 8);

            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => bitboard.SetBitboardCell(-1, 4, true), "sizeX was too small.");
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => bitboard.SetBitboardCell(4, -1, true), "sizeY was too small.");
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => bitboard.SetBitboardCell(8, 4, true), "sizeX was too large.");
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => bitboard.SetBitboardCell(4, 8, true), "sizeY was too large.");
        }

        [TestMethod]
        public void SetBitboardCell_CorrectBit()
        {
            // Arrange
            int size = 8;
            Bitboard bitboard = new Bitboard(0UL, size);

            // Act
            bitboard.SetBitboardCell(0, 0, true);

            // Assert
            Assert.IsTrue(bitboard.BitboardValue == 1UL, $"Expected 1 but got {bitboard.BitboardValue}");

            bitboard = new Bitboard(0UL, 5, 7);
            bitboard.SetBitboardCell(2, 1, true);
            Assert.IsTrue(bitboard.BitboardValue == 128UL, $"Expected 128 but got {bitboard.BitboardValue}");
        }

        /// <summary>
        /// Tests the bounds check for GetBitboardCell(x, y)
        /// </summary>
        [TestMethod]
        public void GetBitboardCell_BoundsCheck()
        {
            Bitboard bitboard = new Bitboard(0UL, 8);

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => bitboard.GetBitboardCell(-1, 4), "sizeX was too small.");
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => bitboard.GetBitboardCell(4, -1), "sizeY was too small.");
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => bitboard.GetBitboardCell(8, 4), "sizeX was too large.");
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => bitboard.GetBitboardCell(4, 8), "sizeY was too large.");

        }

        [TestMethod]
        public void GetBitboardCellIndex_BoundsCheck()
        {
            Bitboard bitboard = new Bitboard(0UL, 4, 7);

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => bitboard.GetBitboardCell(-1), "Index was less than 0, but it didn't throw an exception.");
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => bitboard.GetBitboardCell((bitboard.SizeX * bitboard.SizeY)), "Index was greater than it supposed to be, but it didn't throw an exception.");
        }


        [TestMethod]
        public void GetBitboardCell_CorrectBit()
        {
            // Arrange
            int size = 8;
            Bitboard bitboard = new Bitboard(9295429630892703873UL, size);
            // Example bitboard
            // 1 0 0 0 0 0 0 1
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0
            // 1 0 0 0 0 0 0 1

            // Act & Assert
            Assert.IsTrue(bitboard.GetBitboardCell(0, 0), $"Expected 1 but got {bitboard.GetBitboardCell(0, 0)}");  // Check top left corner
            Assert.IsTrue(bitboard.GetBitboardCell(7, 0), $"Expected 128 but got {bitboard.GetBitboardCell(7, 0)}");  // Check top right corner
            Assert.IsTrue(bitboard.GetBitboardCell(0, 7), $"Expected 72057594037927936 but got {bitboard.GetBitboardCell(0, 7)}");  // Check bottom left corner
            Assert.IsTrue(bitboard.GetBitboardCell(7, 7), $"Expected 9223372036854775808 but got {bitboard.GetBitboardCell(7, 7)}");  // Check bottom right corner

            Assert.IsFalse(bitboard.GetBitboardCell(4, 4), $"Expected 0 but got {bitboard.GetBitboardCell(4, 4)}");
        }

        [TestMethod]
        public void PrintBitboard_bitboard()
        {
            // Test 1

            Bitboard bitboard = new Bitboard(18446744073709551615UL, 8);    // Full
            // Example bitboard
            // 1 1 1 1 1 1 1 1
            // 1 1 1 1 1 1 1 1
            // 1 1 1 1 1 1 1 1
            // 1 1 1 1 1 1 1 1
            // 1 1 1 1 1 1 1 1
            // 1 1 1 1 1 1 1 1
            // 1 1 1 1 1 1 1 1
            // 1 1 1 1 1 1 1 1
            string expectedOutput = "18446744073709551615\nR 1 1 1 1 1 1 1 \nY 1 1 1 1 1 1 1 \nB 1 1 1 1 1 1 1 \n1 1 1 1 1 1 1 1 \n1 1 1 1 1 1 1 1 \n1 1 1 1 1 1 1 1 \n1 1 1 1 1 1 1 1 \n1 1 1 1 1 1 1 1";

            // Act
            string actualOutput = CaptureConsoleOutput(() => bitboard.PrintBitboard());

            Assert.AreEqual(expectedOutput, actualOutput.Trim());


            // Test 2

            bitboard = new Bitboard(0UL, 8);  // Empty
            // Example bitboard
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0

            expectedOutput = "0\nR Y B - - - - - \n- - - - - - - - \n- - - - - - - - \n- - - - - - - - \n- - - - - - - - \n- - - - - - - - \n- - - - - - - - \n- - - - - - - -";

            actualOutput = CaptureConsoleOutput(() => bitboard.PrintBitboard());

            Assert.AreEqual(expectedOutput.Trim(), actualOutput.Trim());
        }

        [TestMethod]
        public void PrintBitboard_orientation()
        {
            
            Bitboard bitboard = new Bitboard(532610UL, 5, 6);
            // Example bitboard
            // 0 1 0 0 0
            // 0 0 1 0 0
            // 0 0 0 1 0
            // 0 0 0 0 1
            // 0 0 0 0 0
            // 0 0 0 0 0
            string expectedOutput = "532610\n- 1 - - - \n- - 1 - - \n- - - 1 - \n- - - - 1 \n- - - - - \n- - - - -";

            // Act
            string actualOutput = CaptureConsoleOutput(() => bitboard.PrintBitboard());

            // Assert
            Assert.AreEqual(expectedOutput, actualOutput.Trim());
        }

        private static string CaptureConsoleOutput(Action action)
        {

            using (StringWriter sw = new StringWriter())
            {
                TextWriter originalOutput = Console.Out;
                Console.SetOut(sw);
                action();
                Console.SetOut(originalOutput);
                return sw.ToString();
            }
        }

        /// <summary>
        /// There needs to be at least 3 empty holes to fills with tiles.<br></br>
        /// There actually needs to be more empty holes than that, but that's a project for future Rob.
        /// </summary>
        [TestMethod]
        public void GetInitialState_NotEnoughEmpty()
        {
            //Bitboard bitboard = new Bitboard(0xffffffffffffffff, 8);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Bitboard(0xffffffffffffffff, 8), "Should have thrown because there is 0 empty spaces");
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Bitboard(0xfffffffffffbffff, 8), "Should have thrown because there is 1 empty space");
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Bitboard(0xfffffffffff3ffff, 8), "Should have thrown because there is 2 empty spaces");
            try
            {
                Bitboard bitboard = new Bitboard(0xfffffffff7f3ffff, 8); // Should not throw an error because there are 3 empty spaces
            }
            catch (Exception ex)
            {
                Assert.Fail($"Expected no exception, but got : {ex.GetType().Name} - {ex.Message}");
            }
        }

        /// <summary>
        /// Makes sure Red it getting placed right.
        /// </summary>
        [TestMethod]
        public void GetInitialState()
        {
            // Case 1 and 2 part 1 : Need to test against only the 1st part of case 2

            // Example bitboard with states
            // 1 1 R Y B 0 0 0
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0



            // Case 1 and 2 part 2 : Need to test against only the 2nd part of case 2
            // Arrange & Act
            Console.WriteLine("Testing case 1 and 2.");
            Bitboard bitboard = new Bitboard(0x1f, 8); // The last 3 cells of the first row are empty.
            bitboard.PrintBitboard();
            // Example bitboard with states
            // 1 1 1 1 1 R Y B
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0

            Assert.AreEqual(bitboard.State & 0x3f, 5, $"Red is {bitboard.State & 0x3f}, but it was supposed to be 5."); // Masks out the other bits
            Assert.AreEqual((bitboard.State >> 6) & 0x3f, 6, $"Yellow is {(bitboard.State >> 6) & 0x3f}, but it was supposed to be 6."); // Masks out the other bits
            Assert.AreEqual((bitboard.State >> 12) & 0x3f, 7, $"Blue is {(bitboard.State >> 12) & 0x3f}, but it was supposed to be 7."); // Masks out the other bits




            // Case 1 and 3
            // Arrange & Act
            Console.WriteLine("Testing case 1 and 3");
            bitboard = new Bitboard(0x3f, 8); // The last 2 cells of the first row are empty.
            bitboard.PrintBitboard();
            // Example bitboard with states
            // 1 1 1 1 1 1 R Y
            // 0 0 0 0 0 0 B 0
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0

            Assert.AreEqual(bitboard.State & 0x3f, 6, $"Red is {bitboard.State & 0x3f}, but it was supposed to be 6."); // Masks out the other bits
            Assert.AreEqual((bitboard.State >> 6) & 0x3f, 7, $"Yellow is {(bitboard.State >> 6) & 0x3f}, but it was supposed to be 7."); // Masks out the other bits
            Assert.AreEqual((bitboard.State >> 12) & 0x3f, 14, $"Blue is {(bitboard.State >> 12) & 0x3f}, but it was supposed to be 14."); // Masks out the other bits




            // Case 1 and 4
            // Arrange & Act
            Console.WriteLine("Testing case 1 and 4");
            bitboard = new Bitboard(0x603f, 8); // The last 2 cells of the first row are empty and the cell below the first empty cell is filled in.
            bitboard.PrintBitboard();
            // Example bitboard with states
            // 1 1 1 1 1 1 R Y
            // 0 0 0 0 0 0 1 B
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0

            Assert.AreEqual(bitboard.State & 0x3f, 6, $"Red is {bitboard.State & 0x3f}, but it was supposed to be 6."); // Masks out the other bits
            Assert.AreEqual((bitboard.State >> 6) & 0x3f, 7, $"Yellow is {(bitboard.State >> 6) & 0x3f}, but it was supposed to be 7."); // Masks out the other bits
            Assert.AreEqual((bitboard.State >> 12) & 0x3f, 15, $"Blue is {(bitboard.State >> 12) & 0x3f}, but it was supposed to be 15."); // Masks out the other bits




            // Case 1 and 5
            // Arrange & Act
            Console.WriteLine("Testing case 1 and 5");
            bitboard = new Bitboard(0UL, 8); // Just so the output doens't look weird.
            bitboard.PrintBitboard();
            // Example bitboard with states (throws an exception before the states ever gets set)
            // 1 1 1 1 1 1 0 0
            // 0 0 0 0 0 0 1 1
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0

            Assert.ThrowsException<Exception>(() => new Bitboard(0xe03f, 8), $"This board should have thrown an exception. ");




            // Case 6 and 7
            // Arrange & Act
            Console.WriteLine("Testing case 6 and 7");
            bitboard = new Bitboard(0x5, 8); // The last 2 cells of the first row are empty and the cell below the first empty cell is filled in.
            bitboard.PrintBitboard();
            // Example bitboard with states
            // 1 1 1 1 1 1 R Y
            // 0 0 0 0 0 0 1 B
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0

            Assert.AreEqual(bitboard.State & 0x3f, 1, $"Red is {bitboard.State & 0x3f}, but it was supposed to be 1."); // Masks out the other bits
            Assert.AreEqual((bitboard.State >> 6) & 0x3f, 8, $"Yellow is {(bitboard.State >> 6) & 0x3f}, but it was supposed to be 8."); // Masks out the other bits
            Assert.AreEqual((bitboard.State >> 12) & 0x3f, 9, $"Blue is {(bitboard.State >> 12) & 0x3f}, but it was supposed to be 9."); // Masks out the other bits




            // Case 6 and 8
            // Arrange & Act
            Console.WriteLine("Testing case 6 and 8");
            bitboard = new Bitboard(0x105, 8); 
            bitboard.PrintBitboard();
            // Example bitboard with states
            // 1 R 1 0 0 0 0 0
            // 1 Y B 0 0 0 0 0
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0

            Assert.AreEqual(bitboard.State & 0x3f, 1, $"Red is {bitboard.State & 0x3f}, but it was supposed to be 1."); // Masks out the other bits
            Assert.AreEqual((bitboard.State >> 6) & 0x3f, 9, $"Yellow is {(bitboard.State >> 6) & 0x3f}, but it was supposed to be 9."); // Masks out the other bits
            Assert.AreEqual((bitboard.State >> 12) & 0x3f, 10, $"Blue is {(bitboard.State >> 12) & 0x3f}, but it was supposed to be 10."); // Masks out the other bits





            // Case 6 and 9 (nice)
            // Arrange & Act
            Console.WriteLine("Testing case 6 and 9");
            bitboard = new Bitboard(0x505, 8);
            bitboard.PrintBitboard();
            // Example bitboard with states
            // 1 R 1 0 0 0 0 0
            // 1 Y 1 0 0 0 0 0
            // 0 B 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0

            Assert.AreEqual(bitboard.State & 0x3f, 1, $"Red is {bitboard.State & 0x3f}, but it was supposed to be 1."); // Masks out the other bits
            Assert.AreEqual((bitboard.State >> 6) & 0x3f, 9, $"Yellow is {(bitboard.State >> 6) & 0x3f}, but it was supposed to be 9."); // Masks out the other bits
            Assert.AreEqual((bitboard.State >> 12) & 0x3f, 17, $"Blue is {(bitboard.State >> 12) & 0x3f}, but it was supposed to be 17."); // Masks out the other bits




            // Case 6 and 10
            // Arrange & Act
            Console.WriteLine("Testing case 6 and 10");
            bitboard = new Bitboard(0UL, 8); // Just so the output doens't look weird.
            bitboard.PrintBitboard();
            // Example bitboard with states
            // 1 0 1 0 0 0 0 0
            // 1 0 1 0 0 0 0 0
            // 0 1 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0 0

            Assert.ThrowsException<Exception>(() => new Bitboard(0x20505, 8), $"This board should have thrown an exception. ");
            
            // Don't forget to test for when 3 random non-edge connected cells are there. It should throw an exception so Assert.Execption<Name of exception>(() => ...);
        }
    }
}
