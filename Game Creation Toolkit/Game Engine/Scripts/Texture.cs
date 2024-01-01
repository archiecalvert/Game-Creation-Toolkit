using Game_Creation_Toolkit.Game_Engine.Handlers;
using Game_Creation_Toolkit.Game_Engine.Menus.Editor;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

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
        string sceneName;
        Texture2D BlankTexture = new Texture2D(Core._graphics.GraphicsDevice, 1, 1);
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
            BlankTexture.SetData(new[] { Color.White }); //sets the textures data to white
            LoadTextureData(EntityID, Data);
        }
        public override void Update()
        {
            return;
        }
        public override void Draw()
        {
            try
            {
                //Location of the texture on screen
                Vector2 location = new Vector2(MainEditor.GameView.WindowBounds.X + MainEditor.GameView.WindowBounds.Width / 2, MainEditor.GameView.WindowBounds.Y + MainEditor.GameView.WindowBounds.Height / 2)
                    + new Vector2(position.X, -position.Y);
                //Creates a rectangle around the texture
                Rectangle TextureRectangle = new Rectangle(Convert.ToInt16(location.X - (texture.Width * scale.X) / 2), Convert.ToInt16(location.Y - (texture.Height * scale.Y) / 2), Convert.ToInt16(texture.Width * scale.X), Convert.ToInt16(texture.Height * scale.Y));
                //distance between the left of the game view and the texture
                float distLeft = TextureRectangle.Left - MainEditor.GameView.WindowBounds.Left;
                //distance between the right of the game view and the texture
                float distRight = MainEditor.GameView.WindowBounds.Right - TextureRectangle.Right;
                //distance between the top of the game view and the texture
                float distTop = TextureRectangle.Top - MainEditor.GameView.WindowBounds.Top;
                //distance between the bottom of the game view and the texture
                float distBottom = MainEditor.GameView.WindowBounds.Bottom - TextureRectangle.Bottom;
                Rectangle sourceRect = new Rectangle(0, 0, texture.Width, texture.Height);
                //true when the texture is outside of the left side of the game view
                if (distLeft < 0)
                {
                    //extracts the section of the texture thats in view
                    sourceRect.Width -= Convert.ToInt16(-distLeft / scale.X);
                    sourceRect.X = Convert.ToInt16(-distLeft / scale.X);
                    location.X -= distLeft;
                }
                //true when the texture is outside of the right side of the game view
                if (distRight < 0)
                {
                    //extracts the section of the texture thats in view
                    sourceRect.Width -= (int)Math.Truncate(-distRight / scale.X);
                }
                //true when the texture is outside of the top side of the game view
                if (distTop < 0)
                {
                    //extracts the section of the texture thats in view
                    sourceRect.Height -= (int)Math.Truncate(-distTop / scale.Y);
                    sourceRect.Y = (int)Math.Truncate(-distTop / scale.Y);
                    location.Y -= distTop;
                }
                //true when the texture is outside of the bottom side of the game view
                if (distBottom < 0)
                {
                    //extracts the section of the texture thats in view
                    sourceRect.Height -= (int)Math.Truncate(-distBottom / scale.Y);
                }
                Core._spriteBatch.Draw(texture: texture,
                    position: location,
                    sourceRectangle: sourceRect,
                    Color.White,
                    0f,
                    new Vector2(texture.Width / 2, texture.Height / 2),
                    scale,
                    SpriteEffects.None,
                    0.06f
                    );
            }
            catch { }
        }
        
        public override void DestroyScript()
        {
            return;
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
            //Checks every script inside of the objects data file
            foreach (string line in File.ReadLines(SystemHandler.CurrentProjectDirectory + "\\GameData\\Scenes\\" + sceneName + "\\" + objectName + "\\object.dat"))
            {
                //Converts the line into a JSON object
                dynamic lineData = JsonConvert.DeserializeObject(line);
                //Updates the textures position with the coordinates stored inside of the coordinate JSON object
                if ((string)lineData["id"] == "coordinate")
                {
                    position = new Vector2((float)Convert.ToDouble(lineData["x"]), (float)(Convert.ToDouble(lineData["y"])));//coordinateData.Coordinates;
                    return;
                }
            }
            //If no coordinate JSON object can be found, the textures position is automatically set to (0,0)
            position = Vector2.Zero;

        }
    }
}
