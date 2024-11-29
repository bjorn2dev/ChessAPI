using ChessAPI.Interfaces;
using ChessAPI.Models;
using ChessAPI.Models.Pieces;
using ChessAPI.Services;
using ChessAPI.Services.Board;
using Xunit;

namespace ChessAPITests.BoardTests
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
