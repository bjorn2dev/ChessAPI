using ChessAPI.Interfaces;
using ChessAPI.Models;
using ChessAPI.Models.Enums;

namespace ChessAPI.Services
{
    public class PlayerSetupService : IPlayerSetupService
    {
        public PlayerSetupService()
        {
        }
        public User SetupPlayer(Color.PlayerColor playerColor, string userAgent, string userIp)
        {
            return new User { color = playerColor, userAgent = userAgent, userIp = userIp };
        }

    }
}
