using Game_Creation_Toolkit.Game_Engine.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Creation_Toolkit.Game_Engine.Handlers
{
    public class SystemHandlers
    {
        public static List<Timer> Timers = new List<Timer>(); //Stores a list of the current timers
        public static string CurrentProjectDirectory = ""; //Stores the current directory of the current project being worked on by the user

        public static void Update()
        {
            for(int i = Timers.Count; i > 0; i--)
            {
                Timers[i-1].Update();//updates each timer each frame
            }
        }
    }
}
