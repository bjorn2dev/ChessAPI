using Microsoft.Extensions.Options;

namespace ChessAPI.Models
{
    public class GameSettings
    {
        public bool IsSinglePlayerGame {  get; set; }
        public bool SkipColorSelection { get; set; }

        public string SkipUserAgent { get; set; }
        public string SkipUserIpAddress {  get; set; }
        public string SinglePlayerUserAgent { get; set; }
        public string SinglePlayerUserIpAddress { get; set; }
    }
}
