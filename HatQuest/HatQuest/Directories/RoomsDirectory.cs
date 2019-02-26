using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HatQuest.Init
{
    class RoomsDirectory
    {
        private static Random random = new Random(01010001);
        public static List<EnemyType[]> layouts = new List<EnemyType[]>();
        

        /// <summary>
        /// Called once during initialization to load all the room layouts
        /// </summary>
        /// <param name="fileName">Name of the file to load rooms from</param>
        public static void ReadRooms(string fileName)
        {
            EnemyType enemy1;
            EnemyType enemy2;
            EnemyType enemy3;
            EnemyType enemy4;
            EnemyType enemy5;


            layouts.Add(new EnemyType[] { EnemiesDirectory.GOBLIN, EnemiesDirectory.GOBLIN, EnemiesDirectory.GOBLIN, EnemiesDirectory.GOBLIN, EnemiesDirectory.GOBLIN, });


        }

        /// <summary>
        /// Returns a random room from the list. DO NOT USE YET
        /// </summary>
        /// <returns>A random room layout</returns>
        public static EnemyType[] GetRandomLayout()
        {
            return layouts[random.Next(layouts.Count)];
        }
    }
}
