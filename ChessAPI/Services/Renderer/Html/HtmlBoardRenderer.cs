using ChessAPI.Interfaces.Renderer;
using ChessAPI.Models;
using ChessAPI.Models.Enums;
using System.Text;

namespace ChessAPI.Services.Renderer.Html
{
    /// <summary>
    /// HtmlBoardRenderer class (responsible for rendering the board as HTML)
    /// </summary>
    public class HtmlBoardRenderer : IBoardRenderer
    {
        const string _tableStart = "<table class=\"chessboard\">";
        const string _tableRowStart = "<tr>";
        const string _tableEnd = "</table>";
        const string _tableRowEnd = "</tr>";
        const string _tableCss = @"    <style>
        /* Ensure the body and html cover the full screen */
        html, body {
            height: 100%;
            margin: 0;
            padding: 0;
            display: flex;
            justify-content: center;
            align-items: center;
        }

        /* Make the chessboard table take the full screen width and height while keeping the square shape */
        .chessboard {
            width: 100vmin; /* Use the minimum value between viewport width and height to maintain square */
            height: 100vmin; /* Make sure the height and width are equal for the chessboard */
            border-collapse: collapse;
        }

        /* Ensure each cell is square and resizable */
        .chessboard td {
            width: 12.5%; /* 100% divided by 8 columns = 12.5% */
            height: 12.5%; /* Same as width to ensure the squares are equal */
            text-align: center;
            vertical-align: middle;
            font-weight: bold;
            font-size: calc(6vmin); /* Responsive font size based on the viewport */
            position: relative;
        }

        /* Dark and light squares for the chessboard */
        .dark-square {
            background-color: #769656; /* Dark green */
        }

        .light-square {
            background-color: #eeeed2; /* Light beige */
        }

        /* Chess pieces in p tags, centered and scaled */
        td p {
            margin: 0;
            padding: 0;
            font-size: inherit; /* Inherit font size from td */
            line-height: 1;
        }
    </style>
                ";
        const string _pageJs = @"<script>
document.addEventListener(""DOMContentLoaded"", function() {
    localStorage.clear();

    // Function to send the XHR request
    function sendMoveRequest(from, to) {
        var xhr = new XMLHttpRequest();
        var gameId = location.pathname.split(""/"")[2];
        var url = `/Game/${gameId}/move/${from}/${to}`;
        xhr.open(""PUT"", url, true);

        // Set request headers if necessary (e.g., Content-Type)
        xhr.setRequestHeader(""Content-Type"", ""application/json"");

        xhr.onload = function () {
            localStorage.removeItem(""firstClick"");
            if (xhr.status === 204) {
                console.log(""Move was successful"");
                location.reload(); 
            } else {
                console.log(""Move failed: "" + xhr.status);
            }
        };

        xhr.onerror = function () {
            console.log(""Request failed"");
        };

        xhr.send(); // Send the request
    }

    // Add event listeners for all <p> tags
    Array.prototype.slice.call(document.getElementsByTagName(""p"")).forEach(function(element) {
        element.addEventListener(""click"", function(event) {
            event.stopPropagation();
            var parent = event.target.closest(""td"");
            var tileAnnotation = parent.dataset.tileAnnotation;

            if (localStorage.getItem(""firstClick"") == null) {
                // Set first click in localStorage
                localStorage.setItem(""firstClick"", tileAnnotation);
                console.log(""set first click"", tileAnnotation);
            } else {
                // Second click, send the move request
                var from = localStorage.getItem(""firstClick"");
                var to = tileAnnotation;
                sendMoveRequest(from, to); // Trigger the move request
            }
        });
    });

    // Add event listeners for all <td> tags
    Array.prototype.slice.call(document.getElementsByTagName(""td"")).forEach(function(element) {
        element.addEventListener(""click"", function(event) {
            var to = event.target.dataset.tileAnnotation;

            if (localStorage.getItem(""firstClick"") != null) {
                var from = localStorage.getItem(""firstClick"");
                sendMoveRequest(from, to); // Trigger the move request
            }
        });
    });
});
</script>
";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public string RenderBoard(SortedList<Tuple<int, int>, Tile> board, Color.PlayerColor showBoardForPlayer)
        {
            var boardStringBuilder = new StringBuilder();
            boardStringBuilder.AppendLine(_pageJs);
            boardStringBuilder.AppendLine(_tableCss);
            boardStringBuilder.AppendLine(_tableStart);


            if (showBoardForPlayer == Color.PlayerColor.White)
            {
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
            }
            else
            {
                // Rendering the tiles based on their positions in the dictionary
                for (int rank = 1; rank <= 8; rank++) // Start from 1, go to 8
                {
                    boardStringBuilder.AppendLine(_tableRowStart);
                    for (int file = 7; file >= 0; file--) // Start from 7, go to 0
                    {
                        var key = Tuple.Create(rank, file);
                        var tile = board[key];
                        boardStringBuilder.AppendLine(tile.html);
                    }
                    boardStringBuilder.AppendLine(_tableRowEnd);
                }
            }
            boardStringBuilder.AppendLine(_tableEnd);
            return boardStringBuilder.ToString();
        }
    }
}
