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
    class PlayerBossHat : Hat
    {
        public PlayerBossHat(Texture2D texture) : base("Boss Hat", "The hat granted by defeating the boss", texture, Color.White, HatRarity.Developer, null, 10, 10, 10, 10)
        {

        }
    }
}
