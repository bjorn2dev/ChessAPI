using ChessAPI.Interfaces;
using ChessAPI.Models;
using ChessAPI.Services;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;

namespace ChessAPITests
{
    public class BoardGeneratorTests
    {
        private Mock<IStartingPositionProvider> _startingPositionProviderMock;
        private Mock<ITileRenderer> _tileRendererMock;
        public BoardGeneratorTests()
        {
            _startingPositionProviderMock = new Mock<IStartingPositionProvider>();
            _tileRendererMock = new Mock<ITileRenderer>();
        }
        

        [Fact]
        public void Test_BoardGeneratorInitial_Expect_BoardNotNull()
        {
            // Act
            var boardGenerator = new BoardGenerator(_startingPositionProviderMock.Object, _tileRendererMock.Object);
            // Assert: 
            Assert.NotNull(boardGenerator.Board);  // Ensure the board has been instantiated
        }


        [Fact]
        public void Test_BoardGeneratorInitial_Expect_EmptyBoard()
        {
            // Act
            var boardGenerator = new BoardGenerator(_startingPositionProviderMock.Object, _tileRendererMock.Object);

            // Assert
            Assert.Empty(boardGenerator.Board.playingFieldDictionary);  // Ensure the board is created but not yet populated
        }

        [Fact]
        public void Test_BoardGeneratorAfterSetup_Expect_PopulatedBoard()
        {
            // Arrange
            var boardGenerator = new BoardGenerator(_startingPositionProviderMock.Object, _tileRendererMock.Object);

            // Act
            boardGenerator.SetupBoard();

            // Assert
            Assert.Equal(64, boardGenerator.Board.playingFieldDictionary.Count);
        }

        [Fact]
        public void Test_BoardGeneratorWithoutSetup_Expect_Error()
        {
            // Arrange
            var boardGenerator = new BoardGenerator(_startingPositionProviderMock.Object, _tileRendererMock.Object);

            // Act & Assert: Expect an exception when trying to add pieces without setting up the board
            Assert.Throws<InvalidOperationException>(() => boardGenerator.AddInitialPieces());
        }

        [Fact]
        public void Test_BoardGeneratorAfterSetup_Expect_SetupAndArrangedBoard()
        {
            // Arrange: Create a new instance of BoardGenerator
            var boardGenerator = new BoardGenerator(_startingPositionProviderMock.Object, _tileRendererMock.Object);

            // Act: Set up the board and add pieces
            boardGenerator.SetupBoard();
            boardGenerator.AddInitialPieces();

            // Assert: Check if the board contains 64 tiles (8x8 chessboard)
            Assert.Equal(64, boardGenerator.Board.playingFieldDictionary.Count);

            var bottomLeftTile = boardGenerator.Board.playingFieldDictionary.GetValueAtIndex(0); // a1
            var topRightTile = boardGenerator.Board.playingFieldDictionary.GetValueAtIndex(63); // h8

            // Assert that the tiles are correctly set
            Assert.Equal(1, bottomLeftTile.rank);
            Assert.Equal(0, bottomLeftTile.fileNumber); // A is represented by 0
            Assert.Equal("A1", bottomLeftTile.tileAnnotation);

            Assert.Equal(8, topRightTile.rank);
            Assert.Equal(7, topRightTile.fileNumber); // H is represented by 7
            Assert.Equal("H8", topRightTile.tileAnnotation);
        }
    }
}
