using ChessAPI.Interfaces;
using ChessAPI.Models;
using ChessAPI.Models.Enums;
using Microsoft.AspNetCore.Http;

namespace ChessAPI.Services
{
    public class PlayerTurnService : IPlayerTurnService
    {
        public List<PlayerTurn> PlayerTurns { get; private set; }
        public PlayerTurnService()
        {
            PlayerTurns = new List<PlayerTurn>();
        }

        public bool IsValidTurn(Color.PieceColor color)
        {
            if (PlayerTurns.Count == 0 && color == Color.PieceColor.White) return true;  // First turn is always valid and white always starts

            return PlayerTurns.Last().color != color;
        }

        public void RecordTurn(Tile fromTile, Tile toTile)
        {
            var playerTurn = new PlayerTurn();
            playerTurn.color = fromTile.piece.color;
            playerTurn.fromTile = fromTile;
            playerTurn.toTile = toTile;
            PlayerTurns.Add(playerTurn);
        }

    }
}
