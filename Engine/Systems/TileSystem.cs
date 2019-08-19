using System;
using DefaultEcs;
using DefaultEcs.System;
using Engine.Components;
using Engine.Interfaces;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Engine.Systems
{
    public class TileSystem : AEntitySystem<float>
    {
        private bool _initialized;
        private Vector2u _mapSize;
        private World _world;
        private Vector2u _tileSize;

        public TileSystem(World world, Vector2u tileSize, Vector2u mapSize) : base(world.GetEntities().With<Tile>().Build())
        {
            _world = world;
            _tileSize = tileSize;
            _mapSize = mapSize;
            _initialized = false;

            for (uint x = 0; x < _mapSize.X; x++)
            {
                for (uint y = 0; y < _mapSize.Y; y++)
                {
                    var index = x + y * _mapSize.X;

                    var tile = _world.CreateEntity();
                    tile.Set<Tile>(new Tile(index));
                    tile.Set<Position>(default);
                }
            }
        }

        private void Initialize()
        {
            _initialized = true;
        }

        protected override void Update(float state, in Entity entity)
        {
            if (!_initialized)
                Initialize();
        }

        protected override void Update(float state, ReadOnlySpan<Entity> entities)
        {
            if (!_initialized)
                Initialize();
        }
    }
}