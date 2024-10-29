using ChessAPI.Helpers;
using ChessAPI.Interfaces;
using static ChessAPI.Models.Enums.Color;

namespace ChessAPI.Models.Pieces
{
    public class Bishop : Piece
    {
        public Bishop()
        {
            this.name = "B";
        }
        public override bool IsValidCapture(Tile from, Tile to, Board board)
        {
            var fromLocation = board.playingFieldDictionary.FirstOrDefault((s) => s.Value == from);
            var fromIndex = board.playingFieldDictionary.IndexOfKey(fromLocation.Key);

            var toLocation = board.playingFieldDictionary.FirstOrDefault(s => s.Value == to);
            var toIndex = board.playingFieldDictionary.IndexOfKey(toLocation.Key);
            
            var difference = from.piece.color == PieceColor.White ? toIndex - fromIndex : fromIndex - toIndex;

            int[] bishopRange = [7, 9];
            return MoveValidatorHelper.CheckTileRange(bishopRange, fromIndex, toIndex, difference, board, MovementType.Capture);
        }

        public override bool IsValidMovement(Tile from, Tile to, Board board, MovementType movementType)
        {
            var fromLocation = board.playingFieldDictionary.FirstOrDefault((s) => s.Value == from);
            var fromIndex = board.playingFieldDictionary.IndexOfKey(fromLocation.Key);

            var toLocation = board.playingFieldDictionary.FirstOrDefault(s => s.Value == to);
            var toIndex = board.playingFieldDictionary.IndexOfKey(toLocation.Key);

            var difference = from.piece.color == PieceColor.White ? toIndex - fromIndex : fromIndex - toIndex;

            int[] bishopRange = [7, 9];
            return MoveValidatorHelper.CheckTileRange(bishopRange, fromIndex, toIndex, difference, board, movementType);
        }
    }
}
