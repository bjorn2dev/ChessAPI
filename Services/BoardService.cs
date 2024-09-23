using ChessAPI.Controllers;
using ChessAPI.Interfaces;
using ChessAPI.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Text;
using static ChessAPI.Models.Enums.Color;

namespace ChessAPI.Services
{
    public class BoardService : IBoardService
    {
        const string _tableStart = "<table class=\"chessboard\">";
        const string _tableRowStart = "<tr>";
        const string _tableEnd = "</table>";
        const string _tableRowEnd = "</tr>";
        const string _tableCss = @"<style>
                .chessboard {
                    border-collapse: collapse;
                    width: 400px;
                    height: 400px;
                    margin: 20px auto;
                }

                    .chessboard td {
                        width: 50px;
                        height: 50px;
                        text-align: center;
                        vertical-align: middle;
                        font-weight: bold;
                        font-size: 18px;
                    }

                .dark-square {
                    background-color: #769656; /* Donker groen */
                    color: white;
                }

                .light-square {
                    background-color: #eeeed2; /* Licht beige */
                    color: black;
                }
                </style>
                ";

        private StringBuilder _boardStringBuilder = new StringBuilder();
        private Board _board;


        public BoardService()
        {
            _boardStringBuilder = new StringBuilder();
            _board = new Board();
        }

        private Dictionary<Tuple<int, int>, Tile> SetupBoard()
        {
            var boardDictionary = new Dictionary<Tuple<int, int>, Tile>();
            // rank starts from 1 at the bottom and goes up to 8
            for (int rank = _board.ranks; rank >= 1; rank--)
            {
                _boardStringBuilder.AppendLine(_tableRowStart);
                // file starts from 7 (h) to 0 (a)
                for (int file = 0; file < _board.files; file++)
                {
                    var key = Tuple.Create(rank, file);
                    if (!boardDictionary.ContainsKey(key))
                    {
                        var tile = new Tile();
                        tile.rank = rank;
                        tile.fileNumber = file;
                        tile.color = (rank + file) % 2 == 0; // if the sum of rank + file = even. color = white

                        _boardStringBuilder.AppendLine(tile.html);

                        //add tile to the created tuple key position in the dictionary
                        boardDictionary[key] = tile;
                    }
                }
                _boardStringBuilder.AppendLine(_tableRowEnd);
            }
            return boardDictionary;
        }

        public string GetInitialBoard()
        {
            _boardStringBuilder.AppendLine(_tableCss);
            _boardStringBuilder.AppendLine(_tableStart);

            this.SetupBoard();
                
            _boardStringBuilder.AppendLine(_tableEnd);

            return _boardStringBuilder.ToString();
        }
    }
}
