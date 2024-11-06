using ChessAPI.Models.Enums;

namespace ChessAPI.Interfaces
{
    public interface IGameService
    {
        string InitializeGame();

        void MovePiece(string from, string to);
        void RegisterPlayerColor(Color.PlayerColor playerColor, string userAgent, string userIp);
    }
}
