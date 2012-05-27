using System;

namespace Venus
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (GameFrame game = new GameFrame())
            {
                game.Run();
            }
        }

    }
}

