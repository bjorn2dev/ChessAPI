using ChessAPI.Helpers;
using ChessAPI.Interfaces;
using ChessAPI.Models;
using ChessAPI.Services;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessAPITests.PieceTests
{
    public class PawnTests
    {
        private Mock<IPieceMoveValidator> _pieceMoveValidatorMock;
        private Mock<IPieceHtmlRenderer> _pieceHtmlRendererMock;
        private IPieceMovingService _pieceMovingService;
        private ITileRenderer _tileRenderer;
        private IStartingPositionProvider _startingPositionProvider;
        public PawnTests()
        {
            _pieceMoveValidatorMock = new Mock<IPieceMoveValidator>();
            _pieceHtmlRendererMock = new Mock<IPieceHtmlRenderer>();
        }

        [Fact]
        public void Test_PawnMove_Expect_True()
        {
            // Arrange
            var settings = new StartingPositionSettings()
            {
                KingWhiteStart = "E1",
                QueenWhiteStart = "D1",
                RookWhiteStart = ["A1", "H1"],
                BishopWhiteStart = ["C1", "F1"],
                KnightWhiteStart = ["B1", "G1"],
                PawnWhiteStart = ["A2", "B2", "C2", "D2", "E2", "F2", "G2", "H2"],
                KingBlackStart = "E8",
                QueenBlackStart = "D8",
                RookBlackStart = ["A8", "H8"],
                BishopBlackStart = ["C8", "F8"],
                KnightBlackStart = ["B8", "G8"],
                PawnBlackStart = ["A7", "B7", "C7", "D7", "E7", "F7", "G7", "H7"]
            };
            IOptions<StartingPositionSettings> options = Options.Create(settings);
            _startingPositionProvider = new StartingPositionService(options);
            _tileRenderer = new TileHtmlRenderer(_pieceHtmlRendererMock.Object);
            var boardGenerator = new BoardGenerator(_startingPositionProvider, _tileRenderer);


            _pieceMovingService = new PieceMovingService(_tileRenderer, boardGenerator, _pieceMoveValidatorMock.Object);
            // Act: Set up the board and add pieces
            boardGenerator.SetupBoard();

            // Ensure that initial pieces are being added correctly
            boardGenerator.AddInitialPieces();

            // Act: Perform move (move A2 to A3)
            var fromTile = boardGenerator.Board.playingFieldDictionary.GetValueAtIndex(8); // A2
            var toTile = boardGenerator.Board.playingFieldDictionary.GetValueAtIndex(16); // A3

            // Act: Move the piece
            _pieceMovingService.MovePiece(fromTile.tileAnnotation, toTile.tileAnnotation);


            // Get updated tiles after move
            var fromTileAfterMove = boardGenerator.Board.playingFieldDictionary.GetValueAtIndex(8); // A2
            var toTileAfterMove = boardGenerator.Board.playingFieldDictionary.GetValueAtIndex(16); // A3

            // Assert that the move was successful
            Assert.Null(fromTileAfterMove.piece);  // fromTile no longer contains the piece
            Assert.NotNull(toTileAfterMove.piece); // toTile now contains the piece
            Assert.Equal("P", toTileAfterMove.piece.name); // Ensure it's the pawn that was moved
        }
    }
}
