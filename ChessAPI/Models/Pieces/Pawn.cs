using ChessAPI.Helpers;
using ChessAPI.Interfaces;
using System.Security.Cryptography.X509Certificates;
using static ChessAPI.Models.Enums.Color;

namespace ChessAPI.Models.Pieces
{
    public class Pawn : Piece
    {
        public Pawn()
        {
            this.name = "P";
            this.movePattern = [MovementType.Vertical];
        }

        public override bool IsValidCapture(Tile from, Tile to, Board board)
        {
            var indexes = MoveValidatorHelper.GetMovementIndexes(from, to, board);
            var difference = MoveValidatorHelper.GetMovementDifference(indexes.fromIndex, indexes.toIndex);// TODO check piece color?

            // Only allow diagonal moves for capturing
            return to.piece != null && (difference == 7 || difference == 9);
        }

        public override bool IsValidMovement(Tile from, Tile to, Board board)
        {
            var indexes = MoveValidatorHelper.GetMovementIndexes(from, to, board);
            var difference = MoveValidatorHelper.GetMovementDifference(indexes.fromIndex, indexes.toIndex); // TODO check piece color?

            // normal pawn move forward
            if (to.piece == null)
            {
                return difference == 8;

            }

            return false;
        }
    }
}

