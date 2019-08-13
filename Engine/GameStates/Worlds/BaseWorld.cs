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
        private Forest _forest;
        private TileMap _map;
        private RenderWindow _window;

        private WorldState _worldState = WorldState.Default;
        protected int Width, Height, GridSize;

        protected BaseWorld()
        {
            Width = 128;
            Height = 128;
            GridSize = 64;
        }

        public event EventHandler<WorldState> WorldStateChanged;

        public abstract void KeyPressed(RenderWindow target, object sender, KeyEventArgs e);
        public abstract void KeyReleased(RenderWindow target, object sender, KeyEventArgs e);
        public abstract void MousePressed(RenderWindow window, object sender, MouseButtonEventArgs e);
        public abstract void MouseReleased(RenderWindow window, object sender, MouseButtonEventArgs e);
        public abstract void MouseMoved(RenderWindow window, object sender, MouseMoveEventArgs e);
        public abstract void MouseWheelScrolled(RenderWindow window, object sender, MouseWheelScrollEventArgs e);

        public WorldState WorldState
        {
            get => _worldState;
            set
            {
                _worldState = value;
                WorldStateChanged(this, WorldState);
            }
        }

        public virtual void Initialize(RenderWindow target)
        {
            _window = target;

            var groundTexture = AssetManager.Instance.Texture.Get(AssetManagerItemName.GroundTexture);
            _map = new TileMap(groundTexture, GridSize, Width, Height);

            var treeTexture = AssetManager.Instance.Texture.Get(AssetManagerItemName.TreeTexture);
            _forest = new Forest(treeTexture, GridSize, Width, Height, 0.1);
        }

        public virtual void Update(RenderWindow target, float deltaTime)
        {
        }

        public virtual void Draw(RenderWindow target)
        {
            if (WorldState != WorldState.Default) return;

            _map.Draw(target);
            _forest.Draw(target);
        }

        internal void MoveWindow(Vector2f direction)
        {
            var view = _window.GetView();
            view.Move(direction);
            _window.SetView(view);
        }

        internal void ZoomWindow(float factor)
        {
            var view = _window.GetView();
            view.Zoom(factor);
            _window.SetView(view);
        }
    }
}