using ChessAPI.Models;

namespace ChessAPI.Interfaces.Piece
{
    public interface IPawnValidator
    {
        bool PromotionChecksKing(Tile from, Tile to, ChessBoard board, ChessPiece promotionType);
        bool EnPassantChecksKing(Tile from, Tile to, ChessBoard board);
    }
}
