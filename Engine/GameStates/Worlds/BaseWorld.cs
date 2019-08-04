using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace Engine.GameStates.Worlds
{
    using Engine.Graphics;
    using Engine.Managers;

    internal abstract class BaseWorld : BaseGameState, IWorld
    {
        TileMap map;
        Forest forest;
        RenderWindow window;

        public event EventHandler<WorldState> WorldStateChanged;

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

        public virtual void Initialize(RenderWindow target)
        {
            window = target;
            int width = 64, height = 64, gridSize = 64;

            var groundTexture = AssetManager.Instance.Texture.Get(AssetManagerItemName.GroundTexture);
            map = new TileMap(groundTexture, gridSize, width, height);

            var treeTexture = AssetManager.Instance.Texture.Get(AssetManagerItemName.TreeTexture);
            forest = new Forest(treeTexture, gridSize, width, height, width * height, 0.1);
        }

        public virtual void Update(RenderWindow target, float deltaTime)
        {

        }

        public abstract void KeyPressed(RenderWindow target, object sender, KeyEventArgs e);
        public abstract void KeyReleased(RenderWindow target, object sender, KeyEventArgs e);

        public void Draw(RenderWindow target)
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
    }
}