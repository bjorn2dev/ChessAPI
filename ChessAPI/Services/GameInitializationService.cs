using ChessAPI.Interfaces;
using ChessAPI.Models.Enums;

public class GameInitializationService : IGameInitializationService
{
    private readonly IBoardService _boardService;
    private readonly IPlayerManagementService _playerManagementService;

    public GameInitializationService(IBoardService boardService, IPlayerManagementService playerManagementService)
    {
        _boardService = boardService;
        _playerManagementService = playerManagementService;
    }

    public void InitializeGame()
    {
        if (_playerManagementService.ArePlayersRegistered())
        {
            _boardService.InitializeBoard();
        }
    }
}