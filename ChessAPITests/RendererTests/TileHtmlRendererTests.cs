using ChessAPI.Interfaces.Renderer;
using ChessAPI.Models;
using ChessAPI.Models.Enums;
using ChessAPI.Models.Pieces;
using ChessAPI.Services.Renderers.Html;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessAPITests.RendererTests
{
    public class TileHtmlRendererTests
    {
        private HtmlTileRenderer _tileHtmlRenderer;
        private Mock<IPieceRenderer> _pieceHtmlRendererMock;

        public TileHtmlRendererTests()
        {
            _pieceHtmlRendererMock = new Mock<IPieceRenderer>();
            _tileHtmlRenderer = new HtmlTileRenderer(_pieceHtmlRendererMock.Object);
        }

        [Fact]
        public void Test_TileHtmlGeneratorWithoutPiece_ExpectEmptyTile()
        {
            Tile tile = new Tile() { color = true, fileNumber = 1, rank = 1, piece = null };
            var result = _tileHtmlRenderer.Render(tile);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Contains(tile.tileAnnotation, result);
        }

        [Fact]
        public void Test_TileHtmlGeneratorWithPiece_ExpectFilledTile()
        {
            _pieceHtmlRendererMock.Setup(r => r.RenderHtml(It.IsAny<ChessPiece>())).Returns("<p>P</p>");
            Tile tile = new Tile() { color = true, fileNumber = 1, rank = 1, piece = new Knight() { color = Color.PieceColor.White } };
            var result = _tileHtmlRenderer.Render(tile);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Contains("<p>P</p>", result);
        }
    }
}
