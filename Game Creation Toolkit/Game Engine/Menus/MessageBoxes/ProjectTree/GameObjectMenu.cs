using Game_Creation_Toolkit.Game_Engine.Handlers;
using Game_Creation_Toolkit.Game_Engine.Tools;
using Game_Creation_Toolkit.Game_Engine.Tools.Dotnet;
using Game_Creation_Toolkit.Game_Engine.UI;
using Game_Creation_Toolkit.Game_Engine.Menus.Editor;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

using System.Threading.Tasks;

namespace Game_Creation_Toolkit.Game_Engine.Menus.MessageBoxes.ProjectTree
{
    public class GameObjectMenu : MessageBox
    {
        Rectangle Bounds;
        string objName;
        SpriteFont Font = Core._content.Load<SpriteFont>("Toolkit/Fonts/defaultfont"); //loads in the default font for the application
        List<string> scenes = new List<string>();
        Dictionary<string, Button> ScenesButtons = new Dictionary<string, Button>();
        Button CancelBtn;
        Timer SwapDelay;
        public GameObjectMenu(int x, int y, int width, int height, string Name)
        {
            Bounds = new Rectangle(x, y, width, height);
            objName = Name;
            int charCount = new string(SystemHandlers.CurrentProjectDirectory + "\\GameData\\Scenes\\").Length;
            foreach (string s in Directory.GetDirectories(SystemHandlers.CurrentProjectDirectory + "\\GameData\\Scenes"))
            {
                scenes.Add(s.Substring(charCount));
            }
            int index = 0;
            foreach(string scene in scenes)
            {
                ScenesButtons.Add(scene, new Button(Core._content.Load<Texture2D>("Toolkit/Assets/MessageBox/AddObjectMenu/Blank"),
                    new Vector2(Bounds.X + 50, Bounds.Y + 75*(index+1)),
                    new(1f)));
                index++;
            }
            CancelBtn = new Button(Core._content.Load<Texture2D>("Toolkit/Assets/MessageBox/Cancel"),
                new Vector2(x + (width / 2) - (0.6f * Core._content.Load<Texture2D>("Toolkit/Assets/InitialMenu/Cancel").Width / 2), y + height - 60),
                new(0.6f));
            SwapDelay = new Timer(0.6f);
            SwapDelay.Begin();
        }
        public override void Draw()
        {
            DrawBackground(Bounds);
            Core._spriteBatch.DrawString(spriteFont: Font,
                text: "Select a Scene",
                position: new Vector2(Bounds.X, Bounds.Y) + new Vector2(20, 20),
                color: Color.Black,
                rotation: 0f,                                           //Draws the description of the message box
                origin: Vector2.Zero,
                scale: 0.4f,
                SpriteEffects.None,
                layerDepth: 0.5f);
            int index = 0;
            foreach(string scene in scenes)
            {
                Core._spriteBatch.DrawString(spriteFont: Font,
                    text: scene,
                    position: new Vector2(Bounds.X + 525, Bounds.Y + 35 + 75 * (index + 1)),
                    color: Color.Black,
                    rotation: 0f,                                           //Draws the description of the message box
                    origin: new Vector2((32*(scene.Length))/2,50),
                    scale: 0.4f,
                    SpriteEffects.None,
                    layerDepth: 0.6f);
                index++;
            }
        }
        public override void Update()
        {
            if (!SwapDelay.isActive)
            {
                foreach (var btn in ScenesButtons)
                {
                    btn.Value.Update();
                }   
            }
            CancelBtn.Update();
            if(CancelBtn.isClicked)
            {
                CancelBtn.isClicked = false;
                DisposeMenu();
            }
            foreach(var btn in ScenesButtons)
            {
                if(btn.Value.isClicked && !SwapDelay.isActive)
                {
                    string SceneName = btn.Key;
                    ProjectFileManager.AddGameObject(objName, SceneName);
                    DisposeMenu();
                    MainEditor.ProjectTree.UpdateList();
                }
            }
        }
        void DisposeMenu()
        {
            Exit();
            UIHandler.Buttons.Remove(CancelBtn);
            foreach(var btn in ScenesButtons)
            {
                UIHandler.Buttons.Remove(btn.Value);
            }
            ScenesButtons.Clear();
        }
    }
}
