using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Numerics;

namespace Game_Creation_Toolkit.Game_Engine.Handlers
{
    public class JSONHandler
    {
        public record TextureJSON()
        {
            public string location { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }
        public void AddTexture(string FileDirectory, TextureJSON data)
        {
               
        }
    }
}
