using ChessAPI.Helpers;
using ChessAPI.Interfaces;
using ChessAPI.Models;
using ChessAPI.Models.Enums;
using Microsoft.Extensions.Options;
namespace ChessAPI.Services
{ 
    public class GameService : IGameService
    {
  

        private readonly IGameInitializationService _gameInitializationService;
        private readonly IPlayerManagementService _playerManagementService;
        private readonly IGameRenderingService _gameRenderingService;
        private readonly IGameMoveValidator _gameMoveValidator;
        public GameService(
            IGameInitializationService gameInitializationService,
            IPlayerManagementService playerManagementService,
            IGameRenderingService gameRenderingService,
            IGameMoveValidator gameMoveValidator)
        {
            this._gameInitializationService = gameInitializationService;
            this._playerManagementService = playerManagementService;
            this._gameRenderingService = gameRenderingService;
            this._gameMoveValidator = gameMoveValidator;
        }

        public void MovePiece(string from, string to, string userAgent, string userIpAddress)
        {
            var player = this._playerManagementService.GetPlayerByInfo(userAgent, userIpAddress);
            this._gameMoveValidator.Move(from, to, player);
        }

        public void RegisterPlayerColor(Color.PlayerColor playerColor, string userAgent, string userIp)
        {
            this._playerManagementService.RegisterPlayerColor(playerColor, userAgent, userIp);
        }

        public string StartGame()
        {
            if (!this._playerManagementService.ArePlayersRegistered())
            {

                return this._gameRenderingService.RenderColorSelector(this._playerManagementService.GetUnregisteredPlayers());
            } 
            else
            {
                this._gameInitializationService.InitializeGame();
                return this._gameRenderingService.RenderBoard();
            }
        }
    }
}
