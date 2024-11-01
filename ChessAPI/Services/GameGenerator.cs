using ChessAPI.Interfaces;

namespace ChessAPI.Services
{
    public class GameGenerator : IGameGenerator
    {
        private readonly IBoardService _boardService;
        private readonly IGameService _gameService;
        private bool _boardInitialized;
        public GameGenerator(IBoardService boardService, IGameService gameService)
        {
            _boardService = boardService;
            _gameService = gameService;
            _boardInitialized = false;
        }

        public string GetBoard(string userAgent, string userIpAddress)
        {
            if (!_boardInitialized)
            {
                return this.ChooseColor(userAgent, userIpAddress); 
            }

            return _boardService.GetBoard(_gameService.ShowBoardForPlayerColor(userAgent, userIpAddress));
        }

        public string GetBoardAsJson() => Newtonsoft.Json.JsonConvert.SerializeObject(_boardService.GetBoard());

        public void InitializeBoard()
        {
            if (!_boardInitialized)
            {
                _boardService.InitializeBoard();
                _boardInitialized = true;
            }
        }

        public string ChooseColor(string userAgent, string userIpAddress)
        {
            if (_gameService.IsGameInitialized())
            {
                this.InitializeBoard();
                return _boardService.GetBoard(_gameService.ShowBoardForPlayerColor(userAgent, userIpAddress));
            }
            else
            {
                return _gameService.GetColorSelector();
            }

        }
    }
}
