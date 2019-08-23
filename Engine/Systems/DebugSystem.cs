using System;
using DefaultEcs;
using DefaultEcs.System;
using Engine.Components;

namespace Engine.Systems
{
    public class DebugSystem : AEntitySystem<DebugWindow>
    {
        private readonly World _world;

        public DebugSystem(World world) : base(world)
        {
            _world = world;
            _world.Subscribe(this);
        }

        protected override void PreUpdate(DebugWindow state)
        {
            base.PreUpdate(state);
        }

        protected override void Update(DebugWindow state, ReadOnlySpan<Entity> entities)
        {
            base.Update(state, entities);
        }

        protected override void Update(DebugWindow state, in Entity entity)
        {
            base.Update(state, entity);
        }
        
        protected override void PostUpdate(DebugWindow state)
        {
            base.PostUpdate(state);
        }
    }
}
