using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HatQuest
{
    class RoomLayout
    {
        //Fields
        private EnemyType[] enemies;

        //Properties
        public EnemyType this[int i]
        {
            get
            {
                if(i >= 0 && i < 5)
                {
                    return enemies[i];
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="enemy1">First enemy</param>
        /// <param name="enemy2">Second enemy</param>
        /// <param name="enemy3"></param>
        /// <param name="enemy4"></param>
        /// <param name="enemy5"></param>
        public RoomLayout(EnemyType enemy1, EnemyType enemy2, EnemyType enemy3, EnemyType enemy4, EnemyType enemy5)
        {
            enemies = new EnemyType[] { enemy1, enemy2, enemy3, enemy4, enemy5 };
        }
    }
}
