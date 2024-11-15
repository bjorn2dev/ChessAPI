using ChessAPI.Models;
using ChessAPI.Models.Enums;
using System.Drawing;
using Color = ChessAPI.Models.Enums.Color;
namespace ChessAPI.Interfaces
{
    public interface IPlayerTurnService
    {
        PlayerTurn ConfigureTurn(Tile fromTile, Tile toTile, User player);
        bool IsValidTurn( User player);
        Color.PlayerColor CheckWhoseTurn();
        public List<PlayerTurn> PlayerTurns { get; }
        void RecordTurn(PlayerTurn playerTurn);
    }
}
