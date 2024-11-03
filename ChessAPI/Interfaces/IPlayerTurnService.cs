using ChessAPI.Models;
using ChessAPI.Models.Enums;
using System.Drawing;
using Color = ChessAPI.Models.Enums.Color;
namespace ChessAPI.Interfaces
{
    public interface IPlayerTurnService
    {
        List<PlayerTurn> PlayerTurns { get; }
        void RecordTurn(Tile fromTile, Tile toTile);
        bool IsValidTurn(Color.PieceColor color, string userAgent, string userIp);
        Color.PlayerColor CheckWhoseTurn();
    }
}
