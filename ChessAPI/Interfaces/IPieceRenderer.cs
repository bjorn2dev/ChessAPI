using ChessAPI.Models;

namespace ChessAPI.Interfaces
{
    public interface IPieceRenderer
    {
        string RenderHtml(Piece piece);
    }
}
