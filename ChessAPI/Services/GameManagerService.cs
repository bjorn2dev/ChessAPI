﻿using ChessAPI.Interfaces;
using System.Collections.Concurrent;

namespace ChessAPI.Services
{
    public class GameManagerService : IGameManagerService
    {
        private readonly ConcurrentDictionary<Guid, IGameService> _activeGames;
        private IPlayerService _playerService;
        private IGameGenerator _gameGenerator;
        private IPlayerTurnService _turnService;
        private IPieceMovingService _peceMovingService;

        public GameManagerService()
        {
            _activeGames = new ConcurrentDictionary<Guid, IGameService>();
        }

        public Guid CreateNewGame()
        {
            _playerService = new PlayerService();
            _gameGenerator = new PlayerTurnService();
            var gameId = Guid.NewGuid();
            var gameService = new GameService(/* inject dependencies here */);

            _activeGames.TryAdd(gameId, gameService);
            return gameId;
        }

        public IGameService GetGameById(Guid gameId)
        {
            return _activeGames.TryGetValue(gameId, out var gameService) ? gameService : null;
        }

        public void EndGame(Guid gameId)
        {
            _activeGames.TryRemove(gameId, out _);
        }

    }
}