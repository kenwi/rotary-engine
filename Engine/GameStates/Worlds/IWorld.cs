using System;
using SFML.Graphics;

namespace Engine.GameStates.Worlds
{
    public interface IWorld : IGameInput
    {
        event EventHandler<WorldState> WorldStateChanged;
        WorldState WorldState { get; set; }
        void Initialize(RenderWindow target);
        void Update(RenderWindow target, float deltaTime);
        void Draw(RenderWindow target);
    }
}