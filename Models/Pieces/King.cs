using static ChessAPI.Models.Enums.Color;

namespace ChessAPI.Models.Pieces
{
    public class King : Piece
    {
        public King()
        {
            this.name = "king";
            this.html = $"<p style=\"background-color:{(this.color == PieceColor.White ? "black" : "white")};color:background-color:{(this.color == PieceColor.White ? "white" : "black")};padding:20px;\">K</p>";
        }
    }
}
