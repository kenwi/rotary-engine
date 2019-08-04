using SFML.Graphics;
using SFML.Window;

namespace Engine.GameStates
{
    internal interface IGameInput
    {
        void KeyPressed(RenderWindow target, object sender, KeyEventArgs e);
        void KeyReleased(RenderWindow target, object sender, KeyEventArgs e);
    }
}