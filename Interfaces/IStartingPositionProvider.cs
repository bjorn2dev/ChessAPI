using ChessAPI.Models;

namespace ChessAPI.Interfaces
{
    public interface IStartingPositionProvider
    {
        Type GetPieceTypeForLocation(string location);
        bool IsWhiteStartingPosition(string location);
        bool IsBlackStartingPosition(string location);
    }
}
