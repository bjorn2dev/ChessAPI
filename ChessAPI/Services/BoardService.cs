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
        public bool BoardInitialized { get; set; }

        public BoardService(IBoardGenerator boardGenerator, IBoardRenderer boardRenderer, IBoardStateService boardStateService)
        {
            this._boardGenerator = boardGenerator;
            this._boardRenderer = boardRenderer;
            this._boardStateService = boardStateService;
        }
        // Method to initialize the board by calling SetupBoard and AddInitialPieces
        public void InitializeBoard()
        {
            this._boardGenerator.SetupBoard(this._boardStateService.Board);
            this._boardGenerator.AddInitialPieces(this._boardStateService.Board);
            this.BoardInitialized = true;
        }

        public string GetBoard(Color.PlayerColor playerColor = Color.PlayerColor.White)
        {
            var board = this._boardStateService.Board;
            // Ensure the board is initialized before returning it
            if (board.playingFieldDictionary.Count == 0)
            {
                this.InitializeBoard();  // Initialize the board if it's not already initialized
            }

            return this._boardRenderer.RenderBoard(board.playingFieldDictionary, playerColor);
        }
    }
}
