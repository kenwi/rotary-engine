using SFML.System;

namespace Engine.Components
{
    public struct Camera
    {
        public Vector2f Position;
        
        public Camera(Vector2f position)
        {
            Position = position;
        }
    }
}
