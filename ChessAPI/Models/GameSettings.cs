using Microsoft.Extensions.Options;

namespace ChessAPI.Models
{
    public class GameSettings
    {
        public bool SkipColorSelection { get; set; }

        public string SkipUserAgent { get; set; }
        public string SkipUserIpAddress {  get; set; }
    }
}
