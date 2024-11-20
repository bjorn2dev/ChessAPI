using ChessAPI.Interfaces;
using ChessAPI.Models;
using ChessAPI.Models.Enums;
using System.Collections.Generic;

public class PlayerManagementService : IPlayerManagementService
{
    private readonly IPlayerService _playerService;
    private readonly IPlayerTurnService _playerTurnService;
    public PlayerManagementService(IPlayerService playerService, IPlayerTurnService playerTurnService)
    {
        this._playerService = playerService;
        this._playerTurnService = playerTurnService;
    }

    public bool ArePlayersRegistered()
    {
        return this._playerService.PlayersInitialized;
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
            return this._playerTurnService.CheckWhoseTurn() == Color.PlayerColor.White ? this._playerService.WhitePlayer : this._playerService.BlackPlayer;
        }
        return userAgent == this._playerService.WhitePlayer.userAgent && userIp == this._playerService.WhitePlayer.userIp ? this._playerService.WhitePlayer : this._playerService.BlackPlayer;
    }

    public List<Color.PlayerColor> GetUnregisteredPlayers()
    {
        var unregisteredPlayers = new List<Color.PlayerColor>();
        if (this._playerService.WhitePlayer == null) unregisteredPlayers.Add(Color.PlayerColor.White);
        if (this._playerService.BlackPlayer == null) unregisteredPlayers.Add(Color.PlayerColor.Black);
        return unregisteredPlayers;
    }
}