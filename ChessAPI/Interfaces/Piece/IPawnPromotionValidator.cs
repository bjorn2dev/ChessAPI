using ChessAPI.Models;

namespace ChessAPI.Interfaces.Piece
{
    public interface IPawnPromotionValidator
    {
        bool CheckPawnPromotion(Tile from, Tile to, ChessBoard board, ChessPiece promotionType);
    }
}
