using ChessAPI.Models;

namespace ChessAPI.Interfaces
{
    public interface IPieceMoveValidator
    {
        bool ValidateMove(Tile from, Tile to);
    }
}
