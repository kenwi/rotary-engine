using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

namespace Engine.Graphics
{
    internal sealed class Forest : Tree
    {
        private readonly Queue<Vector2f> _forest;
        private readonly float _gridSize;

        public Forest(Texture treeTexture, float gridSize, int width, int height, int numberOfTrees, double density)
            : base(1, 1, gridSize, treeTexture, new IntRect(0, 0, 64, 96))
        {
            _gridSize = gridSize;
            _forest = new Queue<Vector2f>();
            var noise = new FastNoise(4);
            noise.SetFrequency(0.02f);
            noise.SetFractalLacunarity(5);
            noise.SetFractalOctaves(8);
            noise.SetFractalGain(0.6f);

            for (var i = 0; i < numberOfTrees; i++)
            {
                var x = i % width;
                var y = i / width;
                var value = noise.GetValueFractal(x, y, 0);
                if (value > density)
                    continue;
                _forest.Enqueue(new Vector2f(x, y));
            }
        }

        public override void Draw(RenderWindow target)
        {
            var offset = new Vector2f(0, -64);
            foreach (var treePosition in _forest)
            {
                Shape.Position = new Vector2f(treePosition.X * _gridSize, treePosition.Y * _gridSize) + offset;
                target.Draw(Shape);
            }
        }
    }
}