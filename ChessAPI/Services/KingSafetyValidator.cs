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
                (!kingChecked && (movementType == MovementType.CastleKingSide || movementType == MovementType.CastleQueenSide) && MoveValidatorHelper.ValidateCastleMovement(playingSideColor, movementType, originalBoard));


            if ()
            {

                


                if (movementType == MovementType.CastleKingSide)
                {
                    var kingSideRook = originalBoard.GetTileByAnnotation(playingSideColor == PieceColor.White ? MoveValidatorHelper.WhiteKingSideCastleRookTileAnnotation : MoveValidatorHelper.BlackKingSideCastleRookTileAnnotation).piece;
                    var checkTiles = playingSideColor == PieceColor.White ? MoveValidatorHelper.WhiteKingSideCastleRookTileAnnotation : MoveValidatorHelper.BlackKingSideCastleRookTileAnnotation;
                }



                if (playingSideColor == PieceColor.White)
                {

                    // king side castle
                    if (to.tileAnnotation == "G1")
                    {
                        var kingSideRook = originalBoard.GetTileByAnnotation("H1").piece;
                        var f1Tile = originalBoard.GetTileByAnnotation("F1");
                        var g1Tile = originalBoard.GetTileByAnnotation("G1");
                        if (kingSideRook != null && kingSideRook.AllowsCastling && f1Tile.piece == null && g1Tile.piece == null)
                        {
                            var moveKingToF1 = this._boardSimulationService.SimulateMove(kingTile, originalBoard.GetTileByAnnotation("F1"), originalBoard);
                            var moveKingToG1 = this._boardSimulationService.SimulateMove(kingTile, originalBoard.GetTileByAnnotation("G1"), originalBoard);
                            // check if moving the king to F1 or G1 tile puts it into check, if not return true allowing castling
                            if (!((King)kingTile.piece).IsInCheck(moveKingToF1) && !((King)kingTile.piece).IsInCheck(moveKingToG1)) 
                            {
                                return true;
                            }
                        }
                    }
                    // queen side castle
                    else if (to.tileAnnotation == "C1")
                    {

                    }

                }
                return false;
            }

           
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
