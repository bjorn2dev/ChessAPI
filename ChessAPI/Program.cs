
using ChessAPI.Helpers;
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


            builder.Services.AddSingleton<IColorSideSelector, HtmlColorSideSelector>();
            builder.Services.AddSingleton<IGameService, GameService>();
            builder.Services.AddSingleton<IGameGenerator, GameGenerator>();
            builder.Services.AddSingleton<IPlayerTurnService, PlayerTurnService>();
            builder.Services.AddSingleton<IBoardStateService, BoardStateService>();
            builder.Services.AddSingleton<IBoardGenerator, BoardGenerator>();
            builder.Services.AddSingleton<IPieceHtmlRenderer, PieceHtmlRenderer>();
            builder.Services.AddSingleton<ITileRenderer, TileHtmlRenderer>();
            builder.Services.AddSingleton<IPieceMoveValidator, PieceMoveValidator>();
            builder.Services.AddSingleton<IPieceMovingService, PieceMovingService>();
            builder.Services.AddSingleton<IStartingPositionProvider, StartingPositionService>();  
            builder.Services.AddSingleton<IBoardRenderer, HtmlBoardRenderer>();
            builder.Services.AddSingleton<IBoardService, BoardService>();
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
