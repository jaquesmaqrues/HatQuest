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
    class PoisonHat : Hat
    {
        public PoisonHat(Texture2D texture) : base("Poison Hat", "A hat that gives all your attacks poison damage", texture, Color.White, HatRarity.Epic, null, 0, 0, 0, 5)
        {

        }
    }
}
