using ChessAPI.Helpers;
using ChessAPI.Interfaces;
using static ChessAPI.Models.Enums.Color;

namespace ChessAPI.Models.Pieces
{
    public class Bishop : Piece
    {
        public Bishop()
        {
            this.name = "<img src=\"https://upload.wikimedia.org/wikipedia/commons/thumb/b/b1/Chess_blt45.svg/1280px-Chess_blt45.svg.png\" width=\"100\" height=\"100\">";
            this.movePattern = [MovementType.Diagonal];
            this.capturePattern = this.movePattern;
        }
        public override bool IsValidCapture(Tile from, Tile to, Board board)
        {
            var indexes = MoveValidatorHelper.GetMovementIndexes(from, to, board);
            var difference = MoveValidatorHelper.GetMovementDifference(indexes.fromIndex, indexes.toIndex);
            var movementType = MoveValidatorHelper.GetMovementType(from, to, board);

            int[] bishopRange = MoveValidatorHelper.GetMovementRange(this.movePattern.First());
            return this.movePattern.Contains(movementType) ? MoveValidatorHelper.CheckTileRange(bishopRange, from, to, board) : false;
        }

        public override bool IsValidMovement(Tile from, Tile to, Board board)
        {
            var indexes = MoveValidatorHelper.GetMovementIndexes(from, to, board);
            var difference = MoveValidatorHelper.GetMovementDifference(indexes.fromIndex, indexes.toIndex);
            var movementType = MoveValidatorHelper.GetMovementType(from, to, board);

            int[] bishopRange = MoveValidatorHelper.GetMovementRange(this.movePattern.First());

            return this.movePattern.Contains(movementType) ? MoveValidatorHelper.CheckTileRange(bishopRange, from, to, board) : false;
        }
    }
}
