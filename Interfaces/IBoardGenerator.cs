using ChessAPI.Models;

namespace ChessAPI.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBoardGenerator
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        void SetupBoard(Dictionary<Tuple<int, int>, Tile> board);
        void AddInitialPieces(Dictionary<Tuple<int, int>, Tile> board);
    }
}
