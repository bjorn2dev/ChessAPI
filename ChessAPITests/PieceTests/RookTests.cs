using ChessAPI.Models.Pieces;
using ChessAPI.Models;
using ChessAPITests.BoardSetup;

namespace ChessAPITests.PieceTests
{
    public class RookTests
    {
        private static readonly CompleteBoardSetup _standardBoardSetup = new CompleteBoardSetup("RookStandardPosition");
        private static readonly CompleteBoardSetup _captureBoardSetup = new CompleteBoardSetup("RookCapturePosition");
        private static readonly CompleteBoardSetup _edgeBoardSetup = new CompleteBoardSetup("RookEdgePosition");

        // MemberData for Rook captures
        public static IEnumerable<object[]> GetRookCaptureMoves()
        {
            yield return new object[] { _captureBoardSetup.GetTileByNotation("D4"), _captureBoardSetup.GetTileByNotation("D3") };
            yield return new object[] { _captureBoardSetup.GetTileByNotation("D4"), _captureBoardSetup.GetTileByNotation("D6") };
            yield return new object[] { _captureBoardSetup.GetTileByNotation("D4"), _captureBoardSetup.GetTileByNotation("E4") };
            yield return new object[] { _captureBoardSetup.GetTileByNotation("D4"), _captureBoardSetup.GetTileByNotation("C4") };
        }

        // MemberData for valid Rook moves within board limits
        public static IEnumerable<object[]> GetStandardRookMoves()
        {
            yield return new object[] { _standardBoardSetup.GetTileByNotation("A1"), _standardBoardSetup.GetTileByNotation("A4") };
            yield return new object[] { _standardBoardSetup.GetTileByNotation("H1"), _standardBoardSetup.GetTileByNotation("H4") };
            yield return new object[] { _standardBoardSetup.GetTileByNotation("A1"), _standardBoardSetup.GetTileByNotation("A7") };
            yield return new object[] { _standardBoardSetup.GetTileByNotation("H1"), _standardBoardSetup.GetTileByNotation("H7") };
            yield return new object[] { _standardBoardSetup.GetTileByNotation("A1"), _standardBoardSetup.GetTileByNotation("D1") };
            yield return new object[] { _standardBoardSetup.GetTileByNotation("H1"), _standardBoardSetup.GetTileByNotation("E1") };
            yield return new object[] { _standardBoardSetup.GetTileByNotation("A1"), _standardBoardSetup.GetTileByNotation("B1") };
            yield return new object[] { _standardBoardSetup.GetTileByNotation("H1"), _standardBoardSetup.GetTileByNotation("G1") };
        }

        // MemberData for invalid moves
        public static IEnumerable<object[]> GetInvalidRookMoves()
        {
            yield return new object[] { _standardBoardSetup.GetTileByNotation("A1"), _standardBoardSetup.GetTileByNotation("B2") };
            yield return new object[] { _standardBoardSetup.GetTileByNotation("A1"), _standardBoardSetup.GetTileByNotation("C3") };
            yield return new object[] { _standardBoardSetup.GetTileByNotation("H1"), _standardBoardSetup.GetTileByNotation("G2") };
            yield return new object[] { _standardBoardSetup.GetTileByNotation("H1"), _standardBoardSetup.GetTileByNotation("F3") };
        }

        // MemberData for edge cases where the Rook is on the board edge or corner
        public static IEnumerable<object[]> GetEdgeRookMoves()
        {
            yield return new object[] { _edgeBoardSetup.GetTileByNotation("A1"), _edgeBoardSetup.GetTileByNotation("A8") };
            yield return new object[] { _edgeBoardSetup.GetTileByNotation("A1"), _edgeBoardSetup.GetTileByNotation("H1") };
            yield return new object[] { _edgeBoardSetup.GetTileByNotation("H8"), _edgeBoardSetup.GetTileByNotation("H1") };
            yield return new object[] { _edgeBoardSetup.GetTileByNotation("H8"), _edgeBoardSetup.GetTileByNotation("A8") };
        }

        // Tests

        [Theory]
        [MemberData(nameof(GetRookCaptureMoves))]
        public void Test_RookMovementCapture(Tile fromTile, Tile toTile)
        {
            bool isValidMove = new Rook().IsValidCapture(fromTile, toTile, _captureBoardSetup._boardStateService.Board);
            Assert.True(isValidMove);
        }

        private void AssertRookMovement(Tile fromTile, Tile toTile, bool expectedOutcome, ChessBoard board)
        {
            bool result = new Rook().IsValidMovement(fromTile, toTile, board);
            Assert.Equal(expectedOutcome, result);
        }

        [Theory]
        [MemberData(nameof(GetStandardRookMoves))]
        public void Test_RookMovementWithinLimits_Expect_True(Tile fromTile, Tile toTile)
        {
            AssertRookMovement(fromTile, toTile, true, _standardBoardSetup._boardStateService.Board);
        }

        [Theory]
        [MemberData(nameof(GetInvalidRookMoves))]
        public void Test_InvalidRookMoves(Tile fromTile, Tile toTile)
        {
            AssertRookMovement(fromTile, toTile, false, _standardBoardSetup._boardStateService.Board);
        }

        [Theory]
        [MemberData(nameof(GetEdgeRookMoves))]
        public void Test_EdgeRookMoves(Tile fromTile, Tile toTile)
        {
            AssertRookMovement(fromTile, toTile, true, _edgeBoardSetup._boardStateService.Board);
        }
    }
}