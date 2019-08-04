using SFML.System;
using SFML.Graphics;
using System.Collections.Generic;

namespace Engine.Graphics
{
    internal class TileMap
    {
        int gridSizeI;
        float gridSizeF;

        Vector2i maxSizeWorldGrid;
        Vector2f maxSizeWorldF;

        Queue<Tile> map;
        Shape collisionBox;

        public TileMap(Texture groundTexture, float gridSize, int width, int height)
        {
            gridSizeF = gridSize;
            gridSizeI = (int)gridSizeF;
            maxSizeWorldGrid.X = width;
            maxSizeWorldGrid.Y = height;
            maxSizeWorldF.X = width * gridSize;
            maxSizeWorldF.Y = height * gridSize;
            map = new Queue<Tile>(width * height);

            for (int i = 0; i < width * height; i++)
            {
                var x = i % width;
                var y = i / width;
                var o = new Tile(x, y, gridSize, groundTexture, new IntRect(0, 0, gridSizeI, gridSizeI));
                map.Enqueue(o);
            }

            collisionBox = new RectangleShape(new Vector2f(gridSize, gridSize))
            {
                OutlineThickness = 1,
                FillColor = new Color(255, 0, 0, 25),
                OutlineColor = Color.Red
            };
        }

        public virtual void Draw(RenderWindow target)
        {
            foreach (var tile in map)
            {
                collisionBox.Position = tile.Position;
                target.Draw(collisionBox);
                tile.Render(target, null);
            }
        }
    }
}
