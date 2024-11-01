using ChessAPI.Interfaces;

namespace ChessAPI.Services
{
    public class GameGenerator : IGameGenerator
    {
        private readonly IBoardService _boardService;
        private readonly IPlayerTurnService _playerTurnService;
        private readonly IGameService _gameService;
        private bool _boardInitialized;
        public GameGenerator(IBoardService boardService, IPlayerTurnService playerTurnService, IGameService gameService)
        {
            _playerTurnService = playerTurnService;
            _boardService = boardService;
            _gameService = gameService;
            _boardInitialized = false;
        }

        public string GetBoard()
        {
            if (!_boardInitialized)
            {
                return this.ChooseColor(); 
            }

            return _boardService.GetBoard();
        }

        public void InitializeBoard()
        {
            if (!_boardInitialized)
            {
                _boardService.InitializeBoard();
                _boardInitialized = true;
            }
        }

        public string ChooseColor()
        {
            //this.InitializeBoard();
            if (_gameService.IsGameInitialized())
            {
                this.InitializeBoard();
                return _boardService.GetBoard();
            }
            else
            {
                return _gameService.GetColorSelector();
            }

        }
    }
}
