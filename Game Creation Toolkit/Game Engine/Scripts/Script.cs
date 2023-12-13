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
        public Script() { }
        public abstract void Update();
        public abstract void Draw();
        public abstract void DestroyScript();
    }
}
