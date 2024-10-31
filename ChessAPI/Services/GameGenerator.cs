using ChessAPI.Interfaces;

namespace ChessAPI.Services
{
    public class GameGenerator : IGameGenerator
    {
        private readonly IBoardService _boardService;
        private readonly IPlayerTurnService _playerTurnService;
        private bool _isGameInitialized;
        public GameGenerator(IBoardService boardService, IPlayerTurnService playerTurnService)
        {
            _playerTurnService = playerTurnService; 
            _boardService = boardService; ;
            _isGameInitialized = false;
        }

        public string GetBoard()
        {
            if (!_isGameInitialized)
            {
                InitializeGame();
            }
            return _boardService.GetBoard();
        }

        public void InitializeGame()
        {
            if (!_isGameInitialized)
            {
                _boardService.InitializeBoard();
                _isGameInitialized = true;
            }
        }
    }
}
