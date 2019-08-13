using System;
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
        public abstract void Update(RenderWindow target, float deltaTime);
        public abstract void KeyPressed(RenderWindow target, object sender, KeyEventArgs e);
        public abstract void KeyReleased(RenderWindow target, object sender, KeyEventArgs e);
        public abstract void MousePressed(RenderWindow window, object sender, MouseButtonEventArgs e);
        public abstract void MouseReleased(RenderWindow window, object sender, MouseButtonEventArgs e);
        public abstract void MouseMoved(RenderWindow window, object sender, MouseMoveEventArgs e);
        public abstract void MouseWheelScrolled(RenderWindow window, object sender, MouseWheelScrollEventArgs e);
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

        internal TileMap Map;
        internal RenderWindow Window;
        internal int Width, Height, GridSize;

        private WorldState _state = WorldState.Default;
        private float _zoomLevel;

        internal BaseWorld()
        {
            _zoomLevel = 1;
            Width = 128;
            Height = 128;
            GridSize = 64;
        }

        internal void MoveWindow(Vector2f direction)
        {
            var view = Window.GetView();
            var newPosition = new Vector2f(direction.X * _zoomLevel, direction.Y * _zoomLevel);
            view.Move(newPosition);
            Window.SetView(view);
        }

        internal void ZoomWindow(float factor)
        {
            var view = Window.GetView();

            _zoomLevel *= factor;
            view.Zoom(factor);
            Window.SetView(view);
        }

        public virtual void Initialize(RenderWindow target)
        {
            Window = target;
            var groundTexture = AssetManager.Instance.Texture.Get(AssetManagerItemName.GroundTexture);
            Map = new TileMap(groundTexture, GridSize, Width, Height);
        }

        public virtual void Draw(RenderWindow target)
        {
            if (WorldState != WorldState.Default) return;
            Map.Draw(target);
        }
    }
}