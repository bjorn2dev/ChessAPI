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
                    file = ConvertFileNumberToLetter(value);
                    _fileNumber = value;
                }
                else
                {
                    throw new InvalidOperationException("fileNumber can't be changed once set");
                }
            }
        }

        public string tileAnnotation => $"{file.ToUpper()}{rank}";

        private string ConvertFileNumberToLetter(int fileNumber)
        {
            return fileNumber switch
            {
                0 => "a",
                1 => "b",
                2 => "c",
                3 => "d",
                4 => "e",
                5 => "f",
                6 => "g",
                7 => "h",
                _ => throw new ArgumentOutOfRangeException("Invalid file number")
            };
        }

    }
}
