using Engine.Components;
using Engine.Systems;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using DefaultEcs;
using DefaultEcs.System;
using Engine.Managers;
using System;
using System.IO;

namespace MyExampleGame
{
    internal sealed class MyEcsGame
    {
        private const float UpdateLimit = 2;

        private readonly Color _clearColor;
        private readonly float _updateRate;
        private readonly ISystem<float> _updateSystem;
        private readonly ISystem<(RenderTarget, RenderStates)> _renderSystem;
        private readonly Entity _windowEntity;

        private readonly World _world;
        private float _deltaTime;
        private Time _time;
        private bool _initialized;

        public MyEcsGame(bool vSync, uint framerateLimit, bool fullScreen)
        {
            _initialized = false;
            _clearColor = Color.Magenta;
            _updateRate = 1.0f / framerateLimit;
            var style = fullScreen ? Styles.Fullscreen : Styles.Default;

            LoadAssets();

            _world = new World();
            var player = _world.CreateEntity();
            player.Set<Position>(default);

            _windowEntity = _world.CreateEntity();
            _windowEntity.Set<RenderWindow>(new RenderWindow(new VideoMode(800, 600, 32), "DefaultTitle", style));
            var window = _windowEntity.Get<RenderWindow>();
            window.Closed += (s, e ) => window.Close();

            SetFrameRateLimit(vSync, framerateLimit, window);

            var camera = _world.CreateEntity();
            camera.Set<Camera>(new Camera(new Vector2f()));

            _updateSystem = new SequentialSystem<float>(
                new GameSystem(_world)
                , new PlayerSystem(_world, window)
                , new CameraSystem(_world, window)
                , new TileSystem(_world
                    , tileSet: AssetManager.Instance.Texture.Get(AssetManagerItemName.TileSetTexture)
                    , tileSize: new Vector2u(32, 32)
                    , mapSize: new Vector2u(32, 32))
            );

            _renderSystem = new SequentialSystem<(RenderTarget, RenderStates)>(
                new RenderSystem(_world, window)
            );

            _world.Subscribe(this);
        }

        public void Initialize()
        {
            _initialized = true;
        }

        private void LoadAssets()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var assetDirectory = currentDirectory.EndsWith("MyExampleGame") ? "../" : "../../../../";

            AssetManager.Instance.Texture.Load(AssetManagerItemName.GroundTexture,
                Path.Combine(assetDirectory, "Assets/Ground.png"));
            AssetManager.Instance.Texture.Load(AssetManagerItemName.TreeTexture,
                Path.Combine(assetDirectory, "Assets/Tree.png"));
            AssetManager.Instance.Texture.Load(AssetManagerItemName.TileSetTexture,
                Path.Combine(assetDirectory, "Assets/Tileset.png"));
        }

        internal void Run()
        {
            if (!_initialized)
                Initialize();

            var window = _windowEntity.Get<RenderWindow>();
            var clock = new Clock();
            while (window.IsOpen)
            {
                var totalTime = 0.0f;
                while (window.IsOpen)
                {
                    _time = clock.Restart();
                    _deltaTime = _time.AsSeconds();
                    if (_deltaTime > 1)
                        _deltaTime = 0;
                    totalTime += _deltaTime;
                    var updateCount = 0;
                    while (totalTime >= _updateRate && updateCount < UpdateLimit)
                    {
                        window.DispatchEvents();
                        _updateSystem.Update(_deltaTime);
                        totalTime -= _updateRate;
                        updateCount++;
                    }

                    window.Clear(_clearColor);
                    if (_renderSystem.IsEnabled)
                        _renderSystem.Update((window, RenderStates.Default));
                    window.Display();
                }
            }
            Quit(window);
        }

        internal void SetFrameRateLimit(bool vSync, uint framerateLimit, Window window)
        {
            if (vSync)
                window.SetVerticalSyncEnabled(true);
            else
                window.SetFramerateLimit(framerateLimit);
        }

        public void Quit(Window window)
        {
            _world.Dispose();
            _updateSystem.Dispose();
            _windowEntity.Dispose();
        }
    }
}