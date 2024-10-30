using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessAPI.Helpers;
using ChessAPI.Models;
using ChessAPI.Models.Pieces;
namespace ChessAPITests
{
    public class MoveValidatorHelperTests
    {
        // create static board setups to avoid creating new instances for every separate test.
        private static readonly CompleteBoardSetup StandardBoardSetup = new CompleteBoardSetup("StartingPosition");
        private static readonly CompleteBoardSetup CaptureBoardSetup = new CompleteBoardSetup("CaptureStartingPosition");

        public static IEnumerable<object[]> GetCaptureBoardSetupData()
        {
            yield return new object[] { CaptureBoardSetup };
        }

        public static IEnumerable<object[]> GetStandardBoardSetupData()
        {
            yield return new object[] { StandardBoardSetup };
        }

        [Theory]
        [MemberData(nameof(GetStandardBoardSetupData))]
        public void Test_DiagonalMovement_Expect_DiagonalMovementType(CompleteBoardSetup boardSetup)
        {
            // Arrange: Fetch specific tiles for testing diagonal movement
            // E2
            var fromTile = boardSetup._boardStateService.Board.playingFieldDictionary.First(c => c.Key.Item1 == 2 && c.Key.Item2 == 4).Value;
            // F3
            var toTile = boardSetup._boardStateService.Board.playingFieldDictionary.First(c => c.Key.Item1 == 3 && c.Key.Item2 == 5).Value;

            // Act
            var movementType = MoveValidatorHelper.GetMovementType(fromTile, toTile, boardSetup._boardStateService.Board);

            // Assert that the detected movement type matches the expected type
            Assert.Equal(MovementType.Diagonal, movementType);
        }

        [Theory]
        [MemberData(nameof(GetStandardBoardSetupData))]
        public void Test_HorizontalMovement_Expect_HorizontalMovementType(CompleteBoardSetup boardSetup)
        {
            // Arrange: Fetch specific tiles for testing horizontal movement
            // E2
            var fromTile = boardSetup._boardStateService.Board.playingFieldDictionary.First(c => c.Key.Item1 == 2 && c.Key.Item2 == 4).Value;
            // D2
            var toTile = boardSetup._boardStateService.Board.playingFieldDictionary.First(c => c.Key.Item1 == 2 && c.Key.Item2 == 4).Value;

            // Act
            var movementType = MoveValidatorHelper.GetMovementType(fromTile, toTile, boardSetup._boardStateService.Board);

            // Assert that the detected movement type matches the expected type
            Assert.Equal(MovementType.Horizontal, movementType);
        }

        [Theory]
        [MemberData(nameof(GetStandardBoardSetupData))]
        public void Test_VerticalMovement_Expect_VerticalMovementType(CompleteBoardSetup boardSetup)
        {
            // Arrange: Fetch specific tiles for testing Vertical movement
            // E2
            var fromTile = boardSetup._boardStateService.Board.playingFieldDictionary.First(c => c.Key.Item1 == 2 && c.Key.Item2 == 4).Value;
            // E3
            var toTile = boardSetup._boardStateService.Board.playingFieldDictionary.First(c => c.Key.Item1 == 3 && c.Key.Item2 == 4).Value;

            // Act
            var movementType = MoveValidatorHelper.GetMovementType(fromTile, toTile, boardSetup._boardStateService.Board);

            // Assert that the detected movement type matches the expected type
            Assert.Equal(MovementType.Vertical, movementType);
        }

        [Theory]
        [MemberData(nameof(GetStandardBoardSetupData))]
        public void Test_LShapedMovement_Expect_LShapedMovementType(CompleteBoardSetup boardSetup)
        {
            // Arrange: Fetch specific tiles for testing L shaped movement
            // B1
            var fromTile = boardSetup._boardStateService.Board.playingFieldDictionary.First(c => c.Key.Item1 == 1 && c.Key.Item2 == 2).Value;
            // C3
            var toTile = boardSetup._boardStateService.Board.playingFieldDictionary.First(c => c.Key.Item1 == 3 && c.Key.Item2 == 3).Value;

            // Act
            var movementType = MoveValidatorHelper.GetMovementType(fromTile, toTile, boardSetup._boardStateService.Board);

            // Assert that the detected movement type matches the expected type
            Assert.Equal(MovementType.LShaped, movementType);
        }

