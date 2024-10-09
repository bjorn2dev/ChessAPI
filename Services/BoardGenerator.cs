using ChessAPI.Interfaces;
using ChessAPI.Models;
using ChessAPI.Models.Pieces;
using static ChessAPI.Models.Enums.Color;

namespace ChessAPI.Services
{
    /// <summary>
    /// BoardGenerator class (responsible for generating the board's structure)
    /// </summary>
    public class BoardGenerator : IBoardGenerator
    {
        private readonly IStartingPositionProvider _startingPositionProvider;
        private readonly ITileRenderer _tileRenderer;

        public Board Board { get; private set; }

        public BoardGenerator(IStartingPositionProvider startingPositionProvider, ITileRenderer tileRenderer)
        {
            Board = new Board();
            _startingPositionProvider = startingPositionProvider;
            _tileRenderer = tileRenderer;

            // TODO change this??
            this.SetupBoard();
            this.AddInitialPieces();

        }
        public void SetupBoard()
        {
            // rank starts from 1 at the bottom and goes up to 8
            for (int rank = Board.ranks; rank >= 1; rank--)
            {
                for (int file = 0; file < Board.files; file++)
                {
                    var key = Tuple.Create(rank, file);
                    var tile = new Tile { rank = rank, fileNumber = file, color = (rank + file) % 2 == 0 };

                    Board.playingFieldDictionary[key] = tile;
                }
            }
        }

        public void AddInitialPieces()
        {
            foreach (var tile in Board.playingFieldDictionary.Values)
            {
                if (!string.IsNullOrEmpty(tile.tileAnnotation))
                {
                    // Determine the type of piece (king, queen, rook, etc.)
                    var pieceType = _startingPositionProvider.GetPieceTypeForLocation(tile.tileAnnotation);

                    if (pieceType != null)
                    {
                        // Instantiate the piece dynamically using reflection
                        tile.piece = (Piece)Activator.CreateInstance(pieceType);

                        // Check if the starting position is for a white or black piece
                        tile.piece.color = _startingPositionProvider.IsWhiteStartingPosition(tile.tileAnnotation)
                        ? PieceColor.White
                        : PieceColor.Black;

                        tile.piece.boardLocation = tile.tileAnnotation;
                    }
                }
                tile.html = _tileRenderer.Render(tile);
            }
        }
    }
}
