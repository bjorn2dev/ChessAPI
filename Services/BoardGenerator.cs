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
        private readonly string[] _kingWhiteStart = ["E1"];
        private readonly string[] _queenWhiteStart = [ "D1"];
        private readonly string[] _rookWhiteStart = [ "A1", "A8"];
        private readonly string[] _bishopWhiteStart = [ "C1", "F1"];
        private readonly string[] _knightWhiteStart = [ "B1", "G1"];
        private readonly string[] _pawnWhiteStart = ["A2", "B2", "C2", "D2", "E2", "F2", "G2", "H2"];
        private readonly string[] _kingBlackStart = ["E8"];
        private readonly string[] _queenBlackStart = ["D8", "D1"];
        private readonly string[] _rookBlackStart = ["A8", "H8"];
        private readonly string[] _bishopBlackStart = ["C8", "F8"];
        private readonly string[] _knightBlackStart = ["B8", "G8"];
        private readonly string[] _pawnBlackStart = ["A7", "B7", "C7", "D7", "E7", "F7", "G7", "H7"];

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
                        if (_kingWhiteStart.Contains(tile.tileAnnotation) || _kingBlackStart.Contains(tile.tileAnnotation)) 
                        {
                            tile.piece = new King();
                        }
                            
                           
                        if (_queenWhiteStart.Contains(tile.tileAnnotation) || _queenBlackStart.Contains(tile.tileAnnotation))
                            tile.piece = new Queen();
                        if (_rookWhiteStart.Contains(tile.tileAnnotation) || _rookBlackStart.Contains(tile.tileAnnotation))
                            tile.piece = new Rook();
                        if (_bishopWhiteStart.Contains(tile.tileAnnotation) || _bishopBlackStart.Contains(tile.tileAnnotation))
                            tile.piece = new Bishop();
                        if (_knightWhiteStart.Contains(tile.tileAnnotation) || _knightBlackStart.Contains(tile.tileAnnotation))
                            tile.piece = new Knight();
                        if (_pawnWhiteStart.Contains(tile.tileAnnotation) || _pawnBlackStart.Contains(tile.tileAnnotation))
                            tile.piece = new Pawn();

                        if (tile.piece != null)
                        {
                           // tile.piece.boardLocation = tile.tileAnnotation;
                        }
                    }
                    boardDictionary[key] = tile;
                }
            }
            return boardDictionary;
        }
    }
}
