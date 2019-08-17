using System;
using System.IO.Compression;
using SFML.Graphics;
using SFML.System;

namespace Engine.Graphics
{
    internal class VertexTileMap : Transformable, Drawable
    {
        private VertexArray _vertices;
        private Texture _tileSet;

        public bool Load(Texture tileSet, Vector2u tileSize, byte[] tiles, uint width, uint height)
        {
            _tileSet = tileSet;
            _vertices = new VertexArray(PrimitiveType.Quads);
            _vertices.Resize(width * height * 4);
            
            for (uint x = 0; x < width; x++)
            {
                for (uint y = 0; y < height; y++)
                {
                    var index = x + y * width;
                    var tileNumber = tiles[x + y * width] + new Random().Next(6);

                    var tu = tileNumber % (_tileSet.Size.X / tileSize.X);
                    var tv = tileNumber / (_tileSet.Size.X / tileSize.X);

                    var vertex1 = new Vertex(new Vector2f(x * tileSize.X, y * tileSize.Y), new Vector2f(tu * tileSize.X, tv * tileSize.Y));
                    var vertex2 = new Vertex(new Vector2f((x + 1) * tileSize.X, y * tileSize.Y), new Vector2f((tu + 1) * tileSize.X, tv * tileSize.Y));
                    var vertex3 = new Vertex(new Vector2f((x + 1) * tileSize.X, (y + 1) * tileSize.Y), new Vector2f((tu + 1) * tileSize.X, (tv + 1) * tileSize.Y));
                    var vertex4 = new Vertex(new Vector2f(x * tileSize.X, (y + 1) * tileSize.Y), new Vector2f(tu  * tileSize.X, (tv + 1) * tileSize.Y));
                    _vertices.Append(vertex1);
                    _vertices.Append(vertex2);
                    _vertices.Append(vertex3);
                    _vertices.Append(vertex4);
                }
            }
            return true;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform = Transform;
            states.Texture = _tileSet;
            target.Draw(_vertices, states);
        }
    }
}