using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Creation_Toolkit.Game_Engine.Scripts
{
    public class Coordinate : Script
    {
        public Vector2 Coordinates;
        public Coordinate(int EntityID, Vector2 StartPosition)
        {
            Coordinates = StartPosition;
        }
        
        public override void Update()
        {
            throw new NotImplementedException();
        }

        public override void Draw()
        {
            throw new NotImplementedException();
        }

        public override void DestroyScript()
        {
            throw new NotImplementedException();
        }
    }
}
