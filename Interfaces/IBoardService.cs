using ChessAPI.Models;
using ChessAPI.Services;

namespace ChessAPI.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBoardService
    {
        string GetBoard();
        string GetBoardDictionary();
    }
}
