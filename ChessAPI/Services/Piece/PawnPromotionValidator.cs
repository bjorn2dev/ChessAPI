using ChessAPI.Interfaces.Board;
using ChessAPI.Interfaces.Piece;
using ChessAPI.Models;
using ChessAPI.Models.Pieces;

namespace ChessAPI.Services.Piece
{
    public class PawnPromotionValidator : IPawnPromotionValidator
    {
        private readonly IBoardSimulationService _boardSimulationService;
        public PawnPromotionValidator(IBoardSimulationService boardSimulationService)
        {
            _boardSimulationService = boardSimulationService;
        }

        public bool PawnPromotionChecksKing(Tile from, Tile to, ChessBoard board, ChessPiece promotionType)
        {
            var simulatedBoard = _boardSimulationService.SimulateMove(from, to ,board);
            var promotionTile = simulatedBoard.GetTileByAnnotation(to.tileAnnotation);
            promotionTile.piece = promotionType;
            promotionTile.piece.color = from.piece.color;
            var kingTile = simulatedBoard.GetKingTile(promotionTile.piece.color);
            return ((King)kingTile.piece).IsInCheck(simulatedBoard);
        }
    }
}
