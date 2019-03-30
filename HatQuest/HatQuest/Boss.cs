using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HatQuest.Init;

namespace HatQuest
{
    class Boss : Enemy
    {
        public Boss(double level, Player player) : base (EnemiesDirectory.BOSS, level, new Microsoft.Xna.Framework.Point(650, 100), 150, 380, player)
        {

        }
    }
}
