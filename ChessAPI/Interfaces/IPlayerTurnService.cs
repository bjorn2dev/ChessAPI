using ChessAPI.Models;
using ChessAPI.Models.Enums;
namespace ChessAPI.Interfaces
{
    public interface IPlayerTurnService
    {
        User? WhitePlayer { get; }
        User? BlackPlayer { get; }
        List<PlayerTurn> PlayerTurns { get; }
        void RecordTurn(Tile fromTile, Tile toTile);
        bool IsValidTurn(Color.PieceColor color, string userAgent, string userIp);
        void SetWhitePlayer(string userAgent, string userIp);
        void SetBlackPlayer(string userAgent, string userIp);
    }
}
