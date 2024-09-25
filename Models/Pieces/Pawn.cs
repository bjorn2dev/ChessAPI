using static ChessAPI.Models.Enums.Color;

namespace ChessAPI.Models.Pieces
{
    public class Pawn : Piece
    {
        public Pawn()
        {
            this.name = "pawn";
            this.html = "";
        }
        public override void HasColor()
        {
            this.html = $"<p style=\"background-color:{(this.color == PieceColor.White ? "white" : "black")};color:{(this.color == PieceColor.White ? "black" : "white")};padding:20px;\">P</p>";
        }
    }
}

