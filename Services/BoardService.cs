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
        // Method to initialize the board by calling SetupBoard and AddInitialPieces
        public void InitializeBoard()
        {
            _boardGenerator.SetupBoard();
            _boardGenerator.AddInitialPieces();
        }

        public string GetBoard()
        {
            // Ensure the board is initialized before returning it
            if (_boardGenerator.Board.playingFieldDictionary.Count == 0)
            {
                InitializeBoard();  // Initialize the board if it's not already initialized
            }

            return _boardRenderer.RenderBoard(_boardGenerator.Board.playingFieldDictionary);
        }

        public string GetBoardDictionary()
        {
            if (_boardGenerator.Board.playingFieldDictionary.Count == 0)
            {
                InitializeBoard(); // Initialize the board if it's not already initialized
            }

            return JsonConvert.SerializeObject(_boardGenerator.Board.playingFieldDictionary);
        }
    }
}
