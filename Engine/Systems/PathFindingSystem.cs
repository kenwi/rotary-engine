using System;
using DefaultEcs;
using DefaultEcs.System;
using Engine.Components;
using SFML.System;
using SFML.Graphics;
using System.Collections.Generic;

namespace Engine.Systems
{
    public class PathFindingSystem : AEntitySystem<float>
    {
        private readonly IntRect _mapSize;

        public PathFindingSystem(World world, IntRect mapSize) : base(world.GetEntities().With<Tile>().Build())
        {
            _mapSize = mapSize;
        }

        protected override void Update(float state, ReadOnlySpan<Entity> entities)
        {
            if (state < 0)
                return;

            Vector2i startPosition = new Vector2i(), endPosition = new Vector2i();
            List<Vector2i> positions = new List<Vector2i>();
            
            // Examples:
            // Walk through all instances and calculate cell coordinates
            uint i = 0;
            foreach(var tileEntity in entities)
            {
                var tile = tileEntity.Get<Tile>();
                var cellCoordinates = new Vector2i((int)(i % _mapSize.Width), (int)(i / _mapSize.Width));
            }

            // calculate 1-dimensional index and look up the cell directly
            int cellX = 4, cellY = 20;
            int index = cellX + cellY * _mapSize.Width;
            var cell = entities[index].Get<Tile>();
        }
    }
}
