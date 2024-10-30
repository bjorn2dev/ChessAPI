using ChessAPI.Helpers;
using System;
using static ChessAPI.Models.Enums.Color;

namespace ChessAPI.Models.Pieces
{
    public class King : Piece
    {
        public King()
        {
            this.name = "K";
            this.movePattern = [MovementType.Diagonal, MovementType.Horizontal, MovementType.Vertical];
            this.capturePattern = this.movePattern;
        }

        public override bool IsValidCapture(Tile from, Tile to, Board board)
        {
            var indexes = MoveValidatorHelper.GetMovementIndexes(from, to, board);
            var difference = MoveValidatorHelper.GetMovementDifference(indexes.fromIndex, indexes.toIndex);

            int[] kingRange = [];

            foreach(var movementType in this.movePattern)
            {
                if (MoveValidatorHelper.GetMovementRange(movementType).Contains(difference))
                {
                    kingRange = MoveValidatorHelper.GetMovementRange(movementType);
                    break;
                }
            }

            // king has multiple ways to move, but can always only move one square, so we make a new int array with the difference found.
            return
                kingRange.Contains(difference) ?
                MoveValidatorHelper.CheckTileRange([difference], from, to, board) :
                false;
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
            return
                kingRange.Contains(difference) ? 
                MoveValidatorHelper.CheckTileRange([difference], from, to, board) : 
                false;
        }
    }
}
