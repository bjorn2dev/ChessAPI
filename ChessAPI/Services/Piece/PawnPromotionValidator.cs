using ChessAPI.Interfaces.Board;
using ChessAPI.Interfaces.Piece;
using ChessAPI.Models;

namespace ChessAPI.Services.Piece
{
    public class PawnPromotionValidator : IPawnPromotionValidator
    {
        private readonly IBoardSimulationService _boardSimulationService;
        public PawnPromotionValidator(IBoardSimulationService boardSimulationService)
        {
            _boardSimulationService = boardSimulationService;
        }

        public bool CheckPawnPromotion(Tile from, Tile to, ChessBoard board, ChessPiece promotionType)
        {
            var simulatedBoard = _boardSimulationService.SimulateMove(from, to ,board);
            var promotionTile = simulatedBoard.GetTileByAnnotation(to.tileAnnotation);
            promotionTile.piece = promotionType;
            promotionTile.piece.color = from.piece.color;
            return promotionTile.piece.IsCheckingKing(promotionTile, simulatedBoard.GetKingTile(promotionTile.piece.color), simulatedBoard);
        }
    }
}
