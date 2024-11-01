using ChessAPI.Interfaces;
using System.Text;
using ChessAPI.Models.Enums;
namespace ChessAPI.Services
{
    public class HtmlColorSideSelector : IColorSideSelector
    {
        public HtmlColorSideSelector()
        {

        }

        public string RenderColorSelector(List<Color.PieceColor> pieceColorsToShow)
        {
            const string _pageCss = @"<style>
        * {            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }

        body, html {            height: 100%;
            width: 100%;
            font-family: Arial, sans-serif;
        }

        .container {            display: flex;
            flex-direction: column;
            height: 100%;
            width: 100%;
            text-align: center;
        }

        .selection {            flex: 1;
            display: flex;
            align-items: center;
            justify-content: center;
            color: white;
            font-size: 2rem;
            cursor: pointer;
            transition: transform 0.3s ease;
        }

        .selection.white {            background-color: #ffffff;
            color: #000000;
            border-bottom: 1px solid #ccc;
        }

        .selection.black {            background-color: #000000;
            color: #ffffff;
            border-top: 1px solid #ccc;
        }

        .selection img {            width: 50px;
            height: 50px;
            margin-right: 10px;
            vertical-align: middle;
        }

        .selection:hover {            transform: scale(1.05);
        }
    </style>";
           
           var whiteDiv = !pieceColorsToShow.Contains(Color.PieceColor.White) ? "" : @$"<div class=""selection white"">
            <img src=""https://upload.wikimedia.org/wikipedia/commons/4/42/Chess_klt45.svg"" alt=""White King"">
            Play as White
        </div>";
            var blackDiv = !pieceColorsToShow.Contains(Color.PieceColor.Black) ? "" : @$"<div class=""selection black"">
            <img src=""https://upload.wikimedia.org/wikipedia/commons/f/f0/Chess_kdt45.svg"" alt=""Black King"">
            Play as Black
        </div>";

            const string _pageJsReload = @"<script>document.addEventListener(""DOMContentLoaded"", function() { document.location.href = ""/board""; });</script>";
            
            const string _pageJs = @" <script>
document.addEventListener(""DOMContentLoaded"", function() {
 function sendMoveRequest(color) {
     var xhr = new XMLHttpRequest();
     var url = `/Game/ChooseColor/${color}`;
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
</script>
";
            var html = @$"<!DOCTYPE html>
<html lang=""en"">
<head>
    {_pageCss}
    {(string.IsNullOrWhiteSpace(whiteDiv) && string.IsNullOrEmpty(blackDiv) ? _pageJsReload : _pageJs)}
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Select Your Side</title>
    
</head>
<body>
    <div class=""container"">
       {whiteDiv}
       {blackDiv}
    </div>
</body>
</html>
";

            return html;
        }
    }
}
