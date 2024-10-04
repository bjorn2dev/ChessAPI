using ChessAPI.Interfaces;
using ChessAPI.Models;

namespace ChessAPI.Helpers
{
    public class TileHtmlRenderer : ITileRenderer
    {
        private readonly IPieceHtmlRenderer _pieceHtmlRenderer;
        public TileHtmlRenderer(IPieceHtmlRenderer pieceHtmlRenderer)
        {
            _pieceHtmlRenderer = pieceHtmlRenderer;
        }
        public string Render(Tile tile)
        {
            var pieceHtml = tile.piece != null ? _pieceHtmlRenderer.RenderHtml(tile.piece) : "";
            return $"<td class=\"{(tile.color ? "light-square" : "dark-square")}\">{tile.tileAnnotation}{pieceHtml}</td>";
        }
    }
}
