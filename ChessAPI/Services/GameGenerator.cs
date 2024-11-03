using ChessAPI.Interfaces;
using ChessAPI.Models;
using Microsoft.Extensions.Options;

namespace ChessAPI.Services
{
    public class GameGenerator : IGameGenerator
    {
        private readonly IBoardService _boardService;
        private readonly IGameService _gameService;
        private bool _boardInitialized;
        private readonly GameSettings _gameSettings;
        public GameGenerator(IBoardService boardService, IGameService gameService, IOptions<GameSettings> gameSettings)
        {
            this._gameSettings = gameSettings.Value;
            this._boardService = boardService;
            this._gameService = gameService;
            this._boardInitialized = false;
        }

        public string GetBoard(string userAgent, string userIpAddress)
        {
            if (!this._boardInitialized)
            {
                return this.ChooseColor(userAgent, userIpAddress); 
            }

            if (this._gameSettings.SkipColorSelection)
            {
                userAgent = this._gameSettings.SkipUserAgent;
                userIpAddress = this._gameSettings.SkipUserIpAddress;
            }

            return _boardService.GetBoard(_gameService.ShowBoardForPlayerColor(userAgent, userIpAddress));
        }

        public void InitializeBoard()
        {
            if (!this._boardInitialized)
            {
                this._boardService.InitializeBoard();
                this._boardInitialized = true;
            }
        }

        public string ChooseColor(string userAgent, string userIpAddress)
        {
            if (this._gameService.IsGameInitialized())
            {
                this.InitializeBoard();
                return this._boardService.GetBoard(this._gameService.ShowBoardForPlayerColor(userAgent, userIpAddress));
            }
            else
            {
                return this._gameService.GetColorSelector();
            }

        }
    }
}
