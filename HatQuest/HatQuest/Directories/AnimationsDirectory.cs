using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using HatQuest.Init;

namespace HatQuest.Directories
{
    static class AnimationsDirectory
    {
        private static Dictionary<string, Texture2D> animationsDirectory;

        //---------CONSTRUCTORS---------

        public static void animations(Game game)
        {
            animationsDirectory = new Dictionary<string, Texture2D>();
            animationsDirectory.Add("Mario", SpritesDirectory.GetSprite("MarioTest"));
        }

        //---------METHODS---------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Texture2D getAnimation(string key)
        {
            try
            {
                return animationsDirectory[key];
            }
            catch (KeyNotFoundException ex)
            {
                return null;
            }
        }
    }
}
