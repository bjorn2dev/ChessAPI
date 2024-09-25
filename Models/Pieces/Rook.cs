using static ChessAPI.Models.Enums.Color;

namespace ChessAPI.Models.Pieces
{
    public class Rook : Piece
    {
        public Rook()
        {
            this.name = "rook";
            this.html = $"<p style=\"background-color:{(this.color == PieceColor.White ? "black" : "white")};color:background-color:{(this.color == PieceColor.White ? "white" : "black")};padding:20px;\">R</p>";
        }
    }
}
