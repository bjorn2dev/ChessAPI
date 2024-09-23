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

        public BoardService()
        {

        }

        public string GetInitialBoard()
        {
            var board = new Board();
            var boardDictionary = new Dictionary<Tuple<int, int>, Tile>();

            // using temporary string builder to show board through controller
            // TODO change this!
            var boardStringBuilder = new StringBuilder();
            // add extra styling to show proper chess board
            // TODO change this!
            boardStringBuilder.AppendLine(@"<style>
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
                ");
            boardStringBuilder.AppendLine(_tableStart);

            // file starts from 7 (h) to 0 (a)
            for (int file = board.files - 1; file >= 0; file--)
            {
                boardStringBuilder.AppendLine(_tableRowStart);
                // rank starts from 1 at the bottom and goes up to 8
                for (int rank = 1; rank < board.ranks + 1; rank++)
                {
                    var key = Tuple.Create(rank, file);
                    if (!boardDictionary.ContainsKey(key))
                    {
                        var tile = new Tile();
                        tile.rank = rank;
                        tile.fileNumber = file;
                        tile.color = (rank + file) % 2 == 0; // if the sum of rank + file = even. color = white

                        boardStringBuilder.AppendLine(tile.html);

                        //add tile to the created tuple key position in the dictionary
                        boardDictionary[key] = tile;
                    }
                }
                boardStringBuilder.AppendLine(_tableRowEnd);
            }
            boardStringBuilder.AppendLine(_tableEnd);

            return boardStringBuilder.ToString();
        }
    }
}
