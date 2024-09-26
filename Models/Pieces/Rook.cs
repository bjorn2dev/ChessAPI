using static ChessAPI.Models.Enums.Color;

namespace ChessAPI.Models.Pieces
{
    public class Rook : Piece
    {
        public Rook()
        {
            this.name = "rook";
        }

        public override void HasColor()
        {
            this.html = $"<p style=\"background-color:{(this.color == PieceColor.White ? "white" : "black")};color:{(this.color == PieceColor.White ? "black" : "white")};padding:20px;\">R</p>";
        }
    }
}
