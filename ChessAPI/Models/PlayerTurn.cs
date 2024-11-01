using ChessAPI.Models.Enums;
namespace ChessAPI.Models
{
    public class PlayerTurn
    {
        public User user {  get; set; }
        public Color.PieceColor color { get; set; }
        public Tile fromTile { get; set; }
        public Tile toTile { get; set; }
    }
}
