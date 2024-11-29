using ChessAPI.Models;
using ChessAPI.Models.Enums;

namespace ChessAPI.Interfaces.Player
{
    public interface IPlayerService
    {
        bool PlayersInitialized { get; }
        bool SameDevice { get; }
        User? WhitePlayer { get; }
        User? BlackPlayer { get; }
        void SetPlayer(User user);
    }
}
