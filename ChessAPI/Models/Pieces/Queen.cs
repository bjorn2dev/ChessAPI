using ChessAPI.Helpers;
using static ChessAPI.Models.Enums.Color;

namespace ChessAPI.Models.Pieces
{
    public class Queen : Piece
    {
        public Queen()
        {
            this.name = "Q";
            this.movePattern = [MovementType.Diagonal, MovementType.Horizontal, MovementType.Vertical];
        }

        public override bool IsValidCapture(Tile from, Tile to, Board board)
        {
            int[] queenRange = [1, 7, 8, 9]; // Queen moves in all directions
            var indexes = MoveValidatorHelper.GetMovementIndexes(from, to, board);
            var difference = MoveValidatorHelper.GetMovementDifference(indexes.fromIndex, indexes.toIndex);

            var movementType = MoveValidatorHelper.GetMovementType(from, to, board);
            // we only need to check the blocks up and down 
            // we only need to check the blocks exactly next to us until the to position has been reached.
            queenRange = difference > 9 ? [7, 8, 9] : movementType == MovementType.Horizontal ? [1] : [8];

            return
                queenRange.Any(q => difference % q == 0) ?
                MoveValidatorHelper.CheckTileRange(queenRange, from, to, board) :
                false;
        }

        public override bool IsValidMovement(Tile from, Tile to, Board board)
        {

            int[] queenRange = [1, 7, 8, 9]; // Queen moves in all directions
            var indexes = MoveValidatorHelper.GetMovementIndexes(from, to, board);
            var difference = MoveValidatorHelper.GetMovementDifference(indexes.fromIndex, indexes.toIndex);

            var movementType = MoveValidatorHelper.GetMovementType(from, to, board);
            // we only need to check the blocks up and down 
            // we only need to check the blocks exactly next to us until the to position has been reached.
            queenRange = difference > 9 ? [7, 8, 9] : movementType == MovementType.Horizontal ? [1] : [8];
            
            return
                queenRange.Any(q => difference % q == 0) ?
                MoveValidatorHelper.CheckTileRange(queenRange, from, to, board) :
                false;
        }
    }
}
