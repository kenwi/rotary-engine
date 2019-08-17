using System.IO;
using Engine;
using Engine.GameStates.Menu;
using Engine.GameStates.Worlds;
using Engine.Interfaces;
using Engine.Managers;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace MyExampleGame
{
    public sealed class MyExampleGame : GameWindow
    {
        private MyExampleState _gameState;
        private View _gui;

        private Shape _guiPlaceHolder = new RectangleShape(new Vector2f(10, 10))
        {
            FillColor = Color.Black
        };

        private IMenu _menu;
        private IWorld _world;

        public MyExampleGame()
            : base(new Vector2u(1024, 768), "MyExampleGame", Color.Black)
        {
        }

        private IGameInput GetGameInput()
        {
            return _world;
        }

        protected override void Initialize()
        {
            _gameState = MyExampleState.Game;
            _world = new World01();
            _world.Initialize(Window);
            _gui = new View(new FloatRect(0, 0, 500, 500));

            _menu = new Menu();
            _menu.Initialize(Window);
        }

        protected override void LoadContent()
        {
            var directory = Directory.GetCurrentDirectory();
            var toSubDir = directory.Contains("bin") ? "../../../../" : "";

            AssetManager.Instance.Texture.Load(AssetManagerItemName.GroundTexture,
                Path.Combine(toSubDir, "Assets/Ground.png"));
            AssetManager.Instance.Texture.Load(AssetManagerItemName.TreeTexture,
                Path.Combine(toSubDir, "Assets/Tree.png"));
            AssetManager.Instance.Texture.Load(AssetManagerItemName.TileSetTexture,
                Path.Combine(toSubDir, "Assets/Tileset.png"));
        }


        protected override void Render()
        {
            if (_gameState == MyExampleState.Game)
            {
                var view = Window.GetView();
                _world.Draw(Window);
                Window.SetView(view);
            }
            else
            {
                _menu.Draw(Window);
            }
        }

        protected override void Update()
        {
            if (_gameState == MyExampleState.Game)
                _world.Update(Window, DeltaTime);
            else
                _menu.Update(Window, DeltaTime);
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
                _gameState = _gameState == MyExampleState.Game ? MyExampleState.Menu : MyExampleState.Game;

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