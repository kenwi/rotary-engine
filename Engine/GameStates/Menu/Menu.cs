using System;
using Engine.Interfaces;
using SFML.Graphics;
using SFML.Window;

namespace Engine.GameStates.Menu
{
    public class Menu : BaseGameState, IMenu
    {
        public void Draw(RenderWindow target)
        {
        }

        public void DrawBackground(RenderWindow target)
        {
            throw new NotImplementedException();
        }

        public void Initialize(RenderWindow target)
        {
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
        }

        public void MouseReleased(RenderWindow window, object sender, MouseButtonEventArgs e)
        {
        }

        public void MouseWheelScrolled(RenderWindow window, object sender, MouseWheelScrollEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void Update(RenderWindow target, float deltaTime)
        {
        }

        public void MouseWheelScrolled(object sender, MouseWheelScrollEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}