using ChessAPI.Interfaces.Board;
using ChessAPI.Models;
using ChessAPI.Models.Enums;
namespace ChessAPI.Services.Board
{
    public class BoardStateService : IBoardStateService
    {

        public ChessBoard Board { get; private set; }

        public BoardStateService()
        {
            Board = new ChessBoard();
        }
    }
}
