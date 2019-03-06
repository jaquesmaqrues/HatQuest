using System;

namespace HatQuest
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        //Seed random object
        public static Random seedRandom;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Seed random object
            seedRandom = new Random();

            using (var game = new HatGame())
                game.Run();
        }
    }
#endif
}
