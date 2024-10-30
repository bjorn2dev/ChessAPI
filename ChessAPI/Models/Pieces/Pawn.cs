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
            this.capturePattern = [MovementType.Diagonal];
        }

        public override bool IsValidCapture(Tile from, Tile to, Board board)
        {
            var indexes = MoveValidatorHelper.GetMovementIndexes(from, to, board);
            var difference = MoveValidatorHelper.GetMovementDifference(indexes.fromIndex, indexes.toIndex);// TODO check piece color?
            var movementType = MoveValidatorHelper.GetMovementType(from, to, board);
            var pawnRange = MoveValidatorHelper.GetMovementRange(movementType);
            // Only allow diagonal moves for capturing
            return this.capturePattern.Contains(movementType) ? MoveValidatorHelper.CheckTileRange(pawnRange, from, to, board) : false;

        }

        public override bool IsValidMovement(Tile from, Tile to, Board board)
        {
            var indexes = MoveValidatorHelper.GetMovementIndexes(from, to, board);
            var difference = MoveValidatorHelper.GetMovementDifference(indexes.fromIndex, indexes.toIndex); // TODO check piece color?
            var movementType = MoveValidatorHelper.GetMovementType(from, to, board);
            var pawnRange = MoveValidatorHelper.GetMovementRange(movementType);
            // Only allow diagonal moves for capturing
            return this.movePattern.Contains(movementType) ? MoveValidatorHelper.CheckTileRange(pawnRange, from, to, board) : false;

        }
    }
}

