using DefaultEcs;
using DefaultEcs.System;
using Engine.Components;
using SFML.Window;

namespace Engine.Systems
{
    public class CameraSystem : AEntitySystem<float>
    {
        private readonly Window _window;

        public CameraSystem(World world, Window window) 
            : base(world.GetEntities().With<Camera>().Build())
        {
            _window = window;
        }

        protected override void Update(float state, in Entity entity)
        {
            var camera = entity.Get<Camera>(); 
        }
    }
}
