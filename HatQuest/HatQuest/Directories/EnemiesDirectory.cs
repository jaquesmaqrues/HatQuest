using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HatQuest.Init
{
    static class EnemiesDirectory
    {
        //Static random object for enemies to use
        public static Random random = new Random(314159);

        //If we're going with the EnemyType idea then this is where they should all be stored
        //Example of how it would be declared (except for the null ability list we just dont have those yet lol)
        public static EnemyType GOBLIN = new EnemyType("Goblin", SpritesDirectory.GetSprite("Goblin"), 10, 2, 5, new Ability[] { AbilitiesDirectory.ATTACK });
    }
}
