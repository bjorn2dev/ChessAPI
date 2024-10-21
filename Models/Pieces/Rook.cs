using ChessAPI.Helpers;
using static ChessAPI.Models.Enums.Color;
using static ChessAPI.Services.PieceMoveValidator;

namespace ChessAPI.Models.Pieces
{
    public class Rook : Piece
    {
        public Rook()
        {
            this.name = "R";
        }

        public override bool IsValidCapture(Tile from, Tile to, Board board)
        {
            var fromLocation = board.playingFieldDictionary.FirstOrDefault((s) => s.Value == from);
            var fromIndex = board.playingFieldDictionary.IndexOfKey(fromLocation.Key);

            var toLocation = board.playingFieldDictionary.FirstOrDefault(s => s.Value == to);
            var toIndex = board.playingFieldDictionary.IndexOfKey(toLocation.Key);

            var difference = from.piece.color == PieceColor.White ? toIndex - fromIndex : fromIndex - toIndex;
           
            var movementType = MoveValidatorHelper.GetMovementType(from, to, board);
            int[] rookRange = movementType == MovementType.Horizontal ? [8] : [1];    

            return MoveValidatorHelper.CheckTileRange(rookRange, fromIndex, toIndex, difference, board, MovementType.Capture);
        }
         
        public override bool IsValidMovement(Tile from, Tile to, Board board, MovementType movementType)
        {

            var fromLocation = board.playingFieldDictionary.FirstOrDefault((s) => s.Value == from);
            var fromIndex = board.playingFieldDictionary.IndexOfKey(fromLocation.Key);

            var toLocation = board.playingFieldDictionary.FirstOrDefault(s => s.Value == to);
            var toIndex = board.playingFieldDictionary.IndexOfKey(toLocation.Key);

            var difference = from.piece.color == PieceColor.White ? toIndex - fromIndex : fromIndex - toIndex;

            int[] rookRange = movementType == MovementType.Horizontal ? [8] : [1];

            return MoveValidatorHelper.CheckTileRange(rookRange, fromIndex, toIndex, difference, board, movementType);
        }
    }
}
