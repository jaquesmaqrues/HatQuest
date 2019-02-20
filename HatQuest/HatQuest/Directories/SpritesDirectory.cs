using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HatQuest.Init
{
    static class SpritesDirectory
    {
        public static Dictionary<string, Texture2D> spriteDirectory;
        public static Dictionary<string, SpriteFont> fontDirectory;

        public static void Init(Game game)
        {
            //Load in sprites
            spriteDirectory = new Dictionary<string, Texture2D>();
            spriteDirectory.Add("Lucario", game.Content.Load<Texture2D>("lucario"));

            //Load in fonts
            fontDirectory = new Dictionary<string, SpriteFont>();
        }

        /// <summary>
        /// Gets a sprite from the spriteDirectory
        /// </summary>
        /// <param name="key">Name of the sprite</param>
        /// <returns>Texture2D associated with the sprite.
        ///     Returns null if the key is not found.</returns>
        public static Texture2D GetSprite(string key)
        {
            try
            {
                return spriteDirectory[key];
            }
            catch(KeyNotFoundException ex)
            {
                //IDK write to the console maybe?
                return null;
            }
        }
    }
}
