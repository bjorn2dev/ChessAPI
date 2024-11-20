using ChessAPI.Helpers;
using static ChessAPI.Models.Enums.Color;

namespace ChessAPI.Models.Pieces
{
    public class Knight : Piece
    {
        public Knight()
        {
            this.name = "<img src=\"https://upload.wikimedia.org/wikipedia/commons/thumb/7/70/Chess_nlt45.svg/1280px-Chess_nlt45.svg.png\" width=\"100\" height=\"100\">";
            this.movePattern = [MovementType.LShaped];
            this.capturePattern = this.movePattern;
        }
        public override bool IsValidCapture(Tile from, Tile to, Board board)
        {
            var indexes = MoveValidatorHelper.GetMovementIndexes(from, to, board);
            var difference = MoveValidatorHelper.GetMovementDifference(indexes.fromIndex, indexes.toIndex);
            var movementType = MoveValidatorHelper.DetermineMovementType(from, to, board);
            var knightRange = MoveValidatorHelper.GetMovementRange(this.capturePattern.First());
            return this.movePattern.First() == movementType ? MoveValidatorHelper.CheckTileRange([difference], from, to, board) : false;
        }

        public override bool IsValidMovement(Tile from, Tile to, Board board)
        {
            var indexes = MoveValidatorHelper.GetMovementIndexes(from, to, board);
            var difference = MoveValidatorHelper.GetMovementDifference(indexes.fromIndex, indexes.toIndex);
            var movementType = MoveValidatorHelper.DetermineMovementType(from, to, board);
            var knightRange = MoveValidatorHelper.GetMovementRange(this.movePattern.First());
            return this.movePattern.First() == movementType ? MoveValidatorHelper.CheckTileRange([difference], from, to, board) : false;
        }

        public override bool IsCheckingKing(Tile from, Tile kingTile, Board board)
        {
            var indexes = MoveValidatorHelper.GetMovementIndexes(from, kingTile, board);
            var difference = MoveValidatorHelper.GetMovementDifference(indexes.fromIndex, indexes.toIndex);
            var movementType = MoveValidatorHelper.DetermineMovementType(from, kingTile, board);

            if (!this.capturePattern.Contains(movementType)) return false;

            var result = false;
            var pieceRangeNumbers = new List<int>();
            foreach (var pattern in this.capturePattern)
            {
                pieceRangeNumbers.AddRange(MoveValidatorHelper.GetMovementRange(pattern));
            }

            result = MoveValidatorHelper.CheckTileRange(pieceRangeNumbers.ToArray(), from, kingTile, board);

            return result;
        }
    }
}
