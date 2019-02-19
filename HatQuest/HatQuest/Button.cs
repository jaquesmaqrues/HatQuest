using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGame
{
    class Button
    {
        //Fields
        private string text;
        private Rectangle rect;
        private Vector2 position;
        private SpriteFont font;
        private Texture2D buttonBack;
        private MouseState mouse;
        private float scale;
        private int padding;

        //Value not changed from the default
        private Vector2 origin;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="text">Text for the button to display</param>
        /// <param name="rect">Size and location of the button</param>
        /// <param name="font">Font for the text to be displayed in</param>
        /// <param name="padding">The distance between the text and the edge of the button</param>
        public Button(string text, Rectangle rect, SpriteFont font, Texture2D buttonBack, int padding = 5)
        {
            this.text = text;
            this.rect = rect;
            this.font = font;
            this.buttonBack = buttonBack;
            this.padding = padding;

            //Un-used parameters in DrawString
            origin = new Vector2(0);

            //Calclates the scale
            Vector2 stringSize = font.MeasureString(text);
            float stringX = stringSize.X;
            float stringY = stringSize.Y;
            scale = Math.Min((rect.Height - (2 * padding))/stringY, (rect.Width - (2 * padding))/stringX);

            //Centers the text position
            position = new Vector2(rect.X + ((rect.Width - (stringX * scale)) * 0.5f), 
                                   rect.Y + ((rect.Height - (stringY * scale)) * 0.5f));
        }

        /// <summary>
        /// Draws the button
        /// </summary>
        /// <param name="batch">SpriteBatch that us drawing all the assets for the game</param>
        public void Draw(SpriteBatch batch)
        {
            mouse = Mouse.GetState();

            //Draw the background of the button
            batch.Draw(buttonBack, rect, Color.OrangeRed);

            //Makes the button text white if it is being hovered over
            if (rect.Contains(mouse.Position))
            {
                batch.DrawString(font, text, position, Color.White, 0, origin, scale, 0, 1);
            }
            //Button text is black by default
            else
            {
                batch.DrawString(font, text, position, Color.Black, 0, origin, scale, 0, 1);
            }
        }

        /// <summary>
        /// Returns if the button is currently being pressed
        /// </summary>
        /// <returns>True if the cursor is over the button and being clicked</returns>
        public bool IsPressed()
        {
            mouse = Mouse.GetState();

            if(rect.Contains(mouse.Position))
            {
                if(mouse.LeftButton == ButtonState.Pressed)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
