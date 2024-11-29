using ChessAPI.Models;

namespace ChessAPI.Interfaces.Piece
{
    public interface IPieceMoveValidator
    {
        MovementType ValidateMove(Tile from, Tile to, ChessBoard board);
    }
}
