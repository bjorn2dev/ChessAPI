using ChessAPI.Models;

namespace ChessAPI.Interfaces
{
    public interface IBoardStateService
    {
        Board Board { get; }
        List<PlayerTurn> PlayerTurns { get; }
        void MovePiece(Tile fromTile, Tile toTile);
    }
}
