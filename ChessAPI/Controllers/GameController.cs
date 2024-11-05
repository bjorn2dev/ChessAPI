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
        public GameController(IGameManagerService gameManagerService) //IPlayerService playerService, IGameService gameService, IOptions<GameSettings> gameSettings)
        {
            this._gameManagerService = gameManagerService;
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

            var boardHtml = gameService.GetBoard();
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

            gameService.MovePiece(from, to);
            return NoContent();
        }





        [HttpGet]
        public IActionResult Get()
        {
            var htmlContent = this._gameService.GetColorSelector();

            if (string.IsNullOrWhiteSpace(htmlContent))
            {
                return RedirectToAction("Get", "Board");
            }

            return Content(htmlContent, "text/html");
        }

        [HttpPut("ChooseColor/{color}")]
        public IActionResult ChooseColor(string color)
        {

            if (Enum.TryParse(color, out Color.PlayerColor colorOut))
            {
                var userIpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
                var userAgent = HttpContext.Request.Headers["User-Agent"].ToString();
                this._playerService.ConfigurePlayer(colorOut, userAgent, userIpAddress);
            }
            if (this._gameSettings.IsSinglePlayerGame)
            {
                var userIpAddress = this._gameSettings.SinglePlayerUserIpAddress;
                var userAgent = this._gameSettings.SinglePlayerUserAgent;
                this._playerService.ConfigurePlayer(colorOut == Color.PlayerColor.White ? Color.PlayerColor.Black : Color.PlayerColor.White, userAgent, userIpAddress);
            }
            return NoContent();
        }

    }
}
