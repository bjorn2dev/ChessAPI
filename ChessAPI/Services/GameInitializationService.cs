using ChessAPI.Interfaces;
using ChessAPI.Models.Enums;

public class GameInitializationService : IGameInitializationService
{
    private readonly IBoardService _boardService;
    private readonly IPlayerManagementService _playerManagementService;

    public GameInitializationService(IBoardService boardService, IPlayerManagementService playerManagementService)
    {
        this._boardService = boardService;
        this._playerManagementService = playerManagementService;
    }

    public void InitializeGame()
    {
        if (this._playerManagementService.ArePlayersRegistered() && !this._boardService.BoardInitialized)
        {
            this._boardService.InitializeBoard();
        }
    }
}