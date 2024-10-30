using ChessAPI.Helpers;
using System;
using static ChessAPI.Models.Enums.Color;

namespace ChessAPI.Models.Pieces
{
    public class Rook : Piece
    {
        public Rook()
        {
            this.name = "R";
            this.movePattern = [MovementType.Vertical, MovementType.Horizontal];
        }

        public override bool IsValidCapture(Tile from, Tile to, Board board)
        {
            var indexes = MoveValidatorHelper.GetMovementIndexes(from, to, board);
            var difference = MoveValidatorHelper.GetMovementDifference(indexes.fromIndex, indexes.toIndex);

            var movementType = MoveValidatorHelper.GetMovementType(from, to, board);
            int[] rookRange = MoveValidatorHelper.GetMovementRange(movementType);
            if (!this.movePattern.Contains(movementType))
            {
                return false;
            }
            return MoveValidatorHelper.CheckTileRange(rookRange, from, to, board);
        }
         
        public override bool IsValidMovement(Tile from, Tile to, Board board)
        {
            var indexes = MoveValidatorHelper.GetMovementIndexes(from, to, board);
            var difference = MoveValidatorHelper.GetMovementDifference(indexes.fromIndex, indexes.toIndex);
            
            var movementType = MoveValidatorHelper.GetMovementType(from, to, board);
            int[] rookRange = MoveValidatorHelper.GetMovementRange(movementType);
            if (!this.movePattern.Contains(movementType))
            {
                return false;
            }
            return MoveValidatorHelper.CheckTileRange(rookRange, from, to, board);
        }
    }
}