        [Theory]
        [MemberData(nameof(GetStandardBoardSetupData))]
        public void Test_InvalidMovement_Expect_InvalidMovementType(CompleteBoardSetup boardSetup)
        {
            // Arrange: Fetch specific tiles for testing invalid movement
            // B1
            var fromTile = boardSetup._boardStateService.Board.playingFieldDictionary.First(c => c.Key.Item1 == 1 && c.Key.Item2 == 1).Value;
            // C4
            var toTile = boardSetup._boardStateService.Board.playingFieldDictionary.First(c => c.Key.Item1 == 4 && c.Key.Item2 == 2).Value;

            // Act
            var movementType = MoveValidatorHelper.GetMovementType(fromTile, toTile, boardSetup._boardStateService.Board);

            // Assert that the detected movement type matches the expected type
            Assert.Equal(MovementType.Invalid, movementType);
        }


        [Theory]
        [MemberData(nameof(GetStandardBoardSetupData))]
        public void Test_CheckTileRange_Expect_True(CompleteBoardSetup boardSetup)
        {
            // Arrange: Fetch specific tiles for testing invalid movement
            // C7
            var fromTile = boardSetup._boardStateService.Board.playingFieldDictionary.First(c => c.Key.Item1 == 7 && c.Key.Item2 == 2).Value;
            // C6
            var toTile = boardSetup._boardStateService.Board.playingFieldDictionary.First(c => c.Key.Item1 == 6 && c.Key.Item2 == 2).Value;

            var indexes = MoveValidatorHelper.GetMovementIndexes(fromTile, toTile, boardSetup._boardStateService.Board);
            var difference = MoveValidatorHelper.GetMovementDifference(indexes.fromIndex, indexes.toIndex);


            var pathClear = MoveValidatorHelper.CheckTileRange([8], fromTile, toTile, boardSetup._boardStateService.Board);

            // Assert that the path is clear
            Assert.True(pathClear);
        }

        [Theory]
        [MemberData(nameof(GetCaptureBoardSetupData))]
        public void Test_CheckTileRange_Expect_False(CompleteBoardSetup boardSetup)
        {
            // this test requires the captureboard starting setup, which has a white pawn on D6 and thus disallowing pawn movement from D6 to D7

            // Arrange: Fetch specific tiles for testing invalid movement
            // D8
            var fromTile = boardSetup._boardStateService.Board.playingFieldDictionary.First(c => c.Key.Item1 == 8 && c.Key.Item2 == 3).Value;
            // D6
            var toTile = boardSetup._boardStateService.Board.playingFieldDictionary.First(c => c.Key.Item1 == 6 && c.Key.Item2 == 3).Value;

            var indexes = MoveValidatorHelper.GetMovementIndexes(fromTile, toTile, boardSetup._boardStateService.Board);
            var difference = MoveValidatorHelper.GetMovementDifference(indexes.fromIndex, indexes.toIndex);


            var pathClear = MoveValidatorHelper.CheckTileRange([8], fromTile, toTile, boardSetup._boardStateService.Board);

            // Assert that the path is not clear
            Assert.False(pathClear);
        }

        [Theory]
        [MemberData(nameof(GetCaptureBoardSetupData))]
        public void Test_CheckTileRangeWithCapture_Expect_True(CompleteBoardSetup boardSetup)
        {
            // this test requires the captureboard starting setup, which has a white pawn on D6 and thus disallowing pawn movement from D6 to D7

            // Arrange: Fetch specific tiles for testing invalid movement
            // D7
            var fromTile = boardSetup._boardStateService.Board.playingFieldDictionary.First(c => c.Key.Item1 == 7 && c.Key.Item2 == 3).Value;
            // D6
            var toTile = boardSetup._boardStateService.Board.playingFieldDictionary.First(c => c.Key.Item1 == 6 && c.Key.Item2 == 3).Value;

            var indexes = MoveValidatorHelper.GetMovementIndexes(fromTile, toTile, boardSetup._boardStateService.Board);
            var difference = MoveValidatorHelper.GetMovementDifference(indexes.fromIndex, indexes.toIndex);
            
            var pathClear = MoveValidatorHelper.CheckTileRange([8], fromTile, toTile, boardSetup._boardStateService.Board);

            // Assert that the path is not clear
            Assert.True(pathClear);
        }

