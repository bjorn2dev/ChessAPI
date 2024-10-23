using ChessAPI.Interfaces;
using System.Security.Cryptography.X509Certificates;
using static ChessAPI.Models.Enums.Color;
using static ChessAPI.Services.PieceMoveValidator;

namespace ChessAPI.Models.Pieces
{
    public class Pawn : Piece
    {
        public Pawn()
        {
            this.name = "P";
            this.movePattern = "^";
        }

        public override bool IsValidCapture(Tile from, Tile to, Board board)
        {
            var fromLocation = board.playingFieldDictionary.FirstOrDefault((s) => s.Value == from);
            var fromIndex = board.playingFieldDictionary.IndexOfKey(fromLocation.Key);

            var toLocation = board.playingFieldDictionary.FirstOrDefault(s => s.Value == to);
            var toIndex = board.playingFieldDictionary.IndexOfKey(toLocation.Key);
            var difference = from.piece.color == PieceColor.White ? toIndex - fromIndex : fromIndex - toIndex;

            // Only allow diagonal moves for capturing
            return to.piece != null && (difference == 7 || difference == 9);
        }

        public override bool IsValidMovement(Tile from, Tile to, Board board, MovementType movementType)
        {
            var fromLocation = board.playingFieldDictionary.FirstOrDefault((s) => s.Value == from);
            var fromIndex = board.playingFieldDictionary.IndexOfKey(fromLocation.Key);

            var toLocation = board.playingFieldDictionary.FirstOrDefault(s => s.Value == to);
            var toIndex = board.playingFieldDictionary.IndexOfKey(toLocation.Key);
            var difference = from.piece.color == PieceColor.White ? toIndex - fromIndex : fromIndex - toIndex;
            
            // normal pawn move forward
            if (to.piece == null)
            {
                return difference == 8;
            }

            return true;
        }
    }
}

