using ChessAPI.Models;

namespace ChessAPI.Interfaces.Renderer
{
    public interface IPieceRenderer
    {
        string RenderHtml(ChessPiece piece);
    }
}
