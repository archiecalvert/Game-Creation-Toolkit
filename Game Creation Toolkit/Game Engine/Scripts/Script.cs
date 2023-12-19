using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Creation_Toolkit.Game_Engine.Scripts
{
    public abstract class Script
    {
        //This is a base class that will be inherited by every other script
        //The methods will be able to be overwritten so that each script can have unique functionality
        public Script() { }
        public abstract void Update();
        public abstract void Draw();
        public abstract void DestroyScript();
    }
}
