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
        public static Hat ATKHAT = new Hat("Attack Hat", "A hat that boosts your attack", SpritesDirectory.GetSprite("Hat"), 0, 0, 5);
        public static Hat BUCKETHAT = new BucketHat(SpritesDirectory.GetSprite("Hat"));

        //Erin's hat
    }
}
