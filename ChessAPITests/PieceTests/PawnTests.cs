using ChessAPI.Helpers;
using ChessAPI.Interfaces;
using ChessAPI.Models;
using ChessAPI.Models.Pieces;
using ChessAPI.Services;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace ChessAPITests.PieceTests
{
    public class PawnTests
    {
        private Mock<IStartingPositionProvider> _startingPositionProviderMock;
        private Mock<IPieceMoveValidator> _pieceMoveValidatorMock;
        private Mock<ITileRenderer> _tileRendererMock;
        private Mock<IBoardStateService> _boardStateServiceMock;
        private Mock<IPieceHtmlRenderer> _pieceHtmlRendererMock;
        private Board _mockBoard;

        public PawnTests()
        {
            _startingPositionProviderMock = new Mock<IStartingPositionProvider>();
            _pieceMoveValidatorMock = new Mock<IPieceMoveValidator>();
            _tileRendererMock = new Mock<ITileRenderer>();
            _boardStateServiceMock = new Mock<IBoardStateService>();
            _pieceHtmlRendererMock = new Mock<IPieceHtmlRenderer>();
            _mockBoard = new Board();
        }

        [Fact]
        public void Test_PawnMove_From_A2_To_A3()
        {
            // Arrange
            _startingPositionProviderMock.CallBase = true;
            _startingPositionProviderMock.Setup(s => s.GetPieceTypeForLocation("A2")).Returns(typeof(Pawn));
            _startingPositionProviderMock.Setup(s => s.IsWhiteStartingPosition("A2")).Returns(true);

            _boardStateServiceMock.CallBase = true;
            _boardStateServiceMock.Setup(bs => bs.Board).Returns(_mockBoard);

            _pieceHtmlRendererMock.Setup(r => r.RenderHtml(It.IsAny<Piece>())).Returns("<p>P</p>");
            _tileRendererMock.Setup(t => t.Render(It.IsAny<Tile>())).Returns("<td>A2</td>");

            var boardGenerator = new BoardGenerator(
                _startingPositionProviderMock.Object,
                _tileRendererMock.Object,
                _boardStateServiceMock.Object
            );

            // Act 1: Set up the board and add pieces
            boardGenerator.SetupBoard();
            boardGenerator.AddInitialPieces();

            // Assert: Check if the board's playfield is filled
            Assert.Equal(64, _mockBoard.playingFieldDictionary.Count);

            var fromTile = _mockBoard.playingFieldDictionary.GetValueAtIndex(8); // A2
            Assert.NotNull(fromTile.piece);
            Assert.IsType<Pawn>(fromTile.piece);
            Assert.Equal("A2", fromTile.tileAnnotation);

            // Partial mock: Call the real method for validation but mock irrelevant dependencies
            _pieceMoveValidatorMock.CallBase = true;
            _pieceMoveValidatorMock.Setup(v => v.ValidateMove(fromTile, It.IsAny<Tile>())).Returns(true);

            var pieceMovingService = new PieceMovingService(_tileRendererMock.Object, _pieceMoveValidatorMock.Object, _boardStateServiceMock.Object);
            
            // Act 2: Move the pawn from A2 to A3
            var toTile = _mockBoard.playingFieldDictionary.GetValueAtIndex(16); // A3
            pieceMovingService.MovePiece(fromTile.tileAnnotation, toTile.tileAnnotation);

            // Assert: Verify the move
            var fromTileAfterMove = _mockBoard.playingFieldDictionary.GetValueAtIndex(8);  // A2
            var toTileAfterMove = _mockBoard.playingFieldDictionary.GetValueAtIndex(16);  // A3

            Assert.Null(fromTileAfterMove.piece);  // From tile should now be empty
            Assert.NotNull(toTileAfterMove.piece); // To tile should now contain the piece
            Assert.IsType<Pawn>(toTileAfterMove.piece);  // Ensure it's still a pawn
        }
    }
}
