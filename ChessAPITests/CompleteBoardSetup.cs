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
        public IPieceRenderer _pieceHtmlRenderer;
        public HtmlTileRenderer _tileHtmlRenderer;
        public CompleteBoardSetup(string configSection = "StartingPosition")
        {
            // Use ConfigurationBuilder to load the appsettings file in the test project’s output directory
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory) // Sets the path to the output directory
                .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
                .Build();

            var settings = configuration.GetSection(configSection).Get<StartingPositionSettings>();
            IOptions<StartingPositionSettings> options = Options.Create(settings);
            _startingPositionProvider = new StartingPositionService(options);

            _pieceHtmlRenderer = new HtmlPieceRenderer();
            _tileHtmlRenderer = new HtmlTileRenderer(_pieceHtmlRenderer);
            _boardStateService = new BoardStateService();
            _boardGenerator = new BoardGenerator(_startingPositionProvider, _tileHtmlRenderer);

            _boardGenerator.SetupBoard(_boardStateService.Board);
            _boardGenerator.AddInitialPieces(_boardStateService.Board);
        }
        public Tile GetTileByNotation(string notation)
        {
            int rank = int.Parse(notation[1].ToString());
            int file = notation[0] - 'A';
            return _boardStateService.Board.playingFieldDictionary.First(c => c.Key.Item1 == rank && c.Key.Item2 == file).Value;
        }

        public Tile GetTile(int rank, int file) => _boardStateService.Board.playingFieldDictionary.First(t => t.Key.Item1 == rank && t.Key.Item2 == file).Value;
    }
}
