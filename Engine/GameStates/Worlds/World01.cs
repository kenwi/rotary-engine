using System;
using System.Collections.Generic;
using Engine.Graphics;
using Engine.Managers;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Engine.GameStates.Worlds
{
    public class World01 : BaseWorld
    {
        private Forest _forest;
        private List<Keyboard.Key> _keyDown;
        private List<Mouse.Button> _mouseButtonDown;
        private Vector2i _mouseDeltaVelocity;
        private Vector2i _mousePreviousPosition;
        private Player _player;

        public override void Initialize(RenderWindow target)
        {
            _keyDown = new List<Keyboard.Key>();
            _mouseButtonDown = new List<Mouse.Button>();
            _player = new Player(new Vector2i(8, 5), 64);

            var treeTexture = AssetManager.Instance.Texture.Get(AssetManagerItemName.TreeTexture);
            _forest = new Forest(treeTexture, GridSize, Width, Height, 0.1);

            base.Initialize(target);
        }

        private void UpdateCamera(float deltaTime)
        {
            const int speed = 200;
            if (KeyPressedAndHolding(Keyboard.Key.Left)) MoveWindow(new Vector2f(-speed * deltaTime, 0));
            if (KeyPressedAndHolding(Keyboard.Key.Right)) MoveWindow(new Vector2f(speed * deltaTime, 0));
            if (KeyPressedAndHolding(Keyboard.Key.Up)) MoveWindow(new Vector2f(0, -speed * deltaTime));
            if (KeyPressedAndHolding(Keyboard.Key.Down)) MoveWindow(new Vector2f(0, speed * deltaTime));
        }

        public override void Update(RenderWindow target, float deltaTime)
        {
            const int speed = 200;
            UpdateCamera(deltaTime);
            if (KeyPressedAndHolding(Keyboard.Key.S)) _player.MovePosition(new Vector2f(0, speed * deltaTime));
            if (KeyPressedAndHolding(Keyboard.Key.W)) _player.MovePosition(new Vector2f(0, -speed * deltaTime));
            if (KeyPressedAndHolding(Keyboard.Key.D)) _player.MovePosition(new Vector2f(speed * deltaTime, 0));
            if (KeyPressedAndHolding(Keyboard.Key.A)) _player.MovePosition(new Vector2f(-speed * deltaTime, 0));
        }

        public override void Draw(RenderWindow target)
        {
            base.Draw(target);
            _player.Draw(target);
            _forest.Draw(target);
        }

        public override void KeyPressed(RenderWindow target, object sender, KeyEventArgs e)
        {
            if (!_keyDown.Contains(e.Code))
                _keyDown.Add(e.Code);

            if (e.Code != Keyboard.Key.Comma && e.Code != Keyboard.Key.Period) return;

            var zoomLevelDelta = e.Code == Keyboard.Key.Comma ? 0.5f : 2f;
            ZoomWindow(zoomLevelDelta);
        }

        private bool KeyPressedAndHolding(Keyboard.Key e)
        {
            return _keyDown.Contains(e);
        }

        private bool MouseDown(Mouse.Button e)
        {
            return _mouseButtonDown.Contains(e);
        }

        public override void MouseReleased(RenderWindow window, object sender, MouseButtonEventArgs e)
        {
            _mouseButtonDown.Remove(e.Button);
        }

        public override void KeyReleased(RenderWindow target, object sender, KeyEventArgs e)
        {
            _keyDown.Remove(e.Code);
        }

        public override void MousePressed(RenderWindow window, object sender, MouseButtonEventArgs e)
        {
            if (!_mouseButtonDown.Contains(e.Button))
                _mouseButtonDown.Add(e.Button);
        }

        public override void MouseMoved(RenderWindow window, object sender, MouseMoveEventArgs e)
        {
            var currentPosition = new Vector2i(e.X, e.Y);
            _mouseDeltaVelocity = _mousePreviousPosition - currentPosition;
            _mousePreviousPosition = currentPosition;

            if (MouseDown(Mouse.Button.Left))
                MoveWindow(new Vector2f(_mouseDeltaVelocity.X, _mouseDeltaVelocity.Y));
        }

        public override void MouseWheelScrolled(RenderWindow window, object sender, MouseWheelScrollEventArgs e)
        {
            var zoomLevelDelta = Math.Abs(e.Delta - 1) < 1.0 ? 0.5f : 2f;
            ZoomWindow(zoomLevelDelta);
        }
    }
}