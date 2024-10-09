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

        public BoardService(IBoardGenerator boardGenerator, IBoardRenderer boardRenderer)
        {
            _boardGenerator = boardGenerator;
            _boardRenderer = boardRenderer;
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
