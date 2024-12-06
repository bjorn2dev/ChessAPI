using ChessAPI.Interfaces.Renderer;
using ChessAPI.Models;
using static ChessAPI.Models.Enums.Color;

namespace ChessAPI.Services.Renderer.Html
{
    public class HtmlPieceRenderer : IPieceRenderer
    {
        public string RenderHtml(ChessPiece piece)
        {
            return $"<p data-piece-color=\"{piece.color}\" style=\"background-color:{(piece.color == PieceColor.White ? "white" : "black")};color:{(piece.color == PieceColor.White ? "black" : "white")};padding:20px;\">{piece.name}</p>";
        }
    }
}
