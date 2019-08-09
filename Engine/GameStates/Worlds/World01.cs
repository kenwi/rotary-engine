using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;

namespace Engine.GameStates.Worlds
{
    public class World01 : BaseWorld
    {
        List<Keyboard.Key> keyDown;
        List<Mouse.Button> mouseButtonDown;
        private Vector2i mousePreviousPosition;
        private Vector2i mouseDeltaVelocity;
        private float zoomLevel;
        Player player;

        public override void Initialize(RenderWindow target)
        {
            zoomLevel = 1;
            keyDown = new List<Keyboard.Key>();
            mouseButtonDown = new List<Mouse.Button>();
            player = new Player(new Vector2i(8, 5), 64);

            base.Initialize(target);
        }

        private void updateCamera(float deltaTime)
        {
            var speed = 200;
            if (KeyPressedAndHolding(Keyboard.Key.Left))
            {
                MoveWindow(new Vector2f(-speed * deltaTime, 0));
            }
            if (KeyPressedAndHolding(Keyboard.Key.Right))
            {
                MoveWindow(new Vector2f(speed * deltaTime, 0));
            }
            if (KeyPressedAndHolding(Keyboard.Key.Up))
            {
                MoveWindow(new Vector2f(0, -speed * deltaTime));
            }
            if (KeyPressedAndHolding(Keyboard.Key.Down))
            {
                MoveWindow(new Vector2f(0, speed * deltaTime));
            }
        }

        public override void Update(RenderWindow target, float deltaTime)
        {
            updateCamera(deltaTime);
            var speed = 200;
            if(KeyPressedAndHolding(Keyboard.Key.S))
            {
                player.MovePosition(new Vector2f(0, speed * deltaTime));
            }
            if (KeyPressedAndHolding(Keyboard.Key.W))
            {
                player.MovePosition(new Vector2f(0, -speed * deltaTime));
            }
            if (KeyPressedAndHolding(Keyboard.Key.D))
            {
                player.MovePosition(new Vector2f(speed * deltaTime, 0));
            }
            if (KeyPressedAndHolding(Keyboard.Key.A))
            {
                player.MovePosition(new Vector2f(-speed * deltaTime, 0));
            }

            base.Update(target, deltaTime);
        }

        public override void Draw(RenderWindow target)
        {
            base.Draw(target);
            player.Draw(target);
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

        private bool KeyPressedAndHolding(Keyboard.Key e) => keyDown.Contains(e);
        private bool MouseDown(Mouse.Button e) => mouseButtonDown.Contains(e);
        public override void MouseReleased(RenderWindow window, object sender, MouseButtonEventArgs e) => mouseButtonDown.Remove(e.Button);
        public override void KeyReleased(RenderWindow target, object sender, KeyEventArgs e) => keyDown.Remove(e.Code);

        public override void MousePressed(RenderWindow window, object sender, MouseButtonEventArgs e)
        {
            if (!mouseButtonDown.Contains(e.Button))
                mouseButtonDown.Add(e.Button);
        }

        public override void MouseMoved(RenderWindow window, object sender, MouseMoveEventArgs e)
        {
            var currentPosition = new Vector2i(e.X, e.Y);
            mouseDeltaVelocity = mousePreviousPosition - currentPosition;
            mousePreviousPosition = currentPosition;
            

            if (MouseDown(Mouse.Button.Left))
                MoveWindow(new Vector2f(mouseDeltaVelocity.X * zoomLevel, mouseDeltaVelocity.Y * zoomLevel));
        }

        public override void MouseWheelScrolled(RenderWindow window, object sender, MouseWheelScrollEventArgs e)
        {
            var zoomLevelDelta = e.Delta == 1 ? 0.5f : 2f;
            zoomLevel *= zoomLevelDelta;
            ZoomWindow(zoomLevelDelta);
        }
    }
}