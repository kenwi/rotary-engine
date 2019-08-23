using DefaultEcs;
using DefaultEcs.System;
using Engine.Components;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Engine.Systems
{
    public class CameraSystem : AEntitySystem<float>
    {
        private readonly RenderTarget _window;

        public CameraSystem(World world, RenderTarget window) 
            : base(world.GetEntities().With<Camera>().Build())
        {
            _window = window;
        }

        protected override void Update(float deltaTime, in Entity entity)
        {
            var camera = entity.Get<Camera>();
            var view = _window.GetView();
            view.Move(new Vector2f(0.0f * deltaTime, 10f * deltaTime));
            _window.SetView(view);
        }
    }
}
