using ChessAPI.Interfaces;
using ChessAPI.Models;
using ChessAPI.Models.Pieces;
using ChessAPI.Services;
using Xunit;

namespace ChessAPITests
{
    public class BoardStateTests
    {
        [Fact]
        public void Test_State_Expect_EmptyBoard()
        {
            // Arrange
            var boardStateService = new BoardStateService();

            // Assert: Ensure the board is created but not yet populated
            Assert.Empty(boardStateService.Board.playingFieldDictionary);
        }

        [Fact]
        public void Test_MovingPiece_ExpectEmptyFromFilledTo()
        {
            // Arrange
            var boardStateService = new BoardStateService();

            // Set up two tiles with a piece on the 'from' tile
            var fromTile = new Tile { piece = new King() };
            var toTile = new Tile();

            // Act
            boardStateService.MovePiece(fromTile, toTile);

            // Assert: Verify the piece has been moved
            Assert.Null(fromTile.piece);  // The 'from' tile should now be empty
            Assert.NotNull(toTile.piece); // The 'to' tile should now contain the piece
            Assert.IsType<King>(toTile.piece); // Verify that the moved piece is a King
        }

        [Fact]
        public void Test_BoardInitialization_ExpectEmptyPlayingFieldDictionary()
        {
            // Arrange
            var boardStateService = new BoardStateService();

            // Assert: Check that the board is initialized but empty
            Assert.NotNull(boardStateService.Board);
            Assert.Empty(boardStateService.Board.playingFieldDictionary);
        }
    }
}
