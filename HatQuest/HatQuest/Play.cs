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
    class Play
    {
        public Play()
        {

        }

        public MainState Update()
        {
            return MainState.Play;
        }

        public void Draw(SpriteBatch batch)
        {

        }
    }
}
