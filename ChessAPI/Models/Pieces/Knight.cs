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

            int[] knightRange = MoveValidatorHelper.GetMovementRange(this.movePattern.First());
            if (!this.movePattern.Contains(movementType))
            {
                return false;
            }
            return MoveValidatorHelper.CheckTileRange([difference], from, to, board);
        }

        public override bool IsValidMovement(Tile from, Tile to, Board board)
        {
            var indexes = MoveValidatorHelper.GetMovementIndexes(from, to, board);
            var difference = MoveValidatorHelper.GetMovementDifference(indexes.fromIndex, indexes.toIndex);
            var movementType = MoveValidatorHelper.GetMovementType(from, to, board);

            int[] knightRange = MoveValidatorHelper.GetMovementRange(this.movePattern.First());
            if (!this.movePattern.Contains(movementType))
            {
                return false;
            }
            return
                 knightRange.Contains(difference) ?
                 MoveValidatorHelper.CheckTileRange([difference], from, to, board) :
                 false;
        }
    }
}
