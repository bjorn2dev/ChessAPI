using ChessAPI.Helpers;
using ChessAPI.Models.Enums;
using System.Drawing;
using static ChessAPI.Models.Enums.Color;

namespace ChessAPI.Models
{
    public class ChessPiece
    {
        public virtual PieceColor color { get; set; }

        public virtual string boardLocation { get; set; }

        public virtual string name { get; set; }

        public virtual bool AllowsCastling => false;
        public virtual MovementType[] movePattern { get; set; }
        public virtual MovementType[] capturePattern { get; set; }

        public virtual bool IsValidMovement(Tile from, Tile to, ChessBoard board) { return false; }

        public virtual bool IsValidCapture(Tile from, Tile to, ChessBoard board) { return false; }
        public virtual bool IsCheckingKing(Tile from, Tile kingTile, ChessBoard board) {
            var indexes = MoveValidatorHelper.GetMovementIndexes(from, kingTile, board);
            var difference = MoveValidatorHelper.GetMovementDifference(indexes.fromIndex, indexes.toIndex);
            var movementType = MoveValidatorHelper.DetermineMovementType(from, kingTile, board);

            if (!this.capturePattern.Contains(movementType)) return false;

            var result = false;
            foreach (var step in MoveValidatorHelper.GetMovementRange(movementType))
            {
                if (difference % step == 0)
                {
                    return MoveValidatorHelper.CheckPath(indexes.fromIndex, indexes.toIndex, step, board, movementType, true);
                }
            }
            return result;

        }

        public virtual ChessPiece Clone()
        {
            // create a shallow copy of the current object and return it
            return (ChessPiece)MemberwiseClone();
        }
    }
}
