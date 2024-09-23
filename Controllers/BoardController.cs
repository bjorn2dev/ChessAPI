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

        public BoardController(ILogger<BoardController> logger, IBoardService boardService)
        {
            _boardService = boardService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var htmlContent = _boardService.GetInitialBoard();
            return Content(htmlContent, "text/html");
        }
    }
}
