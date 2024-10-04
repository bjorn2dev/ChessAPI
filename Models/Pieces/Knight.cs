using static ChessAPI.Models.Enums.Color;

namespace ChessAPI.Models.Pieces
{
    public class Knight : Piece
    {
        public Knight()
        {
            this.name = "knight";
        }
        public override string RenderHtml(Piece piece)
        {
            return $"<p style=\"background-color:{(this.color == PieceColor.White ? "white" : "black")};color:{(this.color == PieceColor.White ? "black" : "white")};padding:20px;\">N</p>";
        }
       
    }
}
