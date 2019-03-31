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
        private string wrapped;
        private string line;

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
            set { text = WordWrap(value); }
        }

        /// <summary>
        /// Gets the rectangle of the textbox
        /// </summary>
        public Rectangle Rect
        {
            get { return rect; }
        }

        /// <summary>
        /// Gets and sets the font of the text
        /// </summary>
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

            //Word Wrap
            string[] words = text.Split(' ');
            Vector2 stringLength = new Vector2(0);
            wrapped = "";

            //Centers the text position
            position = new Vector2(rect.X + 15, rect.Y + 15);

            mouse = Mouse.GetState();
        }

        //---------METHODS---------

        /// <summary>
        /// Calculates word wrap on entered string for textbox
        /// </summary>
        /// <param name="text">String to word wrap</param>
        /// <returns>Word wrapped string</returns>
        public string WordWrap(string text)
        {
            string[] words = text.Split(' ');
            Vector2 stringLength = new Vector2(0);
            Vector2 lineLength = new Vector2(0);
            wrapped = "";
            line = "";

            foreach (string s in words)
            {
                Vector2 wordSize = font.MeasureString(" " + s);

                if ((lineLength.X + wordSize.X) < ((rect.Width - 15) - (padding)))
                {
                    line = line + s + " ";
                    lineLength.X = font.MeasureString(line).X;
                }
                else
                {
                    wrapped = wrapped + line + "\n";
                    line = s + " ";
                    lineLength.X = 0;
                }
            }

            wrapped = wrapped + line;

            return wrapped;
        }

        /// <summary>
        /// Draws the button
        /// </summary>
        /// <param name="batch">SpriteBatch that us drawing all the assets for the game</param>
        public void Draw(SpriteBatch batch)
        {
            batch.Draw(textboxBack, rect, Color.White);
            batch.DrawString(font, text, position, Color.Black);
        }
    }
}