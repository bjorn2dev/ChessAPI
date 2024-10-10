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

        public int moveRadius = 0;
    }
}
