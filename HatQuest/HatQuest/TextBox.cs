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
    /// Kat
    /// </summary>
    class TextBox
    {
        private bool visible;
        private string text;
        private Rectangle rect;
        private Vector2 position;
        private SpriteFont font;
        private Texture2D textboxBack;
        private MouseState mouse;
        private MouseState mousePrev;
        private float scale;
        private int padding;

        //Value not changed from the default
        private Vector2 origin;

        //---------PROPERTIES---------

        /// <summary>
        /// Gets and sets if textbox is visible
        /// </summary>
        public bool IsVisible
        {
            get { return visible; }
            set { visible = value; }
        }

        /// <summary>
        /// Gets and sets the text of the textbox
        /// </summary>
        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        /// <summary>
        /// Gets the rectangle of the textbox
        /// </summary>
        public Rectangle Rect
        {
            get { return rect; }
        }


        public SpriteFont Font
        {
            get { return font; }
            set { font = value; }
        }

        //---------CONSTRUCTORS---------

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="text">Text for the textbox to display</param>
        /// <param name="rect">Size and location of the textbox</param>
        /// <param name="font">Font for the text to be displayed in</param>
        /// <param name="padding">The distance between the text and the edge of the textbox</param>
        public TextBox(string text, Rectangle rect, SpriteFont font, int padding = 5)
        {
            this.text = text;
            this.rect = rect;
            this.font = font;
            this.padding = padding;

            //Un-used parameters in DrawString
            origin = new Vector2(0);

            textboxBack = SpritesDirectory.GetSprite("Button");

            //Calclates the scale
            Vector2 stringSize = font.MeasureString(text);
            float stringX = stringSize.X;
            float stringY = stringSize.Y;
            scale = Math.Min((rect.Height - (2 * padding)) / stringY, (rect.Width - (2 * padding)) / stringX);

            //Centers the text position
            position = new Vector2(rect.X + ((rect.Width - (stringX * scale)) * 0.5f),
                                   rect.Y + ((rect.Height - (stringY * scale)) * 0.5f));

            mouse = Mouse.GetState();
        }

        //---------METHODS---------

        /// <summary>
        /// Draws the button
        /// </summary>
        /// <param name="batch">SpriteBatch that us drawing all the assets for the game</param>
        public void Draw(SpriteBatch batch)
        {
            if (visible)
            {
                batch.Draw(textboxBack, rect, Color.White);
                batch.DrawString(font, text, position, Color.Black, 0, origin, scale, 0, 1);
            }
        }
    }
}