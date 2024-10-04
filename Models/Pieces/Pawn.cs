using static ChessAPI.Models.Enums.Color;

namespace ChessAPI.Models.Pieces
{
    public class Pawn : Piece
    {
        public Pawn()
        {
            this.name = "pawn";
        }

        public override string RenderHtml(Piece piece)
        {
            return $"<p style=\"background-color:{(this.color == PieceColor.White ? "white" : "black")};color:{(this.color == PieceColor.White ? "black" : "white")};padding:20px;\">P</p>";
        }
    }
}

