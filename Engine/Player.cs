using Engine.Interfaces;
using SFML.Graphics;
using SFML.System;

namespace Engine
{
    public class Player : IDrawable, IUpdateable
    {
        protected Shape Shape;

        public Player(Vector2i cellPosition, float gridSize, Texture texture, IntRect textureRect)
        {
            Shape.Position = new Vector2f(cellPosition.X * gridSize, cellPosition.Y * gridSize);
            Shape.Texture = texture;
            Shape.TextureRect = textureRect;
        }

        public Player(Vector2i cellPosition, float gridSize)
        {
            const int size = 31;
            Shape = new CircleShape(size, 128)
            {
                FillColor = new Color(0, 0, 255, 128),
                OutlineColor = new Color(255, 0, 0, 128),
                Position = new Vector2f(cellPosition.X * gridSize, cellPosition.Y * gridSize)
            };
        }

        public void Draw(RenderWindow target)
        {
            target.Draw(Shape);
        }

        public void Update(float deltaTime)
        {
        }

        public void MovePosition(Vector2f deltaPosition)
        {
            Shape.Position += deltaPosition;
        }
    }
}