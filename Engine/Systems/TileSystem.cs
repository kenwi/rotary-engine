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
        private Texture _tileSet;

        public TileSystem(World world, Texture tileSet, Vector2u tileSize, Vector2u mapSize) : base(world.GetEntities().With<Tile>().Build())
        {
            _world = world;
            _tileSize = tileSize;
            _mapSize = mapSize;
            _tileSet = tileSet;
            _initialized = false;

            for (uint x = 0; x < _mapSize.X; x++)
            {
                for (uint y = 0; y < _mapSize.Y; y++)
                {
                    var index = x + y * _mapSize.X;
                    var tileNumber = 0;

                    var tu = (uint)tileNumber % (_tileSet.Size.X / tileSize.X);
                    var tv = (uint)tileNumber / (_tileSet.Size.X / tileSize.X);

                    var tile = _world.CreateEntity();
                    tile.Set<Tile>(new Tile {
                        Index = index,
                        TileNumber = 0,
                        Tu = tu,
                        Tv = tv,
                        Vertex1 = new Vertex(new Vector2f(x * tileSize.X, y * tileSize.Y), new Vector2f(tu * tileSize.X, tv * tileSize.Y)),
                        Vertex2 = new Vertex(new Vector2f((x + 1) * tileSize.X, y * tileSize.Y), new Vector2f((tu + 1) * tileSize.X, tv * tileSize.Y)),
                        Vertex3 = new Vertex(new Vector2f((x + 1) * tileSize.X, (y + 1) * tileSize.Y), new Vector2f((tu + 1) * tileSize.X, (tv + 1) * tileSize.Y)),
                        Vertex4 = new Vertex(new Vector2f(x * tileSize.X, (y + 1) * tileSize.Y), new Vector2f(tu * tileSize.X, (tv + 1) * tileSize.Y))
                    });;
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