using ChessAPI.Interfaces;
using ChessAPI.Models;
using ChessAPI.Models.Pieces;

namespace ChessAPI.Services
{
    /// <summary>
    /// BoardGenerator class (responsible for generating the board's structure)
    /// </summary>
    public class BoardGenerator : IBoardGenerator
    {
        private readonly Board _board;
        private readonly string[] _pawnStart = ["A7", "B7", "C7", "D7", "E7", "F7", "G7", "H7","A2", "B2", "C2", "D2", "E2", "F2", "G2", "H2"];
        private readonly string[] _kingStart = ["E8", "E1"];
        private readonly string[] _queenStart = ["D8", "D1"];
        private readonly string[] _rookStart = ["A8", "H8", "A1", "A8"];
        private readonly string[] _bishopStart = ["C8", "F8", "C1", "F1"];
        private readonly string[] _knightStart = ["B8","G8","B1","G1"];

        public BoardGenerator()
        {
            _board = new Board();
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
                        if (_kingStart.Contains(tile.tileAnnotation))
                            tile.piece = new King();
                        if (_queenStart.Contains(tile.tileAnnotation))
                            tile.piece = new Queen();
                        if (_rookStart.Contains(tile.tileAnnotation))
                            tile.piece = new Rook();
                        if (_bishopStart.Contains(tile.tileAnnotation))
                            tile.piece = new Bishop();
                        if (_knightStart.Contains(tile.tileAnnotation))
                            tile.piece = new Knight();
                        if (_pawnStart.Contains(tile.tileAnnotation))
                            tile.piece = new Pawn();
                    }
                    boardDictionary[key] = tile;
                }
            }
            return boardDictionary;
        }
    }
}
