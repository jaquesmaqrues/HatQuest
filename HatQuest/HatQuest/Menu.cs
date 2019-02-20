using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HatQuest
{
    class Menu
    {
        private Texture2D image;
        private Rectangle position;

        //---------PROPERTIES---------

        public Texture2D Image
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
            }
        }

        //---------CONSTRUCTORS---------
        //We can't pass images into the constructor for the Menu since the menu is initialized before images are
        public Menu(/*Texture2D image, Rectangle position*/)
        {
            //this.image = image;
            //this.position = position;
        }

        //---------METHODS---------
        public MainState Update()
        {
            return MainState.Play;
        }

        public void Draw(SpriteBatch batch)
        {
            //Call sprite using Sprite.cs to draw for menu 
        }
    }
}
