using ChessAPI.Helpers;
using ChessAPI.Interfaces;
using ChessAPI.Models;
using ChessAPI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessAPITests
{
    public class CompleteBoardSetup 
    {
        public BoardGenerator _boardGenerator;
        public IBoardStateService _boardStateService;
        public IStartingPositionProvider _startingPositionProvider;
        public IPieceHtmlRenderer _pieceHtmlRenderer;
        public TileHtmlRenderer _tileHtmlRenderer;
        public CompleteBoardSetup()
        {
            // Use ConfigurationBuilder to load the appsettings file in the test project’s output directory
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory) // Sets the path to the output directory
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var settings = configuration.GetSection("StartingPosition").Get<StartingPositionSettings>();
            IOptions<StartingPositionSettings> options = Options.Create(settings);
            _startingPositionProvider = new StartingPositionService(options);

            _pieceHtmlRenderer = new PieceHtmlRenderer();
            _tileHtmlRenderer = new TileHtmlRenderer(_pieceHtmlRenderer);
            _boardStateService = new BoardStateService();
            _boardGenerator = new BoardGenerator(_startingPositionProvider, _tileHtmlRenderer, _boardStateService);

            _boardGenerator.SetupBoard();
            _boardGenerator.AddInitialPieces();
        }
    }
}
