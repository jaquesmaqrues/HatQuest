using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HatQuest.Abilities;

namespace HatQuest.Init
{
    static class AbilitiesDirectory
    {
        //This is where we could store a list of all abilities so  they only have to be made once and all entities
        //  just have refrences to the ability in this class
        public static Ability ATTACK = new Attack();
    }
}
