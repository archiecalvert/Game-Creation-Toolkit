using Game_Creation_Toolkit.Game_Engine.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Creation_Toolkit.Game_Engine.Base_Classes
{
    public abstract class Script
    {
        public abstract string ID { get;}
        public Script()
        {
            SystemHandlers.Scripts.Add(this);
        }
        abstract public void Update();
    }
}
