using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace Engine.GameStates.Worlds
{
    using Engine.Graphics;

    internal abstract class BaseWorld : BaseGameState, IWorld
    {
        TileMap map;
        Forest forest;
        RenderWindow window;

        public event EventHandler<WorldState> WorldStateChanged;

        internal void MoveWindow(Vector2f direction)
        {
            var view = window.GetView();
            view.Move(direction);
            window.SetView(view);
        }

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
            int width = 16, height = 16, gridSize = 64;
            map = new TileMap(gridSize, width, height);
            forest = new Forest(gridSize, width, height, 256, 0.1);
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
    }
}