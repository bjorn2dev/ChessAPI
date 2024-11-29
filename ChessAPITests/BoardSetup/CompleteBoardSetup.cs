using ChessAPI.Factories;
using ChessAPI.Interfaces.Board;
using ChessAPI.Interfaces.Piece;
using ChessAPI.Interfaces.Renderer;
using ChessAPI.Models;
using ChessAPI.Models.Pieces;
using ChessAPI.Services;
using ChessAPI.Services.Board;
using ChessAPI.Services.Piece;
using ChessAPI.Services.Renderer.Html;
using ChessAPI.Services.Renderers.Html;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessAPITests.BoardSetup
{
    public class CompleteBoardSetup
    {
        public BoardGenerator _boardGenerator;
        public IBoardStateService _boardStateService;
        public IPositionProvider _startingPositionProvider;
        public IPieceRenderer _pieceHtmlRenderer;
        public HtmlTileRenderer _tileHtmlRenderer;
        public IPieceFactory _pieceFactory;
        public IServiceProvider _serviceProvider;
        public CompleteBoardSetup(string configSection = "StartingPosition")
        {
            // Use ConfigurationBuilder to load the appsettings file in the test project’s output directory
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory) // Sets the path to the output directory
                .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
                .Build();

            var settings = configuration.GetSection(configSection).Get<StartingPositionSettings>();
            IOptions<StartingPositionSettings> options = Options.Create(settings);
            _startingPositionProvider = new PositionProvider(options);

            _pieceHtmlRenderer = new HtmlPieceRenderer();
            _tileHtmlRenderer = new HtmlTileRenderer(_pieceHtmlRenderer);
            _boardStateService = new BoardStateService();

            // Setup the service provider
            var services = new ServiceCollection();
            services.AddSingleton<IPositionProvider>(_startingPositionProvider);
            services.AddSingleton<IBoardStateService>(_boardStateService);
            services.AddSingleton<IPieceFactory, PieceFactory>();
            services.AddSingleton<HtmlTileRenderer>(_tileHtmlRenderer);
            services.AddSingleton<HtmlPieceRenderer>(_pieceHtmlRenderer);
            services.AddTransient<King>(); // Ensure King can be instantiated with PieceFactory

            _serviceProvider = services.BuildServiceProvider();

            // Inject the service provider into the factory
            _pieceFactory = _serviceProvider.GetService<IPieceFactory>();

            // Initialize the board generator
            _boardGenerator = new BoardGenerator(_startingPositionProvider, _tileHtmlRenderer, _pieceFactory);
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
