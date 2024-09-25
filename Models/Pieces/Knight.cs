using static ChessAPI.Models.Enums.Color;

namespace ChessAPI.Models.Pieces
{
    public class Knight : Piece
    {
        public Knight()
        {
            this.name = "knight";
            this.html = $"<p style=\"background-color:{(this.color == PieceColor.White ? "black" : "white")};color:background-color:{(this.color == PieceColor.White ? "white" : "black")};padding:20px;\">N</p>";
        }
    }
}
