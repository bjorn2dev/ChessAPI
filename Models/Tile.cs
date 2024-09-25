using System.ComponentModel;
using System.Drawing;

namespace ChessAPI.Models
{
    /// <summary>
    /// todo make this generic no more cases.
    /// </summary>
    public class Tile
    {

        /// <summary>
        /// the color of the tile
        /// </summary>
        public bool color { get; set; }

        /// <summary>
        /// The horizontal rows of squares, called ranks, are numbered 1 to 8 starting from White's side of the board
        /// </summary>
        public int rank;

        /// <summary>
        /// 
        /// </summary>
        public string html => $"<td class=\"{(color ? "light-square" : "dark-square")}\">{tileAnnotation} {piece.html}</td>";

        /// <summary>
        /// The vertical columns of squares, called files, are labeled a through h from White's left (the queenside) to right (the kingside)
        /// </summary>
        private string file { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private int _fileNumber;

        /// <summary>
        /// 
        /// </summary>
        public int fileNumber
        {
            get => _fileNumber;
            set
            {
                if (string.IsNullOrWhiteSpace(file))
                {
                    switch (value)
                    {
                        case 0:
                            file = "a";
                            break;
                        case 1:
                            file = "b";
                            break;
                        case 2:
                            file = "c";
                            break;
                        case 3:
                            file = "d";
                            break;
                        case 4:
                            file = "e";
                            break;
                        case 5:
                            file = "f";
                            break;
                        case 6:
                            file = "g";
                            break;
                        case 7:
                            file = "h";
                            break;

                        default:
                            break;
                    }
                    _fileNumber = value;
                }
                else { throw new InvalidOperationException("MyProperty can't be changed once set"); }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string tileAnnotation => $"{file.ToUpper()}{rank}";
        public Piece piece { get; set; } = new Piece();

    }
}
