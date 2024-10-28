using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessAPI.Helpers;
using ChessAPI.Models;
using ChessAPI.Models.Pieces;
namespace ChessAPITests
{
    public class MoveValidatorHelperTests
    {
        private readonly CompleteBoardSetup _completeBoardSetup;
        public MoveValidatorHelperTests()
        {
            _completeBoardSetup = new CompleteBoardSetup();
        }


        [Theory]
        [ClassData(typeof(CompleteBoardSetupClassData))]
        public void Test_DiagonalMovement_Expect_DiagonalMovementType(CompleteBoardSetup boardSetup)
        {
            // Arrange: Fetch specific tiles for testing diagonal movement
            var fromTile = boardSetup._boardStateService.Board.playingFieldDictionary.First(c => c.Value == new Tile { rank = 1, fileNumber = 1}).Value;
            var toTile = boardSetup._boardStateService.Board.playingFieldDictionary.First(c => c.Value == new Tile { rank = 1, fileNumber = 1 }).Value;

            // Act
            var movementType = MoveValidatorHelper.GetMovementType(fromTile, toTile, boardSetup._boardStateService.Board);

            // Assert that the detected movement type matches the expected type
            Assert.Equal(MovementType.Diagonal, movementType);
        }
    }
}
