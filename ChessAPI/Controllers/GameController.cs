using ChessAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ChessAPI.Models.Enums;
using ChessAPI.Models;
using Microsoft.Extensions.Options;

namespace ChessAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IPlayerService _playerService;
        private readonly IGameService _gameService;
        private readonly GameSettings _gameSettings;
        public GameController(IPlayerService playerService, IGameService gameService, IOptions<GameSettings> gameSettings)
        {
            this._gameSettings = gameSettings.Value;
            this._playerService = playerService;
            this._gameService = gameService;
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
