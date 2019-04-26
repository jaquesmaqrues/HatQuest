using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HatQuest.Abilities;

namespace HatQuest.Init
{
    /// <summary>
    /// Elijah, Kat
    /// </summary>
    static class EnemiesDirectory
    {
        private static List<EnemyType> enemyTypes = new List<EnemyType>();
        //Static random object for enemies to use
        public static Random random = new Random(Program.seedRandom.Next());

        //If we're going with the EnemyType idea then this is where they should all be stored
        //Example of how it would be declared (except for the null ability list we just dont have those yet lol)
        public static EnemyType GOBLIN = new EnemyType("Goblin", SpritesDirectory.GetSprite("Goblin"), 10, 2, 5, new Ability[] { new Attack(null) }, false, 11);
        public static EnemyType FORKGNOME = new EnemyType("Fork-Gnome", SpritesDirectory.GetSprite("Gnome"), 5, 6, 2, new Ability[] { new QuickAttack(null), new Attack(null) }, false, 20);
        public static EnemyType VAMPIREBAT = new EnemyType("Vampire Bat", SpritesDirectory.GetSprite("VampireBat"), 5, 4, 1, new Ability[] { new Attack(null), new LifeSiphon(null) }, false, 52);
        public static EnemyType ANGRYTOASTER = new EnemyType("Angry Toaster", SpritesDirectory.GetSprite("AngryToaster"), 15, 6, 2, new Ability[] { new Attack(null), new Berserk(null) }, false, 10);
        public static EnemyType ALIEN = new EnemyType("Alien", SpritesDirectory.GetSprite("Alien"), 5, 3, 0, new Ability[] { new Attack(null), new Abduct(null) }, false, 10);

        //Boss Boi
        public static EnemyType BOSS = new EnemyType("Ogre", SpritesDirectory.GetSprite("Goblin"), 15, 15, 15, new Ability[] { new Abduct(null), new QuickAttack(null), new LifeSiphon(null)}, true, 10);

        /// <summary>
        /// Adds all EnemyType fields in this class to a list of Hats to use in random hat selection
        /// </summary>
        public static void SetUp()
        {
            //So long as Hat objects are defined in order of their rarity _hats will automatically be sorted

            //Fill the list of hats
            Type t = typeof(EnemiesDirectory);
            foreach (FieldInfo field in t.GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                //Checks if the current field is a Hat
                if (field.FieldType == typeof(EnemyType))
                {
                    if (!((EnemyType)field.GetValue(null)).IsBoss)
                    {
                        enemyTypes.Add((EnemyType)field.GetValue(null));
                    }
                }
            }
        }

        public static EnemyType RANDOM()
        {
            return enemyTypes[random.Next(enemyTypes.Count)];
        }
    }
}
