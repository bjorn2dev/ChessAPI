using ChessAPI.Helpers;
using ChessAPI.Interfaces;
using ChessAPI.Models;
using ChessAPI.Models.Enums;
using Microsoft.AspNetCore.Http;

namespace ChessAPI.Services
{
    public class PlayerTurnService : IPlayerTurnService
    {
      private readonly IPlayerService _playerService;
        public List<PlayerTurn> PlayerTurns { get; private set; }
        private readonly IGameStateService _gameStateService;
        public PlayerTurnService(IPlayerService playerService, IGameStateService gameStateService)
        {
            PlayerTurns = new List<PlayerTurn>();
            _playerService = playerService;
            _gameStateService = gameStateService;
        }

        public bool IsValidTurn(Color.PieceColor color, string userAgent, string userIp)
        {
            if (PlayerTurns.Count == 0 && color == Color.PieceColor.White && 
                (this._playerService.WhitePlayer != null && this._playerService.WhitePlayer.userAgent == userAgent && this._playerService.WhitePlayer.userIp == userIp)) return true;  // First turn is always valid and white always starts

            return PlayerTurns.Count > 0 && PlayerTurns.Last().color != color 
                && (color == Color.PieceColor.White ? 
                (this._playerService.WhitePlayer != null && this._playerService.WhitePlayer.userAgent == userAgent && this._playerService.WhitePlayer.userIp == userIp) :
                (this._playerService.BlackPlayer != null && this._playerService.BlackPlayer.userAgent == userAgent && this._playerService.BlackPlayer.userIp == userIp));
        }

        public Color.PlayerColor CheckWhoseTurn() => PlayerTurns.Count == 0 || this.PlayerTurns.Any() && this.PlayerTurns.Last().color == Color.PieceColor.Black ? Color.PlayerColor.White : Color.PlayerColor.Black;



        public void RecordTurn(Tile fromTile, Tile toTile)
        {
            var playerColor = fromTile.piece.color;
            var playerTurn = new PlayerTurn();
            TimeSpan turnTime = TimeSpan.Zero;
            if (this.PlayerTurns.Any())
            {
                turnTime = this.PlayerTurns.Last((x) => x.color == playerColor).turnTimeSpan;
            } else
            {
                turnTime = DateTime.Now - _gameStateService.GameStartTime;
            }
            
            playerTurn.turnTimeSpan = turnTime;
            playerTurn.color = playerColor;
            playerTurn.user = playerColor == Color.PieceColor.White ? this._playerService.WhitePlayer : this._playerService.BlackPlayer;
            playerTurn.fromTile = fromTile;
            playerTurn.toTile = toTile;
            PlayerTurns.Add(playerTurn);
        }
    }
}
