using ChessAPI.Models.Enums;
namespace ChessAPI.Models
{
    public class PlayerTurn
    {
        public Color.PieceColor color { get; set; }
        public Tile fromTile { get; set; }
        public Tile toTile { get; set; }
    }
}
