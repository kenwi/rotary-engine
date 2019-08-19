using System;
using DefaultEcs.System;
using SFML.Graphics;
using SFML.System;

namespace Engine.Systems
{
    public class RenderSystem : ISystem<RenderWindow>
    {
        public bool IsEnabled { get; set; } = true;

        private bool _initialized;
        private int _frameCount;
        private Clock _outputTimer;
        private Time _timeLimit;

        public void Update(RenderWindow state)
        {
            if (!_initialized)
                Initialize();
            if (_outputTimer.ElapsedTime > _timeLimit)
            {
                Console.WriteLine(_frameCount / _outputTimer.ElapsedTime.AsSeconds());
                _outputTimer.Restart();
                _frameCount = 0;
            }

            _frameCount++;
        }

        private void Initialize()
        {
            _timeLimit = Time.FromSeconds(2);
            _outputTimer = new Clock();
            _frameCount = 0;
            _initialized = true;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}