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
            var random = new Random();
            for(int i=0; i<numberOfTrees; i++)
            {
                if(random.NextDouble() > density)
                    continue;
                    
                var x = i % width;
                var y = i / width;
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
