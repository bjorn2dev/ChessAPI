using ChessAPI.Interfaces;

namespace ChessAPI.Services
{
    public class GameStateService : IGameStateService
    {
        public DateTime GameStartTime { get; private set; }
        public GameStateService()
        {
            
        }

        public TimeSpan TimePerPlayer { get; private set; }

        public void StartGameTimer()
        {
            GameStartTime = DateTime.Now;
        }

        public void SetPlayerTime(int playerTime)
        {
            TimePerPlayer = TimeSpan.FromMinutes(playerTime);
        }
    }
}
