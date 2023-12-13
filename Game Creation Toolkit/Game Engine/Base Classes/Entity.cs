using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Game_Creation_Toolkit.Game_Engine.Base_Classes
{
    public class Entity
    {
        public Vector2 WorldPosition = new Vector2(0,0);
        public readonly int EntityID;
        public Entity(int entityID)
        {
            EntityID = entityID;
        }

    }
}
