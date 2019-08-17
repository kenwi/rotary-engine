using System;
using System.Collections.Generic;
using Engine.Graphics;
using Engine.Interfaces;
using Engine.Managers;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Engine.GameStates.Worlds
{
    public abstract class BaseWorld : BaseGameState, IWorld
    {
        private readonly List<Keyboard.Key> _keyDown;
        private readonly List<Mouse.Button> _mouseButtonDown;
        private Vector2i _mousePreviousPosition;
        private WorldState _state = WorldState.Default;
        private float _zoomLevel;

        internal Vector2i MouseDeltaVelocity;
        internal int Width, Height, GridSize;
        internal RenderWindow Window;

        internal BaseWorld(int width, int height, int gridSize)
        {
            _mouseButtonDown = new List<Mouse.Button>();
            _keyDown = new List<Keyboard.Key>();
            _zoomLevel = 1;
            Width = width;
            Height = height;
            GridSize = gridSize;
        }

        public abstract void Update(RenderWindow target, float deltaTime);

        public virtual void KeyPressed(RenderWindow target, object sender, KeyEventArgs e)
        {
            if (!_keyDown.Contains(e.Code))
                _keyDown.Add(e.Code);
        }

        public virtual void KeyReleased(RenderWindow target, object sender, KeyEventArgs e)
        {
            _keyDown.Remove(e.Code);
        }

        public virtual void MousePressed(RenderWindow window, object sender, MouseButtonEventArgs e)
        {
            if (!_mouseButtonDown.Contains(e.Button))
                _mouseButtonDown.Add(e.Button);
        }

        public virtual void MouseReleased(RenderWindow window, object sender, MouseButtonEventArgs e)
        {
            _mouseButtonDown.Remove(e.Button);
        }

        public virtual void MouseMoved(RenderWindow window, object sender, MouseMoveEventArgs e)
        {
            var currentPosition = new Vector2i(e.X, e.Y);
            MouseDeltaVelocity = _mousePreviousPosition - currentPosition;
            _mousePreviousPosition = currentPosition;
        }

        public virtual void MouseWheelScrolled(RenderWindow window, object sender, MouseWheelScrollEventArgs e)
        {
            var zoomLevelDelta = Math.Abs(e.Delta - 1) < 1.0 ? 0.5f : 2f;
            ZoomWindow(zoomLevelDelta);
        }

        public event EventHandler<WorldState> WorldStateChanged;

        public WorldState WorldState
        {
            get => _state;
            set
            {
                _state = value;
                WorldStateChanged?.Invoke(this, WorldState);
            }
        }

        public virtual void Initialize(RenderWindow target)
        {
            Window = target;
        }

        public virtual void Draw(RenderWindow target)
        {
        }

        public virtual bool MouseDownAndHolding(Mouse.Button e)
        {
            return _mouseButtonDown.Contains(e);
        }

        public virtual bool KeyPressedAndHolding(Keyboard.Key e)
        {
            return _keyDown.Contains(e);
        }

        internal void MoveWindow(Vector2f direction)
        {
            var view = Window.GetView();
            var newPosition = new Vector2f(direction.X * _zoomLevel, direction.Y * _zoomLevel);
            view.Move(newPosition);
            MouseDeltaVelocity = new Vector2i();
            Window.SetView(view);
        }

        internal void ZoomWindow(float factor)
        {
            var view = Window.GetView();
            _zoomLevel *= factor;
            view.Zoom(factor);
            Window.SetView(view);
        }
    }
}