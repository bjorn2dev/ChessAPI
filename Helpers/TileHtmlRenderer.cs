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
            var tileHtml = tile.piece != null ? _pieceHtmlRenderer.RenderHtml(tile.piece) : tile.tileAnnotation;
            return $"<td data-rank=\"{tile.rank}\" data-file=\"{tile.file}\" data-tile-annotation=\"{tile.tileAnnotation}\" class=\"{(tile.color ? "light-square" : "dark-square")}\">{tileHtml}</td>";
        }
    }
}
