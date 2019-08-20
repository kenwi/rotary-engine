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
        private VertexArray _vertices;

        public RenderSystem(World world, Window window)
        {
            _world = world;
            _window = window;
        }

        private void Initialize()
        {
            _timeLimit = Time.FromSeconds(1);
            _outputTimer = new Clock();
            _frameCount = 0;
            _initialized = true;

            _vertices = new VertexArray(PrimitiveType.Quads);
            _vertices.Resize((uint)MathF.Sqrt(_vertices.VertexCount) * 4);
            if (_vertices.VertexCount == 0)
            {
                var entities = _world.GetEntities().With<Tile>().Build();
                foreach (var entity in entities.GetEntities())
                {
                    var tile = entity.Get<Tile>();
                    _vertices.Append(tile.Vertex1);
                    _vertices.Append(tile.Vertex2);
                    _vertices.Append(tile.Vertex3);
                    _vertices.Append(tile.Vertex4);
                }
            }
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

            target.Draw(_vertices);
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