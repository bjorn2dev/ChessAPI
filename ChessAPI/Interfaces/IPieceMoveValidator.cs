using ChessAPI.Models;

namespace ChessAPI.Interfaces
{
    public interface IPieceMoveValidator
    {
        MovementType ValidateMove(Tile from, Tile to, Board board);
    }
}
