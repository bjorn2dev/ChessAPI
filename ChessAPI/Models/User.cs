using ChessAPI.Helpers;
using ChessAPI.Models.Enums;
using Newtonsoft.Json.Linq;

namespace ChessAPI.Models
{
    public class User
    {
        public User()
        {
        }
        public string name { get; private set; }
        private Color.PlayerColor _color;
        public Color.PlayerColor color {
            get => _color;
            set
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    name = $"{value.ToString()} Player";
                    _color = value;
                }
                else
                {
                    throw new InvalidOperationException("Color can't be changed once set");
                }
            }
        }
        public string userAgent { get; set; }
        public string userIp { get; set; }

    }
}
