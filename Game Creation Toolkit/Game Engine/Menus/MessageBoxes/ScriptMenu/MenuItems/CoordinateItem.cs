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
        public CoordinateItem(JObject CoordinateData) : base(CoordinateData, 230, false)
        {
            //base.SetHeight(230);
            TextFieldX = new TextField(225, 35, new Vector2(BackgroundBounds.X + BackgroundBounds.Width - 235, BackgroundBounds.Y + 50), CoordinateData["x"].ToString(), font, TextColour, AccentColour, 0.4f);
            TextFieldX.layerDepth = 0.8f;
            TextFieldY = new TextField(225, 35, new Vector2(BackgroundBounds.X + BackgroundBounds.Width - 235, BackgroundBounds.Y + 100), CoordinateData["y"].ToString(), font, TextColour, AccentColour, 0.4f);
            TextFieldY.layerDepth = 0.8f;
            jsonData = CoordinateData;
            BlankTexture = new Texture2D(Core._graphics.GraphicsDevice, 1, 1);
            BlankTexture.SetData(new[] { AccentColour });
            SaveBtn = new Button(BlankTexture, new Vector2(BackgroundBounds.X + 25, TextFieldY.FieldBounds.Bottom + 10), new Vector2(BackgroundBounds.Width - 40, 60));

        }
        public override void Update()
        {
            base.Update();
            if (SaveBtn.isClicked)
            {
                SaveBtn.isClicked = false;
                //converts the users inputs into floats
                TextFieldX.Text = FilterToFloat(TextFieldX.Text).ToString();
                TextFieldY.Text = FilterToFloat(TextFieldY.Text).ToString();    
                JSONHandler.CoordinateJSON NewData = new JSONHandler.CoordinateJSON 
                {
                    id = "coordinate",
                    x=(float)Convert.ToDouble(TextFieldX.Text),
                    y=(float)(Convert.ToDouble(TextFieldY.Text)),
                };
                WriteNewData(NewData);
                MainEditor.ScriptMenu.ReloadFlag = true;
            }
        }
        public override void Draw()
        {
            base.DrawBackground(title:"Coordinates");
            //draws the text next to the text input boxes
            Core._spriteBatch.DrawString(spriteFont: font,
                            text: "X Coordinate:",
                            position: new Vector2(BackgroundBounds.X + 15, BackgroundBounds.Y + 57),
                            color: TextColour,
                            rotation: 0f,
                            origin: Vector2.Zero,
                            scale: 0.35f,
                            effects: SpriteEffects.None,
                            layerDepth: Core.TextDepth + 0.5f
                            );
            Core._spriteBatch.DrawString(spriteFont: font,
                            text: "Y Coordinate:",
                            position: new Vector2(BackgroundBounds.X + 15, BackgroundBounds.Y + 107),
                            color: TextColour,
                            rotation: 0f,
                            origin: Vector2.Zero,
                            scale: 0.35f,
                            effects: SpriteEffects.None,
                            layerDepth: Core.TextDepth + 0.5f
                            );
            Core._spriteBatch.DrawString(spriteFont: font,
                            text: "Save Coordinate Changes",
                            position: new Vector2(BackgroundBounds.X + 85, SaveBtn.ButtonRect.Top + 15),
                            color: TextColour,
                            rotation: 0f,
                            origin: Vector2.Zero,
                            scale: 0.35f,
                            effects: SpriteEffects.None,
                            layerDepth: Core.TextDepth + 0.5f
                            );
            Core.DrawAccent(SaveBtn.ButtonRect, 7, Core.ButtonDepth + 0.01f);

        }
        public override void UnloadItem()
        {
            base.UnloadItem();
            UIHandler.TextFields.Remove(TextFieldX);
            UIHandler.TextFields.Remove(TextFieldY);
            UIHandler.Buttons.Remove(SaveBtn);
        }
    }
}
