using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace HatQuest.Init
{
    /// <summary>
    /// Elijah
    /// </summary>
    class RoomsDirectory
    {
        private static Random random = new Random(Program.seedRandom.Next());
        public static List<EnemyType[]> layouts = new List<EnemyType[]>();


        /// <summary>
        /// Called once during initialization to load all the room layouts
        /// from a file. File is in \HatQuest\HatQuest\bin\Windows\x86\Debug\Layout_Files
        /// </summary>
        /// <param name="fileName">Name of the file to load rooms from</param>
        public static void ReadRooms(string fileName)
        {
            //File can only be named layouts and only one file is read
            fileName = Path.Combine(Environment.CurrentDirectory, "Layout_Files", "layouts.combat");
            FileStream inStream = null;
            BinaryReader reader = null;

            try//Loads level values from file
            {
                inStream = File.OpenRead(fileName);
                reader = new BinaryReader(inStream);

                //Gets the amount of room layouts first var in file
                int layoutAmount = reader.ReadInt32();

                for (int x = 0; x < layoutAmount; x++)
                {
                layouts.Add(new EnemyType[5]);
                    for (int y = 0; y < 5; y++)
                    {
                        //Gets the enemy number
                        int readerValue = reader.ReadInt32();

                        //Enemy number can be from -1 to 4
                        //-1 is null and more enemies can be added by increasing switch statement
                        switch (readerValue)
                        {
                            case 0:
                                layouts[x][y] = EnemiesDirectory.GOBLIN;
                                break;
                            case 1:
                                layouts[x][y] = EnemiesDirectory.VAMPIREBAT;
                                break;
                            case 2:
                                layouts[x][y] = EnemiesDirectory.FORKGNOME;
                                break;
                            case 3:
                                layouts[x][y] = null;
                                break;
                            case 4:
                                layouts[x][y] = null;
                                break;
                            default:
                                layouts[x][y] = null;
                                break;
                        }
                    }
                }
            }
            //If excepetion is thrown clear layout and makes only layout five goblins
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                layouts.Clear();
                layouts.Add(new EnemyType[] { EnemiesDirectory.GOBLIN, EnemiesDirectory.GOBLIN, EnemiesDirectory.GOBLIN, EnemiesDirectory.GOBLIN, EnemiesDirectory.FORKGNOME, });
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        /// <summary>
        /// Returns a random room from the list. DO NOT USE YET
        /// </summary>
        /// <returns>A random room layout</returns>
        public static EnemyType[] GetRandomLayout()
        {
            return layouts[random.Next(layouts.Count)];
        }
    }
}
