using Game_Creation_Toolkit.Game_Engine.Menus.Editor;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Game_Creation_Toolkit.Game_Engine.Menus.MessageBoxes.ScriptMenu
{
    public abstract class ScriptItem
    {
        Texture2D BlankTexture = new Texture2D(Core._graphics.GraphicsDevice, 1, 1);
        public Rectangle BackgroundBounds;
        public int offset = - 1;
        public int index;
        SpriteFont TextFont = Core._content.Load<SpriteFont>("Toolkit/Fonts/defaultfont");
        
        public void SetHeight(int height)
        {
            //Positions the item correctly in the list so that the all appear in a list
            if(MainEditor.ScriptMenu.ScriptItems.Count == 0)
            {
                offset = 45;
            }
            else
            {
                //Gets the location of the bottom of the previous item in the script menu
                int ListLen = MainEditor.ScriptMenu.ScriptItems.Count;
                offset = MainEditor.ScriptMenu.ScriptItems[ListLen-1].BackgroundBounds.Bottom -
                    MainEditor.ScriptMenu.MenuBounds.Y + 10;
            }
            BackgroundBounds = new Rectangle(MainEditor.ScriptMenu.MenuBounds.X, MainEditor.ScriptMenu.MenuBounds.Y + offset, MainEditor.ScriptMenu.MenuBounds.Width, height);
            MainEditor.ScriptMenu.ScriptItems.Add(this);
            BlankTexture.SetData(new[] { Color.White }); //sets the textures data to white
        }
        public virtual void Update()
        {
            

        }
        public virtual void Draw() { }
        public void DrawBackground(string title)
        { 
            //Draws the Title Text
            Core._spriteBatch.DrawString(spriteFont: TextFont,
                            text: title,
                            position: new Vector2(BackgroundBounds.X, BackgroundBounds.Y) + new Vector2(5, 3),
                            color: Color.White,
                            rotation: 0f,
                            origin: Vector2.Zero,
                            scale: 0.35f,
                            effects: SpriteEffects.None,
                            layerDepth: Core.TextDepth + 0.5f
                            );
            //Draws the background
            Core._spriteBatch.Draw(texture: BlankTexture, 
                position: new(BackgroundBounds.X, BackgroundBounds.Y),
                null,
                new Color(83,83,83),
                rotation: 0f,
                origin: Vector2.Zero,
                scale: new Vector2(BackgroundBounds.Width, BackgroundBounds.Height),
                SpriteEffects.None,
                layerDepth: Core.ButtonDepth - 0.02f);
            //Draws the top window bar
            Core._spriteBatch.Draw(texture: BlankTexture,
                position: new(BackgroundBounds.X, BackgroundBounds.Y),
                null,
                new Color(64, 64, 64),
                rotation: 0f,
                origin: Vector2.Zero,
                scale: new Vector2(BackgroundBounds.Width, 40),
                SpriteEffects.None,
                layerDepth: Core.ButtonDepth - 0.01f);
        }
        public abstract void UnloadItem();
        internal float FilterToFloat(string text)
        {
            bool HasPoint = false;
            string NewText = "";
            //Checks to see if the current character is a -, ., or an integer
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '.' && !HasPoint || (text[i] == '-' && i == 0))
                {
                    NewText += text[i];
                }
                else
                {
                    try
                    {
                        NewText += Convert.ToInt16(text[i].ToString());

                    }
                    catch { }
                }
            }
            //Checks to see if the text was blank or only letters
            if (NewText == "")
            {
                NewText = "0";
            }
            return (float)Convert.ToDouble(NewText);
        }
        internal void WriteNewData(dynamic JSONObject)
        {
            List<string> ObjectData = new List<string>();
            string ObjectDirectory = MainEditor.ScriptMenu.CurrentItemDirectory + "\\object.dat";
            dynamic temp = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(JSONObject));
            string id = temp["id"];
            foreach (string line in File.ReadAllLines(ObjectDirectory))
            {
                dynamic obj = JsonConvert.DeserializeObject(line);
                //checks to see if the current object is the texture object so it can be overwritten
                if ((string)obj["id"] == id)
                {
                    ObjectData.Add(JsonConvert.SerializeObject(JSONObject).ToString());
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
            MainEditor.ScriptMenu.ReloadFlag = true;
        }
        internal bool FilterToBool(string text)
        {
            if(text.ToLower() == "true")
            {
                return true;
            }
            else if(text.ToLower() == "false")
            {
                return false;
            }
            else
            {
                foreach(char c in text)
                {
                    if(char.ToLower(c) == 't')
                    {
                        return true;
                    }
                    else if(char.ToLower(c) == 'f')
                    {
                        return false;
                    }
                }
            }
            return false;
        }
    }
}
