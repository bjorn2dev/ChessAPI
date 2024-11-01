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
        private IGameService _gameService;
        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var htmlContent = _gameService.GetColorSelector();
             
            return string.IsNullOrWhiteSpace(htmlContent) ? RedirectToAction("Board") : Content(htmlContent, "text/html");
        }

        [HttpPut("ChooseColor/{color}")]
        public IActionResult ChooseColor(string color)
        {

            if (Enum.TryParse(color, out Color.PlayerColor colorOut))
            {
                _gameService.SetupPlayer(colorOut);
            }

            return NoContent();
        }

    }
}
