using System;
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
            public float scaleX { get; set; }
            public float scaleY { get; set; }
        }
        public record CoordinateJSON()
        {
            public string id { get; set;}
            public float x { get; set;}
            public float y { get; set;}
        }
        public record SceneDataJSON()
        {
            public string MainScene { get; set; }
        }
        public record ObjectMovementJSON()
        {
            public string id { get; set; }
            //How much the velocity increases by when a movement key is pressed
            public float velocityIncrement { get; set; }
            //how much the velocity will drop off when no movement key is being pressed
            public float velocityDropOff { get; set; }
            public float maximumVelocity { get; set; }
            //if the velocity falls under this value, then it will be automatically set to zero
            public float lowestVelocity { get; set; }
        }
        public record CameraJSON()
        {
            public string id { get; set; }
            //determines whether the camera is attached to a game object
            public bool isAttached { get; set; }
            //the name of the attached game object
            public string ParentGameObject { get; set; }
            public string ParentScene { get; set; }
            //Camera Coordinates
            public float CoordinateX { get; set; }
            public float CoordinateY { get; set; }
        }
        public record MapItem()
        {
            public string id { get; set; }
            public string spriteSheetLocation { get; set; }
            public string mapDataLocation { get; set; }
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
        public static void AddEntityMovement(string Target)
        {
            ObjectMovementJSON movementJSON = new ObjectMovementJSON
            {
                id = "EntityMovement",
                velocityIncrement = 0,
                velocityDropOff = 0,
                maximumVelocity = 0,
                lowestVelocity = 0,
            };
            WriteData(Target, movementJSON);
        }
        public static void AddCoordinatesToFile(string Target)
        {
            CoordinateJSON json = new CoordinateJSON
            {
                id = "coordinate",
                x = 0,
                y= 0,
            };
            WriteData(Target, json);
        }
        public static void AddCameraToFile(string Target, string SceneName)
        {
            CameraJSON json = new CameraJSON
            {
                id = "Camera",
                isAttached = false,
                ParentGameObject = "",
                ParentScene = SceneName,
                CoordinateX = 0,
                CoordinateY = 0,
            };
            WriteData(Target, json);
        }
        public static void AddMapToFile(string Target)
        {
            MapItem json = new MapItem
            {
                id = "Map",
                spriteSheetLocation = "",
                mapDataLocation = "",
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
