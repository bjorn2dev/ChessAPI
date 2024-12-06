using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ChessAPI.Models.Enums;
using ChessAPI.Models;
using Microsoft.Extensions.Options;
using ChessAPI.Services;
using ChessAPI.Interfaces.Game;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using ChessAPI.Models.Pieces;
using static ChessAPI.Models.Enums.Color;

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

        [HttpGet("new")]
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

            var boardHtml = gameService.StartGame();
            return Content(boardHtml, "text/html");
        }

        [HttpPut("{gameId}/{playerColor}/move/{from}/{to}")]
        public IActionResult MakeMove(Guid gameId, string playerColor, string from, string to, [FromQuery] string promotionType = "")
        {
            var gameService = _gameManagerService.GetGameById(gameId);
            if (gameService == null)
            {
                return NotFound("Game not found");
            }

            ChessPiece promotionPiece = null;
            if (!string.IsNullOrEmpty(promotionType))
            {
                // Manually create the `ChessPiece` based on the promotion string
                promotionPiece = this.CreatePromotionPiece(promotionType);
                if (promotionPiece == null)
                {
                    return BadRequest("Invalid promotion piece type");
                }
            }
            Enum.TryParse(playerColor, out PlayerColor parsedPlayerColor);

            var userIpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            var userAgent = HttpContext.Request.Headers["User-Agent"].ToString();
            gameService.MovePiece(from, to, parsedPlayerColor, userAgent, userIpAddress, promotionPiece);
            return NoContent();
        }

        private ChessPiece CreatePromotionPiece(string promotion)
        {
            // You can customize this logic based on your class hierarchy
            switch (promotion.ToLower())
            {
                case "queen":
                    return new Queen();
                case "rook":
                    return new Rook();
                case "bishop":
                    return new Bishop();
                case "knight":
                    return new Knight();
                default:
                    return null; // Invalid type
            }
        }


        [HttpPut("{gameId}/choosecolor/{color}")]
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
