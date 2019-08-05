using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Engine
{
    using Engine.Interfaces;
    internal class Player : IDrawable, IUpdateable, IGameInput
    {
        protected Sprite shape = new Sprite();

        public Player(Vector2i cellPosition, float gridSize, Texture texture, IntRect textureRect)
        {
            shape.Position = new Vector2f(cellPosition.X * gridSize, cellPosition.Y * gridSize);
            shape.Texture = texture;
            shape.TextureRect = textureRect;
        }

        public void Draw(RenderWindow target)
        {
            target.Draw(shape);
        }

        public void KeyPressed(RenderWindow target, object sender, KeyEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void KeyReleased(RenderWindow target, object sender, KeyEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void MouseMoved(RenderWindow window, object sender, MouseMoveEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void MousePressed(RenderWindow window, object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void MouseReleased(RenderWindow window, object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void MouseWheelScrolled(RenderWindow window, object sender, MouseWheelScrollEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void Update(float deltaTime)
        {
            throw new NotImplementedException();
        }
    }
}