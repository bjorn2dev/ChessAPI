using ChessAPI.Helpers;
using ChessAPI.Interfaces.Player;
using ChessAPI.Models;
using ChessAPI.Models.Enums;
using Microsoft.AspNetCore.Http;
using System.Numerics;

namespace ChessAPI.Services.Player
{
    public class PlayerTurnService : IPlayerTurnService
    {

        public List<PlayerTurn> PlayerTurns { get; private set; }
        public PlayerTurnService()
        {
            PlayerTurns = new List<PlayerTurn>();
        }
        public void RecordTurn(PlayerTurn playerTurn)
        {
            PlayerTurns.Add(playerTurn);
        }
        public bool IsValidTurn(User player)
        {
            var turn = CheckWhoseTurn();
             
            return player != null && (PlayerTurns.Count == 0 && turn == Color.PlayerColor.White ||
                   PlayerTurns.Count > 0 && PlayerTurns.Last().user.color != player.color) && 
                   player.color == turn;

        }

        public Color.PlayerColor CheckWhoseTurn() => PlayerTurns.Count == 0 || PlayerTurns.Last().user.color == Color.PlayerColor.Black ? Color.PlayerColor.White : Color.PlayerColor.Black;

        public PlayerTurn ConfigureTurn(Tile fromTile, Tile toTile, User player)
        {
            var playerTurn = new PlayerTurn();
            playerTurn.user = player;

            playerTurn.fromTile = new Tile
            {
                piece = fromTile.piece?.Clone(),
                html = fromTile.html,
                color = fromTile.color,
                fileNumber = fromTile.fileNumber,
                rank = fromTile.rank
            };

            playerTurn.toTile = new Tile
            {
                piece = toTile.piece?.Clone(),
                html = toTile.html,
                color = toTile.color,
                fileNumber = toTile.fileNumber,
                rank = toTile.rank
            };

            return playerTurn;
        }
    }
}
