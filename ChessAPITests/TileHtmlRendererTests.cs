using ChessAPI.Helpers;
using ChessAPI.Interfaces;
using ChessAPI.Models;
using ChessAPI.Models.Enums;
using ChessAPI.Models.Pieces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessAPITests
{
    public class TileHtmlRendererTests
    {
        private TileHtmlRenderer _tileHtmlRenderer;
        private Mock<IPieceHtmlRenderer> _pieceHtmlRendererMock;

        public TileHtmlRendererTests()
        {
            _pieceHtmlRendererMock = new Mock<IPieceHtmlRenderer>();
            _tileHtmlRenderer = new TileHtmlRenderer(_pieceHtmlRendererMock.Object);
        }

        [Fact]
        public void Test_TileHtmlGeneratorWithoutPiece_ExpectEmptyTile()
        {
            Tile tile = new Tile() { color = true, fileNumber = 1, rank = 1 , piece = null};
            var result = _tileHtmlRenderer.Render(tile);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Contains(tile.tileAnnotation, result);
        }

        [Fact]
        public void Test_TileHtmlGeneratorWithPiece_ExpectFilledTile()
        {
            _pieceHtmlRendererMock.Setup(r => r.RenderHtml(It.IsAny<Piece>())).Returns("<p>P</p>");
            Tile tile = new Tile() { color = true, fileNumber = 1, rank = 1, piece = new Pawn() { color = Color.PieceColor.White } };
            var result = _tileHtmlRenderer.Render(tile);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Contains("<p>P</p>", result);
        }
    }
}
