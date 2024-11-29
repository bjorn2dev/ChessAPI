using ChessAPI.Models;
using ChessAPI.Services;
using Color = ChessAPI.Models.Enums.Color;

namespace ChessAPI.Interfaces.Board
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBoardService
    {
        bool BoardInitialized { get; set; }
        void InitializeBoard();
        string GetBoard(Color.PlayerColor playerColor = Color.PlayerColor.White);
    }
}
