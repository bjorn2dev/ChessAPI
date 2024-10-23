using ChessAPI.Interfaces;
using ChessAPI.Models;

namespace ChessAPI.Services
{
    public class BoardStateService : IBoardStateService
    {

        public Board Board { get; private set; }
        public BoardStateService()
        {
            Board = new Board();
        }
        public void MovePiece(Tile fromTile, Tile toTile)
        {
            toTile.piece = fromTile.piece;
            fromTile.piece = null;
        }
    }
}
