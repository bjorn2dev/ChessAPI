using ChessAPI.Models;

namespace ChessAPI.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBoardRenderer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        string RenderBoard(SortedList<Tuple<int, int>, Tile> board);
    }
}
