using System;
using SFML.Graphics;

namespace Engine.GameStates.Menu
{
    public interface IMenu : IGameInput
    {
        // event EventHandler<MenuItemType> MenuItemSelected;

        void Initialize(RenderWindow target);
        void DrawBackground(RenderWindow target);
        void Draw(RenderWindow target);
        void Update(RenderWindow target, float deltaTime);
    }
}