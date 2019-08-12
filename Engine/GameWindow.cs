using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Engine
{
    public abstract class GameWindow
    {
        private const float UpdateLimit = 10;
        private readonly Color _clearColor;
        private readonly float _updateRate;
        protected readonly KeyboardInputType InputType;

        protected readonly RenderWindow Window;

        protected GameWindow(Vector2u windowSize, string windowTitle, Color clearColor
            , uint framerateLimit = 60
            , bool fullScreen = false
            , bool vsync = false
            , KeyboardInputType inputType = KeyboardInputType.EventBased)
        {
            InputType = inputType;
            _clearColor = clearColor;
            _updateRate = 1.0f / framerateLimit;

            var style = fullScreen ? Styles.Fullscreen : Styles.Default;
            Window = new RenderWindow(new VideoMode(windowSize.X, windowSize.Y, 32), windowTitle, style);

            if (vsync)
                Window.SetVerticalSyncEnabled(true);
            else
                Window.SetFramerateLimit(framerateLimit);

            Window.Closed += (sender, arg) => Window.Close();
            Window.Resized += (sender, arg) => Resize(arg.Width, arg.Height);
            Window.MouseButtonPressed += MousePressed;
            Window.MouseButtonReleased += MouseReleased;
            Window.MouseMoved += MouseMoved;
            Window.MouseWheelScrolled += MouseWheelScrolled;

            if (InputType == KeyboardInputType.EventBased)
            {
                Window.KeyPressed += KeyPressed;
                Window.KeyReleased += KeyReleased;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private Time Time { get; set; }
        protected float DeltaTime { get; private set; }


        public void Run()
        {
            LoadContent();
            Initialize();

            var clock = new Clock();
            var totalTime = 0.0f;
            while (Window.IsOpen)
            {
                Time = clock.Restart();
                DeltaTime = Time.AsSeconds();

                if (DeltaTime > 1) DeltaTime = 0;
                totalTime += DeltaTime;
                var updateCount = 0;

                while (totalTime >= _updateRate && updateCount < UpdateLimit)
                {
                    Window.DispatchEvents();
                    Update();
                    totalTime -= _updateRate;
                    updateCount++;
                }

                Window.Clear(_clearColor);
                Render();
                Window.Display();
            }

            Quit();
        }

        protected abstract void LoadContent();
        protected abstract void Initialize();
        protected abstract void Update();
        protected abstract void Render();
        protected abstract void Quit();
        protected abstract void Resize(uint width, uint height);

        protected abstract void KeyPressed(object sender, KeyEventArgs e);
        protected abstract void KeyReleased(object sender, KeyEventArgs e);
        protected abstract void MousePressed(object sender, MouseButtonEventArgs e);
        protected abstract void MouseReleased(object sender, MouseButtonEventArgs e);
        protected abstract void MouseMoved(object sender, MouseMoveEventArgs e);
        protected abstract void MouseWheelScrolled(object sender, MouseWheelScrollEventArgs e);

        protected float GetFps()
        {
            return 1000000.0f / Time.AsMicroseconds();
        }
    }
}