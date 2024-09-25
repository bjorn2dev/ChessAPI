using static ChessAPI.Models.Enums.Color;

namespace ChessAPI.Models.Pieces
{
    public class Queen : Piece
    {
        public Queen()
        {
            this.name = "queen";
            this.html = $"<p style=\"background-color:{(this.color == PieceColor.White ? "black" : "white")};color:background-color:{(this.color == PieceColor.White ? "white" : "black")};padding:20px;\">Q</p>";
        }
    }
}
