using ChessAPI.Models;

namespace ChessAPI.Interfaces
{
    public interface IGameMoveValidator
    {
        void Move(string from, string to, User player);
    }
}
