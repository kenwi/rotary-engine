using SFML.Graphics;
using SFML.System;

namespace Engine.Graphics
{
    internal class Tree
    {
        protected bool Collision = false;
        protected Sprite Shape = new Sprite();

        public Tree(int gridX, int gridY, float gridSize, Texture texture, IntRect textureRect)
        {
            var offset = new Vector2f(0, -64);
            Shape.Position = new Vector2f(gridX * gridSize, gridY * gridSize) + offset;
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

        public virtual void Draw(RenderWindow target)
        {
            target.Draw(Shape);
        }
    }
}