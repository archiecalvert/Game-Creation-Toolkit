using Game_Creation_Toolkit.Game_Engine.Handlers;
using Game_Creation_Toolkit.Game_Engine.Menus.Editor;
using Game_Creation_Toolkit.Game_Engine.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Creation_Toolkit.Game_Engine.Menus.MessageBoxes.ScriptMenu.MenuItems
{
    public class MapItem : ScriptItem
    {
        Texture2D BlankTexture;
        SpriteFont font = Core._content.Load<SpriteFont>("Toolkit/Fonts/defaultfont");
        Button SaveBtn;
        TextField SpriteSheetField;
        TextField DataField;
        TextField ScaleField;
        public MapItem(JObject MapData) : base(MapData, 290, true)
        {
            BlankTexture = new Texture2D(Core._graphics.GraphicsDevice, 1, 1);
            BlankTexture.SetData(new[] { AccentColour });
            SpriteSheetField = new TextField(225, 35, new Vector2(BackgroundBounds.X + BackgroundBounds.Width - 235, BackgroundBounds.Y + 50), MapData["spriteSheetLocation"].ToString(), font, TextColour, AccentColour, 0.4f);
            SpriteSheetField.layerDepth = 0.8f;
            DataField = new TextField(225, 35, new Vector2(BackgroundBounds.X + BackgroundBounds.Width - 235, BackgroundBounds.Y + 100), MapData["mapDataLocation"].ToString(), font, TextColour, AccentColour, 0.4f);
            DataField.layerDepth = 0.8f;
            ScaleField = new TextField(225, 35, new Vector2(BackgroundBounds.Right - 235, BackgroundBounds.Y + 150), MapData["tileScale"].ToString(), font, TextColour, AccentColour, 0.4f);
            ScaleField.layerDepth = 0.8f;
            SaveBtn = new Button(BlankTexture, new Vector2(BackgroundBounds.X + 25, ScaleField.FieldBounds.Bottom + 15), new Vector2(BackgroundBounds.Width - 40, 60));
        }
        public override void Update()
        {
            base.Update();
            if(SaveBtn.isClicked)
            {
                SaveBtn.isClicked = false;
                //converts the users inputs into floats
                JSONHandler.MapItem mapItem = new JSONHandler.MapItem
                {
                    id = "Map",
                    spriteSheetLocation = SpriteSheetField.Text,
                    mapDataLocation = DataField.Text,
                    tileScale = FilterToFloat(ScaleField.Text),
                };

                WriteNewData(mapItem);
                MainEditor.ScriptMenu.ReloadFlag = true;
            }
        }
        public override void Draw()
        {
            DrawBackground(title: "Map");
            Core.DrawAccent(SaveBtn.ButtonRect, 7, Core.ButtonDepth + 0.01f);
            Core._spriteBatch.DrawString(spriteFont: font,
                            text: "Sprite Sheet:",
                            position: new Vector2(BackgroundBounds.X + 15, BackgroundBounds.Y + 57),
                            color: TextColour,
                            rotation: 0f,
                            origin: Vector2.Zero,
                            scale: 0.35f,
                            effects: SpriteEffects.None,
                            layerDepth: Core.TextDepth + 0.5f
                            );
            Core._spriteBatch.DrawString(spriteFont: font,
                            text: "Map Data:",
                            position: new Vector2(BackgroundBounds.X + 15, BackgroundBounds.Y + 107),
                            color: TextColour,
                            rotation: 0f,
                            origin: Vector2.Zero,
                            scale: 0.35f,
                            effects: SpriteEffects.None,
                            layerDepth: Core.TextDepth + 0.5f
                            );
            Core._spriteBatch.DrawString(spriteFont: font,
                            text: "Tile Scale:",
                            position: new Vector2(BackgroundBounds.X + 15, BackgroundBounds.Y + 157),
                            color: TextColour,
                            rotation: 0f,
                            origin: Vector2.Zero,
                            scale: 0.35f,
                            effects: SpriteEffects.None,
                            layerDepth: Core.TextDepth + 0.5f
                            );
            Core._spriteBatch.DrawString(spriteFont: font,
                            text: "Save Changes",
                            position: new Vector2(SaveBtn.ButtonRect.X + SaveBtn.ButtonRect.Width / 2 - 60, SaveBtn.ButtonRect.Y + 15),
                            color: TextColour,
                            rotation: 0f,
                            origin: Vector2.Zero,
                            scale: 0.35f,
                            effects: SpriteEffects.None,
                            layerDepth: Core.TextDepth + 0.5f
                            );
        }
        public override void UnloadItem()
        {
            base.UnloadItem();
            UIHandler.Buttons.Remove(SaveBtn);
            UIHandler.TextFields.Remove(DataField);
            UIHandler.TextFields.Remove(SpriteSheetField);
            UIHandler.TextFields.Remove(ScaleField);
        }
    }
}
