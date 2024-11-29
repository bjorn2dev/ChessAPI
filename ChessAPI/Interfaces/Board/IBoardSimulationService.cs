using ChessAPI.Models;

namespace ChessAPI.Interfaces.Board
{
    public interface IBoardSimulationService
    {
        ChessBoard SimulateMove(Tile from, Tile to, ChessBoard board);
    }
}
