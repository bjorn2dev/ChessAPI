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

        public PlayerTurnService()
        {
        }

        public bool IsValidTurn(List<PlayerTurn> playerTurns, User player)
        {
            var turn = this.CheckWhoseTurn(playerTurns);
            if (turn == Color.PlayerColor.White && playerTurns.Count == 0) return true;  // First turn is always valid and white always starts
            if (turn == Color.PlayerColor.Black && playerTurns.Count == 1) return true;  // Second turn is black 

            return playerTurns.Count > 1 && playerTurns.Last().user.color != player.color
                && playerTurns.Any((b) => b.user.userIp == player.userIp && b.user.userAgent == player.userAgent && b.user.color == player.color);
        }

        public Color.PlayerColor CheckWhoseTurn(List<PlayerTurn> playerTurns) => playerTurns.Count == 0 || playerTurns.Any() && playerTurns.Last().user.color == Color.PlayerColor.Black ? Color.PlayerColor.White : Color.PlayerColor.Black;

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
