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
        private Player _player;
        private int _playerMovementSpeed;

        public override void Initialize(RenderWindow target)
        {
            base.Initialize(target);

            _playerMovementSpeed = 200;
            _player = new Player(new Vector2i(8, 5), 64);
            var treeTexture = AssetManager.Instance.Texture.Get(AssetManagerItemName.TreeTexture);
            _forest = new Forest(treeTexture, GridSize, Width, Height, 0.1);
        }

        public override void Update(RenderWindow target, float deltaTime)
        {
            CheckCameraInput(deltaTime);
            CheckPlayerInput(deltaTime);
        }

        public override void Draw(RenderWindow target)
        {
            base.Draw(target);

            _player.Draw(target);
            _forest.Draw(target);
        }

        private void CheckCameraInput(float deltaTime)
        {
            if (KeyPressedAndHolding(Keyboard.Key.Left))
                MoveWindow(new Vector2f(-_playerMovementSpeed * deltaTime, 0));
            if (KeyPressedAndHolding(Keyboard.Key.Right))
                MoveWindow(new Vector2f(_playerMovementSpeed * deltaTime, 0));
            if (KeyPressedAndHolding(Keyboard.Key.Up))
                MoveWindow(new Vector2f(0, -_playerMovementSpeed * deltaTime));
            if (KeyPressedAndHolding(Keyboard.Key.Down))
                MoveWindow(new Vector2f(0, _playerMovementSpeed * deltaTime));

            if (MouseDownAndHolding(Mouse.Button.Left))
                MoveWindow(new Vector2f(MouseDeltaVelocity.X, MouseDeltaVelocity.Y));
        }

        private void CheckPlayerInput(float deltaTime)
        {
            if (KeyPressedAndHolding(Keyboard.Key.S))
                _player.MovePosition(new Vector2f(0, _playerMovementSpeed * deltaTime));
            if (KeyPressedAndHolding(Keyboard.Key.W))
                _player.MovePosition(new Vector2f(0, -_playerMovementSpeed * deltaTime));
            if (KeyPressedAndHolding(Keyboard.Key.D))
                _player.MovePosition(new Vector2f(_playerMovementSpeed * deltaTime, 0));
            if (KeyPressedAndHolding(Keyboard.Key.A))
                _player.MovePosition(new Vector2f(-_playerMovementSpeed * deltaTime, 0));
        }
    }
}