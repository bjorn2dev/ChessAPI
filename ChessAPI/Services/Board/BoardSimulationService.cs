using ChessAPI.Interfaces.Board;
using ChessAPI.Models;

namespace ChessAPI.Services.Board
{
    public class BoardSimulationService : IBoardSimulationService
    {

        public BoardSimulationService()
        {

        }

        public ChessBoard SimulateMove(Tile from, Tile to, ChessBoard board)
        {
            var simulatedBoard = board.Clone();
            var simulatedFromTile = simulatedBoard.GetTileByRankAndFileNumber(from.rank, from.fileNumber);
            var simulatedToTile = simulatedBoard.GetTileByRankAndFileNumber(to.rank, to.fileNumber);

            simulatedToTile.piece = simulatedFromTile.piece;
            simulatedFromTile.piece = null;

            return simulatedBoard;
        }
    }
}
