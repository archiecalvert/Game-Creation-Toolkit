using Game_Creation_Toolkit.Game_Engine.Handlers;
using Game_Creation_Toolkit.Game_Engine.Menus.Editor;
using Game_Creation_Toolkit.Game_Engine.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Creation_Toolkit.Game_Engine.Menus.MessageBoxes.ScriptMenu.MenuItems
{
    public class CoordinateItem : ScriptItem
    {
        JObject jsonData;
        SpriteFont font = Core._content.Load<SpriteFont>("Toolkit/Fonts/defaultfont");
        TextField TextFieldX;
        TextField TextFieldY;
        Texture2D BlankTexture;
        Button SaveBtn;
        public CoordinateItem(JObject CoordinateData)
        {
            base.SetHeight(200);
            TextFieldX = new TextField(225, 35, new Vector2(BackgroundBounds.X + BackgroundBounds.Width - 235, BackgroundBounds.Y + 50), CoordinateData["x"].ToString(), font, Color.White, new Color(64,64,64), 0.4f);
            TextFieldX.layerDepth = 0.8f;
            TextFieldY = new TextField(225, 35, new Vector2(BackgroundBounds.X + BackgroundBounds.Width - 235, BackgroundBounds.Y + 100), CoordinateData["y"].ToString(), font, Color.White, new Color(64, 64, 64), 0.4f);
            TextFieldY.layerDepth = 0.8f;
            jsonData = CoordinateData;
            BlankTexture = new Texture2D(Core._graphics.GraphicsDevice, 1, 1);
            BlankTexture.SetData(new[] { new Color(64, 64, 64) });
            SaveBtn = new Button(BlankTexture, new Vector2(BackgroundBounds.X + 10, TextFieldY.FieldBounds.Bottom + 10), new Vector2(BackgroundBounds.Width - 20, 50));

        }
        public override void Update()
        {
            base.Update();
            if (SaveBtn.isClicked)
            {
                TextFieldX.Text = FilterToFloat(TextFieldX.Text).ToString();
                TextFieldY.Text = FilterToFloat(TextFieldY.Text).ToString();

                SaveBtn.isClicked = false;
                string ObjectDirectory = MainEditor.ScriptMenu.CurrentItemDirectory + "\\object.dat";
                JSONHandler.CoordinateJSON NewData = new JSONHandler.CoordinateJSON { id = "coordinate", x=(float)Convert.ToDouble(TextFieldX.Text), y=(float)(Convert.ToDouble(TextFieldY.Text)), };
                List<string> ObjectData = new List<string>();
                foreach(string line in File.ReadAllLines(ObjectDirectory))
                {
                    dynamic obj = JsonConvert.DeserializeObject(line);
                    if ((string)obj["id"] == "coordinate")
                    {
                        ObjectData.Add(JsonConvert.SerializeObject(NewData).ToString());
                    }
                    else
                    {
                        ObjectData.Add(line);
                    }
                }
                using(StreamWriter sw = new StreamWriter(ObjectDirectory))
                {
                    foreach(string line in ObjectData)
                    {
                        sw.WriteLine(line);
                    }
                    sw.Close();
                }
                MainEditor.ScriptMenu.ReloadFlag = true;
            }
        }
        public override void Draw()
        {
            base.DrawBackground("Coordinates");
            Core._spriteBatch.DrawString(spriteFont: font,
            text: "X Coordinate:",
                            position: new Vector2(BackgroundBounds.X + 15, BackgroundBounds.Y + 50),
                            color: Color.White,
                            rotation: 0f,
                            origin: Vector2.Zero,
                            scale: 0.35f,
                            effects: SpriteEffects.None,
                            layerDepth: Core.TextDepth + 0.5f
                            );
            Core._spriteBatch.DrawString(spriteFont: font,
            text: "Y Coordinate:",
                            position: new Vector2(BackgroundBounds.X + 15, BackgroundBounds.Y + 100),
                            color: Color.White,
                            rotation: 0f,
                            origin: Vector2.Zero,
                            scale: 0.35f,
                            effects: SpriteEffects.None,
                            layerDepth: Core.TextDepth + 0.5f
                            );
            Core._spriteBatch.DrawString(spriteFont: font,
            text: "Save Coordinate Changes",
                            position: new Vector2(BackgroundBounds.X + 85, SaveBtn.ButtonRect.Top + 7),
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
            UIHandler.TextFields.Remove(TextFieldX);
            UIHandler.TextFields.Remove(TextFieldY);
            UIHandler.Buttons.Remove(SaveBtn);
        }
    }
}
