using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using HatQuest.Init;
using HatQuest.Effects;

namespace HatQuest.Hats
{
    class PlayerBossHat : Hat
    {
        /// <summary>
        /// An unused hat that would be given to the player upon defeating the final boss, which gives the BossHatEffect
        /// </summary>
        /// <param name="texture">The texture of the hat</param>
        public PlayerBossHat(Texture2D texture) : base("Boss Hat", "The hat granted by defeating the boss", texture, Color.White, HatRarity.Developer, null, 10, 10, 10, 10)
        {

        }

        public override void Equip(Entity entity)
        {
            new BossHatEffect(entity);
            base.Equip(entity);
        }
    }
}
