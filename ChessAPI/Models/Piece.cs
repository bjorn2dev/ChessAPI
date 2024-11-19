using ChessAPI.Models;
using ChessAPI.Models.Enums;
using System.Drawing;
using static ChessAPI.Models.Enums.Color;

namespace ChessAPI.Models
{
    public class Piece
    {
        public PieceColor color { get; set; }

        public string boardLocation { get; set; }

        public string name = string.Empty;

        public MovementType[] movePattern { get; set; }
        public MovementType[] capturePattern { get; set; }

        public virtual bool IsValidMovement(Tile from, Tile to, Board board) { return false; }

        public virtual bool IsValidCapture(Tile from, Tile to, Board board) { return false; }
        public virtual bool IsCheckingKing(Tile from, Tile to, Board board) { return false; }

        public virtual Piece Clone()
        {
            // create a shallow copy of the current object and return it
            return (Piece)MemberwiseClone();
        }
    }
}
