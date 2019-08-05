using SFML.Graphics;
using SFML.Window;

namespace Engine.Interfaces
{
    public interface IGameInput
    {
        void KeyPressed(RenderWindow target, object sender, KeyEventArgs e);
        void KeyReleased(RenderWindow target, object sender, KeyEventArgs e);
        void MousePressed(RenderWindow window, object sender, MouseButtonEventArgs e);
        void MouseReleased(RenderWindow window, object sender, MouseButtonEventArgs e);
        void MouseMoved(RenderWindow window, object sender, MouseMoveEventArgs e);
        void MouseWheelScrolled(RenderWindow window, object sender, MouseWheelScrollEventArgs e);
    }
}