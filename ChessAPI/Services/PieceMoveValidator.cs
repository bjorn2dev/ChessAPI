using ChessAPI.Helpers;
using ChessAPI.Interfaces;
using ChessAPI.Models;
using ChessAPI.Models.Enums;
using ChessAPI.Models.Pieces;

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
            var check = IsCheckMove(from, to, board);
            if (movementType != MovementType.Capture)
            {
                return fromPiece.IsValidMovement(from, to, board);
            }
            else
            {
                return fromPiece.IsValidCapture(from, to, board);
            }
        }

        public bool IsCheckMove(Tile from, Tile to, Board board)
        {
            var kingTile = this.GetKingTiles(board);
          
             return this.IsKingChecked(from, to, from.piece.color == Color.PieceColor.White ? kingTile.blackKingTile : kingTile.whiteKingTile, board);

        }

        private bool IsKingChecked(Tile from, Tile to, Tile kingTile, Board board)
        {
            var movementType = MoveValidatorHelper.GetMovementType(kingTile, to, board);
            if(from.piece.movePattern.Contains(movementType))
            {
                return true;
            }
            return false;
        }

        private (Tile whiteKingTile, Tile blackKingTile) GetKingTiles(Board board)
        {
            var whiteKing = board.playingFieldDictionary.FirstOrDefault(x => x.Value.piece is King && x.Value.piece.color == Color.PieceColor.White).Value;
            var blackKing = board.playingFieldDictionary.FirstOrDefault(x => x.Value.piece is King && x.Value.piece.color == Color.PieceColor.Black).Value;
            return (whiteKing, blackKing);
        }
    }
}
