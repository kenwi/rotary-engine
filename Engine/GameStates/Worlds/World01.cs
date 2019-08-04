using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Engine.GameStates.Worlds
{
    internal class World01 : BaseWorld
    {
        public override void KeyPressed(RenderWindow target, object sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Left)
            {
                base.MoveWindow(new Vector2f(-2, 0));
            }
            if (e.Code == Keyboard.Key.Right)
            {
                base.MoveWindow(new Vector2f(2, 0));
            }
            if (e.Code == Keyboard.Key.Up)
            {
                base.MoveWindow(new Vector2f(0, -2));
            }
            if (e.Code == Keyboard.Key.Down)
            {
                base.MoveWindow(new Vector2f(0, 2));
            }
        }

        public override void KeyReleased(RenderWindow target, object sender, KeyEventArgs e)
        {
        }
    }
}