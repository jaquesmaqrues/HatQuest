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
    /// <summary>
    /// Elijah, Iain
    /// </summary>
    class PoisonHat : Hat
    {
        public PoisonHat(Texture2D texture) : base("Poison Hat", "A hat that gives all your attacks poison damage", texture, Color.MediumPurple, HatRarity.Epic, null, 0, 0, 0, 5)
        {

        }

        public override void Equip(Entity entity)
        {
            entity.AttackPostEvent += ApplyPoison;
            base.Equip(entity);
        }

        private void ApplyPoison(Entity attacker, Entity defender)
        {
            new PoisonEffect(defender, 1);
        }
    }
}
