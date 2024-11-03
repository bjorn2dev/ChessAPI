using ChessAPI.Models;
using ChessAPI.Models.Enums;

namespace ChessAPI.Interfaces
{
    public interface IPlayerSetupService
    {
        User SetupPlayer(Color.PlayerColor playerColor, string userAgent, string userIp);
    }
}
