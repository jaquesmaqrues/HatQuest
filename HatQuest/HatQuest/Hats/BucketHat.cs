using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using HatQuest.Init;

namespace HatQuest.Hats
{
    class BucketHat : Hat
    {
        public BucketHat(Texture2D texture) : base("Bucket Hat", "A bucket to hold your tears", texture, Color.White, HatRarity.Epic, null, -5, 5)
        {

        }

        public override void Equip(Entity entity)
        {
            base.Equip(entity);
        }
    }
}
