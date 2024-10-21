using ChessAPI.Helpers;
using static ChessAPI.Models.Enums.Color;
using static ChessAPI.Services.PieceMoveValidator;

namespace ChessAPI.Models.Pieces
{
    public class Knight : Piece
    {
        public Knight()
        {
            this.name = "N";
        }
        public override bool IsValidCapture(Tile from, Tile to, Board board)
        {
            var fromLocation = board.playingFieldDictionary.FirstOrDefault((s) => s.Value == from);
            var fromIndex = board.playingFieldDictionary.IndexOfKey(fromLocation.Key);

            var toLocation = board.playingFieldDictionary.FirstOrDefault(s => s.Value == to);
            var toIndex = board.playingFieldDictionary.IndexOfKey(toLocation.Key);

            var difference = from.piece.color == PieceColor.White ? toIndex - fromIndex : fromIndex - toIndex;

            int[] knightRange = [5, 6, 10, 11, 15, 17];
            return MoveValidatorHelper.CheckTileRange(knightRange, fromIndex, toIndex, difference, board, MovementType.Capture);
        }

        public override bool IsValidMovement(Tile from, Tile to, Board board, MovementType movementType)
        {
            var fromLocation = board.playingFieldDictionary.FirstOrDefault((s) => s.Value == from);
            var fromIndex = board.playingFieldDictionary.IndexOfKey(fromLocation.Key);

            var toLocation = board.playingFieldDictionary.FirstOrDefault(s => s.Value == to);
            var toIndex = board.playingFieldDictionary.IndexOfKey(toLocation.Key);

            var difference = from.piece.color == PieceColor.White ? toIndex - fromIndex : fromIndex - toIndex; ;

            int[] knightRange = [5, 6, 10, 11, 15, 17];
            return
                 knightRange.Contains(difference) ?
                 MoveValidatorHelper.CheckTileRange([difference], fromIndex, toIndex, difference, board, movementType) :
                 false;
        }
    }
}
