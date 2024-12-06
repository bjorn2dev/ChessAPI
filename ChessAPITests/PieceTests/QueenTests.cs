using ChessAPI.Models.Pieces;
using ChessAPI.Models;
using ChessAPITests.BoardSetup;

namespace ChessAPITests.PieceTests
{
    public class QueenTests
    {
        private static readonly CompleteBoardSetup _standardBoardSetup = new CompleteBoardSetup("QueenStandardPosition");
        private static readonly CompleteBoardSetup _captureBoardSetup = new CompleteBoardSetup("QueenCapturePosition");
        private static readonly CompleteBoardSetup _edgeBoardSetup = new CompleteBoardSetup("QueenEdgePosition");
        private static readonly CompleteBoardSetup _checkBoardSetup = new CompleteBoardSetup("QueenCheckPosition");

        // MemberData for Queen captures
        public static IEnumerable<object[]> GetQueenCaptureMoves()
        {
            yield return new object[] { _captureBoardSetup.GetTileByNotation("D4"), _captureBoardSetup.GetTileByNotation("C5") };
            yield return new object[] { _captureBoardSetup.GetTileByNotation("D4"), _captureBoardSetup.GetTileByNotation("E5") };
            yield return new object[] { _captureBoardSetup.GetTileByNotation("D4"), _captureBoardSetup.GetTileByNotation("D5") };
        }

        // MemberData for valid Queen moves within board limits
        public static IEnumerable<object[]> GetStandardQueenMoves()
        {
            yield return new object[] { _standardBoardSetup.GetTileByNotation("D1"), _standardBoardSetup.GetTileByNotation("D3") };
            yield return new object[] { _standardBoardSetup.GetTileByNotation("D1"), _standardBoardSetup.GetTileByNotation("F3") };
            yield return new object[] { _standardBoardSetup.GetTileByNotation("D1"), _standardBoardSetup.GetTileByNotation("D7") };
            yield return new object[] { _standardBoardSetup.GetTileByNotation("D1"), _standardBoardSetup.GetTileByNotation("A4") };
        }

        // MemberData for invalid moves
        public static IEnumerable<object[]> GetInvalidQueenMoves()
        {
            yield return new object[] { _standardBoardSetup.GetTileByNotation("D1"), _standardBoardSetup.GetTileByNotation("E3") };
            yield return new object[] { _standardBoardSetup.GetTileByNotation("D1"), _standardBoardSetup.GetTileByNotation("G5") };
            yield return new object[] { _standardBoardSetup.GetTileByNotation("D1"), _standardBoardSetup.GetTileByNotation("C4") };
        }

        // MemberData for edge cases where the Queen is on the board edge or corner
        public static IEnumerable<object[]> GetEdgeQueenMoves()
        {
            yield return new object[] { _edgeBoardSetup.GetTileByNotation("A1"), _edgeBoardSetup.GetTileByNotation("H1") };
            yield return new object[] { _edgeBoardSetup.GetTileByNotation("A1"), _edgeBoardSetup.GetTileByNotation("A8") };
            yield return new object[] { _edgeBoardSetup.GetTileByNotation("H8"), _edgeBoardSetup.GetTileByNotation("H1") };
            yield return new object[] { _edgeBoardSetup.GetTileByNotation("H8"), _edgeBoardSetup.GetTileByNotation("A8") };
        }

        // MemberData for moves that place or remove the King from check
        public static IEnumerable<object[]> GetQueenCheckMoves()
        {
            yield return new object[] { _checkBoardSetup.GetTileByNotation("D1"), _checkBoardSetup.GetTileByNotation("E2") }; // Block diagonal check
            yield return new object[] { _checkBoardSetup.GetTileByNotation("D1"), _checkBoardSetup.GetTileByNotation("D4") }; // Move without affecting check
        }

        // Tests

        [Theory]
        [MemberData(nameof(GetQueenCaptureMoves))]
        public void Test_QueenMovementCapture_Expect_True(Tile fromTile, Tile toTile)
        {
            bool isValidMove = new Queen().IsValidCapture(fromTile, toTile, _captureBoardSetup._boardStateService.Board);
            Assert.True(isValidMove);
        }

        private void AssertQueenMovement(Tile fromTile, Tile toTile, bool expectedOutcome, ChessBoard board)
        {
            bool result = new Queen().IsValidMovement(fromTile, toTile, board);
            Assert.Equal(expectedOutcome, result);
        }

        [Theory]
        [MemberData(nameof(GetStandardQueenMoves))]
        public void Test_QueenMovementWithinLimits_Expect_True(Tile fromTile, Tile toTile)
        {
            AssertQueenMovement(fromTile, toTile, true, _standardBoardSetup._boardStateService.Board);
        }

        [Theory]
        [MemberData(nameof(GetInvalidQueenMoves))]
        public void Test_InvalidQueenMoves(Tile fromTile, Tile toTile)
        {
            AssertQueenMovement(fromTile, toTile, false, _standardBoardSetup._boardStateService.Board);
        }

        [Theory]
        [MemberData(nameof(GetEdgeQueenMoves))]
        public void Test_EdgeQueenMoves(Tile fromTile, Tile toTile)
        {
            AssertQueenMovement(fromTile, toTile, true, _edgeBoardSetup._boardStateService.Board);
        }

        [Theory]
        [MemberData(nameof(GetQueenCheckMoves))]
        public void Test_QueenMovesWithCheck(Tile fromTile, Tile toTile)
        {
            AssertQueenMovement(fromTile, toTile, true, _checkBoardSetup._boardStateService.Board);
        }
    }
}