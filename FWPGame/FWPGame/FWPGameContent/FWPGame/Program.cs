using System;

namespace FWPGame
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (FWPGame game = new FWPGame())
            {
                game.Run();
            }
        }
    }
#endif
}

