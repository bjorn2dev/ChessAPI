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
        Dictionary<Tuple<int, int>, Tile> GenerateBoard();
    }
}
