using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HatQuest.Init
{
    static class EnemiesDirectory
    {
        //If we're going with the EnemyType idea then this is where they should all be stored
        //Example of how it would be declared (except for the null ability list we just dont have those yet lol)
        public static EnemyType GOBLIN = new EnemyType("Lucario", SpritesDirectory.GetSprite("Lucario"), 10, 2, 5, null);
    }
}
