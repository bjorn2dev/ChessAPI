using ChessAPI.Models;

namespace ChessAPI.Interfaces.Board
{
    public interface IBoardStateService
    {
        ChessBoard Board { get; }
    }
}
