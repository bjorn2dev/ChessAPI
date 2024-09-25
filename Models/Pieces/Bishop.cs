using static ChessAPI.Models.Enums.Color;

namespace ChessAPI.Models.Pieces
{
    public class Bishop : Piece
    {
        public Bishop()
        {
            this.name = "bishop";
            this.html = $"<p style=\"background-color:{(this.color == PieceColor.White ? "black" : "white")}black;color:background-color:{(this.color == PieceColor.White ? "white" : "black")};padding:20px;\">B</p>";
        }
    }
}
