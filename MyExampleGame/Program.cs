using System;

namespace MyExampleGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new MyEcsGame(vSync: false, framerateLimit: 60000, fullScreen: false);
            game.Run();
        }
    }
}
