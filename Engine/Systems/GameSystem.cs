using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using DefaultEcs;
using DefaultEcs.System;
using Engine.Components;
using SFML.System;
using SFML.Window;

namespace Engine.Systems
{
    public class GameSystem : ISystem<float>
    {
        private readonly World _world;
        private readonly EntitySet _tiles;
        private bool _initialized;

        private uint _numUpdates;
        private Clock _outputTimer;
        private Time _updateOutputTimeLimit;

        public GameSystem(World world)
        {
            _world = world;
            _tiles = _world.GetEntities().With<Tile>().Build();
            _initialized = false;
            _world.Subscribe(this);
        }

        private void Initialize()
        {
            _numUpdates = 0;
            _updateOutputTimeLimit = Time.FromSeconds(0.25f);
            _outputTimer = new Clock();

            Span<Entity> tiles = stackalloc Entity[_tiles.Count];
            foreach (var tile in tiles)
            {
                tile.Dispose();
            }
            _initialized = true;
        }

        public bool IsEnabled { get; set; } = true;

        public void Dispose()
        {
            _tiles.Dispose();
        }

        public void Update(float deltaTime)
        {
            if (!_initialized)
                Initialize();
            CalculateUpdateRate();
        }
        
        private void CalculateUpdateRate()
        {
            if (_outputTimer.ElapsedTime > _updateOutputTimeLimit)
            {
                Console.WriteLine($"[{DateTime.Now.ToLongTimeString()}] FPS: {_numUpdates / _outputTimer.ElapsedTime.AsSeconds()}");
                _outputTimer.Restart();
                _numUpdates = 0;
            }
            _numUpdates++;
        }
    }
}
