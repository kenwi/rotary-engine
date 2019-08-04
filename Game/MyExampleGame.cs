using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

using Engine.GameStates;
using Engine.GameStates.Worlds;
using Engine.GameStates.Menu;
using Engine.Managers;

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
            if(e.Code == Keyboard.Key.Escape)
                Window.Close();

            if(e.Code == Keyboard.Key.Enter)
                gameState = gameState == MyExampleState.Game ? MyExampleState.Menu : MyExampleState.Game;

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
            AssetManager.Instance.Texture.Load(AssetManagerItemName.GroundTexture, "Assets/Ground.png");
            AssetManager.Instance.Texture.Load(AssetManagerItemName.TreeTexture, "Assets/Tree.png");
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