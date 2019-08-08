using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Engine
{
    using Engine.Interfaces;

    public class Player : IDrawable, IUpdateable
    {
        protected Shape shape;

        public Player(Vector2i cellPosition, float gridSize, Texture texture, IntRect textureRect)
        {
            shape.Position = new Vector2f(cellPosition.X * gridSize, cellPosition.Y * gridSize);
            shape.Texture = texture;
            shape.TextureRect = textureRect;
        }

        public void MovePosition(Vector2f deltaPosition)
        {
            shape.Position += deltaPosition;
        }

        public Player(Vector2i cellPosition, float gridSize)
        {
            var size = 31;
            shape = new CircleShape(size, 128);
            shape.FillColor = new Color(0, 0, 255, 128);
            shape.OutlineColor = new Color(255, 0, 0, 128);
            shape.Position = new Vector2f(cellPosition.X * gridSize, cellPosition.Y * gridSize);
        }

        public void Draw(RenderWindow target)
        {
            target.Draw(shape);
        }

        public void Update(float deltaTime)
        {
        }
    }
}