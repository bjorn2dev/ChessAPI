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
        private Dictionary<Tuple<int, int>, Tile> _boardDictionary;
        private readonly ITileRenderer _tileRenderer;

        public BoardService(IBoardGenerator boardGenerator, IBoardRenderer boardRenderer, ITileRenderer tileRenderer)
        {
            _boardGenerator = boardGenerator;
            _boardRenderer = boardRenderer;
            _tileRenderer = tileRenderer;
            _boardDictionary = new Dictionary<Tuple<int, int>, Tile>();

            _boardGenerator.SetupBoard(_boardDictionary);
            _boardGenerator.AddInitialPieces(_boardDictionary);
        }

        public string GetBoard()
        {
            return _boardRenderer.RenderBoard(_boardDictionary);
        }

        public string GetBoardDictionary()
        {
            return JsonConvert.SerializeObject(_boardDictionary);
        }


        public void MovePiece(string from, string to)
        {
            var fromFileLetter = from.Substring(0, 1);
            var fromFile = TileHelper.ConvertLetterToFileNumber(fromFileLetter);
            var fromRank = Int32.Parse(from.Substring(1, 1));

            Tile fromTile = null;

            foreach (var location in _boardDictionary.Keys)
            {
                // find item by rank + file combination. this because the board generates from rank to file.
                if (location.Item1 == fromRank && location.Item2 == fromFile)
                {
                    fromTile = _boardDictionary[location];
                }
            }

            var toFileLetter = to.Substring(0, 1);
            var toFile = TileHelper.ConvertLetterToFileNumber(toFileLetter);
            var toRank = Int32.Parse(to.Substring(1, 1));

            Tile toTile = null;

            foreach (var location in _boardDictionary.Keys)
            {
                if (location.Item1 == toRank && location.Item2 == toFile && fromTile != null)
                {
                    toTile = _boardDictionary[location];
                    toTile.piece = fromTile.piece;
                    toTile.html = _tileRenderer.Render(toTile);
                    _boardDictionary[location] = toTile;
                }
            }

            foreach (var location in _boardDictionary.Keys)
            {
                // find item by rank + file combination. this because the board generates from rank to file.
                if (location.Item1 == fromRank && location.Item2 == fromFile)
                {
                    fromTile = _boardDictionary[location];
                    fromTile.piece = null;
                    fromTile.html = _tileRenderer.Render(fromTile);
                    _boardDictionary[location] = fromTile;
                }
            }

        }
    }
}
