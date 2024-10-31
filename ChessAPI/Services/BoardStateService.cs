using ChessAPI.Interfaces;
using ChessAPI.Models;
using ChessAPI.Models.Enums;
namespace ChessAPI.Services
{
    public class BoardStateService : IBoardStateService
    {
       
        public Board Board { get; private set; }

        public BoardStateService()
        {
            Board = new Board();
            
        }
    }
}
