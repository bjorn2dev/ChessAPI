using ChessAPI.Interfaces;
using ChessAPI.Models;
using ChessAPI.Models.Enums;

namespace ChessAPI.Services
{
    public class PlayerService : IPlayerService
    {
        public bool PlayersInitialized { get; private set; }

        readonly IPlayerSetupService _playerSetupService;
        public User? WhitePlayer { get; set; }
        public User? BlackPlayer { get; set; }
        private void SetWhitePlayer(User user) => WhitePlayer = user;
        private void SetBlackPlayer(User user) => BlackPlayer = user;

        public PlayerService(IPlayerSetupService playerSetupService)
        {
            _playerSetupService = playerSetupService;
        }
        public User GetPlayerByInfo(string userAgent, string userIp)
        {
            return userAgent == this.WhitePlayer.userAgent && userIp == this.WhitePlayer.userIp ? this.WhitePlayer : this.BlackPlayer;
        }

        public bool WhiteAndBlackAreSimilarPlayer()
        {
            return this.WhitePlayer.userAgent == this.BlackPlayer.userAgent && this.WhitePlayer.userIp == this.BlackPlayer.userIp;
        }

        public void ConfigurePlayer(Color.PlayerColor playerColor, string userAgent, string userIp)
        {
            User player = _playerSetupService.SetupPlayer(playerColor, userAgent, userIp);

            if (playerColor == Color.PlayerColor.White)
            {
                this.SetWhitePlayer(player);
            }
            else
            {
                this.SetBlackPlayer(player);
            }

            if (this.WhitePlayer != null && this.BlackPlayer != null) 
            {
                this.PlayersInitialized = true;
            }
        }

    }
}
