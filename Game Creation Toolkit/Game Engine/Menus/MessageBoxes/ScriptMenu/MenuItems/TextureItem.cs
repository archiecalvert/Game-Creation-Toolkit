using Game_Creation_Toolkit.Game_Engine.Menus.Editor;
using Game_Creation_Toolkit.Game_Engine.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Game_Creation_Toolkit.Game_Engine.Handlers.JSONHandler;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Game_Creation_Toolkit.Game_Engine.Handlers;
using System.IO;

namespace Game_Creation_Toolkit.Game_Engine.Menus.MessageBoxes.ScriptMenu.MenuItems
{
    public class TextureItem : ScriptItem
    {
        JObject jsonData;
        Button ChangeTextureBtn;
        Button SaveBtn;
        SpriteFont TextFont = Core._content.Load<SpriteFont>("Toolkit/Fonts/defaultfont");
        string location;
        Vector2 Dimensions;
        Vector2 Scale;
        Texture2D BlankTexture = new Texture2D(Core._graphics.GraphicsDevice, 1,1);
        Texture2D TexturePreview;
        TextField ScaleX;
        TextField ScaleY;
        string TextureLocation;
        public TextureItem(JObject TextureData)
        {
            base.SetHeight(475);
            jsonData = TextureData;
            //Text field that will allow for me to set the width scale of the image
            ScaleX = new TextField(225, 35, new Vector2(BackgroundBounds.X + BackgroundBounds.Width - 235, BackgroundBounds.Y + 260),
                TextureData["scaleX"].ToString(),
                TextFont,
                Color.White,
                new Color(64, 64, 64), 0.4f);
            ScaleX.layerDepth = 0.8f;
            //Text field that will allow me to set the height scale of the image
            ScaleY = new TextField(225, 35, new Vector2(BackgroundBounds.X + BackgroundBounds.Width - 235, BackgroundBounds.Y + 310),
                TextureData["scaleY"].ToString(),
                TextFont, Color.White,
                new Color(64, 64, 64),
                0.4f);
            ScaleY.layerDepth = 0.8f;
            BlankTexture.SetData(new[] { new Color(64,64,64) }); //sets the textures data to white
            TexturePreview = Texture2D.FromFile(Core._graphics.GraphicsDevice, (string)TextureData["location"]);
            TextureLocation = (string)TextureData["location"];
            ChangeTextureBtn = new Button(BlankTexture, new Vector2(BackgroundBounds.X + 10, ScaleY.FieldBounds.Bottom + 10), new Vector2(MainEditor.ScriptMenu.MenuBounds.Width - 20, 50));
            SaveBtn = new Button(BlankTexture, new Vector2(BackgroundBounds.X + 10, ChangeTextureBtn.ButtonRect.Bottom + 10), new Vector2(MainEditor.ScriptMenu.MenuBounds.Width - 20, 50));
            location = (string)TextureData["location"];
            Dimensions = new Vector2((int)TextureData["width"], (int)TextureData["height"]);

        }
        public override void Update()
        {
            base.Update();
            if(SaveBtn.isClicked)
            {
                SaveBtn.isClicked = false;
                //Removes all invalid characters and makes the value into a float
                ScaleX.Text = FilterToFloat(ScaleX.Text).ToString();
                ScaleY.Text = FilterToFloat(ScaleY.Text).ToString();
                string ObjectDirectory = MainEditor.ScriptMenu.CurrentItemDirectory + "\\object.dat";
                //Creates a new json object that stores all of the texures data
                TextureJSON NewData = new TextureJSON { id = "Texture",
                    height = TexturePreview.Height,
                    width = TexturePreview.Width,
                    location = TextureLocation,
                    scaleX = (float)(Convert.ToDouble(ScaleX.Text)),
                    scaleY = (float)(Convert.ToDouble(ScaleY.Text)) };
                List<string> ObjectData = new List<string>();
                foreach (string line in File.ReadAllLines(ObjectDirectory))
                {
                    dynamic obj = JsonConvert.DeserializeObject(line);
                    //checks to see if the current object is the texture object so it can be overwritten
                    if ((string)obj["id"] == "Texture")
                    {
                        ObjectData.Add(JsonConvert.SerializeObject(NewData).ToString());
                    }
                    else
                    {
                        ObjectData.Add(line);
                    }
                }
                //Rewrites the json objects to the data file
                using (StreamWriter sw = new StreamWriter(ObjectDirectory))
                {
                    foreach (string line in ObjectData)
                    {
                        sw.WriteLine(line);
                    }
                    sw.Close();
                }
                //reloads the script menu and the game view
                MainEditor.ScriptMenu.ReloadFlag = true;
            }
            
        }
        public override void Draw()
        {
            DrawBackground(title: "Texture");
            Core._spriteBatch.Draw(texture: TexturePreview,
                destinationRectangle: new Rectangle(BackgroundBounds.X, BackgroundBounds.Y + 50, MainEditor.ScriptMenu.MenuBounds.Width, 200),
                null,
                Color.White,
                0f,
                Vector2.Zero,
                SpriteEffects.None,
                Core.ButtonDepth
                ) ;
            Core._spriteBatch.DrawString(spriteFont: TextFont,
            text: "Change Texture",
                            position: new Vector2(ChangeTextureBtn.ButtonRect.X + ChangeTextureBtn.ButtonRect.Width/2 - 70, ChangeTextureBtn.ButtonRect.Y + 7),
                            color: Color.White,
                            rotation: 0f,
                            origin: Vector2.Zero,
                            scale: 0.35f,
                            effects: SpriteEffects.None,
                            layerDepth: Core.TextDepth + 0.5f
                            );
            Core._spriteBatch.DrawString(spriteFont: TextFont,
            text: "Save Changes",
                            position: new Vector2(SaveBtn.ButtonRect.X + SaveBtn.ButtonRect.Width / 2 - 60, SaveBtn.ButtonRect.Y + 7),
                            color: Color.White,
                            rotation: 0f,
                            origin: Vector2.Zero,
                            scale: 0.35f,
                            effects: SpriteEffects.None,
                            layerDepth: Core.TextDepth + 0.5f
                            );
            Core._spriteBatch.DrawString(spriteFont: TextFont,
            text: "Width Scale:",
                            position: new Vector2(BackgroundBounds.X + 10, BackgroundBounds.Y + 260),
                            color: Color.White,
                            rotation: 0f,
                            origin: Vector2.Zero,
                            scale: 0.35f,
                            effects: SpriteEffects.None,
                            layerDepth: Core.TextDepth + 0.5f
                            );
            Core._spriteBatch.DrawString(spriteFont: TextFont,
            text: "Height Scale:",
                            position: new Vector2(BackgroundBounds.X + 10, BackgroundBounds.Y + 310),
                            color: Color.White,
                            rotation: 0f,
                            origin: Vector2.Zero,
                            scale: 0.35f,
                            effects: SpriteEffects.None,
                            layerDepth: Core.TextDepth + 0.5f
                            );
        }
        public override void UnloadItem()
        {
            UIHandler.Buttons.Remove(ChangeTextureBtn);
            UIHandler.Buttons.Remove(SaveBtn);
            UIHandler.TextFields.Remove(ScaleX);
            UIHandler.TextFields.Remove(ScaleY);
        }
    }
}
