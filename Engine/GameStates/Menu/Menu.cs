using SFML.Graphics;
using SFML.Window;

namespace Engine.GameStates.Menu
{
    internal class Menu : BaseGameState, IMenu
    {
        public void Draw(RenderWindow target)
        {
        }

        public void DrawBackground(RenderWindow target)
        {
            throw new System.NotImplementedException();
        }

        public void Initialize(RenderWindow target)
        {

        }

        public void KeyPressed(RenderWindow target, object sender, KeyEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        public void KeyReleased(RenderWindow target, object sender, KeyEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        public void Update(RenderWindow target, float deltaTime)
        {
        }
    }
}