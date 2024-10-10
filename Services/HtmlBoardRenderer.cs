using ChessAPI.Interfaces;
using ChessAPI.Models;
using System.Text;

namespace ChessAPI.Services
{
    /// <summary>
    /// HtmlBoardRenderer class (responsible for rendering the board as HTML)
    /// </summary>
    public class HtmlBoardRenderer :IBoardRenderer
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
                }

                .light-square {
                    background-color: #eeeed2; /* Licht beige */
                }
                </style>
                ";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public string RenderBoard(SortedList<Tuple<int, int>, Tile> board)
        {
            var boardStringBuilder = new StringBuilder();
            boardStringBuilder.AppendLine(_tableCss);
            boardStringBuilder.AppendLine(_tableStart);

            // Rendering the tiles based on their positions in the dictionary
            for (int rank = 8; rank >= 1; rank--)
            {
                boardStringBuilder.AppendLine(_tableRowStart);
                for (int file = 0; file < 8; file++)
                {
                    var key = Tuple.Create(rank, file);
                    var tile = board[key];
                    boardStringBuilder.AppendLine(tile.html);
                }
                boardStringBuilder.AppendLine(_tableRowEnd);
            }

            boardStringBuilder.AppendLine(_tableEnd);
            return boardStringBuilder.ToString();
        }
    }
}
