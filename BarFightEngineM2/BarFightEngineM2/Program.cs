using System;

namespace BarFightEngineM2
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (BarFight game = new BarFight())
            {
                game.Run();
            }
        }
    }
#endif
}

