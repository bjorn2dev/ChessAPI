using ChessAPI.Helpers;
using ChessAPI.Interfaces;
using ChessAPI.Interfaces.Piece;
using ChessAPI.Models.Enums;
using System.Security.Cryptography.X509Certificates;
using static ChessAPI.Models.Enums.Color;

namespace ChessAPI.Models.Pieces
{
    public class Pawn : ChessPiece
    {

        private bool _hasMoved = false;
        public bool AllowMoveDoubleAdvance => !this._hasMoved;
        private readonly IPawnPromotionValidator _promotionValidator;

        public Pawn(IPawnPromotionValidator promotionValidator)
        {
            this._promotionValidator = promotionValidator;
            this.name = "<img src=\"https://upload.wikimedia.org/wikipedia/commons/thumb/4/45/Chess_plt45.svg/1280px-Chess_plt45.svg.png\" width=\"100\" height=\"100\" data-name=\"pawn\">";
            this.movePattern = [MovementType.Vertical, MovementType.Promotion];
            this.capturePattern = [MovementType.Diagonal, MovementType.Promotion];
        }

        public void MarkAsMoved()
        {
            this._hasMoved = true;
        }

        public override bool IsValidCapture(Tile from, Tile to, ChessBoard board)
        {
            var indexes = MoveValidatorHelper.GetMovementIndexes(from, to, board);
            var difference = MoveValidatorHelper.GetMovementDifference(indexes.fromIndex, indexes.toIndex);
            var movementType = MoveValidatorHelper.DetermineMovementType(from, to, board);

            var pawnRange = MoveValidatorHelper.GetMovementRange(this.capturePattern.First());
            return this.capturePattern.Contains(movementType) && pawnRange.Contains(difference) ? MoveValidatorHelper.CheckTileRange(pawnRange, from, to, board, true) : false;
        }

        public override bool IsValidMovement(Tile from, Tile to, ChessBoard board)
        {
            var indexes = MoveValidatorHelper.GetMovementIndexes(from, to, board);
            var difference = MoveValidatorHelper.GetMovementDifference(indexes.fromIndex, indexes.toIndex);
            var movementType = MoveValidatorHelper.DetermineMovementType(from, to, board);

            // Pawns may only move forward vertically without capturing, 
            var pawnRange = MoveValidatorHelper.GetMovementRange(MovementType.Vertical);
     
            return this.movePattern.Contains(movementType) && (difference == pawnRange.First() || this.AllowMoveDoubleAdvance && (pawnRange.First() * 2) == difference) ?
                MoveValidatorHelper.CheckTileRange(pawnRange, from, to, board, false) :
                false;
        }
        
        public override bool IsCheckingKing(Tile from, Tile kingTile, ChessBoard board)
        {
            var indexes = MoveValidatorHelper.GetMovementIndexes(from, kingTile, board);
            var difference = MoveValidatorHelper.GetMovementDifference(indexes.fromIndex, indexes.toIndex);
            var movementType = MoveValidatorHelper.DetermineMovementType(from, kingTile, board);

            if (!this.capturePattern.Contains(movementType)) return false;
            var step = MoveValidatorHelper.GetMovementRange(movementType).FirstOrDefault((rangeItem) => difference % rangeItem == 0);
            return difference == step ? MoveValidatorHelper.CheckPath(indexes.fromIndex, indexes.toIndex, step, board, MovementType.Capture, true) : false;
        }

        public bool IsValidEnPassant(Tile from, Tile to, ChessBoard board)
        {
            return false;
        }

        public bool CanPromote(Tile from, Tile to, ChessBoard board, ChessPiece promoteTo)
        {
            var movementType = MoveValidatorHelper.DetermineMovementType(from, to, board);
            return !this._promotionValidator.PawnPromotionChecksKing(from, to, board, promoteTo) && movementType != MovementType.Capture
              ? from.piece.IsValidMovement(from, to, board)
              : from.piece.IsValidCapture(from, to, board);
        }
    }
}

