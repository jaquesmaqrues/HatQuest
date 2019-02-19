using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

/* Iain Davis
 * The Enemy class that each enemy will inherit from
 * There are no known issues */

namespace HatQuest
{
    class Enemy : Entity
    {
        private Player player;

        public Enemy(Texture2D texture, Point position, int width, int height, Player player) : base(texture, position, width, height)
        {
            this.player = player;
        }

        public void AttackPlayer(Ability ability)
        {
            ability.Activate(this, player);
        }
    }
}
