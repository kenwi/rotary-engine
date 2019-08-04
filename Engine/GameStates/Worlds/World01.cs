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
        List<Mouse.Button> mouseButtonDown;
        private Vector2i previousMousePosition;
        private Vector2i mouseDeltaVelocity;
        private float zoomLevel;

        public override void Initialize(RenderWindow target)
        {
            zoomLevel = 1;
            keyDown = new List<Keyboard.Key>();
            mouseButtonDown = new List<Mouse.Button>();
            base.Initialize(target);
        }

        private void updateCamera(float deltaTime)
        {
            var speed = 200;
            if (KeyDown(Keyboard.Key.Left))
            {
                MoveWindow(new Vector2f(-speed * deltaTime, 0));
            }
            if (KeyDown(Keyboard.Key.Right))
            {
                MoveWindow(new Vector2f(speed * deltaTime, 0));
            }
            if (KeyDown(Keyboard.Key.Up))
            {
                MoveWindow(new Vector2f(0, -speed * deltaTime));
            }
            if (KeyDown(Keyboard.Key.Down))
            {
                MoveWindow(new Vector2f(0, speed * deltaTime));
            }
            if(MouseDown(Mouse.Button.Left))
            {
                MoveWindow(new Vector2f(mouseDeltaVelocity.X, mouseDeltaVelocity.Y));
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

            if (e.Code == Keyboard.Key.Comma || e.Code == Keyboard.Key.Period)
            {
                var zoomLevelDelta = e.Code == Keyboard.Key.Comma ? 0.5f : 2f;
                zoomLevel *= zoomLevelDelta;
                base.ZoomWindow(zoomLevelDelta);
            }
        }

        private bool KeyDown(Keyboard.Key e) => keyDown.Contains(e);
        private bool MouseDown(Mouse.Button e) => mouseButtonDown.Contains(e);
        private void RemoveKey(Keyboard.Key e) => keyDown.Remove(e);

        public override void KeyReleased(RenderWindow target, object sender, KeyEventArgs e)
        {
            if (KeyDown(e.Code))
                RemoveKey(e.Code);
        }

        public override void MousePressed(RenderWindow window, object sender, MouseButtonEventArgs e)
        {
            if (!mouseButtonDown.Contains(e.Button))
                mouseButtonDown.Add(e.Button);
        }

        public override void MouseReleased(RenderWindow window, object sender, MouseButtonEventArgs e)
        {
            if (mouseButtonDown.Contains(e.Button))
                mouseButtonDown.Remove(e.Button);
        }

        public override void MouseMoved(RenderWindow window, object sender, MouseMoveEventArgs e)
        {
            var currentPosition = new Vector2i(e.X, e.Y);
            mouseDeltaVelocity = previousMousePosition - currentPosition;
            previousMousePosition = currentPosition;
        }
    }
}