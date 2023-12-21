using Game_Creation_Toolkit.Game_Engine.Base_Classes;
using Game_Creation_Toolkit.Game_Engine.Handlers;
using Game_Creation_Toolkit.Game_Engine.Menus.Editor;
using Game_Creation_Toolkit.Game_Engine.Menus.MessageBoxes.ProjectTree;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace Game_Creation_Toolkit.Game_Engine.Scripts
{
    public class Texture : Script
    {
        Texture2D texture;
        Vector2 scale;
        int id;
        Vector2 position;
        dynamic objectData;
        string objectName;
        Coordinate coordinateData;
        string sceneName;
        public Texture(int EntityID, JObject Data, string SceneName)
        {
            sceneName = SceneName;
            foreach(string line in File.ReadLines(SystemHandler.CurrentProjectDirectory + "\\GameData\\Scenes\\" + SceneName + "\\id.csv"))
            {
                if (line.Split(",")[0] == EntityID.ToString())
                {
                    objectName = line.Split(",")[1].Split("\\")[1]; 
                }
            }
            LoadTextureData(EntityID, Data);
        }
        public override void Update()
        {
            return;
        }
        public override void Draw()
        {
            Core._spriteBatch.Draw(texture: texture,
                position: new Vector2(MainEditor.GameView.WindowBounds.X + MainEditor.GameView.WindowBounds.Width/2, MainEditor.GameView.WindowBounds.Y + MainEditor.GameView.WindowBounds.Height/2)
                + new Vector2(position.X, -position.Y),
                null,
                Color.White,
                0f,
                new Vector2(texture.Width/2, texture.Height/2),
                scale,
                SpriteEffects.None,
                0.06f
                );
        }
        public override void DestroyScript()
        {
            
        }
        public void LoadTextureData(int EntityID, JObject Data)
        {
            id = EntityID;
            texture = Texture2D.FromFile(Core._graphics.GraphicsDevice, (string)Data["location"]);
            scale = new Vector2((float)Data["scaleX"], (float)Data["scaleY"]);
            CheckPosition();
        }
        void CheckPosition()
        {
            foreach (string line in File.ReadLines(SystemHandler.CurrentProjectDirectory + "\\GameData\\Scenes\\" + sceneName + "\\" + objectName + "\\object.dat"))
            {
                dynamic lineData = JsonConvert.DeserializeObject(line);
                if ((string)lineData["id"] == "coordinate")
                {
                    coordinateData = new Coordinate(id, new Vector2((float)Convert.ToDouble(lineData["x"]), (float)(Convert.ToDouble(lineData["y"]))));
                    position = coordinateData.Coordinates;
                    return;
                }
            }
            position = Vector2.Zero;

        }
    }
}
