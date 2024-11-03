using ChessAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ChessAPI.Models.Enums;

namespace ChessAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IPlayerService _playerService;
        private readonly IGameService _gameService;
        public GameController(IPlayerService playerService, IGameService gameService)
        {
            _playerService = playerService;
            _gameService = gameService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var htmlContent = _gameService.GetColorSelector();

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

            return NoContent();
        }

    }
}
