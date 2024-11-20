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

            return  (this.PlayerTurns.Count == 0 && turn == Color.PlayerColor.White) || 
                    (this.PlayerTurns.Count > 0 && this.PlayerTurns.Last().user.color != turn && player.color != turn);
              
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
