using ChessAPI.Models;
using ChessAPI.Services;

namespace ChessAPI.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBoardService
    {
        void InitializeBoard();
        string GetBoard();
        string GetBoardDictionary();
    }
}
