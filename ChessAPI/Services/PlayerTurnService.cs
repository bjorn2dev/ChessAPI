using ChessAPI.Helpers;
using ChessAPI.Interfaces;
using ChessAPI.Models;
using ChessAPI.Models.Enums;
using Microsoft.AspNetCore.Http;
using System.Numerics;

namespace ChessAPI.Services
{
    public class PlayerTurnService : IPlayerTurnService
    {

        public List<PlayerTurn> PlayerTurns { get; private set; }
        public PlayerTurnService()
        {
            this.PlayerTurns = new List<PlayerTurn>();
        }
        public void RecordTurn(PlayerTurn playerTurn)
        {
            this.PlayerTurns.Add(playerTurn);
        }
        public bool IsValidTurn(User player)
        {
            var turn = this.CheckWhoseTurn();
            if (turn == Color.PlayerColor.White && this.PlayerTurns.Count == 0) return true;  // First turn is always valid and white always starts
            if (turn == Color.PlayerColor.Black && this.PlayerTurns.Count == 1) return true;  // Second turn is black 

            return this.PlayerTurns.Count > 1 && this.PlayerTurns.Last().user.color != player.color
                && this.PlayerTurns.Any((b) => b.user.userIp == player.userIp && b.user.userAgent == player.userAgent && b.user.color == player.color);
        }

        public Color.PlayerColor CheckWhoseTurn() => this.PlayerTurns.Count == 0 || this.PlayerTurns.Last().user.color == Color.PlayerColor.Black ? Color.PlayerColor.White : Color.PlayerColor.Black;

        public PlayerTurn ConfigureTurn(Tile fromTile, Tile toTile, User player)
        {
            var playerTurn = new PlayerTurn();
            playerTurn.user = player;
            playerTurn.fromTile = fromTile;
            playerTurn.toTile = toTile;
            return playerTurn;
        }
    }
}
