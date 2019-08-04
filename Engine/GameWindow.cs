using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Engine
{
    internal abstract class GameWindow
    {
        private const float UpdateLimit = 10;
        private readonly float updateRate;
        private readonly Color clearColor;
        private Time Time { get; set; }

        protected readonly RenderWindow Window;
        protected readonly KeyboardInputType InputType; 
        protected float DeltaTime { get; private set; }

        protected GameWindow(Vector2u windowSize, string windowTitle, Color clearColor
            , uint framerateLimit = 60
            , bool fullScreen = false
            , bool vsync = false
            , KeyboardInputType inputType = KeyboardInputType.EventBased)
        {
            InputType = inputType;
            this.clearColor = clearColor;
            updateRate = 1.0f / framerateLimit;
            
            var style = fullScreen ? Styles.Fullscreen : Styles.Default;
            Window = new RenderWindow(new VideoMode(windowSize.X, windowSize.Y, 32), windowTitle, style);

            if (vsync)
            {
                Window.SetVerticalSyncEnabled(true);
            }
            else
            {
                Window.SetFramerateLimit(framerateLimit);
            }

            Window.Closed += (sender, arg) => Window.Close();
            Window.Resized += (sender, arg) => Resize(arg.Width, arg.Height);
            Window.MouseButtonPressed += MousePressed;
            Window.MouseButtonReleased += MouseReleased;
            Window.MouseMoved += MouseMoved;

            if(InputType == KeyboardInputType.EventBased)
            {
                Window.KeyPressed += KeyPressed;
                Window.KeyReleased += KeyReleased;
            }
            else {
                throw new NotImplementedException();
            }
        }


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

                if (DeltaTime > 1)
                {
                    DeltaTime = 0;
                }
                totalTime += DeltaTime;
                var updateCount = 0;

                while (totalTime >= updateRate && updateCount < UpdateLimit)
                {
                    Window.DispatchEvents();
                    Update();
                    totalTime -= updateRate;
                    updateCount++;
                }
                Window.Clear(clearColor);
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

        protected float GetFps()
        {
            return (1000000.0f / Time.AsMicroseconds());
        }
    }
}