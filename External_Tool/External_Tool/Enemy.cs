using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace External_Tool
{
    //Every enemey holds both a difficutly enum and and enemyNum
    //EnemyNum is from the int value of the pictureBoxes in the groupBoxSource
    //This means that each picturebox in the top row has a unique int value that each 
    //enemy gets when the image is dragged in to the bottom picturebox
    class Enemy
    {
        Difficulty diff;
        int enemyNum;

        public Difficulty Diff
        {
            get{ return diff; }
            set { diff = value; }
        }

        public int EnemyNum
        {
            get { return enemyNum; }
            set { enemyNum = value; }
        }


        public Enemy(Difficulty diff, int enemyNum)
        {
            this.diff = diff;
            this.enemyNum = enemyNum;
        }
    }
}
