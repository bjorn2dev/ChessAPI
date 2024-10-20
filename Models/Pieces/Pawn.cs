using System.Security.Cryptography.X509Certificates;
using static ChessAPI.Models.Enums.Color;

namespace ChessAPI.Models.Pieces
{
    public class Pawn : Piece
    {
        public Pawn()
        {
            this.name = "P";
            this.movePattern = "^";
        }

        public override bool IsValidMove()
        {

        }
    }
}

