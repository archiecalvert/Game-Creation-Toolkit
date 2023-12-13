﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Numerics;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using System.Data.Common;
using static Game_Creation_Toolkit.Game_Engine.Handlers.JSONHandler;

namespace Game_Creation_Toolkit.Game_Engine.Handlers
{
    public class JSONHandler
    {
        public record TextureJSON() //holds the structure of the Texture JSON object
        {
            public string id { get; set; }
            public string location { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public int scaleX { get; set; }
            public int scaleY { get; set; }
        }
        public record CoordinateJSON()
        {
            public string id { get; set;}
            public float x { get; set;}
            public float y { get; set;}
        }
        
        public static void AddTextureToFile(string Target, string TextureDirectory)
        {
            //Converts the image file passed into the method to a texture2d
            Texture2D temp = Texture2D.FromFile(Core._graphics.GraphicsDevice, TextureDirectory);
            TextureJSON textureJSON = new TextureJSON
            {
                id = "Texture",
                location = TextureDirectory,
                height = temp.Height,
                width = temp.Width,
                scaleX = 1,
                scaleY = 1,
            };
            //reads the previous data in the file and writes the new json object to the file
            WriteData(Target, textureJSON);
        }
        public static void AddCoordinatesToFile(string Target, string TextureDirectory)
        {
            CoordinateJSON json = new CoordinateJSON
            {
                id = "coordinate",
                x = 0,
                y= 0,
            };
            WriteData(Target, json);
        }
        static void WriteData(string Target, dynamic JSON)
        {
            using (StreamReader sr = new StreamReader(Target + "\\object.dat"))
            {
                string data = sr.ReadToEnd() + JsonSerializer.Serialize(JSON);
                sr.Close();
                using (StreamWriter sw = new StreamWriter(Target + "\\object.dat"))
                {
                    sw.WriteLine(data);
                    sw.Close();
                }
            }
        }
    }
}
