using ChessAPI.Helpers;
using System.ComponentModel;
using System.Drawing;

namespace ChessAPI.Models
{
    /// <summary>
    /// todo make this generic no more cases.
    /// </summary>
    public class Tile
    {
        public Piece? piece { get; set; }
        public bool color { get; set; }  // Represents the color of the tile TODO change this to use color model

        public string html { get; set; }    
        public int rank { get; set; }    // The row of the tile
        public string file { get; private set; }

        private int _fileNumber;
        public int fileNumber
        {
            get => _fileNumber;
            set
            {
                if (string.IsNullOrWhiteSpace(file))
                {
                    file = TileHelper.ConvertFileNumberToLetter(value);
                    _fileNumber = value;
                }
                else
                {
                    throw new InvalidOperationException("fileNumber can't be changed once set");
                }
            }
        }

        public string tileAnnotation => $"{file.ToUpper()}{rank}";
    }
}
