using ChessAPI.Helpers;
using ChessAPI.Interfaces;
using ChessAPI.Models;
using Newtonsoft;
using Newtonsoft.Json;
using Color = ChessAPI.Models.Enums.Color;

namespace ChessAPI.Services
{
    /// <summary>
    /// BoardService class (using composition to separate concerns)
    /// </summary>
    public class BoardService : IBoardService
    {

        private readonly IBoardGenerator _boardGenerator;
        private readonly IBoardRenderer _boardRenderer;
        private readonly IBoardStateService _boardStateService;

        public BoardService(IBoardGenerator boardGenerator, IBoardRenderer boardRenderer, IBoardStateService boardStateService)
        {
            _boardGenerator = boardGenerator;
            _boardRenderer = boardRenderer;
            _boardStateService = boardStateService;
        }
        // Method to initialize the board by calling SetupBoard and AddInitialPieces
        public void InitializeBoard()
        {
            _boardGenerator.SetupBoard();
            _boardGenerator.AddInitialPieces();
        }

        public string GetBoard(Color.PlayerColor playerColor = Color.PlayerColor.White)
        {
            var board = _boardStateService.Board;
            // Ensure the board is initialized before returning it
            if (board.playingFieldDictionary.Count == 0)
            {
                InitializeBoard();  // Initialize the board if it's not already initialized
            }

            return _boardRenderer.RenderBoard(board.playingFieldDictionary, playerColor);
        }
    }
}
