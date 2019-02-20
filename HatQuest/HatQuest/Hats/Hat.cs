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
    class Hat
    {
        int maxHealth;
        int def;
        int atk;
        int maxMana;
        Texture2D texture;
        Rectangle position;
    }
}
