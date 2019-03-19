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
    class Button
    {
        //Fields
        private bool active;
        private bool visible;
        private string text;
        private Rectangle rect;
        private Vector2 position;
        private SpriteFont font;
        private Texture2D buttonBack;
        private Texture2D buttonBackClicked;
        private MouseState mouse;
        private MouseState mousePrev;
        private float scale;
        private int padding;
        private bool clicked;

        //Value not changed from the default
        private Vector2 origin;

        //Properties
        public bool IsActive
        {
            get { return active; }
            set { active = value; }
        }
        
        public bool IsVisible
        {
            get { return visible; }
            set { visible = value; }
        }

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public bool Clicked
        {
            get { return clicked; }
            set { clicked = value; }
        }

        public Rectangle Rect
        {
            get { return rect; }
        }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="text">Text for the button to display</param>
        /// <param name="rect">Size and location of the button</param>
        /// <param name="font">Font for the text to be displayed in</param>
        /// <param name="padding">The distance between the text and the edge of the button</param>
        public Button(string text, Rectangle rect, SpriteFont font, int padding = 5)
        {
            this.text = text;
            this.rect = rect;
            this.font = font;
            this.padding = padding;

            //Un-used parameters in DrawString
            origin = new Vector2(0);

            clicked = false;
            buttonBack = SpritesDirectory.GetSprite("Button");
            buttonBackClicked = SpritesDirectory.GetSprite("ButtonClicked");

            //Calclates the scale
            Vector2 stringSize = font.MeasureString(text);
            float stringX = stringSize.X;
            float stringY = stringSize.Y;
            scale = Math.Min((rect.Height - (2 * padding))/stringY, (rect.Width - (2 * padding))/stringX);

            //Centers the text position
            position = new Vector2(rect.X + ((rect.Width - (stringX * scale)) * 0.5f), 
                                   rect.Y + ((rect.Height - (stringY * scale)) * 0.5f));

            mouse = Mouse.GetState();
        }

        /// <summary>
        /// Draws the button
        /// </summary>
        /// <param name="batch">SpriteBatch that us drawing all the assets for the game</param>
        public void Draw(SpriteBatch batch)
        {
            if (visible)
            {
                //Draw the background of the button
                if (clicked)
                {
                    batch.Draw(buttonBackClicked, rect, Color.White);
                }
                else
                {
                    batch.Draw(buttonBack, rect, Color.White);
                }
                //Draw the text of the button
                mousePrev = mouse;
                mouse = Mouse.GetState();

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
        }

        /// <summary>
        /// Returns if the button is currently being pressed
        /// </summary>
        /// <returns>True if the cursor is over the button and being clicked for the first time</returns>
        public bool IsPressed(MouseState prev, MouseState current)
        {
            mousePrev = prev;
            mouse = current;

            if(rect.Contains(mouse.Position))
            {
                if(mouse.LeftButton == ButtonState.Pressed && prev.LeftButton != ButtonState.Pressed)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
