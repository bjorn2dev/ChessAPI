using ChessAPI.Models.Pieces;
using ChessAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessAPITests
{
    public class PieceHtmlRendererTests
    {
        private HtmlPieceRenderer _pieceHtmlRenderer;
        public PieceHtmlRendererTests()
        {
            _pieceHtmlRenderer = new HtmlPieceRenderer();
        }

        [Fact]
        public void Test_PieceHtmlRender_Expect_RenderedPiece()
        {
            var pawn = new Pawn() { color = ChessAPI.Models.Enums.Color.PieceColor.White};
            var pieceHtml = _pieceHtmlRenderer.RenderHtml(pawn);

            Assert.NotNull(pieceHtml);
            // check background color
            Assert.Contains("background-color:white", pieceHtml);

            // check piece color, use the semi colon to not trigger on background-color
            Assert.Contains(";color:black", pieceHtml);
        }
    }
}
