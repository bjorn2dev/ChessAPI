using ChessAPI.Models;

namespace ChessAPI.Interfaces
{
    public interface IGameStateService
    {
        DateTime GameStartTime { get; }
        TimeSpan TimePerPlayer { get; }
        void StartGameTimer();
        void SetPlayerTime(int playerTime);
    }
}
