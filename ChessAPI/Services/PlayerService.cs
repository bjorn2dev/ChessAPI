using ChessAPI.Interfaces;
using ChessAPI.Models;
using ChessAPI.Models.Enums;

namespace ChessAPI.Services
{
    public class PlayerService : IPlayerService
    {
        public bool PlayersInitialized { get; private set; }

        readonly IPlayerSetupService _playerSetupService;
        public User? WhitePlayer { get; private set; }
        public User? BlackPlayer { get; private set; }
        private void SetWhitePlayer(User user) => this.WhitePlayer = user;
        private void SetBlackPlayer(User user) => this.BlackPlayer = user;
        public List<PlayerTurn> PlayerTurns { get; private set; }
        public bool SameDevice { get; private set; }

        public PlayerService(IPlayerSetupService playerSetupService)
        {
            this.PlayerTurns = new List<PlayerTurn>();
            this._playerSetupService = playerSetupService;
        }

        public User GetPlayerByInfo(string userAgent, string userIp)
        {
            if(this.SameDevice)
            {
                return this.PlayerTurns.Any() && this.PlayerTurns.Last().user == this.BlackPlayer || !this.PlayerTurns.Any() ? this.WhitePlayer : this.BlackPlayer;
            }
            return userAgent == this.WhitePlayer.userAgent && userIp == this.WhitePlayer.userIp ? this.WhitePlayer : this.BlackPlayer;
        }

        public void RecordTurn(PlayerTurn playerTurn)
        {
            this.PlayerTurns.Add(playerTurn); 
        }

        public void ConfigurePlayer(Color.PlayerColor playerColor, string userAgent, string userIp)
        {
            User player = this._playerSetupService.SetupPlayer(playerColor, userAgent, userIp);

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
                if (this.WhitePlayer.userIp == this.BlackPlayer.userIp && this.WhitePlayer.userAgent == this.BlackPlayer.userAgent)
                {
                    this.SameDevice = true;
                }
                this.PlayersInitialized = true;
            }
        }

    }
}
