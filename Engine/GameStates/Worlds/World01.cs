using Engine.Graphics;
using Engine.Managers;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace Engine.GameStates.Worlds
{
    public class World01 : BaseWorld
    {
        private Player _player;
        private int _playerMovementSpeed;
        private VertexTileMap _vertexTileMap;

        public World01() : base(128, 128, 32)
        {
        }

        public override void Initialize(RenderWindow target)
        {
            base.Initialize(target);
            var level = new byte[Width * Height];
            var length = (uint) MathF.Sqrt(Width * Height);
            var tileSet = AssetManager.Instance.Texture.Get(AssetManagerItemName.TileSetTexture);
            _vertexTileMap = new VertexTileMap();
            _vertexTileMap.Load(tileSet, new Vector2u(32, 32), level, length, length);
            _player = new Player(new Vector2i(), GridSize);
            _playerMovementSpeed = 200;
            ZoomWindow(0.5f);
        }

        public override void Update(RenderWindow target, float deltaTime)
        {
            CheckCameraInput(deltaTime);
            CheckPlayerInput(deltaTime);
        }

        public override void Draw(RenderWindow target)
        {
            Window.Draw(_vertexTileMap);
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