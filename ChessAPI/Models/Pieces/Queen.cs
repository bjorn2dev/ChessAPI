using ChessAPI.Helpers;
using ChessAPI.Models.Enums;
using static ChessAPI.Models.Enums.Color;

namespace ChessAPI.Models.Pieces
{
    public class Queen : ChessPiece
    {
        public Queen()
        {
            this.name = "<img src=\"https://upload.wikimedia.org/wikipedia/commons/thumb/1/15/Chess_qlt45.svg/1280px-Chess_qlt45.svg.png\"\" width=\"100\" height=\"100\">";
            this.movePattern = [MovementType.Diagonal, MovementType.Horizontal, MovementType.Vertical];
            this.capturePattern = this.movePattern;
        }

        public override bool IsValidCapture(Tile from, Tile to, ChessBoard board)
        {
            var indexes = MoveValidatorHelper.GetMovementIndexes(from, to, board);
            var difference = MoveValidatorHelper.GetMovementDifference(indexes.fromIndex, indexes.toIndex);

            var movementType = MoveValidatorHelper.DetermineMovementType(from, to, board);
            var queenRange = MoveValidatorHelper.GetMovementRange(movementType);

            return this.capturePattern.Contains(movementType) ? MoveValidatorHelper.CheckTileRange(queenRange, from, to, board, true) : false;
        }

        public override bool IsValidMovement(Tile from, Tile to, ChessBoard board)
        {
            var indexes = MoveValidatorHelper.GetMovementIndexes(from, to, board);
            var difference = MoveValidatorHelper.GetMovementDifference(indexes.fromIndex, indexes.toIndex);

            var movementType = MoveValidatorHelper.DetermineMovementType(from, to, board);
            var queenRange = MoveValidatorHelper.GetMovementRange(movementType);

            return this.movePattern.Contains(movementType) ? MoveValidatorHelper.CheckTileRange(queenRange, from, to, board, false) : false;
        }
    }
}
