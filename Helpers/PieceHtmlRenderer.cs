using ChessAPI.Interfaces;
using ChessAPI.Models;
using static ChessAPI.Models.Enums.Color;

namespace ChessAPI.Helpers
{
    public class PieceHtmlRenderer : IPieceHtmlRenderer
    {
        public string RenderHtml(Piece piece)
        {
            return $"<p style=\"background-color:{(piece.color == PieceColor.White ? "white" : "black")};color:{(piece.color == PieceColor.White ? "black" : "white")};padding:20px;\">{piece.name}</p>";
        }
    }
}
