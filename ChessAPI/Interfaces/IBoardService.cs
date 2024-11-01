using ChessAPI.Models;
using ChessAPI.Services;
using Color = ChessAPI.Models.Enums.Color;

namespace ChessAPI.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBoardService
    {
        void InitializeBoard();
        string GetBoard(Color.PlayerColor playerColor = Color.PlayerColor.White);
        Board GetBoard();
    }
}
