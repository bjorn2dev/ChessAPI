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
Array.prototype.slice.call(document.getElementsByTagName(""p"") ).forEach(function(element) {
   element.addEventListener(""click"", function(event) {
        event.stopPropagation();
        if(localStorage.getItem(""firstClick"") == null) {
            
            var parent = event.target.parentElement;
            var tileAnnotation = parent.dataset.tileAnnotation;

	        localStorage.setItem(""firstClick"", tileAnnotation);
            console.log(""set first click"", tileAnnotation);
        } 
   });
});
Array.prototype.slice.call(document.getElementsByTagName(""td"")).forEach(function(element) {
    element.addEventListener(""click"", function(event) {
        // Check if the <td> doesn't have a <p> and if a first click is stored in localStorage
        if (event.target.querySelector(""p"") == null && localStorage.getItem(""firstClick"") != null) {
            var from = localStorage.getItem(""firstClick"");
            var to = event.target.dataset.tileAnnotation;

            // Prepare the PUT request
            var xhr = new XMLHttpRequest();
            var url = `/Board/${from}/${to}`;
            xhr.open(""PUT"", url, true);

            // Set request headers if necessary (e.g., Content-Type, Authorization)
            xhr.setRequestHeader(""Content-Type"", ""application/json"");

            // Optional: define what to do when the request finishes successfully
            xhr.onload = function () {
                if (xhr.status === 204) {
                    localStorage.removeItem(""firstClick"");
                    console.log(""Move was successful"");
                    location.reload(); 
                } else {
                    console.log(""Move failed: "" + xhr.status);
                }
            };

            // Optional: define what to do in case of an error
            xhr.onerror = function () {
                console.log(""Request failed"");
            };

            // Send the request without a body (since we're only dealing with the URL parameters here)
            xhr.send();            
        }
    });
});
});
</script>";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public string RenderBoard(SortedList<Tuple<int, int>, Tile> board)
        {
            var boardStringBuilder = new StringBuilder();
            boardStringBuilder.AppendLine(_pageJs);
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
