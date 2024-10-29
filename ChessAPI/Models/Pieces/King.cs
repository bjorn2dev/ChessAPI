using ChessAPI.Helpers;
using System;
using static ChessAPI.Models.Enums.Color;

namespace ChessAPI.Models.Pieces
{
    public class King : Piece
    {
        public King()
        {
            this.name = "K";
        }

        public override bool IsValidCapture(Tile from, Tile to, Board board)
        {
            int[] kingRange = [1, 7, 8, 9];
            var fromLocation = board.playingFieldDictionary.FirstOrDefault((s) => s.Value == from);
            var fromIndex = board.playingFieldDictionary.IndexOfKey(fromLocation.Key);

            var toLocation = board.playingFieldDictionary.FirstOrDefault(s => s.Value == to);
            var toIndex = board.playingFieldDictionary.IndexOfKey(toLocation.Key);
            var difference = from.piece.color == PieceColor.White ? toIndex - fromIndex : fromIndex - toIndex;

            // king has multiple ways to move, but can always only move one square, so we make a new int array with the difference found.
            return
                kingRange.Contains(difference) ?
                MoveValidatorHelper.CheckTileRange([difference], fromIndex, toIndex, difference, board, MovementType.Capture) :
                false;
        }

        public override bool IsValidMovement(Tile from, Tile to, Board board, MovementType movementType)
        {

            int[] kingRange = [1, 7, 8, 9];
            var fromLocation = board.playingFieldDictionary.FirstOrDefault((s) => s.Value == from);
            var fromIndex = board.playingFieldDictionary.IndexOfKey(fromLocation.Key);

            var toLocation = board.playingFieldDictionary.FirstOrDefault(s => s.Value == to);
            var toIndex = board.playingFieldDictionary.IndexOfKey(toLocation.Key);
            var difference = from.piece.color == PieceColor.White ? toIndex - fromIndex : fromIndex - toIndex;
            
            // king has multiple ways to move, but can always only move one square, so we make a new int array with the difference found.
            return
                kingRange.Contains(difference) ? 
                MoveValidatorHelper.CheckTileRange([difference], fromIndex, toIndex, difference, board, movementType) : 
                false;
        }
    }
}
