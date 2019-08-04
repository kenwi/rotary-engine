using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

using Engine.GameStates;
using Engine.GameStates.Worlds;
using Engine.GameStates.Menu;

namespace Engine
{
    internal sealed class MyExampleGame : Engine.GameWindow
    {
        MyExampleState gameState;
        IWorld world;
        IMenu menu;

        public MyExampleGame()
            : base(new Vector2u(1024, 768), "MyExampleGame", Color.Black)
        {

        }

        private IGameInput GetGameInput() => world;
        protected override void KeyPressed(object sender, KeyEventArgs e)
        {
            GetGameInput().KeyPressed(Window, sender, e);
        }

        protected override void KeyReleased(object sender, KeyEventArgs e) 
        { 
            GetGameInput().KeyReleased(Window, sender, e);
        }

        protected override void Initialize()
        {
            gameState = MyExampleState.Game;
            world = new World01();
            world.Initialize(Window);

            menu = new Menu();
            menu.Initialize(Window);
        }

        protected override void LoadContent()
        {

        }


        protected override void Render()
        {
            if(gameState == MyExampleState.Game)
                world.Draw(Window);
            else
                menu.Draw(Window);
        }

        protected override void Update()
        {
            if(gameState == MyExampleState.Game)
                world.Update(Window, DeltaTime);
            else
                menu.Update(Window, DeltaTime);
        }

        protected override void Quit()
        {

        }

        protected override void Resize(uint width, uint height)
        {
            
        }
    }
}