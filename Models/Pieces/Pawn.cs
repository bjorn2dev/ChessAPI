using static ChessAPI.Models.Enums.Color;

namespace ChessAPI.Models.Pieces
{
    public class Pawn : Piece
    {
      public Pawn()
        {
            this.name = "pawn";
            this.html = $"<p style=\"background-color:{(this.color == PieceColor.White ? "black" : "white")};color:background-color:{(this.color == PieceColor.White ? "white" : "black")};padding:20px;\">P</p>";
        }
    }
}
