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

namespace Game_Creation_Toolkit.Game_Engine.Menus.MessageBoxes.ScriptMenu.MenuItems
{
    public class TextureItem : ScriptItem
    {
        JObject jsonData;
        Button ChangeTextureBtn;
        SpriteFont TextFont = Core._content.Load<SpriteFont>("Toolkit/Fonts/defaultfont");
        string location;
        Vector2 Dimensions;
        Vector2 Scale;
        Texture2D BlankTexture = new Texture2D(Core._graphics.GraphicsDevice, 1,1);
        Texture2D TexturePreview;
        public TextureItem(JObject TextureData)
        {
            base.SetHeight(320);
            jsonData = TextureData;
            BlankTexture.SetData(new[] { new Color(64,64,64) }); //sets the textures data to white
            TexturePreview = Texture2D.FromFile(Core._graphics.GraphicsDevice, (string)TextureData["location"]);
            ChangeTextureBtn = new Button(BlankTexture, new Vector2(BackgroundBounds.X + 3, BackgroundBounds.Y + 260), new Vector2(MainEditor.ScriptMenu.MenuBounds.Width - 6, 50));
            location = (string)TextureData["location"];
            Dimensions = new Vector2((int)TextureData["width"], (int)TextureData["height"]);
        }
        public override void Update()
        {
            base.Update();
            
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
        }
        public override void UnloadItem()
        {
            UIHandler.Buttons.Remove(ChangeTextureBtn);
        }
    }
}
