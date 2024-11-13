using ChessAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ChessAPI.Models.Enums;
using ChessAPI.Models;
using Microsoft.Extensions.Options;
using ChessAPI.Services;

namespace ChessAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameManagerService _gameManagerService;
        public GameController(IGameManagerService gameManagerService)
        {
            this._gameManagerService = gameManagerService;
        }

        [HttpGet]
        public IActionResult GetAllGames()
        {
            var gameId = _gameManagerService.GetAllGames();
            return Ok(gameId);
        }

        [HttpPost("new")]
        public IActionResult CreateNewGame()
        {
            var gameId = _gameManagerService.CreateNewGame();
            return Ok(gameId);
        }

        [HttpGet("{gameId}")]
        public IActionResult GetGameState(Guid gameId)
        {
            var gameService = _gameManagerService.GetGameById(gameId);
            if (gameService == null)
            {
                return NotFound("Game not found");
            }

            var boardHtml = gameService.InitializeGame();
            return Content(boardHtml, "text/html");
        }

        [HttpPut("{gameId}/move/{from}/{to}")]
        public IActionResult MakeMove(Guid gameId, string from, string to)
        {
            var gameService = _gameManagerService.GetGameById(gameId);
            if (gameService == null)
            {
                return NotFound("Game not found");
            }
            var userIpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            var userAgent = HttpContext.Request.Headers["User-Agent"].ToString();
            gameService.MovePiece(from, to, userAgent, userIpAddress);
            return NoContent();
        }

        [HttpPut("{gameId}/ChooseColor/{color}")]
        public IActionResult ChooseColor(Guid gameId, string color)
        {

            if (Enum.TryParse(color, out Color.PlayerColor colorOut))
            {
                var gameService = _gameManagerService.GetGameById(gameId);
                var userIpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
                var userAgent = HttpContext.Request.Headers["User-Agent"].ToString();
                gameService.RegisterPlayerColor(colorOut, userAgent, userIpAddress);
            }
            //if (this._gameSettings.IsSinglePlayerGame)
            //{
            //    var userIpAddress = this._gameSettings.SinglePlayerUserIpAddress;
            //    var userAgent = this._gameSettings.SinglePlayerUserAgent;
            //    this._playerService.ConfigurePlayer(colorOut == Color.PlayerColor.White ? Color.PlayerColor.Black : Color.PlayerColor.White, userAgent, userIpAddress);
            //}
            return NoContent();
        }

    }
}
