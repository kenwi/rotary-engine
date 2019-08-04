using SFML.Graphics;
using SFML.System;

namespace Engine.Graphics
{
    internal class Tree
    {
        protected Sprite shape = new Sprite();
        protected bool collision = false;

        public Vector2f Position => shape.Position;

        public Tree(int GridX, int GridY, float gridSize, Texture texture, IntRect textureRect)
        {
            var offset = new Vector2f(0, -64);
            shape.Position = new Vector2f(GridX * gridSize, GridY * gridSize) + offset;
            shape.Texture = texture;
            shape.TextureRect = textureRect;
        }

        public bool Intersects(FloatRect bounds) => shape.GetGlobalBounds().Intersects(bounds);

        public virtual void Update()
        {

        }

        public virtual void Draw(RenderWindow target)
        {
            target.Draw(shape);
        }
    }
}
