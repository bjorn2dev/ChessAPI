using ChessAPI.Interfaces;
using ChessAPI.Models;

namespace ChessAPI.Services
{
    public class BoardSimulationService : IBoardSimulationService
    {
        
        public BoardSimulationService()
        { 
            
        }

        public Board SimulateMove(Tile from, Tile to, Board board)
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
