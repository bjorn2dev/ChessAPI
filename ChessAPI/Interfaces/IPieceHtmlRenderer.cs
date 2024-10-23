using ChessAPI.Models;

namespace ChessAPI.Interfaces
{
    public interface IPieceHtmlRenderer
    {
        string RenderHtml(Piece piece);
    }
}
