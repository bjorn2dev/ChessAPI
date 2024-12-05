using ChessAPI.Helpers;
using ChessAPI.Models.Enums;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System;
using static ChessAPI.Models.Enums.Color;

namespace ChessAPI.Models.Pieces
{
    public class Rook : ChessPiece
    {
        private bool _hasMoved = false;       
        public override bool AllowsCastling => !this._hasMoved;
        public Rook()
        {
            this.name = "<img src=\"https://upload.wikimedia.org/wikipedia/commons/thumb/7/72/Chess_rlt45.svg/1280px-Chess_rlt45.svg.png\" width=\"100\" height=\"100\" data-name=\"rook\">";
            this.movePattern = [MovementType.Vertical, MovementType.Horizontal];
            this.capturePattern = this.movePattern;
        }

        public void MarkAsMoved()
        {
            this._hasMoved = true;
        }

        public override bool IsValidCapture(Tile from, Tile to, ChessBoard board)
        {
            var indexes = MoveValidatorHelper.GetMovementIndexes(from, to, board);
            var difference = MoveValidatorHelper.GetMovementDifference(indexes.fromIndex, indexes.toIndex);
            var movementType = MoveValidatorHelper.DetermineMovementType(from, to, board);
            var rookRange = MoveValidatorHelper.GetMovementRange(movementType);
            return this.capturePattern.Contains(movementType) ? MoveValidatorHelper.CheckTileRange(rookRange, from, to, board, true) : false;
        }
         
        public override bool IsValidMovement(Tile from, Tile to, ChessBoard board)
        {
            var indexes = MoveValidatorHelper.GetMovementIndexes(from, to, board);
            var difference = MoveValidatorHelper.GetMovementDifference(indexes.fromIndex, indexes.toIndex);
            var movementType = MoveValidatorHelper.DetermineMovementType(from, to, board);
            var rookRange = MoveValidatorHelper.GetMovementRange(movementType);
            if (this.movePattern.Contains(movementType))
            {
                if(MoveValidatorHelper.CheckTileRange(rookRange, from, to, board, false))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
