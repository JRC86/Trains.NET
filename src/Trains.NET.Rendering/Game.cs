﻿using System;
using System.Collections.Generic;
using SkiaSharp;
using Trains.NET.Engine;

namespace Trains.NET.Rendering
{
    internal class Game : IGame
    {
        private int _width;
        private int _height;

        private readonly IGameBoard _gameBoard;
        private readonly IEnumerable<IBoardRenderer> _boardRenderers;
        private readonly IPixelMapper _pixelMapper;

        public Tool CurrentTool { get; set; }

        public Game(IGameBoard gameBoard, IEnumerable<IBoardRenderer> boardRenderers, IPixelMapper pixelMapper)
        {
            _gameBoard = gameBoard;
            _boardRenderers = boardRenderers;
            _pixelMapper = pixelMapper;
        }

        public void SetSize(int width, int height)
        {
            (int columns, int rows) = _pixelMapper.PixelsToCoords(width, height);
            columns = Math.Max(columns, 1);
            rows = Math.Max(rows, 1);

            (_width, _height) = _pixelMapper.CoordsToPixels(columns, rows);

            _gameBoard.Columns = columns;
            _gameBoard.Rows = rows;
        }

        public void Render(SKCanvas canvas)
        {
            if (canvas is null)
            {
                throw new ArgumentNullException(nameof(canvas));
            }

            canvas.Translate(1, 1);
            canvas.Clear(SKColors.White);
            canvas.ClipRect(new SKRect(0, 0, _width + 2, _height + 2), SKClipOperation.Intersect, false);

            foreach (IBoardRenderer renderer in _boardRenderers)
            {
                if (!renderer.Enabled)
                {
                    continue;
                }

                canvas.Save();
                renderer.Render(canvas, _width, _height);
                canvas.Restore();
            }
        }

        public void OnMouseDown(int x, int y, bool isRightMouseDown)
        {
            (int column, int row) = _pixelMapper.PixelsToCoords(x, y);

            if (this.CurrentTool == Tool.Eraser || isRightMouseDown)
            {
                _gameBoard.RemoveTrack(column, row);
            }
            else if (this.CurrentTool == Tool.Track)
            {
                _gameBoard.AddTrack(column, row);
            }
        }
    }
}
