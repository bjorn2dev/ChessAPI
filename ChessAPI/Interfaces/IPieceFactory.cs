using ChessAPI.Models;
using static ChessAPI.Models.Enums.Color;

namespace ChessAPI.Interfaces
{
    public interface IPieceFactory
    {
        Piece CreatePiece(Type pieceType, PieceColor color);
    }
}
