using ChessAPI.Interfaces.Board;
using ChessAPI.Interfaces.Game;
using ChessAPI.Models;
using ChessAPI.Models.Enums;
using Microsoft.Extensions.Options;

namespace ChessAPI.Services.Game
{
    public class GameGenerator : IGameGenerator
    {
        private readonly IBoardService _boardService;
        private readonly GameSettings _gameSettings;
        public GameGenerator(IBoardService boardService, IOptions<GameSettings> gameSettings)
        {
            _gameSettings = gameSettings.Value;
            _boardService = boardService;
        }

        public string GetBoard(Color.PlayerColor playerColorToShow)
        {
            return _boardService.GetBoard(playerColorToShow);
        }

        public void InitializeBoard()
        {
            if (!_boardService.BoardInitialized)
            {
                _boardService.InitializeBoard();
            }
        }


    }
}
