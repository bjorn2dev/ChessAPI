using ChessAPI.Models;
using ChessAPI.Models.Enums;

namespace ChessAPI.Interfaces.Piece
{
    public interface IPieceMovingService
    {
        void MovePiece(Tile fromTile, Tile toTile, MovementType movementType, ChessBoard board, ChessPiece promotionType = null);
    }
}
