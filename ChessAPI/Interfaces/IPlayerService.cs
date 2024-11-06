using ChessAPI.Models;
using ChessAPI.Models.Enums;

namespace ChessAPI.Interfaces
{
    public interface IPlayerService
    {
        List<PlayerTurn> PlayerTurns { get; }
        bool PlayersInitialized { get; }
        User? WhitePlayer { get; set; }
        User? BlackPlayer { get; set; }
        User GetPlayerByInfo(string userAgent, string userIp);
        void ConfigurePlayer(Color.PlayerColor playerColor, string userAgent, string userIp);
    }
}
