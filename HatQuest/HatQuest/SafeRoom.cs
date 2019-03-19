using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using HatQuest.Init;

namespace HatQuest
{
    /// <summary>
    /// Elijah
    /// </summary>
    class SafeRoom
    {
        //FIelds
        private double timer;
        SpriteFont font;

        /// <summary>
        /// Constructor
        /// </summary>
        public SafeRoom()
        {
            timer = 0;
            font = SpritesDirectory.GetFont("Arial40");
        }

        public PlayState Update(GameTime time)
        {
            timer -= time.ElapsedGameTime.TotalSeconds;
            if (timer > 0)
            {
                return PlayState.SafeRoom;
            }
            else
            {
                return PlayState.PlayerInput;
            }
        }

        public void Draw(SpriteBatch batch)
        {
            batch.DrawString(font, string.Format("{0:F3}", timer), new Vector2(150, 150), Color.Black);
        }

        /// <summary>
        /// Sets up the safe room for use. Always call before entering the safe room
        /// </summary>
        public void SetUp()
        {
            timer = 5;
        }
    }
}
