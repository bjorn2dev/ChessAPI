using ChessAPI.Helpers;
using ChessAPI.Interfaces;
using static ChessAPI.Models.Enums.Color;

namespace ChessAPI.Models.Pieces
{
    public class Bishop : Piece
    {
        public Bishop()
        {
            this.name = "B";
            this.movePattern = [MovementType.Diagonal];
        }
        public override bool IsValidCapture(Tile from, Tile to, Board board)
        {
            var indexes = MoveValidatorHelper.GetMovementIndexes(from, to, board);
            var difference = MoveValidatorHelper.GetMovementDifference(indexes.fromIndex, indexes.toIndex);

            int[] bishopRange = MoveValidatorHelper.GetMovementRange(this.movePattern.First());
            return MoveValidatorHelper.CheckTileRange(bishopRange, from, to, board);
        }

        public override bool IsValidMovement(Tile from, Tile to, Board board)
        {
            var indexes = MoveValidatorHelper.GetMovementIndexes(from, to, board);
            var difference = MoveValidatorHelper.GetMovementDifference(indexes.fromIndex, indexes.toIndex);
            var movementType = MoveValidatorHelper.GetMovementType(from, to, board);

            int[] bishopRange = MoveValidatorHelper.GetMovementRange(this.movePattern.First());
            if (!this.movePattern.Contains(movementType))
            {
                return false;
            }
            return MoveValidatorHelper.CheckTileRange(bishopRange, from, to, board);
        }
    }
}
