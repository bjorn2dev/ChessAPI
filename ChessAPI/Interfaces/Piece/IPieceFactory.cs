using ChessAPI.Models;
using static ChessAPI.Models.Enums.Color;

namespace ChessAPI.Interfaces.Piece
{
    public interface IPieceFactory
    {
        ChessPiece CreatePiece(Type pieceType, PieceColor color);
    }
}
