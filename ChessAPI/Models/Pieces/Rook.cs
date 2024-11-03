using ChessAPI.Helpers;
using System;
using static ChessAPI.Models.Enums.Color;

namespace ChessAPI.Models.Pieces
{
    public class Rook : Piece
    {
        public Rook()
        {
            this.name = "<img src=\"https://upload.wikimedia.org/wikipedia/commons/thumb/7/72/Chess_rlt45.svg/1280px-Chess_rlt45.svg.png\" width=\"100\" height=\"100\">";
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
