using ChessAPI.Interfaces.Board;
using ChessAPI.Interfaces.Piece;
using ChessAPI.Interfaces.Renderer;
using ChessAPI.Models;
using ChessAPI.Models.Pieces;
using static ChessAPI.Models.Enums.Color;

namespace ChessAPI.Services.Board
{
    /// <summary>
    /// BoardGenerator class (responsible for generating the board's structure)
    /// </summary>
    public class BoardGenerator : IBoardGenerator
    {
        private readonly IPositionProvider _startingPositionProvider;
        private readonly ITileRenderer _tileRenderer;
        private readonly IPieceFactory _pieceFactory;

        public BoardGenerator(IPositionProvider startingPositionProvider, ITileRenderer tileRenderer, IPieceFactory pieceFactory)
        {
            _startingPositionProvider = startingPositionProvider;
            _tileRenderer = tileRenderer;
            _pieceFactory = pieceFactory;
        }

        public void SetupBoard(ChessBoard board)
        {
            // rank starts from 1 at the bottom and goes up to 8
            for (int rank = board.ranks; rank >= 1; rank--)
            {
                for (int file = 0; file < board.files; file++)
                {
                    var key = Tuple.Create(rank, file);
                    var tile = new Tile { rank = rank, fileNumber = file, color = (rank + file) % 2 == 0 };

                    board.playingFieldDictionary[key] = tile;
                }
            }
        }

        public void AddInitialPieces(ChessBoard board)
        {
            if (board.playingFieldDictionary.Count == 0)
            {
                throw new InvalidOperationException("no board");
            }

            foreach (var tile in board.playingFieldDictionary.Values)
            {
                if (!string.IsNullOrEmpty(tile.tileAnnotation))
                {
                    // Determine the type of piece (king, queen, rook, etc.)
                    var pieceType = _startingPositionProvider.GetPieceTypeForLocation(tile.tileAnnotation);

                    if (pieceType != null)
                    {
                        var isWhite = _startingPositionProvider.IsWhiteStartingPosition(tile.tileAnnotation);
                        var color = isWhite ? PieceColor.White : PieceColor.Black;

                        tile.piece = _pieceFactory.CreatePiece(pieceType, color);
                        tile.piece.boardLocation = tile.tileAnnotation;
                    }
                }
                tile.html = _tileRenderer.Render(tile);
            }
        }
    }
}
