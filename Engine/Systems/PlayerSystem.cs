using DefaultEcs;
using DefaultEcs.System;
using Engine.Components;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Engine.Systems
{
    public class PlayerSystem : AEntitySystem<float>
    {
        private readonly Window _window;
        private Vector2i _mousePosition;

        public PlayerSystem(World world, Window window)
            : base(world.GetEntities().With<Position>().Build())
        {
            _window = window;
        }

        protected override void PreUpdate(float state)
        {
            _mousePosition = Mouse.GetPosition();
        }

        protected override void Update(float state, in Entity entity)
        {
            entity.Get<Position>().Value = _mousePosition;
        }
    }
}
