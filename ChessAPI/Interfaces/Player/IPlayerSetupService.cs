﻿using ChessAPI.Models;
using ChessAPI.Models.Enums;

namespace ChessAPI.Interfaces.Player
{
    public interface IPlayerSetupService
    {
        User SetupPlayer(Color.PlayerColor playerColor, string userAgent, string userIp);
    }
}
