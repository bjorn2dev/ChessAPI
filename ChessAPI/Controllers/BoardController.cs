using ChessAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChessAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BoardController : ControllerBase
    {
        private readonly ILogger<BoardController> _logger;
        private readonly IPieceMovingService _pieceMovingService;
        private readonly IGameGenerator _gameGenerator;

        public BoardController(ILogger<BoardController> logger, IPieceMovingService pieceMovingService, IGameGenerator gameGenerator)
        {
            _pieceMovingService = pieceMovingService;
            _logger = logger;
            _gameGenerator = gameGenerator;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var htmlContent = _gameGenerator.GetBoard();
            return Content(htmlContent, "text/html");
        }

        [HttpPut("{from}/{to}")]
        public IActionResult MovePiece(string from, string to)
        {
            var userIpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            var userAgent = HttpContext.Request.Headers["User-Agent"].ToString();
            _pieceMovingService.MovePiece(from, to, userAgent, userIpAddress);
            return NoContent();
        }

    }
}
