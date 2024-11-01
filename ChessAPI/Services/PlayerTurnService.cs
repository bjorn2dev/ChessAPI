using ChessAPI.Helpers;
using ChessAPI.Interfaces;
using ChessAPI.Models;
using ChessAPI.Models.Enums;
using Microsoft.AspNetCore.Http;

namespace ChessAPI.Services
{
    public class PlayerTurnService : IPlayerTurnService
    {
        public User? WhitePlayer { get; private set; }
        public User? BlackPlayer { get; private set; }
        public List<PlayerTurn> PlayerTurns { get; private set; }

        public PlayerTurnService()
        {
            PlayerTurns = new List<PlayerTurn>();
        }

        public bool IsValidTurn(Color.PieceColor color, string userAgent, string userIp)
        {
            if (PlayerTurns.Count == 0 && color == Color.PieceColor.White && 
                (this.WhitePlayer != null && this.WhitePlayer.userAgent == userAgent && this.WhitePlayer.userIp == userIp)) return true;  // First turn is always valid and white always starts

            return PlayerTurns.Count > 0 && PlayerTurns.Last().color != color 
                && (color == Color.PieceColor.White ? 
                (this.WhitePlayer != null && this.WhitePlayer.userAgent == userAgent && this.WhitePlayer.userIp == userIp) :
                (this.BlackPlayer != null && this.BlackPlayer.userAgent == userAgent && this.BlackPlayer.userIp == userIp));
        }

        public Color.PlayerColor CheckWhoseTurn() => PlayerTurns.Count == 0 || this.PlayerTurns.Any() && this.PlayerTurns.Last().color == Color.PieceColor.Black ? Color.PlayerColor.White : Color.PlayerColor.Black;

        public bool WhiteAndBlackAreSimilarPlayer()
        {
            return this.WhitePlayer.userAgent == this.BlackPlayer.userAgent && this.WhitePlayer.userIp == this.BlackPlayer.userIp;
        }

        public void RecordTurn(Tile fromTile, Tile toTile)
        {
            var playerTurn = new PlayerTurn();
            playerTurn.color = fromTile.piece.color;
            playerTurn.user = playerTurn.color == Color.PieceColor.White ? this.WhitePlayer : this.BlackPlayer;
            playerTurn.fromTile = fromTile;
            playerTurn.toTile = toTile;
            PlayerTurns.Add(playerTurn);
        }
        public void SetWhitePlayer(string userAgent, string userIp)
        {
            this.WhitePlayer = new User {color = Color.PlayerColor.White, userAgent = userAgent, userIp = userIp };
        }

        public void SetBlackPlayer(string userAgent, string userIp)
        {
            this.BlackPlayer = new User { color = Color.PlayerColor.Black, userAgent = userAgent, userIp = userIp };
        }

        public User GetPlayerByInfo(string userAgent, string userIp)
        {
            return userAgent == this.WhitePlayer.userAgent && userIp == this.WhitePlayer.userIp ? this.WhitePlayer : this.BlackPlayer;
        }
    }
}
