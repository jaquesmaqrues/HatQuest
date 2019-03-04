using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HatQuest.Hats;

namespace HatQuest.Init
{
    static class HatsDirectory
    {
        //Yall already know what it is bois
        //---- F-Tier Hats ----//
        public static Hat ATKHAT = new Hat("Attack Hat", "A hat that boosts your attack", SpritesDirectory.GetSprite("Hat"), 0, 0, 5);
        public static Hat HPHAT = new Hat("Health Hat", "A hat that boosts your health", SpritesDirectory.GetSprite("Hat"), 5);
        public static Hat DEFHAT = new Hat("Defense Hat", "A hat that boosts your defense", SpritesDirectory.GetSprite("Hat"), 0, 5);
        public static Hat MANAHAT = new Hat("Mana Hat", "A hat that boosts your mana", SpritesDirectory.GetSprite("Hat"), 0, 0, 0, 5);

        //---- S-Tier Hats ----//
        public static Hat BUCKETHAT = new BucketHat(SpritesDirectory.GetSprite("Hat"));

        //Erin's hat
    }
}
