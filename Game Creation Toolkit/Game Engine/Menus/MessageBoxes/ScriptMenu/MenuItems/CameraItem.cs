using Game_Creation_Toolkit.Game_Engine.Handlers;
using Game_Creation_Toolkit.Game_Engine.Menus.Editor;
using Game_Creation_Toolkit.Game_Engine.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json.Linq;
namespace Game_Creation_Toolkit.Game_Engine.Menus.MessageBoxes.ScriptMenu.MenuItems
{
    public class CameraItem : ScriptItem
    {
        SpriteFont font = Core._content.Load<SpriteFont>("Toolkit/Fonts/defaultfont");
        Button SaveBtn;
        TextField isAttachedField;
        TextField GameObjectNameField;
        TextField CoordinateXField;
        TextField CoordinateYField;
        public CameraItem(JObject CameraData)
        {
            base.SetHeight(330);
            isAttachedField = new TextField(225, 35, new Vector2(BackgroundBounds.X + BackgroundBounds.Width - 235, BackgroundBounds.Y + 50), CameraData["isAttached"].ToString(), font, TextColour, AccentColour, 0.4f);
            isAttachedField.layerDepth = 0.8f;
            GameObjectNameField = new TextField(225, 35, new Vector2(BackgroundBounds.X + BackgroundBounds.Width - 235, BackgroundBounds.Y + 100), CameraData["ParentGameObject"].ToString(), font, TextColour, AccentColour, 0.4f);
            GameObjectNameField.layerDepth = 0.8f;
            CoordinateXField = new TextField(225, 35, new Vector2(BackgroundBounds.Right - 235, BackgroundBounds.Y + 150), CameraData["CoordinateX"].ToString(), font, TextColour, AccentColour, 0.4f);
            CoordinateXField.layerDepth = 0.8F;
            CoordinateYField = new TextField(225, 35, new Vector2(BackgroundBounds.Right - 235, BackgroundBounds.Y + 200),
                CameraData["CoordinateY"].ToString(),
                font,
                TextColour,
                AccentColour,
                0.4f);
            CoordinateYField.layerDepth = 0.8f;
            Texture2D BlankTexture = new Texture2D(Core._graphics.GraphicsDevice, 1, 1);
            BlankTexture.SetData(new[] { AccentColour });
            SaveBtn = new Button(BlankTexture, new Vector2(BackgroundBounds.X + 25, BackgroundBounds.Bottom - 80), new Vector2(BackgroundBounds.Width - 40, 60));

        }
        public override void Update()
        {
            base.Update();
            if (SaveBtn.isClicked)
            {
                SaveBtn.isClicked= false;
                //creates a new camera record
                JSONHandler.CameraJSON newData = new JSONHandler.CameraJSON
                {
                    id = "Camera",
                    isAttached = FilterToBool(isAttachedField.Text),
                    ParentGameObject = GameObjectNameField.Text,
                    ParentScene = MainEditor.CurrentScene,
                    CoordinateX = FilterToFloat(CoordinateXField.Text),
                    CoordinateY = FilterToFloat(CoordinateYField.Text),
                };
                //writes the camera data to the object data
                WriteNewData(newData);
                MainEditor.ScriptMenu.ReloadFlag = true;
            }
        }
        public override void Draw()
        {
            DrawBackground("Camera");
            Core._spriteBatch.DrawString(spriteFont: font,
                            text: "is Attached?:",
                            position: new Vector2(BackgroundBounds.X + 15, BackgroundBounds.Y + 57),
                            color: TextColour,
                            rotation: 0f,
                            origin: Vector2.Zero,
                            scale: 0.35f,
                            effects: SpriteEffects.None,
                            layerDepth: Core.TextDepth + 0.5f
                            );
            Core._spriteBatch.DrawString(spriteFont: font,
                            text: "Game Object:",
                            position: new Vector2(BackgroundBounds.X + 15, BackgroundBounds.Y + 107),
                            color: TextColour,
                            rotation: 0f,
                            origin: Vector2.Zero,
                            scale: 0.35f,
                            effects: SpriteEffects.None,
                            layerDepth: Core.TextDepth + 0.5f
                            );
            Core._spriteBatch.DrawString(spriteFont: font,
                            text: "Coordinate X:",
                            position: new Vector2(BackgroundBounds.X + 15, BackgroundBounds.Y + 157),
                            color: TextColour,
                            rotation: 0f,
                            origin: Vector2.Zero,
                            scale: 0.35f,
                            effects: SpriteEffects.None,
                            layerDepth: Core.TextDepth + 0.5f
                            );
            Core._spriteBatch.DrawString(spriteFont: font,
                            text: "Coordinate Y:",
                            position: new Vector2(BackgroundBounds.X + 15, BackgroundBounds.Y + 207),
                            color: TextColour,
                            rotation: 0f,
                            origin: Vector2.Zero,
                            scale: 0.35f,
                            effects: SpriteEffects.None,
                            layerDepth: Core.TextDepth + 0.5f
                            );
            Core._spriteBatch.DrawString(spriteFont: font,
                            text: "Save Changes",
                            position: new Vector2(BackgroundBounds.X + 140, SaveBtn.ButtonRect.Top + 15),
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
            UIHandler.TextFields.Remove(isAttachedField);
            UIHandler.TextFields.Remove(GameObjectNameField);
            UIHandler.TextFields.Remove(CoordinateYField);
            UIHandler.TextFields.Remove(CoordinateXField);
            UIHandler.Buttons.Remove(SaveBtn);

        }
    }
}
