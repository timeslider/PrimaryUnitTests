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
        /// Tests that the Width is getting set properly.
        /// </summary>
        [TestMethod]
        public void Constructor_Width()
        {
            // Arrange & Act
            Bitboard bb = new Bitboard(0UL, 5, 2);


            // Assert
            Assert.AreEqual(5, bb.Width, $"Expected {5}, but got {bb.Width} instead");

        }

        /// <summary>
        /// Tests that the Height is getting set properly
        /// </summary>
        [TestMethod]
        public void Constructor_Height()
        {
            // Arrange & Act
            Bitboard bitboard = new Bitboard(0UL, 5);


            // Assert
            Assert.AreEqual(5, bitboard.Height, $"Expected {5}, but got {bitboard.Height} instead");

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
            Assert.IsTrue(bitboard.WallData == 1UL, $"Expected 1 but got {bitboard.WallData}");

            bitboard = new Bitboard(0UL, 5, 7);
            bitboard.SetBitboardCell(2, 1, true);
            Assert.IsTrue(bitboard.WallData == 128UL, $"Expected 128 but got {bitboard.WallData}");
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
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => bitboard.GetBitboardCell((bitboard.Width * bitboard.Height)), "Index was greater than it supposed to be, but it didn't throw an exception.");
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




        /// <summary>
        /// A private method used for tests that print to the console
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
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
            // 8x8
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Bitboard(0xffffffffffffffff, 8), "Should have thrown because there is 0 empty spaces");
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Bitboard(0xfffffffffffbffff, 8), "Should have thrown because there is 1 empty space");
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Bitboard(0xfffffffffff3ffff, 8), "Should have thrown because there is 2 empty spaces");

            // nxm
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Bitboard(16777209UL, 6, 4), "Should have thrown because there is 2 empty spaces");

            try
            {
                // These should not throw an error because there are 3 empty spaces
                Bitboard bitboard = new Bitboard(0xfffffffff7f3ffff, 8);
                bitboard = new Bitboard(16777208UL, 6, 4);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Expected no exception, but got : {ex.GetType().Name} - {ex.Message}");
            }
        }




        /// <summary>
        /// Makes sure the initial state getting set right
        /// I need to go through and break up the parts into smaller bits
        /// For example, sometimes we check more than one thing for each case
        /// Those things would need their own test
        /// It could also reveal if something is not needed
        /// </summary>
        [TestMethod]
        public void GetInitialState_Red()
        {
            // Tests red was 0
            Bitboard bitboard = new Bitboard(0x1f4, 3);
            // Example bitboard
            // 0 0 1
            // 0 1 1
            // 1 1 1

            Assert.AreEqual(0, bitboard.GetState() & 0x3f, $"Expected 0, but got {bitboard.GetState() & 0x3f} instead");

            // Tests red was not 0
            bitboard = new Bitboard(0xfe8b, 4);
            // Example bitboard
            // 1 1 0 1
            // 0 0 0 1
            // 0 1 1 1
            // 1 1 1 1

            Assert.AreEqual(2, bitboard.GetState() & 0x3f, $"Expected 2, but got {bitboard.GetState() & 0x3f} instead");
        }




        /// <summary>
        /// Tests the output of CanMove
        /// </summary>
        [TestMethod]
        public void GetInitialState_CanMove()
        {
            // Testing right in the middle
            Bitboard bitboard = new Bitboard(0x1, 3);
            // Example bitboard
            // 1 0 0
            // 0 0 0
            // 0 0 0
            int actual = bitboard.CanMove(Bitboard.Direction.Right, 1);
            Assert.AreEqual(2, actual, $"Expected 2, but got {actual} instead");


            // Testing right on an edge
            bitboard = new Bitboard(0x3, 3);
            // Example bitboard
            // 1 1 0
            // 0 0 0
            // 0 0 0
            actual = bitboard.CanMove(Bitboard.Direction.Right, 2);
            Assert.AreEqual(0, actual, $"Expected 0, but got {actual} instead"); // We're expecting 0 here because we can't move right


            // Testing left in the middle
            bitboard = new Bitboard(0x3, 3);
            // Example bitboard
            // 1 1 0
            // 0 0 0
            // 0 0 0
            actual = bitboard.CanMove(Bitboard.Direction.Left, 4);
            Assert.AreEqual(3, actual, $"Expected 3, but got {actual} instead");


            // Testing left on the edge
            bitboard = new Bitboard(0x3, 3);
            // Example bitboard
            // 1 1 0
            // 0 0 0
            // 0 0 0
            actual = bitboard.CanMove(Bitboard.Direction.Left, 3);
            Assert.AreEqual(0, actual, $"Expected 0, but got {actual} instead"); // Can't move left from index 3


            // Testing down in the middle
            bitboard = new Bitboard(0x3, 3);
            // Example bitboard
            // 1 1 0
            // 0 0 0
            // 0 0 0
            actual = bitboard.CanMove(Bitboard.Direction.Down, 4);
            Assert.AreEqual(7, actual, $"Expected 7, but got {actual} instead");


            // Testing down on the edge
            bitboard = new Bitboard(0x3, 3);
            // Example bitboard
            // 1 1 0
            // 0 0 0
            // 0 0 0
            actual = bitboard.CanMove(Bitboard.Direction.Down, 7);
            Assert.AreEqual(0, actual, $"Expected 0, but got {actual} instead"); // Can't move down from index 7
        }

        [TestMethod]
        public void GetInitialState_TooFewCells()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Bitboard(0UL, 1), $"This board is only 1 by 1 and should always throw an exception.");
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Bitboard(3UL, 2), $"This board is 2 by 2 but has 2 walls so it should throw an error.");
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Bitboard(5UL, 2), $"This board is 2 by 2 but has 2 walls so it should throw an error.");
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Bitboard(9UL, 2), $"This board is 2 by 2 but has 2 walls so it should throw an error.");
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Bitboard(12UL, 2), $"This board is 2 by 2 but has 2 walls so it should throw an error.");
        }
    }
}
