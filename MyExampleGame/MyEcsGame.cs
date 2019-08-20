using System;
using DefaultEcs;
using DefaultEcs.System;
using Engine.Components;
using Engine.Systems;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace MyExampleGame
{
    internal sealed class MyEcsGame
    {
        private const float UpdateLimit = 60;

        private readonly Color _clearColor;
        private readonly float _updateRate;
        private readonly ISystem<float> _updateSystem;
        private readonly ISystem<(RenderTarget, RenderStates)> _renderSystem;
        private readonly RenderWindow _window;

        private readonly World _world;
        private float _deltaTime;
        private Time _time;

        public MyEcsGame(bool vSync, uint framerateLimit, bool fullScreen)
        {
            _clearColor = Color.Magenta;
            _updateRate = 1.0f / framerateLimit;
            var style = fullScreen ? Styles.Fullscreen : Styles.Default;
            _window = new RenderWindow(new VideoMode(800, 600, 32), "DefaultTitle", style);
            SetFrameRateLimit(vSync, framerateLimit);

            _world = new World();
            var player = _world.CreateEntity();
            player.Set<Position>(default);

            _updateSystem = new SequentialSystem<float>(
                new GameSystem(_world)
                , new PlayerSystem(_world, _window)
                , new TileSystem(_world, tileSet: new Texture(64, 64), tileSize: new Vector2u(64, 64), mapSize: new Vector2u(8, 8))
            );

            _renderSystem = new SequentialSystem<(RenderTarget, RenderStates)>(
                new RenderSystem(_world, _window)
            );

            _world.Subscribe(this);
        }

        internal void Run()
        {
            var clock = new Clock();
            while (_window.IsOpen)
            {
                var totalTime = 0.0f;
                while (_window.IsOpen)
                {
                    _time = clock.Restart();
                    _deltaTime = _time.AsSeconds();
                    if (_deltaTime > 1)
                        _deltaTime = 0;
                    totalTime += _deltaTime;
                    var updateCount = 0;
                    while (totalTime >= _updateRate && updateCount < UpdateLimit)
                    {
                        _window.DispatchEvents();
                        _updateSystem.Update(_deltaTime);
                        totalTime -= _updateRate;
                        updateCount++;
                    }

                    _window.Clear(_clearColor);
                    if(_renderSystem.IsEnabled)
                        _renderSystem.Update((_window, RenderStates.Default));
                    _window.Display();
                }

                Quit();
            }
        }

        internal void SetFrameRateLimit(bool vSync, uint framerateLimit)
        {
            if (vSync)
                _window.SetVerticalSyncEnabled(true);
            else
                _window.SetFramerateLimit(framerateLimit);
        }

        public void Quit()
        {
            _world.Dispose();
            _updateSystem.Dispose();
            _window.Dispose();
        }
    }
}