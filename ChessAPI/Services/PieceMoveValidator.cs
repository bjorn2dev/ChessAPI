using ChessAPI.Helpers;
using ChessAPI.Interfaces;
using ChessAPI.Models;
using ChessAPI.Models.Enums;
using ChessAPI.Models.Pieces;
using System.Linq;

namespace ChessAPI.Services
{
    public class PieceMoveValidator : IPieceMoveValidator
    {
        public PieceMoveValidator()
        {
        }

        public bool ValidateMove(Tile from, Tile to, Board board)
        {
            var fromPiece = from.piece;

            var movementType = MoveValidatorHelper.GetMovementType(from, to, board);
            movementType = MoveValidatorHelper.CheckIfCapture(from, to, movementType) ? MovementType.Capture : movementType;

            // every move is done with a piece and cant capture pieces of the same color
            if (fromPiece == null || (to.piece != null && fromPiece.color == to.piece.color) || movementType == MovementType.Invalid) return false;

            var indexes = MoveValidatorHelper.GetMovementIndexes(from, to, board);
            
            // verify it is a legal move or capture
            var isValidMove = movementType != MovementType.Capture ? fromPiece.IsValidMovement(from, to, board) : fromPiece.IsValidCapture(from, to, board);

            // check if the move is a move that sets the king in check
            isValidMove = IsCheckMove(from, to, board);


            return isValidMove;
           
        }

        public bool IsCheckMove(Tile from, Tile to, Board board)
        {
            // Clone the board for simulation
            var simulatedBoard = board.Clone();

            // Get tiles from the cloned board
            var simulatedFromTile = simulatedBoard.playingFieldDictionary.FirstOrDefault(x => x.Value.fileNumber == from.fileNumber && x.Value.rank == from.rank).Value;
            var simulatedToTile = simulatedBoard.playingFieldDictionary.FirstOrDefault(x => x.Value.fileNumber == to.fileNumber && x.Value.rank == to.rank).Value;

            // Simulate the move
            simulatedToTile.piece = simulatedFromTile.piece;
            simulatedFromTile.piece = null;

            var kingTiles = this.GetKingTiles(simulatedBoard);
            var playingSideColor = from.piece.color == Color.PieceColor.White ? Color.PieceColor.White : Color.PieceColor.Black;
            var opponentSideColor = playingSideColor == Color.PieceColor.White ? Color.PieceColor.Black : Color.PieceColor.White;
            var playingSideKingTile = playingSideColor == Color.PieceColor.White ? kingTiles.whiteKingTile : kingTiles.blackKingTile;
            var oppositeSidePieceTiles = simulatedBoard.playingFieldDictionary.Select((x) => x.Value).Where((x) => x.piece != null && x.piece.color == opponentSideColor).ToList();


            foreach(var oppositeSidePieceTile in oppositeSidePieceTiles)
            {
                var pieceRangeNumbers = new List<int>();
                foreach(var pattern in oppositeSidePieceTile.piece.capturePattern)
                {
                    pieceRangeNumbers.AddRange(MoveValidatorHelper.GetMovementRange(pattern));
                }

                return MoveValidatorHelper.CheckTileRange(pieceRangeNumbers.ToArray(), oppositeSidePieceTile, playingSideKingTile, simulatedBoard);
            }
            // Check if the opponent's king is in check
            var movementType = MoveValidatorHelper.GetMovementType(simulatedToTile, simulatedToTile.piece.color == Color.PieceColor.White ? kingTiles.blackKingTile : kingTiles.whiteKingTile, simulatedBoard);
            var opponentKingInCheck = simulatedToTile.piece.capturePattern.Contains(movementType);

            //    this.IsKingChecked(
            //    simulatedFromTile,
            //    simulatedToTile,
            //    simulatedToTile.piece.color == Color.PieceColor.White ? kingTiles.whiteKingTile : kingTiles.blackKingTile,
            //    simulatedBoard
            //);

            // If the player's king is under check after the move, return false
             return opponentKingInCheck;
        }

        private bool IsKingChecked(Tile from, Tile to, Tile kingTile, Board board)
        {
            // simulate move
            var toTile = board.playingFieldDictionary.FirstOrDefault(x => x.Value == to).Value;
            var fromTile = board.playingFieldDictionary.FirstOrDefault(x => x.Value == from).Value;

            toTile.piece = from.piece;
            fromTile.piece = null;

            var movementType = MoveValidatorHelper.GetMovementType(toTile, kingTile, board);

            return toTile.piece.capturePattern.Contains(movementType);
        }

        private (Tile whiteKingTile, Tile blackKingTile) GetKingTiles(Board board)
        {
            var whiteKing = board.playingFieldDictionary.FirstOrDefault(x => x.Value.piece is King && x.Value.piece.color == Color.PieceColor.White).Value;
            var blackKing = board.playingFieldDictionary.FirstOrDefault(x => x.Value.piece is King && x.Value.piece.color == Color.PieceColor.Black).Value;
            return (whiteKing, blackKing);
        }
    }
}
