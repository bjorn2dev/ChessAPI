using ChessAPI.Interfaces;
using ChessAPI.Models;
using ChessAPI.Models.Enums;

public class PlayerManagementService : IPlayerManagementService
{
    private readonly IPlayerService _playerService;

    public PlayerManagementService(IPlayerService playerService)
    {
        _playerService = playerService;
    }

    public bool ArePlayersRegistered()
    {
        return _playerService.PlayersInitialized;
    }

    public void RegisterPlayerColor(Color.PlayerColor playerColor, string userAgent, string userIp)
    {
        User player = new User { color = playerColor, userAgent = userAgent, userIp = userIp };

        this._playerService.SetPlayer(player);
    }

    public User GetPlayerByInfo(string userAgent, string userIp)
    {
        if (this._playerService.SameDevice)
        {
            //return this.PlayerTurns.Any() && this.PlayerTurns.Last().user == this.BlackPlayer || !this.PlayerTurns.Any() ? this.WhitePlayer : this.BlackPlayer;
        }
        return userAgent == this._playerService.WhitePlayer.userAgent && userIp == this._playerService.WhitePlayer.userIp ? this._playerService.WhitePlayer : this._playerService.BlackPlayer;
    }
}