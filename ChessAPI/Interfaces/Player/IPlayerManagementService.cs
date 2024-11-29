using ChessAPI.Models;
using ChessAPI.Models.Enums;

namespace ChessAPI.Interfaces.Player
{
    public interface IPlayerManagementService
    {
        void RegisterPlayerColor(Color.PlayerColor playerColor, string userAgent, string userIp);
        bool ArePlayersRegistered();
        User GetPlayerByInfo(string userAgent, string userIp);
        List<Color.PlayerColor> GetUnregisteredPlayers();
    }
}
