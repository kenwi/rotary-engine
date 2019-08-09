using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

namespace Engine.Graphics
{
    sealed internal class Forest : Tree
    {
        Queue<Vector2f> forest;
        float gridSize;
    
        public Forest(Texture treeTexture, float gridSize, int width, int height, int numberOfTrees, double density) 
            : base(1, 1, gridSize, treeTexture, new IntRect(0, 0, 64, 96))
        {
            this.gridSize = gridSize;
            forest = new Queue<Vector2f>();
            var noise = new FastNoise(4);
            noise.SetFrequency(0.02f);
            noise.SetFractalLacunarity(5);
            noise.SetFractalOctaves(8);
            noise.SetFractalGain(0.6f);

            for(int i=0; i<numberOfTrees; i++)
            {
                var x = i % width;
                var y = i / width;
                var value = noise.GetValueFractal(x, y, 0);
                if(value > density)
                    continue;
                forest.Enqueue(new Vector2f(x, y));
            }
        }

        public override void Draw(RenderWindow target)
        {
            var offset = new Vector2f(0, -64);
            foreach(var treePosition in forest)
            {
                shape.Position = new Vector2f(treePosition.X * gridSize, treePosition.Y * gridSize) + offset;
                target.Draw(shape);
            }
        }
    }
}
