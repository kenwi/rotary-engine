using SFML.Graphics;
using SFML.System;

namespace Engine.Graphics
{
    internal class Tile
    {
        protected bool Collision = false;
        protected Sprite Shape = new Sprite();

        public Tile(int gridX, int gridY, float gridSize, Texture texture, IntRect textureRect)
        {
            Shape.Position = new Vector2f(gridX * gridSize, gridY * gridSize);
            Shape.Texture = texture;
            Shape.TextureRect = textureRect;
        }

        public Vector2f Position => Shape.Position;

        public bool Intersects(FloatRect bounds)
        {
            return Shape.GetGlobalBounds().Intersects(bounds);
        }

        public virtual void Update()
        {
        }

        public virtual void Render(RenderTarget target, Shader shader)
        {
            target.Draw(Shape);
        }
    }
}