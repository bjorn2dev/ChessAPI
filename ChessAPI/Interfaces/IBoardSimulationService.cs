using ChessAPI.Models;

namespace ChessAPI.Interfaces
{
    public interface IBoardSimulationService
    {
        Board SimulateMove(Tile from, Tile to, Board board);
    }
}
