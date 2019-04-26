using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HatQuest.Hats;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using HatQuest.Init;
using HatQuest.Abilities;

namespace HatQuest.Init
{
    /// <summary>
    /// Elijah
    /// </summary>
    static class HatsDirectory
    {
        public static Random random = new Random(Program.seedRandom.Next());

        #region Hat Creation
        //List for storing all hats. Filled by the SetUp() method
        private static List<Hat> _hats = new List<Hat>();
        //Stores the number of hats of each rarity
        //0 - Common
        //1 - Uncommon
        //2 - Rare
        //3 - Epic
        private static int[] hatQuantities = new int[3];

        //---- Common Hats ----//
        private const float COMMON_RARITY = 0.8f;
        public static Hat ATKHAT = new Hat("Attack Hat", "A hat that boosts your attack", SpritesDirectory.GetSprite("Hat"), Color.PaleVioletRed, HatRarity.Common, null, 0, 0, 5);
        public static Hat HPHAT = new Hat("Health Hat", "A hat that boosts your health", SpritesDirectory.GetSprite("Hat"), Color.LightGreen, HatRarity.Common, null, 5);
        public static Hat DEFHAT = new Hat("Defense Hat", "A hat that boosts your defense", SpritesDirectory.GetSprite("Hat"), Color.LightYellow, HatRarity.Common, null, 0, 5);
        public static Hat MANAHAT = new Hat("Mana Hat", "A hat that boosts your mana", SpritesDirectory.GetSprite("Hat"), Color.LightSkyBlue, HatRarity.Common, null, 0, 0, 0, 5);
        //public static Hat EVENT_TEST = new EventTestingHat("One Hat", "A hat that makes you take one damage", SpritesDirectory.GetSprite("Hat"), HatRarity.Common);
        //public static Hat TESTHAT = new Hat("Super useful testing hat", "A hat that gives you the \'Attack\' ability", SpritesDirectory.GetSprite("Hat"), HatRarity.Common, AbilitiesDirectory.ATTACK, 69);

        //---- Uncommon Hats ----//
        private const float UNCOMMON_RARITY = 0.5f;

        public static Hat VAMPIREHAT = new Hat("Vampire Hat", "A hat that allows you to take on the power of a vampire and drain the life of your enemies", SpritesDirectory.GetSprite("VampireHat"), Color.White, HatRarity.Uncommon, new LifeSiphon(null), 0, 0, 0, 5);
        public static Hat TOASTERHAT = new Hat("Toaster Hat", "A hat that allows you to tap into unrelenting fury of a toaster", SpritesDirectory.GetSprite("ToasterHat"), Color.White, HatRarity.Uncommon, new Berserk(null), 5, 0, 0, 0);
        public static Hat CUTLERYHAT = new Hat("Cutlery Hat", "A hat that allows you to utilise the dexterity of the fork-gnomes", SpritesDirectory.GetSprite("Hat"), Color.White, HatRarity.Uncommon, new QuickAttack(null), 0, 0, 0, 5);
        public static Hat ALIENHAT = new Hat("Alien Hat", "A hat that allows you to call upon the power of THE CLAAAAAAWWWWW", SpritesDirectory.GetSprite("AlienHat"), Color.White, HatRarity.Uncommon, new Abduct(null), 0, 0, 5, 0);
        public static Hat SPIDERHAT = new Hat("Spider Hat", "A hat full of spiders on the inside", SpritesDirectory.GetSprite("Hat"), Color.MediumPurple, HatRarity.Uncommon, new VenomBite(null), 0, 0, 2, 3);
        
        //---- Epic Hats ----//
        private const float EPIC_RARITY = 1.0f;

        public static Hat BUCKETHAT = new BucketHat(SpritesDirectory.GetSprite("BucketHat"));
        public static Hat KNIGHTHAT = new KnightHelmet(SpritesDirectory.GetSprite("KnightHelm"));
        public static Hat POISONHAT = new PoisonHat(SpritesDirectory.GetSprite("Hat"));

