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
        public Dictionary<Tuple<int, int>, Tile> playingFieldDictionary { get; } = new Dictionary<Tuple<int, int>, Tile>();
    }
}
