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
        private TextBox playerStats;

        //player stat fields
        private int hatsCollected;
        private int roomsCleared;
        private int enemiesDefeated;

        /// <summary>
        /// Constructor
        /// </summary>
        public SafeRoom()
        {
            timer = 0;
            playerStats = new TextBox("player stats",
                                      new Rectangle((int)(SpritesDirectory.width * 0.25),
                                                    (int)(SpritesDirectory.height * 0.7),
                                                    (int)(SpritesDirectory.width * 0.5),
                                                    (int)(SpritesDirectory.height * 0.25)),
                                      SpritesDirectory.GetFont("Arial16"));
        }

        public PlayState Update(KeyboardState current, KeyboardState last)
        {
            if(timer < 1)
            {
                if (current.IsKeyDown(Keys.Enter) && last.IsKeyDown(Keys.Enter))
                {
                    return PlayState.PlayerInput;
                }
                else
                {
                    return PlayState.SafeRoom;
                }
            }
            else
            {
                timer--;
                return PlayState.SafeRoom;
            }
        }

        public void Draw(SpriteBatch batch)
        {
            playerStats.Draw(batch);
        }

        /// <summary>
        /// Sets up the safe room for use. Always call before entering the safe room
        /// </summary>
        /// <param name="hatsCollected">The number of hats the player has</param>
        public void SetUp(int hatsCollected)
        {
            this.hatsCollected = hatsCollected;
            timer = 5;

            playerStats.Text = String.Format("Enemies defeated: {0}\nRooms cleared: {1}\nHats collected: {2}", enemiesDefeated, roomsCleared, hatsCollected);
        }

        /// <summary>
        /// Reset all the saved player statistics
        /// </summary>
        public void Reset()
        {
            hatsCollected = 0;
            roomsCleared = 0;
            enemiesDefeated = 0;
        }

        public void UpdateEnemyStat(Entity attacker, Entity defender)
        {
            if(!defender.IsActive)
            {
                enemiesDefeated++;
            } 
        }

        public void UpdateRoomStat()
        {
            roomsCleared++;
        }
    }
}
