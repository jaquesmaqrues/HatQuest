using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HatQuest
{
    class Room
    {
        //Fields
        private Enemy[] enemies;
        private int currentAttacker;
        private float timer;

        //Properties
        public Enemy this[int i]
        {
            get { return enemies[i]; }
        }

        public Room(Enemy en1, Enemy en2 = null, Enemy en3 = null, Enemy en4 = null, Enemy en5 = null)
        {
            enemies = new Enemy[5];
            enemies[0] = en1;
            enemies[1] = en2;
            enemies[2] = en3;
            enemies[3] = en4;
            enemies[4] = en5;
        }

        /// <summary>
        /// Used as an Update method for the Room class while the Play class is in the EnemyTurn state
        /// </summary>
        /// <param name="player">The current player for the enemies to target</param>
        public void TakeEnemyTurn(Player player)
        {

        }

        public void Draw(SpriteBatch batch)
        {

        }
    }
}
