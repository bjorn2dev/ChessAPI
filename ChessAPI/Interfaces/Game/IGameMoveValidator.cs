using ChessAPI.Models;

namespace ChessAPI.Interfaces.Game
{
    public interface IGameMoveValidator
    {
        void Move(string from, string to, User player);
    }
}
