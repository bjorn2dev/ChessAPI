using ChessAPI.Models.Enums;

namespace ChessAPI.Interfaces.Game
{
    public interface IGameService
    {
        string StartGame();

        void MovePiece(string from, string to, string userAgent, string userIpAddress);
        void RegisterPlayerColor(Color.PlayerColor playerColor, string userAgent, string userIp);
    }
}
