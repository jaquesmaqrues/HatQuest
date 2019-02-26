using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HatQuest.Init
{
    class RoomsDirectory
    {
        private Random random = new Random();
        public static List<RoomLayout> layouts = new List<RoomLayout>();
        
        /// <summary>
        /// Called once during initialization to load all the room layouts
        /// </summary>
        /// <param name="fileName">Name of the file to load rooms from</param>
        public void ReadRooms(string fileName)
        {
            EnemyType enemy1;
            EnemyType enemy2;
            EnemyType enemy3;
            EnemyType enemy4;
            EnemyType enemy5;
            int difficulty;
        }

        /// <summary>
        /// Returns a random room from the list. DO NOT USE YET
        /// </summary>
        /// <returns>A random room layout</returns>
        public RoomLayout GetRandomLayout()
        {
            return layouts[random.Next(layouts.Count)];
        }
    }
}
