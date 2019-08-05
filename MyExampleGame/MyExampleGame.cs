using SFML.Graphics;
using SFML.System;
using SFML.Window;


namespace MyExampleGame
{
    using Engine.Managers;
    using Engine.Interfaces;
    using Engine.GameStates.Worlds;
    using Engine.GameStates.Menu;

    public sealed class MyExampleGame : Engine.GameWindow
    {
        MyExampleState gameState;
        IWorld world;
        IMenu menu;
        View gui;

        public MyExampleGame()
            : base(new Vector2u(1024, 768), "MyExampleGame", Color.Black)
        {

        }

        private IGameInput GetGameInput() => world;

        protected override void Initialize()
        {
            gameState = MyExampleState.Game;
            world = new World01();
            world.Initialize(Window);
            gui = new View(new FloatRect(0, 0, 500, 500));

            menu = new Menu();
            menu.Initialize(Window);
        }

        protected override void LoadContent()
        {
            AssetManager.Instance.Texture.Load(AssetManagerItemName.GroundTexture,  "Assets/Ground.png");
            AssetManager.Instance.Texture.Load(AssetManagerItemName.TreeTexture, "Assets/Tree.png");
        }

        Shape guiPlaceHolder = new RectangleShape(new Vector2f(10, 10))
        {
            FillColor = Color.Black
        };


        protected override void Render()
        {
            if (gameState == MyExampleState.Game)
            {
                var view = Window.GetView();
                world.Draw(Window);
                Window.SetView(view);
            }
            else
                menu.Draw(Window);
        }

        protected override void Update()
        {
            if (gameState == MyExampleState.Game)
                world.Update(Window, DeltaTime);
            else
                menu.Update(Window, DeltaTime);
        }

        protected override void Quit()
        {

        }

        protected override void Resize(uint width, uint height)
        {
            var view = Window.GetView();
            view.Size = new Vector2f(width, height);
            Window.SetView(view);
        }

        protected override void KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Escape)
                Window.Close();

            if (e.Code == Keyboard.Key.Enter)
                gameState = gameState == MyExampleState.Game ? MyExampleState.Menu : MyExampleState.Game;

            GetGameInput().KeyPressed(Window, sender, e);
        }

        protected override void KeyReleased(object sender, KeyEventArgs e)
        {
            GetGameInput().KeyReleased(Window, sender, e);
        }

        protected override void MousePressed(object sender, MouseButtonEventArgs e)
        {
            GetGameInput().MousePressed(Window, sender, e);
        }

        protected override void MouseReleased(object sender, MouseButtonEventArgs e)
        {
            GetGameInput().MouseReleased(Window, sender, e);
        }

        protected override void MouseMoved(object sender, MouseMoveEventArgs e)
        {
            GetGameInput().MouseMoved(Window, sender, e);
        }

        protected override void MouseWheelScrolled(object sender, MouseWheelScrollEventArgs e)
        {
            GetGameInput().MouseWheelScrolled(Window, sender, e);
        }
    }
}