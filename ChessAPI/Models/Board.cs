using ChessAPI.Models.Enums;
using ChessAPI.Models.Pieces;

namespace ChessAPI.Models
{
    public class Board
    {
        /// <summary>
        /// 
        /// </summary>
        public int files { get; set; } = 8;
        /// <summary>
        /// 
        /// </summary>
        public int ranks { get; set; } = 8;
        public SortedList<Tuple<int, int>, Tile> playingFieldDictionary { get; private set; } = new SortedList<Tuple<int, int>, Tile>();

        public Board Clone()
        {
            var clonedBoard = new Board
            {
                playingFieldDictionary = new SortedList<Tuple<int, int>, Tile>(
                    this.playingFieldDictionary.ToDictionary(
                        entry => entry.Key,
                        entry => new Tile
                        {
                            piece = entry.Value.piece?.Clone(),
                            html = entry.Value.html,
                            color = entry.Value.color,
                            fileNumber = entry.Value.fileNumber,
                            rank = entry.Value.rank
                        }
                    )
                )
            };

            return clonedBoard;
        }

        public Tile GetTileByRankAndFileNumber(int rank, int file) => this.playingFieldDictionary.First(t => t.Key.Item1 == rank && t.Key.Item2 == file).Value;
        public Tile GetTileByAnnotation(string annotation) => this.playingFieldDictionary.First(t => t.Value.tileAnnotation == annotation).Value;
        public Tile GetKingTile(Color.PieceColor pieceColor = Color.PieceColor.White) => this.playingFieldDictionary.First(x => x.Value.piece is King && x.Value.piece.color == pieceColor).Value;


    }
}
