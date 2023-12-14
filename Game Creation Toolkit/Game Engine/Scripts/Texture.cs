using Game_Creation_Toolkit.Game_Engine.Handlers;
using Game_Creation_Toolkit.Game_Engine.Menus.Editor;
using Game_Creation_Toolkit.Game_Engine.Menus.MessageBoxes.ProjectTree;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace Game_Creation_Toolkit.Game_Engine.Scripts
{
    public class Texture : Script
    {
        Texture2D texture;
        Vector2 scale;
        int id;
        Vector2 position;
        dynamic objectData;
        Coordinate coordinateData;
        public Texture(int EntityID, JObject Data)
        {
            Reload(EntityID, Data);
            position = new Vector2(0, 0);
            
        }
        public override void Update()
        {
            
        }
        public override void Draw()
        {
            Core._spriteBatch.Draw(texture: texture,
                position: new Vector2(MainEditor.GameView.WindowBounds.X + MainEditor.GameView.WindowBounds.Width/2, MainEditor.GameView.WindowBounds.Y + MainEditor.GameView.WindowBounds.Height/2)
                + position,
                null,
                Color.White,
                0f,
                new Vector2(texture.Width/2, texture.Height/2),
                scale,
                SpriteEffects.None,
                0.06f
                );
        }
        public override void DestroyScript()
        {
            
        }
        public void Reload(int EntityID, JObject Data)
        {
            id = EntityID;
            texture = Texture2D.FromFile(Core._graphics.GraphicsDevice, (string)Data["location"]);
            scale = new Vector2((float)Data["scaleX"], (float)Data["scaleY"]);
        }
    }
}
