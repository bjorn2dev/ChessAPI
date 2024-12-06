using ChessAPI.Models.Pieces;
using ChessAPI.Models;
using ChessAPITests.BoardSetup;
using Microsoft.Extensions.DependencyInjection;
using ChessAPI.Models.Enums;

namespace ChessAPITests.PieceTests
{
    public class PawnTests
    {
        // create static board setups to avoid creating new instances for every separate test.
        private static readonly CompleteBoardSetup _standardBoardSetup = new CompleteBoardSetup("StartingPosition");
        private static readonly CompleteBoardSetup _captureBoardSetup = new CompleteBoardSetup("PawnCaptureStartingPosition");
        private static readonly CompleteBoardSetup _enPassantBoardSetup = new CompleteBoardSetup("PawnEnPassantPosition");
        private static readonly CompleteBoardSetup _promotionBoardSetup = new CompleteBoardSetup("PawnPromotionPosition");
        private static readonly CompleteBoardSetup _invalidMovesBoardSetup = new CompleteBoardSetup("PawnInvalidMovesPosition");
        public static IEnumerable<object[]> GetInvalidPawnMoves()
        {
            // White pawn tests
            yield return new object[] { _invalidMovesBoardSetup.GetTileByNotation("C3"), _invalidMovesBoardSetup.GetTileByNotation("B3") }; // Sideways move
            yield return new object[] { _invalidMovesBoardSetup.GetTileByNotation("C3"), _invalidMovesBoardSetup.GetTileByNotation("C2") }; // Backward move
            yield return new object[] { _invalidMovesBoardSetup.GetTileByNotation("C3"), _invalidMovesBoardSetup.GetTileByNotation("E4") }; // Diagonal without capture
            yield return new object[] { _invalidMovesBoardSetup.GetTileByNotation("C3"), _invalidMovesBoardSetup.GetTileByNotation("C6") };
            // Black pawn tests
            yield return new object[] { _invalidMovesBoardSetup.GetTileByNotation("D6"), _invalidMovesBoardSetup.GetTileByNotation("C6") }; // Sideways move
            yield return new object[] { _invalidMovesBoardSetup.GetTileByNotation("D6"), _invalidMovesBoardSetup.GetTileByNotation("D7") }; // Backward move
            yield return new object[] { _invalidMovesBoardSetup.GetTileByNotation("D6"), _invalidMovesBoardSetup.GetTileByNotation("B5") }; // Diagonal without capture
        }

        public static IEnumerable<object[]> GetStandardPawnMoves()
        {
            yield return new object[] { _standardBoardSetup.GetTileByNotation("A2"), _standardBoardSetup.GetTileByNotation("A3") };
            yield return new object[] { _standardBoardSetup.GetTileByNotation("A7"), _standardBoardSetup.GetTileByNotation("A6") };
            yield return new object[] { _standardBoardSetup.GetTileByNotation("A2"), _standardBoardSetup.GetTileByNotation("A4") };
        }

        public static IEnumerable<object[]> GetCapturePawnMoves()
        {
            yield return new object[] { _captureBoardSetup.GetTileByNotation("D4"), _captureBoardSetup.GetTileByNotation("E5") }; // White captures Black
            yield return new object[] { _captureBoardSetup.GetTileByNotation("E4"), _captureBoardSetup.GetTileByNotation("D5") }; // Black captures White
        }

        public static IEnumerable<object[]> GetEnPassantMoves()
        {
            yield return new object[] { _enPassantBoardSetup.GetTileByNotation("D5"), _enPassantBoardSetup.GetTileByNotation("E6") }; // White en passant capture
        }

        public static IEnumerable<object[]> GetPromotionMoves()
        {
            yield return new object[] { _promotionBoardSetup.GetTileByNotation("A7"), _promotionBoardSetup.GetTileByNotation("A8"), new Queen { color = Color.PieceColor.White } };
            yield return new object[] { _promotionBoardSetup.GetTileByNotation("A7"), _promotionBoardSetup.GetTileByNotation("A8"), new Rook { color = Color.PieceColor.Black } };
        }

        private void AssertPawnMovement(Tile fromTile, Tile toTile, bool expectedOutcome)
        {
            ChessBoard board = expectedOutcome ? _standardBoardSetup._boardStateService.Board : _invalidMovesBoardSetup._boardStateService.Board;
            var pawn = fromTile.piece as Pawn;
            if (pawn == null)
            {
                throw new InvalidOperationException("The piece on the fromTile is not a pawn.");
            }

            bool result = pawn.IsValidMovement(fromTile, toTile, board);

            Assert.Equal(expectedOutcome, result);
        }


        [Theory]
        [MemberData(nameof(GetStandardPawnMoves))]
        public void Test_PawnMovementWithinLimits_Expect_True(Tile fromTile, Tile toTile)
        {
            AssertPawnMovement(fromTile, toTile, true);
        }

        [Theory]
        [MemberData(nameof(GetInvalidPawnMoves))]
        public void Test_PawnMovementWithinLimits_Expect_False(Tile fromTile, Tile toTile)
        {
            AssertPawnMovement(fromTile, toTile, false);
        }

        [Theory]
        [MemberData(nameof(GetCapturePawnMoves))]
        public void Test_PawnMovementCapture(Tile fromTile, Tile toTile)
        {
            var pawn = fromTile.piece as Pawn;
            if (pawn == null)
            {
                throw new InvalidOperationException("The piece on the fromTile is not a pawn.");
            }
            bool result = pawn.IsValidCapture(fromTile, toTile, _captureBoardSetup._boardStateService.Board);
            Assert.True(result);
        }

        [Theory]
        [MemberData(nameof(GetEnPassantMoves))]
        public void Test_PawnMovementEnPassant(Tile fromTile, Tile toTile)
        {
            var pawn = fromTile.piece as Pawn;
            if (pawn == null)
            {
                throw new InvalidOperationException("The piece on the fromTile is not a pawn.");
            }
            bool result = pawn.IsValidMovement(fromTile, toTile, _enPassantBoardSetup._boardStateService.Board);
            Assert.True(result);
        }

        [Theory]
        [MemberData(nameof(GetPromotionMoves))]
        public void Test_PawnPromotion(Tile fromTile, Tile toTile, ChessPiece promoteTo)
        {
            var pawn = fromTile.piece as Pawn;
            if (pawn == null)
            {
                throw new InvalidOperationException("The piece on the fromTile is not a pawn.");
            }
            bool result = pawn.CanPromote(fromTile, toTile, _enPassantBoardSetup._boardStateService.Board, promoteTo);
            Assert.True(result);
        }


    }
}