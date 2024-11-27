using ChessAPI.Interfaces;
using ChessAPI.Models.Pieces;
using ChessAPI.Models;
using static ChessAPI.Models.Enums.Color;
using ChessAPI.Helpers;

namespace ChessAPI.Services
{
    public class KingSafetyValidator : IKingSafetyValidator
    {

        private readonly IBoardSimulationService _boardSimulationService;
        public KingSafetyValidator(IBoardSimulationService boardSimulationService)
        {
            this._boardSimulationService = boardSimulationService;
        }

        public bool ValidateKingSafety(Tile kingTile, Tile to, MovementType movementType, Board originalBoard)
        {
            var simulatedBoard = this._boardSimulationService.SimulateMove(kingTile, to, originalBoard);
            var playingSideColor = kingTile.piece.color;
            var playingSideKing = simulatedBoard.GetKingTile(playingSideColor);
            var kingChecked = ((King)playingSideKing.piece).IsInCheck(simulatedBoard);
            return !kingChecked ||
                (!kingChecked && (movementType == MovementType.CastleKingSide || movementType == MovementType.CastleQueenSide) && this.ValidateCastleMovement(playingSideColor, movementType, originalBoard));
        }

        public bool ValidateKingTileSafety(Tile checkTile, Board board)
        {
            var opponentSideColor = checkTile.piece.color == PieceColor.White ? PieceColor.Black : PieceColor.White;
            var oppositeSidePieceTiles = board.playingFieldDictionary.Select((x) => x.Value).Where((x) => x.piece != null && x.piece.color == opponentSideColor).ToList();

            foreach (var oppositeSidePieceTile in oppositeSidePieceTiles)
            {
                if (oppositeSidePieceTile.piece.IsCheckingKing(oppositeSidePieceTile, checkTile, board))
                {
                    return false;
                }
            }
            return true;
        }

        public bool ValidateCastleMovement(PieceColor playerColor, MovementType castleDirection, Board board)
        {
            if (castleDirection != MovementType.CastleKingSide || castleDirection != MovementType.CastleQueenSide) return false;

            var castleTiles = CastleHelper.GetCastleTiles(playerColor, castleDirection);
            var kingTo = board.GetTileByAnnotation(castleTiles.KingToTileAnnotation);
            var rookTile = board.GetTileByAnnotation(castleTiles.RookTileAnnotation).piece;
            return rookTile != null && rookTile.AllowsCastling && this.CheckCastlingTiles(castleTiles.KingTileAnnotation, castleTiles.CheckTiles, board);
        }

        private bool CheckCastlingTiles(string kingTileAnnotation, string[] checkTiles, Board board)
        {
            var kingTile = board.GetTileByAnnotation(kingTileAnnotation);
            foreach (var tile in checkTiles)
            {
                var kingToTile = board.GetTileByAnnotation(tile);

                if (kingToTile.piece != null) return false;

                var simulatedBoard = this._boardSimulationService.SimulateMove(kingTile, kingToTile, board);
               
                return this.ValidateKingTileSafety(kingToTile, simulatedBoard);   
            }

            return true;
        }
    }
}
