using Game_Creation_Toolkit.Game_Engine.Handlers;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Game_Creation_Toolkit.Game_Engine.Tools
{
    public class Timer
    {
        public bool isActive = false;
        public float Duration;
        float dur; //Used as the timer that will store the current duration (i.e how long it should be until the timer runs out)
        public Timer(float duration) 
        {
            dur = duration;
            Duration = dur;
            SystemHandlers.Timers.Add(this); //adds this to the timers list so it can be updated
        }
        public void Update()
        {
            if (isActive)
            {
                dur -= Core.ElapsedGameTime; //Used to take away a different amount based on the games fps
            }
            if(dur < 0 ) //chack to see if the timer has run out
            {
                isActive = false;
                dur = Duration;
            }
        }
        public void Begin()
        {
            isActive = true;
        }
        public void Dispose()
        {
            SystemHandlers.Timers.Remove(this);
        }
    }
}
