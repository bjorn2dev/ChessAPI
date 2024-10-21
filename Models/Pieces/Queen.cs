using ChessAPI.Helpers;
using static ChessAPI.Models.Enums.Color;

namespace ChessAPI.Models.Pieces
{
    public class Queen : Piece
    {
        public Queen()
        {
            this.name = "Q";
        }

        public override bool IsValidCapture(Tile from, Tile to, Board board)
        {
            int[] queenRange = [1, 7, 8, 9]; // Queen moves in all directions
            var fromLocation = board.playingFieldDictionary.FirstOrDefault((s) => s.Value == from);
            var fromIndex = board.playingFieldDictionary.IndexOfKey(fromLocation.Key);

            var toLocation = board.playingFieldDictionary.FirstOrDefault(s => s.Value == to);
            var toIndex = board.playingFieldDictionary.IndexOfKey(toLocation.Key);
            var difference = from.piece.color == PieceColor.White ? toIndex - fromIndex : fromIndex - toIndex;

            var movementType = MoveValidatorHelper.GetMovementType(from, to, board);
            // we only need to check the blocks up and down 
            // we only need to check the blocks exactly next to us until the to position has been reached.
            queenRange = difference > 9 ? [7, 8, 9] : movementType == MovementType.Horizontal ? [1] : [8];

            return
                queenRange.Any(q => difference % q == 0) ?
                MoveValidatorHelper.CheckTileRange(queenRange, fromIndex, toIndex, difference, board, movementType) :
                false;
        }

        public override bool IsValidMovement(Tile from, Tile to, Board board, MovementType movementType)
        {

            int[] queenRange = [1, 7, 8, 9]; // Queen moves in all directions
            var fromLocation = board.playingFieldDictionary.FirstOrDefault((s) => s.Value == from);
            var fromIndex = board.playingFieldDictionary.IndexOfKey(fromLocation.Key);

            var toLocation = board.playingFieldDictionary.FirstOrDefault(s => s.Value == to);
            var toIndex = board.playingFieldDictionary.IndexOfKey(toLocation.Key);
            var difference = from.piece.color == PieceColor.White ? toIndex - fromIndex : fromIndex - toIndex;

            // we only need to check the blocks up and down 
            // we only need to check the blocks exactly next to us until the to position has been reached.
            queenRange = difference > 9 ? [7, 8, 9] : movementType == MovementType.Horizontal ? [1] : [8];
            
            return
                queenRange.Any(q => difference % q == 0) ?
                MoveValidatorHelper.CheckTileRange(queenRange, fromIndex, toIndex, difference, board, movementType) :
                false;
        }
    }
}
