using ChessAPI.Models;
using ChessAPI.Models.Enums;
namespace ChessAPI.Interfaces
{
    public interface IPlayerTurnService
    {
        List<PlayerTurn> PlayerTurns { get; }
        void RecordTurn(Tile fromTile, Tile toTile);
        bool IsValidTurn(Color.PieceColor color);
    }
}
