using Game_Creation_Toolkit.Game_Engine.Handlers;
using Game_Creation_Toolkit.Game_Engine.Menus.Editor;
using Game_Creation_Toolkit.Game_Engine.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PVRTexLibNET;
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
        //how much the item will be shifted down. This allows for the menu items to be in a list
        public int offset = - 1;
        public int index;
        public Color AccentColour = new Color(192,192,192);
        public Color TextColour = Color.Black;
        SpriteFont TextFont = Core._content.Load<SpriteFont>("Toolkit/Fonts/defaultfont");
        string data;
        Button DeleteBtn = new Button(Core._content.Load<Texture2D>("Toolkit/Assets/MainEditor/ScriptMenu/delete"), new Vector2(0,0), Vector2.One);

        public ScriptItem(JObject jsonObject, int height, bool isDeletable)
        {
            data = (string)JsonConvert.SerializeObject(jsonObject);
            SetHeight(height);
            if (isDeletable)
            {
                DeleteBtn.ButtonRect.X = BackgroundBounds.Right - 32;
                DeleteBtn.ButtonRect.Y = BackgroundBounds.Top + 10;
            }
            else
            {
                UIHandler.Buttons.Remove(DeleteBtn);
            }
            
        }

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
            BackgroundBounds = new Rectangle(MainEditor.ScriptMenu.MenuBounds.X, MainEditor.ScriptMenu.MenuBounds.Y + offset, MainEditor.ScriptMenu.MenuBounds.Width - 14, height);
            MainEditor.ScriptMenu.ScriptItems.Add(this);
            BlankTexture.SetData(new[] { Color.White }); //sets the textures data to white
        }
        public virtual void Update()
        {
            if (DeleteBtn.isClicked)
            {
                DeleteBtn.isClicked = false;
                RemoveScript();
            }
        }
        public virtual void Draw()
        {
            Core.DrawAccent(DeleteBtn.ButtonRect, 10, 0.9f);

        }
        public void DrawBackground(string title)
        { 
            //Draws the Title Text
            Core._spriteBatch.DrawString(spriteFont: TextFont,
                            text: title,
                            position: new Vector2(BackgroundBounds.X + 7, BackgroundBounds.Y) + new Vector2(5, 13),
                            color: Core.TitleColour,
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
                new Color(192,192,192),
                rotation: 0f,
                origin: Vector2.Zero,
                scale: new Vector2(BackgroundBounds.Width, BackgroundBounds.Height),
                SpriteEffects.None,
                layerDepth: Core.ButtonDepth - 0.02f);
            //Draws the top window bar
            Core._spriteBatch.Draw(texture: BlankTexture,
                position: new(BackgroundBounds.X, BackgroundBounds.Y),
                null,
                Core.NavColour,
                rotation: 0f,
                origin: Vector2.Zero,
                scale: new Vector2(BackgroundBounds.Width, 40),
                SpriteEffects.None,
                layerDepth: Core.ButtonDepth - 0.01f);
            Core.DrawAccent(new Rectangle(BackgroundBounds.X, BackgroundBounds.Y, BackgroundBounds.Width + 14, BackgroundBounds.Height), 7, 0.8f);
            
        }
        public virtual void UnloadItem()
        {
            UIHandler.Buttons.Remove(DeleteBtn);

        }
        internal float FilterToFloat(string text)
        {
            bool HasPoint = false;
            string NewText = "";
            //Checks to see if the current character is a "-", ".", or an integer
            for (int i = 0; i < text.Length; i++)
            {
                //Checks to see if the current character is a decimal point, and also one in the number doesnt already exist
                if (text[i] == '.' && !HasPoint)
                {
                    NewText += text[i];
                    HasPoint = true;
                }
                //checks to see whether the first character is a minus sign
                else if(text[i] == '-' && i == 0)
                {
                    NewText += text[i];
                }
                else
                {
                    //tries to convert the character into an integer
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
            //checks to see if the input is already in a valid state
            if(text.ToLower() == "true" || text == "1")
            {
                return true;
            }
            else if(text.ToLower() == "false" || text == "0")
            {
                return false;
            }
            //if the input isn't already valid
            else
            {
                //tries to find the first 't' or 'f' character inside of the string
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
            //if nothing could be found, the input will automatically be set to false
            return false;
        }
        internal void RemoveScript()
        {
            string newData = "";
            foreach(string line in File.ReadLines(MainEditor.ScriptMenu.CurrentItemDirectory + "\\object.dat"))
            {
                if(line != data)
                {
                    newData+= line + "\n";
                }
            }
            using(StreamWriter sw = new StreamWriter(MainEditor.ScriptMenu.CurrentItemDirectory + "\\object.dat"))
            {
                sw.Write(newData);
                sw.Close();
            }
            UnloadItem();
            MainEditor.ScriptMenu.ReloadFlag = true;
            
           
        }
    }
}
