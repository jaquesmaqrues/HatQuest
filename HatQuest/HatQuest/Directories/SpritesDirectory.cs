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
    /// <summary>
    /// Elijah, Kat
    /// </summary>
    static class SpritesDirectory
    {
        public static Dictionary<string, Texture2D> spriteDirectory;
        public static Dictionary<string, SpriteFont> fontDirectory;
        public static int width;
        public static int height;

        public static void Init(Game game)
        {
            //Load in sprites
            spriteDirectory = new Dictionary<string, Texture2D>();
            spriteDirectory.Add("Lucario", game.Content.Load<Texture2D>("lucario"));
            spriteDirectory.Add("Elion", game.Content.Load<Texture2D>("Main_Character_Color"));
            spriteDirectory.Add("Button", game.Content.Load<Texture2D>("Sprites/Button Background"));
            spriteDirectory.Add("ButtonClicked", game.Content.Load<Texture2D>("Sprites/buttonBackground"));
            spriteDirectory.Add("CombatBackground", game.Content.Load<Texture2D>("Sprites/Combat_Background"));
            spriteDirectory.Add("Goblin", game.Content.Load<Texture2D>("Sprites/Goblin"));
            spriteDirectory.Add("Hat", game.Content.Load<Texture2D>("Sprites/SuperFancyHat"));
            spriteDirectory.Add("BucketHat", game.Content.Load<Texture2D>("Sprites/BucketOfTears"));

            //Animation Sprite Sheets
            spriteDirectory.Add("MarioTest", game.Content.Load<Texture2D>("Mario"));

            //Load in fonts
            fontDirectory = new Dictionary<string, SpriteFont>();
            fontDirectory.Add("Arial", game.Content.Load<SpriteFont>("File"));
            fontDirectory.Add("Arial40", game.Content.Load<SpriteFont>("Fonts/Arial40"));

            height = game.GraphicsDevice.Viewport.Height;
            width = game.GraphicsDevice.Viewport.Width;
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

        public static SpriteFont GetFont(string key)
        {
            try
            {
                return fontDirectory[key];
            }
            catch (KeyNotFoundException ex)
            {
                //IDK write to the console maybe?
                return null;
            }
        }
    }
}
