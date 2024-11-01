using ChessAPI.Models.Enums;

namespace ChessAPI.Models
{
    public class User
    {
        public User()
        {
            
        }
        public string name { get; set; }

        private Color.PlayerColor color;

        public Color.PlayerColor GetColor()
        {
            return color;
        }

        public void SetColor(Color.PlayerColor value)
        {
            this.color = value;
            this.name = $"{value.ToString()} Player";
        }
    }
}
