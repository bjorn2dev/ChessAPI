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

        private readonly IPawnPromotionValidator _promotionValidator;

        public Pawn(IPawnPromotionValidator promotionValidator)
        {
            this._promotionValidator = promotionValidator;
            this.name = "<img src=\"https://upload.wikimedia.org/wikipedia/commons/thumb/4/45/Chess_plt45.svg/1280px-Chess_plt45.svg.png\" width=\"100\" height=\"100\">";
            this.movePattern = [MovementType.Vertical];
            this.capturePattern = [MovementType.Diagonal];
        }

        public override bool IsValidCapture(Tile from, Tile to, ChessBoard board)
        {
            var indexes = MoveValidatorHelper.GetMovementIndexes(from, to, board);
            var difference = MoveValidatorHelper.GetMovementDifference(indexes.fromIndex, indexes.toIndex);
            var movementType = MoveValidatorHelper.DetermineMovementType(from, to, board);

            if (movementType == MovementType.Promotion)
            {
                return this.CanPromote(from, to, board);
            }

            var pawnRange = MoveValidatorHelper.GetMovementRange(this.capturePattern.First());
            return this.capturePattern.First() == movementType && pawnRange.Contains(difference) ? MoveValidatorHelper.CheckTileRange(pawnRange, from, to, board, true) : false;
        }

        public override bool IsValidMovement(Tile from, Tile to, ChessBoard board)
        {
            var indexes = MoveValidatorHelper.GetMovementIndexes(from, to, board);
            var difference = MoveValidatorHelper.GetMovementDifference(indexes.fromIndex, indexes.toIndex);
            var movementType = MoveValidatorHelper.DetermineMovementType(from, to, board);

            if (movementType == MovementType.Promotion)
            {
                return this.CanPromote(from, to, board, promoteTo);
            }

            var pawnRange = MoveValidatorHelper.GetMovementRange(this.movePattern.First());
            return this.movePattern.First() == movementType && difference == pawnRange.First() ? MoveValidatorHelper.CheckTileRange(pawnRange, from, to, board, false) : false;

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
            return _promotionValidator.CheckPawnPromotion(from, to, board, promoteTo);
        }
    }
}

