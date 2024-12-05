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

        const string _promotionBlock = @"<div id=""promotion-picker"" style=""display: none; text-align: center; margin-bottom: 10px;"">
    <p>Select a piece for promotion:</p>
    <button class=""promotion-option"" data-piece=""Queen"">Queen</button>
    <button class=""promotion-option"" data-piece=""Rook"">Rook</button>
    <button class=""promotion-option"" data-piece=""Bishop"">Bishop</button>
    <button class=""promotion-option"" data-piece=""Knight"">Knight</button>
</div>";
        const string _tableStart = "<table class=\"chessboard\">";
        const string _tableRowStart = "<tr>";
        const string _tableEnd = "</table>";
        const string _tableRowEnd = "</tr>";
        const string _pageCss = @"    <style>
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

#promotion-picker {
    background-color: #f9f9f9;
    padding: 10px;
    border: 1px solid #ccc;
    border-radius: 5px;
    width: 50%;
    margin: 0 auto;
}

.promotion-option {
    padding: 10px 15px;
    margin: 5px;
    border: none;
    background-color: #769656;
    color: white;
    cursor: pointer;
}

.promotion-option:hover {
    background-color: #5a8f45;
}

    </style>
                ";
        const string _pageJs = @"<script>
document.addEventListener(""DOMContentLoaded"", function() {
    localStorage.clear();



const promotionPicker = document.getElementById(""promotion-picker"");

// Function to send the XHR request
function sendMoveRequest(from, to, promotionType = null) {
	const xhr = new XMLHttpRequest();
	const gameId = location.pathname.split(""/"")[2];
	let url = `/Game/${gameId}/move/${from}/${to}`;
	if (promotionType) {
		url += `?promotionType=${promotionType}`;
	}

	xhr.open(""PUT"", url, true);
	xhr.setRequestHeader(""Content-Type"", ""application/json"");

	xhr.onload = function () {
		    localStorage.clear();
		promotionPicker.style.display = ""none""; // Hide the picker after the move
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

	xhr.send();
}

// Event listener for promotion options
document.querySelectorAll("".promotion-option"").forEach((button) => {
	button.addEventListener(""click"", function () {
		const promotionType = this.dataset.piece;
		const from = localStorage.getItem(""firstClick"");
		const to = localStorage.getItem(""promotionTarget"");
		if (from && to && promotionType) {
			sendMoveRequest(from, to, promotionType);
		}
	});
});

function isPromotion(from, to) {
	// Check if it's a promotion move

            var pieceType = localStorage.getItem(""firstClickType"");
			const isPromotion = pieceType != null && pieceType == ""pawn"" &&
				((to.endsWith(""8"") && from.endsWith(""7"")) || // White pawn promotion
				(to.endsWith(""1"") && from.endsWith(""2""))); // Black pawn promotion

			if (isPromotion) {
				promotionPicker.style.display = ""block""; // Show the picker
				localStorage.setItem(""promotionTarget"", to);
			} else {
				sendMoveRequest(from, to);
			}
}

// Add event listeners for all <p> tags
Array.from(document.getElementsByTagName(""p"")).forEach(function (element) {
	element.addEventListener(""click"", function (event) {
		event.stopPropagation();
		const parent = event.target.closest(""td"");
		const tileAnnotation = parent.dataset.tileAnnotation;
        const pieceType = element.querySelector(""img"").dataset.name;
		if (localStorage.getItem(""firstClick"") == null) {
			// Set first click in localStorage
			localStorage.setItem(""firstClick"", tileAnnotation);
			localStorage.setItem(""firstClickType"", pieceType);
			console.log(""Set first click"", tileAnnotation);
		} else {
			// Second click, handle the move
			const from = localStorage.getItem(""firstClick"");
			const to = tileAnnotation;

			isPromotion(from, to, pieceType);
		}
	});
});

// Add event listeners for all <td> tags
    Array.prototype.slice.call(document.getElementsByTagName(""td"")).forEach(function(element) {
        element.addEventListener(""click"", function(event) {
            var to = event.target.dataset.tileAnnotation;

            if (localStorage.getItem(""firstClick"") != null) {
                var from = localStorage.getItem(""firstClick"");
                isPromotion(from, to);
                
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
            boardStringBuilder.AppendLine(_pageCss);
            boardStringBuilder.AppendLine(_promotionBlock);
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
