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
        float dur;
        
        public Timer(float duration) 
        {
            dur = duration;
            Duration = dur;
            SystemHandlers.Timers.Add(this);
        }
        public void Update()
        {
            if (isActive)
            {
                dur -= Core.ElapsedGameTime;
            }
            if(dur < 0 )
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
