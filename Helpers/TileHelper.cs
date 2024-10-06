namespace ChessAPI.Helpers
{
    public class TileHelper
    {
        public static string ConvertFileNumberToLetter(int fileNumber)
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
                _ => throw new ArgumentOutOfRangeException(nameof(fileNumber), "Invalid file number")
            };
        }

        public static int ConvertLetterToFileNumber(string letter)
        {
            return letter.ToLower() switch
            {
                "a" => 0,
                "b" => 1,
                "c" => 2,
                "d" => 3,
                "e" => 4,
                "f" => 5,
                "g" => 6,
                "h" => 7,
                _ => throw new ArgumentOutOfRangeException(nameof(letter), "Invalid file letter")
            };
        }
    }
}
