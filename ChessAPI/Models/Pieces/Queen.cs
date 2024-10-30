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
            this.capturePattern = this.movePattern;
        }

        public override bool IsValidCapture(Tile from, Tile to, Board board)
        {
            var indexes = MoveValidatorHelper.GetMovementIndexes(from, to, board);
            var difference = MoveValidatorHelper.GetMovementDifference(indexes.fromIndex, indexes.toIndex);

            var movementType = MoveValidatorHelper.GetMovementType(from, to, board);
            var queenRange = MoveValidatorHelper.GetMovementRange(movementType);

            return this.capturePattern.Contains(movementType) ? MoveValidatorHelper.CheckTileRange(queenRange, from, to, board) : false;
        }

        public override bool IsValidMovement(Tile from, Tile to, Board board)
        {
            var indexes = MoveValidatorHelper.GetMovementIndexes(from, to, board);
            var difference = MoveValidatorHelper.GetMovementDifference(indexes.fromIndex, indexes.toIndex);

            var movementType = MoveValidatorHelper.GetMovementType(from, to, board);
            var queenRange = MoveValidatorHelper.GetMovementRange(movementType);

            return this.movePattern.Contains(movementType) ? MoveValidatorHelper.CheckTileRange(queenRange, from, to, board) : false;
        }
    }
}
