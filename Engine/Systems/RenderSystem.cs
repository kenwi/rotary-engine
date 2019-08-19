using System;
using DefaultEcs;
using DefaultEcs.System;
using Engine.Components;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Engine.Systems
{
    public class RenderSystem : ISystem<(RenderTarget target, RenderStates state)>
    {
        private bool _initialized;
        private int _frameCount;
        private Clock _outputTimer;
        private Time _timeLimit;
        private World _world;
        private Window _window;
        private uint _width;
        private uint _height;
        private VertexArray _vertices;

        public RenderSystem(World world, Window window, uint width, uint height)
        {
            _world = world;
            _window = window;
            _width = width;
            _height = height;
        }

        private void Initialize()
        {
            _timeLimit = Time.FromSeconds(1);
            _outputTimer = new Clock();
            _frameCount = 0;
            _initialized = true;
        }

        public bool IsEnabled { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Update((RenderTarget target, RenderStates state) drawInfo)
        {
            if (!_initialized)
                Initialize();

            CalculateFps();

            ref var target = ref drawInfo.target;
            ref var state = ref drawInfo.state;
            var entities = _world.GetEntities().With<Tile>().Build();
            foreach(var entity in entities.GetEntities())
            {
                var tile = entity.Get<Tile>();
            }
        }

        private void CalculateFps()
        {
            if (_outputTimer.ElapsedTime > _timeLimit)
            {
                Console.WriteLine(_frameCount / _outputTimer.ElapsedTime.AsSeconds());
                _outputTimer.Restart();
                _frameCount = 0;
            }
            _frameCount++;
        }
    }
}