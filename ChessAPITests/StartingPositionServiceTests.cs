using ChessAPI.Interfaces;
using ChessAPI.Models;
using ChessAPI.Models.Pieces;
using ChessAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessAPITests
{
    public class StartingPositionServiceTests
    {
        private IPositionProvider _startingPositionProvider;
        public StartingPositionServiceTests()
        {
            // Use ConfigurationBuilder to load the appsettings file in the test project’s output directory
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory) // Sets the path to the output directory
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var settings = configuration.GetSection("StartingPosition").Get<StartingPositionSettings>();
            IOptions<StartingPositionSettings> options = Options.Create(settings);
            _startingPositionProvider = new PositionProvider(options);
        }

        [Theory]
        [InlineData("E1", typeof(King))]
        [InlineData("E8", typeof(King))]
        public void Test_StartingPosition_King_Expect_True(string location, Type expectedType)
        {
            // Act
            var pieceType = _startingPositionProvider.GetPieceTypeForLocation(location);

            // Assert
            Assert.NotNull(pieceType);
            Assert.Equal(expectedType, pieceType);
        }

        [Theory]
        [InlineData("D1", typeof(Queen))]
        [InlineData("D8", typeof(Queen))]
        public void Test_StartingPosition_Queen_Expect_True(string location, Type expectedType)
        {
            // Act
            var pieceType = _startingPositionProvider.GetPieceTypeForLocation(location);

            // Assert
            Assert.NotNull(pieceType);
            Assert.Equal(expectedType, pieceType);
        }

        [Theory]
        [InlineData("A1", typeof(Rook))]
        [InlineData("H1", typeof(Rook))]
        [InlineData("A8", typeof(Rook))]
        [InlineData("H8", typeof(Rook))]
        public void Test_StartingPosition_Rook_Expect_True(string location, Type expectedType)
        {
            // Act
            var pieceType = _startingPositionProvider.GetPieceTypeForLocation(location);

            // Assert
            Assert.NotNull(pieceType);
            Assert.Equal(expectedType, pieceType);
        }

        [Theory]
        [InlineData("C1", typeof(Bishop))]
        [InlineData("F1", typeof(Bishop))]
        [InlineData("C8", typeof(Bishop))]
        [InlineData("F8", typeof(Bishop))]
        public void Test_StartingPosition_Bishop_Expect_True(string location, Type expectedType)
        {
            // Act
            var pieceType = _startingPositionProvider.GetPieceTypeForLocation(location);

            // Assert
            Assert.NotNull(pieceType);
            Assert.Equal(expectedType, pieceType);
        }

        [Theory]
        [InlineData("B1", typeof(Knight))]
        [InlineData("G1", typeof(Knight))]
        [InlineData("B8", typeof(Knight))]
        [InlineData("G8", typeof(Knight))]
        public void Test_StartingPosition_Knight_Expect_True(string location, Type expectedType)
        {
            // Act
            var pieceType = _startingPositionProvider.GetPieceTypeForLocation(location);

            // Assert
            Assert.NotNull(pieceType);
            Assert.Equal(expectedType, pieceType);
        }

        [Theory]
        [InlineData("A2", typeof(Pawn))]
        [InlineData("B2", typeof(Pawn))]
        [InlineData("C2", typeof(Pawn))]
        [InlineData("D2", typeof(Pawn))]
        [InlineData("E2", typeof(Pawn))]
        [InlineData("F2", typeof(Pawn))]
        [InlineData("G2", typeof(Pawn))]
        [InlineData("H2", typeof(Pawn))]
        [InlineData("A7", typeof(Pawn))]
        [InlineData("B7", typeof(Pawn))]
        [InlineData("C7", typeof(Pawn))]
        [InlineData("D7", typeof(Pawn))]
        [InlineData("E7", typeof(Pawn))]
        [InlineData("F7", typeof(Pawn))]
        [InlineData("G7", typeof(Pawn))]
        [InlineData("H7", typeof(Pawn))]
        public void Test_StartingPosition_Pawn_Expect_True(string location, Type expectedType)
        {
            // Act
            var pieceType = _startingPositionProvider.GetPieceTypeForLocation(location);

            // Assert
            Assert.NotNull(pieceType);
            Assert.Equal(expectedType, pieceType);
        }

    }
}
