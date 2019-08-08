using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace Engine.GameStates.Worlds
{
    using Engine.Interfaces;
    using Engine.Graphics;
    using Engine.Managers;

    public abstract class BaseWorld : BaseGameState, IWorld
    {
        TileMap map;
        Forest forest;
        RenderWindow window;
        protected int width, height, gridSize;

        public event EventHandler<WorldState> WorldStateChanged;

        public abstract void KeyPressed(RenderWindow target, object sender, KeyEventArgs e);
        public abstract void KeyReleased(RenderWindow target, object sender, KeyEventArgs e);
        public abstract void MousePressed(RenderWindow window, object sender, MouseButtonEventArgs e);
        public abstract void MouseReleased(RenderWindow window, object sender, MouseButtonEventArgs e);
        public abstract void MouseMoved(RenderWindow window, object sender, MouseMoveEventArgs e);
        public abstract void MouseWheelScrolled(RenderWindow window, object sender, MouseWheelScrollEventArgs e);

        private WorldState worldState = WorldState.Default;
        public WorldState WorldState
        {
            get => worldState;
            set
            {
                worldState = value;
                WorldStateChanged(this, WorldState);
            }
        }

        public BaseWorld()
        {
            width = 128;
            height = 128;
            gridSize = 64;
        }

        public virtual void Initialize(RenderWindow target)
        {
            window = target;
            
            var groundTexture = AssetManager.Instance.Texture.Get(AssetManagerItemName.GroundTexture);
            map = new TileMap(groundTexture, gridSize, width, height);

            var treeTexture = AssetManager.Instance.Texture.Get(AssetManagerItemName.TreeTexture);
            forest = new Forest(treeTexture, gridSize, width, height, width * height, 0.5);
        }

        public virtual void Update(RenderWindow target, float deltaTime)
        {

        }

        public virtual void Draw(RenderWindow target)
        {
            if (WorldState == WorldState.Default)
            {
                map.Draw(target);
                forest.Draw(target);
            }
        }

        internal void MoveWindow(Vector2f direction)
        {
            var view = window.GetView();
            view.Move(direction);
            window.SetView(view);
        }

        internal void ZoomWindow(float factor)
        {
            var view = window.GetView();
            view.Zoom(factor);
            window.SetView(view);
        }

        public void MouseWheelScrolled(object sender, MouseWheelScrollEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}