using ChessAPI.Models;
using ChessAPI.Models.Enums;

namespace ChessAPI.Interfaces
{
    public interface IPlayerService
    {
        List<PlayerTurn> PlayerTurns { get; }
        bool PlayersInitialized { get; }
        bool SameDevice { get; }
        User? WhitePlayer { get; }
        User? BlackPlayer { get; }
        User GetPlayerByInfo(string userAgent, string userIp);
        void ConfigurePlayer(Color.PlayerColor playerColor, string userAgent, string userIp);
        void RecordTurn(PlayerTurn playerTurn);
    }
}
