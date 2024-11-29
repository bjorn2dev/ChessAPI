using ChessAPI.Models;

namespace ChessAPI.Interfaces.Piece
{
    public interface IPieceMovingService
    {
        void MovePiece(Tile fromTile, Tile toTile, MovementType movementType, ChessBoard board);
    }
}
