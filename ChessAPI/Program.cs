using ChessAPI.Controllers;
using ChessAPI.Interfaces;
using ChessAPI.Models;
using ChessAPI.Services;
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
            builder.Services.AddSingleton<IGameManagerService, GameManagerService>(); // Tracks all games as a Singleton

            // Scoped services, specific to each game instance
            builder.Services.AddScoped<IGameService, GameService>();
            builder.Services.AddScoped<IBoardService, BoardService>();
            builder.Services.AddScoped<IPlayerService, PlayerService>();
            builder.Services.AddScoped<IGameGenerator, GameGenerator>();
            builder.Services.AddScoped<IBoardStateService, BoardStateService>();
            builder.Services.AddScoped<IPlayerTurnService, PlayerTurnService>();

            // Singleton services for stateless rendering
            builder.Services.AddSingleton<IPieceMoveValidator, PieceMoveValidator>();
            builder.Services.AddSingleton<IBoardGenerator, BoardGenerator>();
            builder.Services.AddSingleton<IColorSideSelector, HtmlColorSideSelector>();
            builder.Services.AddSingleton<IPlayerSetupService, PlayerSetupService>();
            builder.Services.AddSingleton<IStartingPositionProvider, StartingPositionService>();
            builder.Services.AddSingleton<IPieceRenderer, HtmlPieceRenderer>();
            builder.Services.AddSingleton<ITileRenderer, HtmlTileRenderer>();
            builder.Services.AddSingleton<IBoardRenderer, HtmlBoardRenderer>();

            // Transient for services used briefly and independently per operation
            builder.Services.AddTransient<IPieceMovingService, PieceMovingService>();

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
