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
            this.name = "<img src=\"https://upload.wikimedia.org/wikipedia/commons/thumb/4/45/Chess_plt45.svg/1280px-Chess_plt45.svg.png\" width=\"100\" height=\"100\">";
            this.movePattern = [MovementType.Vertical];
            this.capturePattern = [MovementType.Diagonal];
        }

        public override bool IsValidCapture(Tile from, Tile to, Board board)
        {
            var indexes = MoveValidatorHelper.GetMovementIndexes(from, to, board);
            var difference = MoveValidatorHelper.GetMovementDifference(indexes.fromIndex, indexes.toIndex);
            var movementType = MoveValidatorHelper.GetMovementType(from, to, board);
            var pawnRange = MoveValidatorHelper.GetMovementRange(this.capturePattern.First());
            return this.capturePattern.First() == movementType ? MoveValidatorHelper.CheckTileRange(pawnRange, from, to, board) : false;

        }

        public override bool IsValidMovement(Tile from, Tile to, Board board)
        {
            var indexes = MoveValidatorHelper.GetMovementIndexes(from, to, board);
            var difference = MoveValidatorHelper.GetMovementDifference(indexes.fromIndex, indexes.toIndex); 
            var movementType = MoveValidatorHelper.GetMovementType(from, to, board);
            var pawnRange = MoveValidatorHelper.GetMovementRange(this.movePattern.First());
            return this.movePattern.First() == movementType ? MoveValidatorHelper.CheckTileRange(pawnRange, from, to, board) : false;

        }

        public bool IsValidEnPassant(Tile from, Tile to, Board board)
        {
            return false;
        }

        public bool CanPromote(Tile from, Tile to, Board board)
        {
            return false;
        }
    }
}

