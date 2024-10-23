using ChessAPI.Models;

namespace ChessAPI.Interfaces
{
    public interface IPieceMovingService
    {
        void MovePiece(string from, string to);
    }
}
