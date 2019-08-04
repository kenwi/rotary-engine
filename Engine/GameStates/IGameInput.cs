using SFML.Graphics;
using SFML.Window;

namespace Engine.GameStates
{
    internal interface IGameInput
    {
        void KeyPressed(RenderWindow target, object sender, KeyEventArgs e);
        void KeyReleased(RenderWindow target, object sender, KeyEventArgs e);
        void MousePressed(RenderWindow window, object sender, MouseButtonEventArgs e);
        void MouseReleased(RenderWindow window, object sender, MouseButtonEventArgs e);
    }
}