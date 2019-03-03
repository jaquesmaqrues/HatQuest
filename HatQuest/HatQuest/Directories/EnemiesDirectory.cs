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
        public static EnemyType FORKGNOME = new EnemyType("Fork-Gnome", SpritesDirectory.GetSprite("Goblin"), 5, 4, 2, new Ability[] { AbilitiesDirectory.QUICKATTACK, AbilitiesDirectory.ATTACK });
        public static EnemyType VAMPIREBAT = new EnemyType("Vampire Bat", SpritesDirectory.GetSprite("Goblin"), 5, 4, 1, new Ability[] { AbilitiesDirectory.ATTACK, AbilitiesDirectory.LIFESIPHON });
        public static EnemyType ANGRYTOASTER = new EnemyType("Angry Sentient Toaster", SpritesDirectory.GetSprite("Goblin"), 15, 6, 2, new Ability[] { AbilitiesDirectory.ATTACK, AbilitiesDirectory.BERSERK });
    }
}
