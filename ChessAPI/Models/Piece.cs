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

        public string movePattern = string.Empty;

        public virtual bool IsValidMovement(Tile from, Tile to, Board board, MovementType movementType) { return false; }

        public virtual bool IsValidCapture(Tile from, Tile to, Board board) { return false; }
    }
}
