using ChessAPI.Models;
using ChessAPI.Services;

namespace ChessAPI.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBoardService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
       string GetBoard();

        string GetBoardDictionary();

        void MovePiece(string from, string to);
    }
}
