using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Game_Creation_Toolkit.Game_Engine.Handlers;
using Game_Creation_Toolkit.Game_Engine.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Game_Creation_Toolkit.Game_Engine.Menus.MessageBoxes.ScriptMenu.MenuItems
{
    public class MovementItem : ScriptItem
    {
        SpriteFont TextFont = Core._content.Load<SpriteFont>("Toolkit/Fonts/defaultfont");
        TextField velocityIncrementField;
        TextField velocityDropOffField;
        TextField maxVelocityField;
        TextField lowestVelocityField;
        Button SaveBtn;
        Texture2D BlankTexture = new Texture2D(Core._graphics.GraphicsDevice, 1, 1);
        dynamic jsonData;
        public MovementItem(JObject JSONData)
        {
            SetHeight(340);
            jsonData = JSONData;
            //used for the save button
            BlankTexture.SetData(new[] { AccentColour });
            //creates text input boxes that allow for the user to change the data about the game objects
            //the layer depth needs to be changed so they appear infront of the menu
            velocityIncrementField = new TextField(180, 35, new Vector2(BackgroundBounds.Right - 190, BackgroundBounds.Y + 50), 
                jsonData["velocityIncrement"].ToString(),
                TextFont,
                TextColour,
                AccentColour,
                0.4f);
            velocityIncrementField.layerDepth = 0.8f;
            velocityDropOffField = new TextField(180, 35, new Vector2(BackgroundBounds.Right - 190, BackgroundBounds.Y + 100),
                jsonData["velocityDropOff"].ToString(),
                TextFont,
                TextColour,
                AccentColour,
                0.4f);
            velocityDropOffField.layerDepth = 0.8f;
            maxVelocityField = new TextField(180, 35, new Vector2(BackgroundBounds.Right - 190, BackgroundBounds.Y + 150),
                jsonData["maximumVelocity"].ToString(),
                TextFont,
                TextColour,
                AccentColour,
                0.4f);
            maxVelocityField.layerDepth = 0.8f;
            lowestVelocityField = new TextField(180, 35, new Vector2(BackgroundBounds.Right - 190, BackgroundBounds.Y + 200),
                jsonData["lowestVelocity"].ToString(),
                TextFont,
                TextColour,
                AccentColour,
                0.4f);
            lowestVelocityField.layerDepth = 0.8f;
            SaveBtn = new Button(BlankTexture, new Vector2(BackgroundBounds.X + 25, lowestVelocityField.FieldBounds.Bottom + 20), new Vector2(BackgroundBounds.Width - 40, 60));
        }
        public override void Draw()
        {
            DrawBackground(title:"Entity Movement");
            Core.DrawAccent(SaveBtn.ButtonRect, 7, Core.ButtonDepth + 0.01f);
            //draws the text next to the input boxes
            Core._spriteBatch.DrawString(spriteFont: TextFont,
                            text: "Velocity Increment: ",
                            position: new Vector2(BackgroundBounds.X + 10, BackgroundBounds.Y + 57),
                            color: TextColour,
                            rotation: 0f,
                            origin: Vector2.Zero,
                            scale: 0.35f,
                            effects: SpriteEffects.None,
                            layerDepth: Core.TextDepth + 0.5f
                            );
            Core._spriteBatch.DrawString(spriteFont: TextFont,
                            text: "Velocity Drop Off: ",
                            position: new Vector2(BackgroundBounds.X + 10, BackgroundBounds.Y + 107),
                            color: TextColour,
                            rotation: 0f,
                            origin: Vector2.Zero,
                            scale: 0.35f,
                            effects: SpriteEffects.None,
                            layerDepth: Core.TextDepth + 0.5f
                            );
            Core._spriteBatch.DrawString(spriteFont: TextFont,
                            text: "Maximum Velocity: ",
                            position: new Vector2(BackgroundBounds.X + 10, BackgroundBounds.Y + 157),
                            color: TextColour,
                            rotation: 0f,
                            origin: Vector2.Zero,
                            scale: 0.35f,
                            effects: SpriteEffects.None,
                            layerDepth: Core.TextDepth + 0.5f
                            );
            Core._spriteBatch.DrawString(spriteFont: TextFont,
                            text: "Lowest Velocity: ",
                            position: new Vector2(BackgroundBounds.X + 10, BackgroundBounds.Y + 207),
                            color: TextColour,
                            rotation: 0f,
                            origin: Vector2.Zero,
                            scale: 0.35f,
                            effects: SpriteEffects.None,
                            layerDepth: Core.TextDepth + 0.5f
                            );
            Core._spriteBatch.DrawString(spriteFont: TextFont,
                            text: "Save Changes",
                            position: new Vector2(BackgroundBounds.X + 140, SaveBtn.ButtonRect.Top + 15),
                            color: TextColour,
                            rotation: 0f,
                            origin: Vector2.Zero,
                            scale: 0.35f,
                            effects: SpriteEffects.None,
                            layerDepth: Core.TextDepth + 0.5f
                            );
        }
        public override void Update()
        {
            base.Update();
            if(SaveBtn.isClicked)
            {
                SaveBtn.isClicked = false;
                JSONHandler.ObjectMovementJSON newData = new JSONHandler.ObjectMovementJSON
                {
                    id = "EntityMovement",
                    velocityIncrement = FilterToFloat(velocityIncrementField.Text),
                    velocityDropOff = FilterToFloat(velocityDropOffField.Text),
                    maximumVelocity = FilterToFloat(maxVelocityField.Text),
                    lowestVelocity = FilterToFloat(lowestVelocityField.Text),
                };
                WriteNewData(newData);
            }
        }
        public override void UnloadItem()
        {
            UIHandler.Buttons.Remove(SaveBtn);
            UIHandler.TextFields.Remove(velocityIncrementField);
            UIHandler.TextFields.Remove(velocityDropOffField);
            UIHandler.TextFields.Remove(maxVelocityField);
            UIHandler.TextFields.Remove(lowestVelocityField);

        }
    }
}
