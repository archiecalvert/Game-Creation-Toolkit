using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Game_Creation_Toolkit.Game_Engine.Scripts
{
    public class Texture : Script
    {
        Texture2D texture;
        Vector2 scale;
        public Texture(int EntityID, JObject Data)
        {
            texture = Texture2D.FromFile(Core._graphics.GraphicsDevice, (string)Data["location"]);
            scale = new Vector2((float)Data["scaleX"], (float)Data["scaleY"]);
        }
        public override void Update()
        {
            
        }
        public override void Draw()
        {
            
        }
        public override void DestroyScript()
        {
            
        }
    }
}
