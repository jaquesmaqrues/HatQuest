using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HatQuest
{
    class Menu
    {
        public Menu()
        {

        }

        public MainState Update()
        {
            return MainState.Menu;
        }

        public void Draw(SpriteBatch batch)
        {

        }
    }
}
