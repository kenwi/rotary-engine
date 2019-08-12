using System;
using Engine.GameStates.Worlds;
using SFML.Graphics;

namespace Engine.Interfaces
{
    public interface IWorld : IGameInput
    {
        WorldState WorldState { get; set; }
        event EventHandler<WorldState> WorldStateChanged;
        void Initialize(RenderWindow target);
        void Update(RenderWindow target, float deltaTime);
        void Draw(RenderWindow target);
    }
}