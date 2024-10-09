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
        private readonly IBoardService _boardService;
        private readonly IPieceMovingService _pieceMovingService;

        public BoardController(ILogger<BoardController> logger, IBoardService boardService, IPieceMovingService pieceMovingService)
        {
            _boardService = boardService;
            _pieceMovingService = pieceMovingService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var htmlContent = _boardService.GetBoard();
            return Content(htmlContent, "text/html");
        }


        [HttpGet("GetBoardDictionary")]
        public string GetBoardDictionary()
        {
            return _boardService.GetBoardDictionary();
        }


        [HttpPut("{from}/{to}")]
        public IActionResult MovePiece(string from, string to)
        {
            _pieceMovingService.MovePiece(from, to);
            return NoContent();
        }

    }
}
