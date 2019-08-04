using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;

namespace Engine.GameStates.Worlds
{
    internal class World01 : BaseWorld
    {
        List<Keyboard.Key> keyDown;

        public override void Initialize(RenderWindow target)
        {
            keyDown = new List<Keyboard.Key>();
            base.Initialize(target);
        }

        private void updateCamera(float deltaTime)
        {
            var speed = 200;
            if (keyDown.Contains(Keyboard.Key.Left))
            {
                MoveWindow(new Vector2f(-speed * deltaTime, 0));
            }
            if (keyDown.Contains(Keyboard.Key.Right))
            {
                MoveWindow(new Vector2f(speed * deltaTime, 0));
            }
            if (keyDown.Contains(Keyboard.Key.Up))
            {
                MoveWindow(new Vector2f(0, -speed * deltaTime));
            }
            if (keyDown.Contains(Keyboard.Key.Down))
            {
                MoveWindow(new Vector2f(0, speed * deltaTime));
            }
        }

        public override void Update(RenderWindow target, float deltaTime)
        {
            updateCamera(deltaTime);
            base.Update(target, deltaTime);
        }

        public override void KeyPressed(RenderWindow target, object sender, KeyEventArgs e)
        {
            if (!keyDown.Contains(e.Code))
                keyDown.Add(e.Code);

            if(e.Code == Keyboard.Key.Comma || e.Code == Keyboard.Key.Period)
            {
                base.ZoomWindow(e.Code == Keyboard.Key.Comma ? 0.5f : 2f);
            } 

        }

        public override void KeyReleased(RenderWindow target, object sender, KeyEventArgs e)
        {
            if (keyDown.Contains(e.Code))
                keyDown.Remove(e.Code);
        }

        public override void MousePressed(RenderWindow window, object sender, MouseButtonEventArgs e)
        {
        }

        public override void MouseReleased(RenderWindow window, object sender, MouseButtonEventArgs e)
        {
        }
    }
}