using ChessAPI.Interfaces;
using ChessAPI.Models;
using ChessAPI.Models.Enums;
namespace ChessAPI.Services
{
    public class BoardStateService : IBoardStateService
    {
        public List<PlayerTurn> PlayerTurns { get; private set; }
        public Board Board { get; private set; }

        public BoardStateService()
        {
            Board = new Board();
            PlayerTurns = new List<PlayerTurn>();
        }
        public void MovePiece(Tile fromTile, Tile toTile)
        {
            toTile.piece = fromTile.piece;
            fromTile.piece = null;
        }

        public void AddPlayerMove(Tile fromTile, Tile toTile)
        {
            this.PlayerTurns.Add(new PlayerTurn { color = fromTile.piece.color, fromTile = fromTile, toTile = toTile });
        }
    }
}
