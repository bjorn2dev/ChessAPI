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
        private void SetWhitePlayer(User user) => this. WhitePlayer = user;
        private void SetBlackPlayer(User user) => this.BlackPlayer = user;
        public List<PlayerTurn> PlayerTurns { get; private set; }

        public PlayerService(IPlayerSetupService playerSetupService)
        {
            PlayerTurns = new List<PlayerTurn>();
            this._playerSetupService = playerSetupService;
        }

        public User GetPlayerByInfo(string userAgent, string userIp)
        {
            return userAgent == this.WhitePlayer.userAgent && userIp == this.WhitePlayer.userIp ? this.WhitePlayer : this.BlackPlayer;
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
