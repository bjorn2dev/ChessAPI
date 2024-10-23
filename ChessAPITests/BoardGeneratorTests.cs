using ChessAPI.Interfaces;
using ChessAPI.Models;
using ChessAPI.Services;
using Microsoft.Extensions.Options;
using Moq;

namespace ChessAPITests
{
    public class BoardGeneratorTests
    {
        private Mock<IStartingPositionProvider> _startingPositionProviderMock;
        private Mock<ITileRenderer> _tileRendererMock;
        private Mock<IBoardStateService> _boardStateServiceMock;

        public BoardGeneratorTests()
        {
            _startingPositionProviderMock = new Mock<IStartingPositionProvider>();
            _tileRendererMock = new Mock<ITileRenderer>();
            _boardStateServiceMock = new Mock<IBoardStateService>();
        }

        [Fact]
        public void Test_BoardGeneratorInitial_Expect_BoardNotNull()
        {
            // Arrange
            var boardStateService = new BoardStateService();
            _boardStateServiceMock.Setup(bs => bs.Board).Returns(boardStateService.Board);

            // Act
            var boardGenerator = new BoardGenerator(_startingPositionProviderMock.Object, _tileRendererMock.Object, _boardStateServiceMock.Object);

            // Assert: Ensure the board is instantiated through BoardStateService
            Assert.NotNull(boardStateService.Board);
        }

        [Fact]
        public void Test_BoardGeneratorInitial_Expect_EmptyBoard()
        {
            // Arrange
            var boardStateService = new BoardStateService();
            _boardStateServiceMock.Setup(bs => bs.Board).Returns(boardStateService.Board);

            // Assert: Ensure the board is created but not yet populated
            Assert.Empty(boardStateService.Board.playingFieldDictionary);
        }

        [Fact]
        public void Test_BoardGeneratorAfterSetup_Expect_PopulatedBoard()
        {
            // Arrange
            var boardStateService = new BoardStateService();
            _boardStateServiceMock.Setup(bs => bs.Board).Returns(boardStateService.Board);

            var boardGenerator = new BoardGenerator(_startingPositionProviderMock.Object, _tileRendererMock.Object, _boardStateServiceMock.Object);

            // Act
            boardGenerator.SetupBoard();

            // Assert: Ensure the board has 64 tiles after setup
            Assert.Equal(64, boardStateService.Board.playingFieldDictionary.Count);
        }

        [Fact]
        public void Test_BoardGeneratorWithoutSetup_Expect_Error()
        {
            // Arrange
            var boardStateService = new BoardStateService();
            _boardStateServiceMock.Setup(bs => bs.Board).Returns(boardStateService.Board);

            var boardGenerator = new BoardGenerator(_startingPositionProviderMock.Object, _tileRendererMock.Object, _boardStateServiceMock.Object);

            // Act & Assert: Expect an exception when trying to add pieces without setting up the board
            Assert.Throws<InvalidOperationException>(() => boardGenerator.AddInitialPieces());
        }

        [Fact]
        public void Test_BoardGeneratorAfterSetup_Expect_SetupAndArrangedBoard()
        {
            // Arrange
            var boardStateService = new BoardStateService();
            _boardStateServiceMock.Setup(bs => bs.Board).Returns(boardStateService.Board);

            var boardGenerator = new BoardGenerator(_startingPositionProviderMock.Object, _tileRendererMock.Object, _boardStateServiceMock.Object);

            // Act: Set up the board and add pieces
            boardGenerator.SetupBoard();
            boardGenerator.AddInitialPieces();

            // Assert: Check if the board contains 64 tiles (8x8 chessboard)
            Assert.Equal(64, boardStateService.Board.playingFieldDictionary.Count);

            var bottomLeftTile = boardStateService.Board.playingFieldDictionary.GetValueAtIndex(0); // a1
            var topRightTile = boardStateService.Board.playingFieldDictionary.GetValueAtIndex(63); // h8

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
