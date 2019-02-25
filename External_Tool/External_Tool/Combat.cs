using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace External_Tool
{
    //Each combat holds an array of eneimes
    //The array is based on the amount of pictureBoxes in the groupBoxResult
    class Combat
    {
        Enemy[] enemies;

        public Enemy this[int index]
        {
            get { return enemies[index]; }
            set { enemies[index] = value; }
        }

        public Combat(Enemy[] enemies=null)
        {
            this.enemies = enemies;
        }
    }
}
