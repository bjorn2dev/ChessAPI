using ChessAPI.Helpers;
using static ChessAPI.Models.Enums.Color;

namespace ChessAPI.Models.Pieces
{
    public class Knight : Piece
    {
        public Knight()
        {
            this.name = "N";
            this.movePattern = [MovementType.LShaped];
            this.capturePattern = this.movePattern;
        }
        public override bool IsValidCapture(Tile from, Tile to, Board board)
        {
            var indexes = MoveValidatorHelper.GetMovementIndexes(from, to, board);
            var difference = MoveValidatorHelper.GetMovementDifference(indexes.fromIndex, indexes.toIndex);
            var movementType = MoveValidatorHelper.GetMovementType(from, to, board);
            var knightRange = MoveValidatorHelper.GetMovementRange(this.capturePattern.First());
            return this.movePattern.First() == movementType ? MoveValidatorHelper.CheckTileRange([difference], from, to, board) : false;
        }

        public override bool IsValidMovement(Tile from, Tile to, Board board)
        {

            var indexes = MoveValidatorHelper.GetMovementIndexes(from, to, board);
            var difference = MoveValidatorHelper.GetMovementDifference(indexes.fromIndex, indexes.toIndex);
            var movementType = MoveValidatorHelper.GetMovementType(from, to, board);
            var knightRange = MoveValidatorHelper.GetMovementRange(this.movePattern.First());
            return this.movePattern.First() == movementType ? MoveValidatorHelper.CheckTileRange([difference], from, to, board) : false;

        }
    }
}
