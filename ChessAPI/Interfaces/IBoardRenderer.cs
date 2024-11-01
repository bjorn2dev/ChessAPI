using ChessAPI.Models;
using Color = ChessAPI.Models.Enums.Color;

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
        string RenderBoard(SortedList<Tuple<int, int>, Tile> board, Color.PlayerColor showBoardForPlayer);
    }
}
