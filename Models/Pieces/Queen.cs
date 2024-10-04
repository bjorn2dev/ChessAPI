using static ChessAPI.Models.Enums.Color;

namespace ChessAPI.Models.Pieces
{
    public class Queen : Piece
    {
        public Queen()
        {
            this.name = "queen";
        }
        public override string RenderHtml(Piece piece)
        {
            return $"<p style=\"background-color:{(this.color == PieceColor.White ? "white" : "black")};color:{(this.color == PieceColor.White ? "black" : "white")};padding:20px;\">Q</p>";
        }
    }
}
