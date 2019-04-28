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
    class Instructions
    {
        private Rectangle backPosition;
        private Rectangle backstoryPosition;
        private Rectangle howToPlayPosition;

        //Fields for player input
        private MouseState mouseCurrent;
        private MouseState mouseLast;

        //Buttons
        private Button backButton;

        //Text
        private string backstory;
        private string howToPlay;

        //---------CONSTRUCTORS---------

        public Instructions()
        {
            //Buttons

            backPosition = new Rectangle((int)(SpritesDirectory.width * .0125), //10
                                         (int)(SpritesDirectory.height * 0.02083), //10
                                         (int)(SpritesDirectory.width * .125), //100 
                                         (int)(SpritesDirectory.height * .0833)); //40
            backButton = new Button("Back", backPosition, SpritesDirectory.GetFont("Arial40"));

            backButton.IsActive = backButton.IsVisible = true;

            //-----TEXT-----
            backstoryPosition = new Rectangle((int)(SpritesDirectory.width * .0125), //10
                                         (int)(SpritesDirectory.height * .4167), //200
                                         SpritesDirectory.width, //screen width 
                                         SpritesDirectory.height); //screen height

            howToPlayPosition = new Rectangle((int)(SpritesDirectory.width * .0125), //10
                                         (int)(SpritesDirectory.height * .625), //300
                                         SpritesDirectory.width, //screen width 
                                         SpritesDirectory.height); //screen height

            //Backstory
            backstory = "You play an elf named Elion. He saw a hat on an Ogre and decided to follow it into a cave to take his hat.";
            howToPlay = "Click on an ability button and then click on an enemy to attack.";
        }

        //---------METHODS---------
        public MainState Update()
        {
            //Update the current keyboard and mouse state
            mouseLast = mouseCurrent;
            mouseCurrent = Mouse.GetState();

            if (backButton.IsPressed(mouseLast, mouseCurrent))
            {
                return MainState.Menu;
            }
            else
            {
                return MainState.Instructions;
            }
        }

        public void Draw(SpriteBatch batch)
        {
            //Draw background
            batch.Draw(SpritesDirectory.GetSprite("CombatBackground"), new Rectangle(0, 0, SpritesDirectory.width, SpritesDirectory.height), Color.White);


            //Draw Header
            batch.DrawString(
                SpritesDirectory.GetFont("Arial40"),
                "Instructions",
                new Vector2((SpritesDirectory.width / 2) - (SpritesDirectory.GetFont("Arial40").MeasureString("Instructions").X / 2), (int)(SpritesDirectory.height * .04167)), //Half, 20
                Color.White);

            //Draw Backstory
            batch.DrawString(
                SpritesDirectory.GetFont("Arial16"),
                WordWrap(backstory, SpritesDirectory.GetFont("Arial16"), backstoryPosition),
                new Vector2((int)(SpritesDirectory.width * .04375), (int)(SpritesDirectory.height * .4167)), //35, 200
                Color.White);

            //Draw Instructions
            batch.DrawString(
                SpritesDirectory.GetFont("Arial16"),
                WordWrap(howToPlay, SpritesDirectory.GetFont("Arial16"), howToPlayPosition),
                new Vector2((SpritesDirectory.width / 2) - (SpritesDirectory.GetFont("Arial16").MeasureString(howToPlay).X / 2), (int)(SpritesDirectory.height * .625)),
                Color.White);

            //Draw Button
            backButton.Draw(batch);
        }

        public string WordWrap(string text, SpriteFont font, Rectangle rect, int padding = 5)
        {
            string[] words = text.Split(' ');
            Vector2 stringLength = new Vector2(0);
            Vector2 lineLength = new Vector2(0);
            string wrapped = "";
            string line = "";

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
    }
}
