using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;

namespace Engine.Graphics
{
    internal class TileMap
    {
        private readonly Shape _collisionBox;
        private readonly bool _debug;

        private readonly Queue<Tile> _map;
        private Vector2f _maxSizeWorldF;
        private Vector2i _maxSizeWorldGrid;

        public TileMap(Texture groundTexture, float gridSize, int width, int height, bool showBounds = false)
        {
            var gridSizeI = (int) gridSize;
            _maxSizeWorldGrid.X = width;
            _maxSizeWorldGrid.Y = height;
            _maxSizeWorldF.X = width * gridSize;
            _maxSizeWorldF.Y = height * gridSize;
            _map = new Queue<Tile>(width * height);
            _debug = showBounds;

            for (var i = 0; i < width * height; i++)
            {
                var x = i % width;
                var y = i / width;
                var o = new Tile(x, y, gridSize, groundTexture, new IntRect(0, 0, gridSizeI, gridSizeI));
                _map.Enqueue(o);
            }

            _collisionBox = new RectangleShape(new Vector2f(gridSize, gridSize))
            {
                OutlineThickness = 1,
                FillColor = new Color(255, 0, 0, 25),
                OutlineColor = Color.Red
            };
        }

        public Vector2i PlayerPosition { get; set; }

        public virtual void Draw(RenderWindow target)
        {
            foreach (var tile in _map)
            {
                if (_debug)
                {
                    _collisionBox.Position = tile.Position;
                    target.Draw(_collisionBox);
                }

                tile.Render(target, null);
            }
        }
    }
}