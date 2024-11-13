using ChessAPI.Helpers;
using ChessAPI.Interfaces;
using ChessAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ChessAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BoardController : ControllerBase
    {
        private readonly ILogger<BoardController> _logger;
        private readonly IPieceMovingService _pieceMovingService;
        private readonly IGameGenerator _gameGenerator;
        private readonly GameSettings _gameSettings;

        public BoardController(ILogger<BoardController> logger, IPieceMovingService pieceMovingService, IGameGenerator gameGenerator, IOptions<GameSettings> gameSettings)
        {
            this._gameSettings = gameSettings.Value;
            this._pieceMovingService = pieceMovingService;
            this._logger = logger;
            this._gameGenerator = gameGenerator;
        }

        //[HttpGet]
        //public IActionResult Get()
        //{
        //    var userIpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        //    var userAgent = HttpContext.Request.Headers["User-Agent"].ToString();

        //    if (this._gameSettings.SkipColorSelection)
        //    {
        //        userAgent = this._gameSettings.SkipUserAgent;
        //        userIpAddress = this._gameSettings.SkipUserIpAddress;
        //    }

        //    var htmlContent = _gameGenerator.GetBoard(userAgent, userIpAddress);
        //    return Content(htmlContent, "text/html");
        //}

        //[HttpPut("{from}/{to}")]
        //public IActionResult MovePiece(string from, string to)
        //{
        //    var userIpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        //    var userAgent = HttpContext.Request.Headers["User-Agent"].ToString();

        //    if (this._gameSettings.SkipColorSelection)
        //    {
        //        userAgent = this._gameSettings.SkipUserAgent;
        //        userIpAddress = this._gameSettings.SkipUserIpAddress;
        //    }
        //    var fromTile = TileHelper.GetTileByAnnotation(from, this._boardStateService.Board);
        //    var toTile = TileHelper.GetTileByAnnotation(to, this._boardStateService.Board);

        //    this._pieceMovingService.MovePiece(fromTile, toTile, userAgent, userIpAddress);
        //    return NoContent();
        //}

    }
}
