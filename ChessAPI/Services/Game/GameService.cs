using ChessAPI.Helpers;
using ChessAPI.Interfaces.Game;
using ChessAPI.Interfaces.Player;
using ChessAPI.Models;
using ChessAPI.Models.Enums;
using Microsoft.Extensions.Options;
namespace ChessAPI.Services.Game
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
            _gameInitializationService = gameInitializationService;
            _playerManagementService = playerManagementService;
            _gameRenderingService = gameRenderingService;
            _gameMoveValidator = gameMoveValidator;
        }

        public void MovePiece(string from, string to, string userAgent, string userIpAddress, ChessPiece promoteTo = null)
        {
            var player = _playerManagementService.GetPlayerByInfo(userAgent, userIpAddress);
            _gameMoveValidator.Move(from, to, player, promoteTo);
        }

        public void RegisterPlayerColor(Color.PlayerColor playerColor, string userAgent, string userIp)
        {
            _playerManagementService.RegisterPlayerColor(playerColor, userAgent, userIp);
        }

        public string StartGame()
        {
            if (!_playerManagementService.ArePlayersRegistered())
            {

                return _gameRenderingService.RenderColorSelector(_playerManagementService.GetUnregisteredPlayers());
            }
            else
            {
                _gameInitializationService.InitializeGame();
                return _gameRenderingService.RenderBoard();
            }
        }
    }
}
