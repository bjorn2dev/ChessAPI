using ChessAPI.Models;

namespace ChessAPI.Interfaces
{
    public interface IPieceMovingService
    {
        void MovePiece(Tile fromTile, Tile toTile, MovementType movementType, Board board);
    }
}
