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
        private readonly Board _board;
        private readonly IStartingPositionProvider _startingPositionProvider;
        private readonly ITileRenderer _tileRenderer;


        public BoardGenerator(IStartingPositionProvider startingPositionProvider, ITileRenderer tileRenderer)
        {
            _board = new Board();
            _startingPositionProvider = startingPositionProvider;
            _tileRenderer = tileRenderer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Dictionary<Tuple<int, int>, Tile> GenerateBoard()
        {
            var boardDictionary = new Dictionary<Tuple<int, int>, Tile>();
            // rank starts from 1 at the bottom and goes up to 8
            for (int rank = _board.ranks; rank >= 1; rank--)
            {
                for (int file = 0; file < _board.files; file++)
                {
                    var key = Tuple.Create(rank, file);
                    var tile = new Tile { rank = rank, fileNumber = file, color = (rank + file) % 2 == 0 };
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
                    boardDictionary[key] = tile;
                }
            }
            return boardDictionary;
        }
    }
}
