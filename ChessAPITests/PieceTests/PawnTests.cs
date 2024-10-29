using ChessAPI.Models.Pieces;
using ChessAPI.Models;
using ChessAPITests;

public class PawnTests
{
    // create static board setups to avoid creating new instances for every separate test.
    private static readonly CompleteBoardSetup _standardBoardSetup = new CompleteBoardSetup("StartingPosition");
    private static readonly CompleteBoardSetup _captureBoardSetup = new CompleteBoardSetup("CaptureStartingPosition");

    public static IEnumerable<object[]> GetCaptureBoardSetupData()
    {
        yield return new object[] { _captureBoardSetup };
    }

    public static IEnumerable<object[]> GetStandardBoardSetupData()
    {
        yield return new object[] { _standardBoardSetup };
    }

    public static IEnumerable<object[]> GetInvalidPawnMoves()
    {
        yield return new object[] { _standardBoardSetup.GetTile(2, 1), _standardBoardSetup.GetTile(2, 2), MovementType.Vertical };  // A2 to A3
    }
    public static IEnumerable<object[]> GetStandardPawnMoves()
    {
        yield return new object[] { _standardBoardSetup.GetTile(2, 1), _standardBoardSetup.GetTile(3, 1), MovementType.Vertical };  // A2 to A3
        //yield return new object[] { StandardBoardSetup.GetTile(2, 1), StandardBoardSetup.GetTile(2, 2), MovementType.Horizontal };  // A2 to A4 for initial two-square move
    }

    public static IEnumerable<object[]> GetCapturePawnMoves()
    {
        yield return new object[] { _captureBoardSetup.GetTile(6, 3), _captureBoardSetup.GetTile(7, 4), MovementType.Vertical };  // D6 to E7
        yield return new object[] { _captureBoardSetup.GetTile(6, 3), _captureBoardSetup.GetTile(7, 2), MovementType.Vertical };  // D6 to C7
    }

    [Theory]
    [MemberData(nameof(GetStandardPawnMoves))]
    public void Test_PawnMovementWithinLimits_Expect_True(Tile fromTile, Tile toTile, MovementType expectedType)
    {
        // Act
        bool isValidMove = new Pawn().IsValidMovement(fromTile, toTile, _standardBoardSetup._boardStateService.Board, expectedType);
        // Assert
        Assert.True(isValidMove);
    }

    [Theory]
    [MemberData(nameof(GetInvalidPawnMoves))]
    public void Test_PawnMovementWithinLimits_Expect_False(Tile fromTile, Tile toTile, MovementType expectedType)
    {
        // Act
        bool isInvalidMove = new Pawn().IsValidMovement(fromTile, toTile, _standardBoardSetup._boardStateService.Board, expectedType);
        // Assert
        Assert.False(isInvalidMove);
    }

    [Theory]
    [MemberData(nameof(GetCapturePawnMoves))]
    public void Test_PawnMovementCapture(Tile fromTile, Tile toTile, MovementType expectedType)
    {
        // Act
        bool isValidMove = new Pawn().IsValidCapture(fromTile, toTile, _captureBoardSetup._boardStateService.Board);
        // Assert
        Assert.True(isValidMove);
    }
}