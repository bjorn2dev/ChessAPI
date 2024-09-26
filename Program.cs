
using ChessAPI.Interfaces;
using ChessAPI.Models.Pieces;
using ChessAPI.Services;
using Microsoft.Extensions.Configuration;
using System;

namespace ChessAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.Configure<StartingLocation>(builder.Configuration.GetSection("StartingLocation"));
            builder.Services.AddSingleton<IBoardGenerator, BoardGenerator>();
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
