using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using DefaultEcs;
using DefaultEcs.System;
using Engine.Components;

namespace Engine.Systems
{
    public class GameSystem : ISystem<float>
    {
        private readonly World _world;
        private readonly EntitySet _tiles;
        private bool _initialized;

        public GameSystem(World world)
        {
            _world = world;
            _tiles = _world.GetEntities().With<Tile>().Build();
            _initialized = false;
            _world.Subscribe(this);
        }

        private void Initialize()
        {
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
            if(!_initialized)
                Initialize();
        }
    }
}