        //---- Developer Hats ----//
        public static Hat GODMODE = new Hat("BORGER", "BORGER", SpritesDirectory.GetSprite("Hat"), Color.White, HatRarity.Developer, null, 5000, 5000, 5000, 5000);
        public static Hat BOSSHAT = new Hat("Boss Hat", "The Boss's version of his hat, you did something wrong if you have this equipped", SpritesDirectory.GetSprite("Hat"), Color.White, HatRarity.Developer, null, 15, 15, 15);
        public static Hat PLAYERBOSSHAT = new PlayerBossHat(SpritesDirectory.GetSprite("Hat"));
        #endregion

        /// <summary>
        /// Adds all Hat fields in this class to a list of Hats to use in random hat selection
        /// </summary>
        public static void SetUp()
        {
            //So long as Hat objects are defined in order of their rarity _hats will automatically be sorted
            #region SetUp()
            //Fill the list of hats
            Type t = typeof(HatsDirectory);
            foreach(FieldInfo field in t.GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                //Checks if the current field is a Hat
                if(field.FieldType == typeof(Hat))
                {
                    _hats.Add((Hat)field.GetValue(null));
                }
            }

            //Count the number of hats for each rarity
            foreach (Hat hat in _hats)
            {
                switch (hat.Rarity)
                {
                    case HatRarity.Common:
                        hatQuantities[0]++;
                        break;
                    case HatRarity.Uncommon:
                        hatQuantities[1]++;
                        break;
                    case HatRarity.Epic:
                        hatQuantities[2]++;
                        break;
                }
            }
            #endregion
        }

        /// <summary>
        /// Returns a randon hat priortizing hats from the given list
        /// </summary>
        /// <param name="floorLevel">Level the hat was found on</param>
        /// <param name="droppedHats">Hats to priortize</param>
        /// <returns></returns>
        public static Hat GetRandomHat(double floorLevel, Hat[] droppedHats = null)
        {
            #region Get a random hat priortizing those dropped by enemies
            double hatRoll = random.NextDouble();
            double rarityValue = 0f;
            floorLevel = Math.Pow(1.5, floorLevel-1);

            //Checks for hat drops of each rarity
            for(int r = 0; r < 3; r++)
            {
                //Sets the rarity for this iteration
                switch(r)
                {
                    case 0: rarityValue = Math.Pow(COMMON_RARITY, floorLevel); break;
                    case 1: rarityValue = Math.Pow(UNCOMMON_RARITY, floorLevel); break;
                    case 2: rarityValue = Math.Pow(EPIC_RARITY, floorLevel); break;
                }

                //Checks if the player rolled a higher rarity
                if(hatRoll > rarityValue)
                {
                    hatRoll -= rarityValue;
                }
                //Gives the player a hat of the rarity they rolled 
                else
                {
                    //Gets all dropped hats of the rolled rarity
                    List<Hat> possibleHats = new List<Hat>();
                    if(droppedHats != null)
                    {
                        foreach (Hat hat in droppedHats)
                        {
                            if (hat.Rarity == (HatRarity)r)
                                possibleHats.Add(hat);
                        }
                    }
                    
                    //Returns a random dropped hat of the rolled rarity if one exists
                    if(possibleHats.Count > 0)
                    {
                        return possibleHats[random.Next(possibleHats.Count)];
                    }
                    //Returns a random hat of the rolled rarity if none was dropped
                    else
                    {
                        return GetRandomHat((HatRarity)r);
                    }
                }
            }
            //Should never run but required to compile
            return GetRandomHat(HatRarity.Common);
            #endregion
        }

        /// <summary>
        /// Returns a random hat of the given rarity. Used by the public GetRandomHat methods
        /// </summary>
        /// <param name="rarity">Rarity of the hat to get</param>
        private static Hat GetRandomHat(HatRarity rarity)
        {
            #region Get a random hat of the given rarity
            //Find that beginning and end index of _hats that contains hats of the given rarity
            int startIndex = 0;
            int endIndex = 0;
            for(int k = 0; k < (int)rarity; k++)
            {
                startIndex += hatQuantities[k];
            }
            endIndex = startIndex + hatQuantities[(int)rarity];

            //Occurs when there are no hats of the given rarity in _hats
            if(endIndex == startIndex)
            {
                return GetRandomHat(rarity - 1);
            }
            //Returns a hat of the given rarity
            else
            {
                return _hats[random.Next(startIndex, endIndex)];
            }
            #endregion
        }
    }
}
