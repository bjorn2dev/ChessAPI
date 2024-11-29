using ChessAPI.Models;

namespace ChessAPI.Interfaces.Piece
{
    public interface IPositionProvider
    {
        Type GetPieceTypeForLocation(string location);
        bool IsWhiteStartingPosition(string location);
        bool IsBlackStartingPosition(string location);


    }
}
