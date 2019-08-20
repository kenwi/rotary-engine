using SFML.System;

namespace Engine.Components
{
    public struct Position
    {
        public Vector2i Value;

        public Position(int x, int y) : this()
        {
            Value = new Vector2i(x, y);
        }
    }
}