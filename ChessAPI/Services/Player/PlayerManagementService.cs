using ChessAPI.Interfaces.Player;
using ChessAPI.Models;
using ChessAPI.Models.Enums;
using ChessAPI.Models.Pieces;
using System.Collections.Generic;

public class PlayerManagementService : IPlayerManagementService
{
    private readonly IPlayerService _playerService;
    private readonly IPlayerTurnService _playerTurnService;

    public PlayerManagementService(IPlayerService playerService, IPlayerTurnService playerTurnService)
    {
        _playerService = playerService;
        _playerTurnService = playerTurnService;
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

    public User? GetPlayerByInfo(string userAgent, string userIp, Color.PlayerColor playerColor)
    {
        if (!this.ArePlayersRegistered()) return null;

        if (String.Equals($"{userAgent}_{playerColor}", this._playerService.WhitePlayer.userAgent, StringComparison.OrdinalIgnoreCase) &&
            String.Equals($"{userIp}_{playerColor}", this._playerService.WhitePlayer.userIp, StringComparison.OrdinalIgnoreCase)) return this._playerService.WhitePlayer;

        if (String.Equals($"{userAgent}_{playerColor}", this._playerService.BlackPlayer.userAgent, StringComparison.OrdinalIgnoreCase) &&
           String.Equals($"{userIp}_{playerColor}", this._playerService.BlackPlayer.userIp, StringComparison.OrdinalIgnoreCase)) return this._playerService.BlackPlayer;

        return null;
    }    

    public List<Color.PlayerColor> GetUnregisteredPlayers()
    {
        var unregisteredPlayers = new List<Color.PlayerColor>();
        if (this._playerService.WhitePlayer == null) unregisteredPlayers.Add(Color.PlayerColor.White);
        if (this._playerService.BlackPlayer == null) unregisteredPlayers.Add(Color.PlayerColor.Black);
        return unregisteredPlayers;
    }
}