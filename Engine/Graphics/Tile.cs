using SFML.System;
using SFML.Graphics;

namespace Engine.Graphics
{
    internal class Tile
    {
        protected Sprite shape = new Sprite();
        protected bool collision = false;

        public Vector2f Position => shape.Position;

        public Tile(int GridX, int GridY, float gridSize, Texture texture, IntRect textureRect)
        {
            shape.Position = new Vector2f(GridX * gridSize, GridY * gridSize);
            shape.Texture = texture;
            shape.TextureRect = textureRect;
        }

        public bool Intersects(FloatRect bounds) => shape.GetGlobalBounds().Intersects(bounds);

        public virtual void Update()
        {

        }

        public virtual void Render(RenderTarget target, Shader shader)
        {
            target.Draw(shape);
        }
    }
}
