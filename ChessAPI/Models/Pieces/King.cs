using ChessAPI.Helpers;
using ChessAPI.Interfaces;
using ChessAPI.Models.Enums;
using System;
using static ChessAPI.Models.Enums.Color;

namespace ChessAPI.Models.Pieces
{
    public class King : Piece
    {
        private bool _hasMoved = false;
        public override bool AllowsCastling => !this._hasMoved;
        private readonly IKingSafetyValidator _kingSafetyValidator;
        public King(IKingSafetyValidator kingSafetyValidator)
        {
            this.name = "<img src=\"https://upload.wikimedia.org/wikipedia/commons/thumb/4/42/Chess_klt45.svg/1280px-Chess_klt45.svg.png\" width=\"100\" height=\"100\">";
            this.movePattern = [MovementType.Diagonal, MovementType.Horizontal, MovementType.Vertical];
            this.capturePattern = this.movePattern;
            this._kingSafetyValidator = kingSafetyValidator;
        }

        public void MarkAsMoved()
        {
            this._hasMoved = true;
        }

        public override bool IsValidCapture(Tile from, Tile to, Board board)
        {
            var indexes = MoveValidatorHelper.GetMovementIndexes(from, to, board);
            var difference = MoveValidatorHelper.GetMovementDifference(indexes.fromIndex, indexes.toIndex);

            int[] kingRange = [];

            foreach(var movementType in this.capturePattern)
            {
                if (MoveValidatorHelper.GetMovementRange(movementType).Contains(difference))
                {
                    kingRange = MoveValidatorHelper.GetMovementRange(movementType);
                    break;
                }
            }
            // king has multiple ways to move, but can always only move one square, so we make a new int array with the difference found.
            return kingRange.Contains(difference) ? MoveValidatorHelper.CheckTileRange([difference], from, to, board) : false;
        }

        public override bool IsValidMovement(Tile from, Tile to, Board board)
        {

            var indexes = MoveValidatorHelper.GetMovementIndexes(from, to, board);
            var difference = MoveValidatorHelper.GetMovementDifference(indexes.fromIndex, indexes.toIndex);

            int[] kingRange = [];

            foreach (var movementType in this.movePattern)
            {
                if (MoveValidatorHelper.GetMovementRange(movementType).Contains(difference))
                {
                    kingRange = MoveValidatorHelper.GetMovementRange(movementType);
                    break;
                }
            }

            // king has multiple ways to move, but can always only move one square, so we make a new int array with the difference found.
            return kingRange.Contains(difference) ? MoveValidatorHelper.CheckTileRange([difference], from, to, board) : false;
        }

        public bool IsInCheck(Board board)
        {
            var kingTile = board.GetKingTile(this.color);
            return this._kingSafetyValidator.ValidateKingTileSafety(kingTile, board);
        }

        public bool IsSafeToCastle(Board board)
        {
            return true;
        }

        public override bool IsCheckingKing(Tile from, Tile kingTile, Board board)
        {
            return false;
        }
    }
}
