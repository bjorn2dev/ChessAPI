using ChessAPI.Interfaces;
using ChessAPI.Models;
using ChessAPI.Models.Enums;

namespace ChessAPI.Services
{
    public class PlayerService : IPlayerService
    {
        public bool PlayersInitialized { get; private set; }
        public User? WhitePlayer { get; private set; }
        public User? BlackPlayer { get; private set; }
        private void SetWhitePlayer(User user) => this.WhitePlayer = user;
        private void SetBlackPlayer(User user) => this.BlackPlayer = user;
        public bool SameDevice { get; private set; }

        public PlayerService()
        {
        }

        public void SetPlayer(User user)
        {
            if (user.color == Color.PlayerColor.White)
            {
                this.SetWhitePlayer(user);
            }
            else
            {
                this.SetBlackPlayer(user);
            }

            if (this.WhitePlayer != null && this.BlackPlayer != null) 
            {
                if (this.WhitePlayer.userIp == this.BlackPlayer.userIp && this.WhitePlayer.userAgent == this.BlackPlayer.userAgent)
                {
                    this.SameDevice = true;
                }
                this.PlayersInitialized = true;
            }
        }

    }
}
