using ChessAPI.Models.Enums;

namespace ChessAPI.Interfaces
{
    public interface IGameService
    {
        string GetColorSelector();
        bool IsGameInitialized();
        Color.PlayerColor ShowBoardForPlayerColor(string userAgent, string userIpAddress);
    }
}
