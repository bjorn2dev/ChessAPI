using ChessAPI.Interfaces;
using ChessAPI.Models;
using ChessAPI.Models.Enums;
using static ChessAPI.Models.Enums.Color;

namespace ChessAPI.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerSetupService _playerSetupService;
        public bool PlayersInitialized { get; private set; }

        public User? WhitePlayer { get; set; }
        public User? BlackPlayer { get; set; }

        private void SetPlayer(User user)
        {
            switch (user.color)
            {
                case PlayerColor.White:
                    this.WhitePlayer = user;
                    break;
                case PlayerColor.Black:
                    this.BlackPlayer = user;
                    break;
                default:
                    break;
            }

            if (this.WhitePlayer != null && this.BlackPlayer != null)
            {
                this.PlayersInitialized = true;
            }
        }

        public PlayerService(IPlayerSetupService playerSetupService, )
        {
            this._playerSetupService = playerSetupService;
        }
        public User GetPlayerByInfo(string userAgent, string userIp)
        {
            return userAgent == this.WhitePlayer.userAgent && userIp == this.WhitePlayer.userIp ? this.WhitePlayer : this.BlackPlayer;
        }

        public void ConfigurePlayer(Color.PlayerColor playerColor, string userAgent, string userIp)
        {
            User player = this._playerSetupService.SetupPlayer(playerColor, userAgent, userIp);
            this.SetPlayer(player);
        }

    }
}
