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
            this.capturePattern = this.movePattern;
        }

        public override bool IsValidCapture(Tile from, Tile to, Board board)
        {
            var indexes = MoveValidatorHelper.GetMovementIndexes(from, to, board);
            var difference = MoveValidatorHelper.GetMovementDifference(indexes.fromIndex, indexes.toIndex);
            var movementType = MoveValidatorHelper.GetMovementType(from, to, board);
            var rookRange = MoveValidatorHelper.GetMovementRange(movementType);
            return this.capturePattern.Contains(movementType) ? MoveValidatorHelper.CheckTileRange(rookRange, from, to, board) : false;
        }
         
        public override bool IsValidMovement(Tile from, Tile to, Board board)
        {
            var indexes = MoveValidatorHelper.GetMovementIndexes(from, to, board);
            var difference = MoveValidatorHelper.GetMovementDifference(indexes.fromIndex, indexes.toIndex);
            var movementType = MoveValidatorHelper.GetMovementType(from, to, board);
            var rookRange = MoveValidatorHelper.GetMovementRange(movementType);
            return this.movePattern.Contains(movementType) ? MoveValidatorHelper.CheckTileRange(rookRange, from, to, board) : false;
        }
    }
}
