using static ChessAPI.Models.Enums.Color;

namespace ChessAPI.Models.Pieces
{
    public class Pawn : Piece
    {
        public Pawn()
        {
            this.name = "P";
            this.movePattern = "^";
            this.moveRadius = 8;
        }
    }
}

