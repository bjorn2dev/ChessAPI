using ChessAPI.Interfaces;
using System.Text;
using ChessAPI.Models.Enums;
namespace ChessAPI.Services
{
    public class HtmlDashboardRenderer : IDashboardRenderer
    {
        public HtmlDashboardRenderer()
        {

        }

        public string Render(List<Color.PlayerColor> pieceColorsToShow)
        {
            const string _pageCss = @"<style>
        body {
            margin: 0;
            padding: 0;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            background-color: white;
            font-family: Arial, sans-serif;
        }

        #container {
            text-align: center;
        }

        .option {
            margin: 20px auto;
            padding: 20px;
            border: 1px solid #ccc;
            border-radius: 8px;
            width: 300px;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
            transition: transform 0.2s, box-shadow 0.2s;
        }

        .option:hover {
            transform: scale(1.05);
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
        }

        .sub-options {
            margin-top: 10px;
            display: flex;
            justify-content: space-around;
        }

        button {
            padding: 10px 20px;
            background-color: #007BFF;
            color: white;
            border: none;
            border-radius: 4px;
            cursor: pointer;
        }

        button:hover {
            background-color: #0056b3;
        }
    </style>";

            const string _pageJsReload = @"<script>document.addEventListener(""DOMContentLoaded"", function() { document.location.href = ""/board""; });</script>";
            
            const string _pageJs = @" <script>
document.addEventListener(""DOMContentLoaded"", function() {
 function sendMoveRequest(color) {
     var xhr = new XMLHttpRequest();
    var gameId = location.pathname.split(""/"")[2];
     var url = `/Game/${gameId}/ChooseColor/${color}`;
     xhr.open(""PUT"", url, true);

     // Set request headers if necessary (e.g., Content-Type)
     xhr.setRequestHeader(""Content-Type"", ""application/json"");

     xhr.onload = function () {
         location.reload(); 
     };

     xhr.onerror = function () {
         console.log(""Request failed"");
     };

     xhr.send(); // Send the request
 }

document.querySelectorAll("".selection"").forEach(function (element) {
        element.addEventListener(""click"", function () {
            var color = element.classList.contains(""white"") ? ""White"" : ""Black"";
            sendMoveRequest(color);
        });
    });

});
</script>
"; // {(string.IsNullOrWhiteSpace(whiteDiv) && string.IsNullOrEmpty(blackDiv) ? _pageJsReload : _pageJs)}
            var html = @$"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Chess Game</title>
    {_pageCss}
</head>
<body>
    <div id=""container"">
        <div class=""option"">
            <h2>New Game (vs Computer)</h2>
            <div class=""sub-options"">
                <button onclick=""startNewGame('white')"">Play as White</button>
                <button onclick=""startNewGame('black')"">Play as Black</button>
            </div>
        </div>
        <div class=""option"">
            <h2>New Game (Online)</h2>
            <button onclick=""startOnlineGame()"">Start Online Game</button>
        </div>
        <div class=""option"">
            <h2>New Game (vs Friend Couch Co-op)</h2>
            <button onclick=""startCouchCoop()"">Start Couch Co-op Game</button>
        </div>
    </div>
</body>
</html>
";

            return html;
        }
    }
}