        [Theory]
        [MemberData(nameof(GetStandardBoardSetupData))]
        public void Test_CheckPath_Expect_True(CompleteBoardSetup boardSetup)
        {
            // Arrange: Fetch specific tiles for testing invalid movement
            // D7
            var fromTile = boardSetup._boardStateService.Board.playingFieldDictionary.First(c => c.Key.Item1 == 7 && c.Key.Item2 == 3).Value;
            // D6
            var toTile = boardSetup._boardStateService.Board.playingFieldDictionary.First(c => c.Key.Item1 == 6 && c.Key.Item2 == 3).Value;

            var indexes = MoveValidatorHelper.GetMovementIndexes(fromTile, toTile, boardSetup._boardStateService.Board);
            var difference = MoveValidatorHelper.GetMovementDifference(indexes.fromIndex, indexes.toIndex);


            var pathClear = MoveValidatorHelper.CheckPath(indexes.fromIndex, indexes.toIndex, difference, boardSetup._boardStateService.Board, MovementType.Vertical);

            // Assert that the path is not clear
            Assert.True(pathClear);
        }

        [Theory]
        [MemberData(nameof(GetCaptureBoardSetupData))]
        public void Test_CheckPath_Expect_False(CompleteBoardSetup boardSetup)
        {
            // this test requires the captureboard starting setup, which has a white pawn on D6 and thus disallowing pawn movement from D6 to D7

            // Arrange: Fetch specific tiles for testing invalid movement
            // D7
            var fromTile = boardSetup._boardStateService.Board.playingFieldDictionary.First(c => c.Key.Item1 == 7 && c.Key.Item2 == 3).Value;
            // D6
            var toTile = boardSetup._boardStateService.Board.playingFieldDictionary.First(c => c.Key.Item1 == 6 && c.Key.Item2 == 3).Value;

            var indexes = MoveValidatorHelper.GetMovementIndexes(fromTile, toTile, boardSetup._boardStateService.Board);
            var difference = MoveValidatorHelper.GetMovementDifference(indexes.fromIndex, indexes.toIndex);


            var pathClear = MoveValidatorHelper.CheckPath(indexes.fromIndex, indexes.toIndex, difference, boardSetup._boardStateService.Board, MovementType.Vertical);

            // Assert that the path is not clear
            Assert.False(pathClear);
        }

        [Theory]
        [MemberData(nameof(GetCaptureBoardSetupData))]
        public void Test_CheckPathWithCapture_Expect_True(CompleteBoardSetup boardSetup)
        {
            // Arrange: Fetch specific tiles for testing invalid movement
            // D7
            var fromTile = boardSetup._boardStateService.Board.playingFieldDictionary.First(c => c.Key.Item1 == 7 && c.Key.Item2 == 3).Value;
            // D6
            var toTile = boardSetup._boardStateService.Board.playingFieldDictionary.First(c => c.Key.Item1 == 6 && c.Key.Item2 == 3).Value;

            var indexes = MoveValidatorHelper.GetMovementIndexes(fromTile, toTile, boardSetup._boardStateService.Board);
            var difference = MoveValidatorHelper.GetMovementDifference(indexes.fromIndex, indexes.toIndex);


            var pathClear = MoveValidatorHelper.CheckPath(indexes.fromIndex, indexes.toIndex, difference, boardSetup._boardStateService.Board, MovementType.Capture);

            // Assert that the path is not clear
            Assert.True(pathClear);
        }
    }
}
