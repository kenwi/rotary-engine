using SFML.Graphics;
using SFML.System;

namespace Engine.Components
{
    public struct DebugWindow
    {
        public DebugWindow(Vector2f position) : this()
        {
            Shape.Position = position;
        }

        public Shape Shape { get; }
        public float OutlineThickness => Shape.OutlineThickness;
        public Color FillColor => Shape.FillColor;
        public Font Font { get; }
        public float FontSize { get; }
    }
}
