using ChessAPI.Helpers;
using ChessAPI.Interfaces;
using ChessAPI.Models;
using Newtonsoft;
using Newtonsoft.Json;

namespace ChessAPI.Services
{
    /// <summary>
    /// BoardService class (using composition to separate concerns)
    /// </summary>
    public class BoardService : IBoardService
    {

        private readonly IBoardGenerator _boardGenerator;
        private readonly IBoardRenderer _boardRenderer;
        private readonly ITileRenderer _tileRenderer;
        private readonly IPieceMovingService _pieceMovingService;

        public BoardService(IBoardGenerator boardGenerator, IBoardRenderer boardRenderer, ITileRenderer tileRenderer, IPieceMovingService pieceMovingService)
        {
            _boardGenerator = boardGenerator;
            _boardRenderer = boardRenderer;
            _tileRenderer = tileRenderer;
            _pieceMovingService = pieceMovingService;
        }

        public string GetBoard()
        {
            return _boardRenderer.RenderBoard(_boardGenerator.Board.playingFieldDictionary);
        }

        public string GetBoardDictionary()
        {
            return JsonConvert.SerializeObject(_boardGenerator.Board.playingFieldDictionary);
        }
    }
}
