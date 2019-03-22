using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public static EnemyType GOBLIN = new EnemyType("Goblin", SpritesDirectory.GetSprite("Goblin"), 10, 2, 5, new Ability[] { AbilitiesDirectory.ATTACK });
        public static EnemyType FORKGNOME = new EnemyType("Fork-Gnome", SpritesDirectory.GetSprite("Goblin"), 5, 4, 2, new Ability[] { AbilitiesDirectory.QUICKATTACK, AbilitiesDirectory.ATTACK });
        public static EnemyType VAMPIREBAT = new EnemyType("Vampire Bat", SpritesDirectory.GetSprite("Goblin"), 5, 4, 1, new Ability[] { AbilitiesDirectory.ATTACK, AbilitiesDirectory.LIFESIPHON });
        public static EnemyType ANGRYTOASTER = new EnemyType("Angry Sentient Toaster", SpritesDirectory.GetSprite("Goblin"), 15, 6, 2, new Ability[] { AbilitiesDirectory.ATTACK, AbilitiesDirectory.BERSERK });
        public static EnemyType ALIEN = new EnemyType("An Alien", SpritesDirectory.GetSprite("Goblin"), 5, 3, 0, new Ability[] { AbilitiesDirectory.ATTACK, AbilitiesDirectory.ABDUCT });

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
                    enemyTypes.Add((EnemyType)field.GetValue(null));
                }
            }
        }

        public static EnemyType RANDOM()
        {
            return enemyTypes[random.Next(enemyTypes.Count)];
        }
    }
}
