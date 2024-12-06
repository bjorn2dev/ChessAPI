using ChessAPI.Interfaces.Player;
using ChessAPI.Models;
using ChessAPI.Models.Enums;

namespace ChessAPI.Services.Player
{
    public class PlayerService : IPlayerService
    {
        public bool PlayersInitialized { get; private set; }
        public User? WhitePlayer { get; private set; }
        public User? BlackPlayer { get; private set; }
        private void SetWhitePlayer(User user) => WhitePlayer = user;
        private void SetBlackPlayer(User user) => BlackPlayer = user;
        public bool SameDevice { get; private set; }

        public PlayerService()
        {
        }

        public void SetPlayer(User user)
        {
            if (user.color == Color.PlayerColor.White)
            {
                SetWhitePlayer(user);
            }
            else
            {
                SetBlackPlayer(user);
            }

            if (WhitePlayer != null && BlackPlayer != null)
            {
                if (WhitePlayer.userIp == BlackPlayer.userIp && WhitePlayer.userAgent == BlackPlayer.userAgent)
                {
                    AdjustSameDevicePlayers();
                }
                PlayersInitialized = true;
            }
        }

        private void AdjustSameDevicePlayers()
        {
            SameDevice = true;
            WhitePlayer.userAgent = $"{WhitePlayer.userAgent}_white";
            WhitePlayer.userIp = $"{WhitePlayer.userIp}_white";
            BlackPlayer.userAgent = $"{BlackPlayer.userAgent}_black";
            BlackPlayer.userIp = $"{BlackPlayer.userIp}_black";
        }

    }
}
