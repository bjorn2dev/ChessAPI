using ChessAPI.Interfaces;
using ChessAPI.Models.Pieces;
using ChessAPI.Models;
using static ChessAPI.Models.Enums.Color;

namespace ChessAPI.Services
{
    public class KingSafetyValidator : IKingSafetyValidator
    {
        private readonly IBoardSimulationService _boardSimulationService;
        public KingSafetyValidator(IBoardSimulationService boardSimulationService)
        {
            this._boardSimulationService = boardSimulationService;
        }

        public bool ValidateKingSafety(Tile from, Tile to, MovementType movementType, Board board)
        {
            var simulatedBoard = this._boardSimulationService.SimulateMove(from, to, board);
            var playingSideColor = from.piece.color;
            var playingSideKing = simulatedBoard.GetKingTile(playingSideColor);
            var kingChecked = ((King)playingSideKing.piece).IsInCheck(simulatedBoard);
            if (movementType == MovementType.Castle && !kingChecked)
            {
                if (playingSideColor == PieceColor.White)
                {
                    // king side castle
                    if (to.tileAnnotation == "G1")
                    {
                        var kingSideRook = board.GetTileByAnnotation("H1").piece;
                        if (kingSideRook != null && kingSideRook.AllowsCastling)
                        {

                        }
                    }
                    // queen side castle
                    else if (to.tileAnnotation == "C1")
                    {

                    }

                }
                return false;
            }

            return !kingChecked; // Ensure king is not in check

        }

        public bool ValidateKingTileSafety(Tile checkTile, Board board)
        {
            var opponentSideColor = checkTile.piece.color == PieceColor.White ? PieceColor.Black : PieceColor.White;
            var oppositeSidePieceTiles = board.playingFieldDictionary.Select((x) => x.Value).Where((x) => x.piece != null && x.piece.color == opponentSideColor).ToList();

            foreach (var oppositeSidePieceTile in oppositeSidePieceTiles)
            {
                if (oppositeSidePieceTile.piece.IsCheckingKing(oppositeSidePieceTile, checkTile, board))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
