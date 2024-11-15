using ChessAPI.Interfaces;
using ChessAPI.Models;
using ChessAPI.Models.Enums;
using Microsoft.Extensions.Options;

namespace ChessAPI.Services
{
    public class GameGenerator : IGameGenerator
    {
        private readonly IBoardService _boardService;
        private readonly GameSettings _gameSettings;
        public GameGenerator(IBoardService boardService, IOptions<GameSettings> gameSettings)
        {
            this._gameSettings = gameSettings.Value;
            this._boardService = boardService;
        }

        public string GetBoard(Color.PlayerColor playerColorToShow)
        {
            return this._boardService.GetBoard(playerColorToShow);
        }

        public void InitializeBoard()
        {
            if (!this._boardService.BoardInitialized)
            {
                this._boardService.InitializeBoard();
            }
        }

       
    }
}
