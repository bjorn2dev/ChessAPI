using ChessAPI.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Collections.Concurrent;

namespace ChessAPI.Services
{
    public class GameManagerService : IGameManagerService
    {
        private readonly ConcurrentDictionary<Guid, (IServiceScope scope, IGameService gameService)> _activeGames;
        private readonly IServiceScopeFactory _scopeFactory;

        public GameManagerService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
            _activeGames = new ConcurrentDictionary<Guid, (IServiceScope, IGameService)>();
        }

        public Guid CreateNewGame()
        {
            var scope = _scopeFactory.CreateScope();
            var gameService = scope.ServiceProvider.GetRequiredService<IGameService>();

            var gameId = Guid.NewGuid();
            _activeGames.TryAdd(gameId, (scope, gameService));

            return gameId;
        }

        public IGameService GetGameById(Guid gameId)
        {
            return _activeGames.TryGetValue(gameId, out var gameTuple) ? gameTuple.gameService : null;
        }

        public void EndGame(Guid gameId)
        {
            if (_activeGames.TryRemove(gameId, out var gameTuple))
            {
                // Dispose of the scope to release all scoped services
                gameTuple.scope.Dispose();
            }
        }

        public string GetAllGames()
        {
            return JsonConvert.SerializeObject(_activeGames.Keys);
        }

    }
}
