using ChessAPI.Controllers;
using ChessAPI.Factories;
using ChessAPI.Interfaces.Board;
using ChessAPI.Interfaces.Game;
using ChessAPI.Interfaces.Piece;
using ChessAPI.Interfaces.Player;
using ChessAPI.Interfaces.Renderer;
using ChessAPI.Models;
using ChessAPI.Models.Pieces;
using ChessAPI.Services.Board;
using ChessAPI.Services.Game;
using ChessAPI.Services.Piece;
using ChessAPI.Services.Player;
using ChessAPI.Services.Renderer.Html;
using ChessAPI.Services.Renderers.Html;
using System;

namespace ChessAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // add options
            builder.Services.Configure<StartingPositionSettings>(builder.Configuration.GetSection("StartingPosition"));
            builder.Services.Configure<GameSettings>(builder.Configuration.GetSection("GameSettings"));

            // Add services to the container.

            // Tracks all games as a Singleton
            builder.Services.AddSingleton<IGameManagerService, GameManagerService>(); 

            // Scoped services, specific to each game instance
            builder.Services.AddScoped<IGameService, GameService>();
            builder.Services.AddScoped<IGameGenerator, GameGenerator>();
            builder.Services.AddScoped<IGameInitializationService, GameInitializationService>();
            builder.Services.AddScoped<IGameRenderingService, GameRenderingService>();
            builder.Services.AddScoped<IGameMoveValidator, GameMoveValidator>();

            builder.Services.AddScoped<IPlayerService, PlayerService>();
            builder.Services.AddScoped<IPlayerTurnService, PlayerTurnService>();

            builder.Services.AddScoped<IPlayerManagementService, PlayerManagementService>();
            builder.Services.AddScoped<IBoardStateService, BoardStateService>();
            builder.Services.AddScoped<IBoardService, BoardService>();

            // Singleton services for stateless rendering
            builder.Services.AddSingleton<IPieceMoveValidator, PieceMoveValidator>();
            builder.Services.AddSingleton<IBoardRenderer, HtmlBoardRenderer>();
            builder.Services.AddSingleton<IBoardGenerator, BoardGenerator>();

            // Transient for services used briefly and independently per operation
            builder.Services.AddTransient<IKingSafetyValidator, KingSafetyValidator>();
            builder.Services.AddTransient<IBoardSimulationService, BoardSimulationService>();
            builder.Services.AddTransient<IColorSideSelector, HtmlColorSideSelector>();
            builder.Services.AddTransient<ITileRenderer, HtmlTileRenderer>();
            builder.Services.AddTransient<IPieceRenderer, HtmlPieceRenderer>();
            builder.Services.AddTransient<IPositionProvider, PositionProvider>();
            builder.Services.AddTransient<IPawnValidator, PawnValidator>();
            builder.Services.AddTransient<IPieceFactory, PieceFactory>();
            builder.Services.AddTransient<IPieceMovingService, PieceMovingService>();
            builder.Services.AddTransient<King>();
            builder.Services.AddTransient<Pawn>();

            builder.Services.AddControllersWithViews();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
