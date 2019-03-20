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
    class KnightHelmet : Hat
    {
        public KnightHelmet(Texture2D texture) : base("Knight's Helm", "A knight's helmet, which gives thee extra'rdinary defense", texture, HatRarity.Epic, null, 0, 15, 0, 0) { }
    }
}
