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
using System.Security.AccessControl;

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
        public bool isCamera = false;
        public GameObjectMenu(int x, int y, int width, int height, string Name)
        {
            Bounds = new Rectangle(x, y, width, height); //Window bounds
            objName = Name; //Name of the game object inputted by the player
            int charCount = new string(SystemHandler.CurrentProjectDirectory + "\\GameData\\Scenes\\").Length; //Gets the length of the directory before the game object name
            foreach (string s in Directory.GetDirectories(SystemHandler.CurrentProjectDirectory + "\\GameData\\Scenes"))
            {
                scenes.Add(s.Substring(charCount)); //Gets the names of the scenes from the project directory
            }
            int index = 0;
            //Creates a button for each scene in the project directory and stores it with the scene name in a dictionary
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
            DrawBackground(Bounds); //Draws the message box background
            Core._spriteBatch.DrawString(spriteFont: Font, 
                text: "Select a Scene",
                position: new Vector2(Bounds.X, Bounds.Y) + new Vector2(20, 20),
                color: Color.Black,
                rotation: 0f,                                           //Draws the "Select a Scene" text
                origin: Vector2.Zero,
                scale: 0.4f,
                SpriteEffects.None,
                layerDepth: 0.5f);
            int index = 0;
            foreach(string scene in scenes)
            {
                
                Core._spriteBatch.DrawString(spriteFont: Font,
                    text: scene,
                    position: new Vector2(Bounds.X + 525, Bounds.Y + 42 + 75 * (index + 1)),
                    color: Color.Black,
                    rotation: 0f,                                           //Draws the Scene name on top of each button
                    origin: new Vector2((32*(scene.Length))/2,50),
                    scale: 0.4f,
                    SpriteEffects.None,
                    layerDepth: 0.6f);
                index++;
            }
            foreach(Button btn in ScenesButtons.Values)
            {
                Core.DrawAccent(btn.ButtonRect, 4, Core.ButtonDepth + 0.01f);
            }
        }
        public override void Update()
        {
            if (!SwapDelay.isActive)
            {
                foreach (var btn in ScenesButtons)
                {
                    btn.Value.Update(); //Updates each button
                }   
            }
            CancelBtn.Update();
            if(CancelBtn.isClicked)
            {
                CancelBtn.isClicked = false;
                DisposeMenu(); //Closes the menu
            }
            foreach(var btn in ScenesButtons)
            {
                if(btn.Value.isClicked && !SwapDelay.isActive)
                {
                    string SceneName = btn.Key;
                    ProjectFileManager.AddGameObject(objName, SceneName);       //Determines what button was pressed and adds a game object to it
                    DisposeMenu();
                    if(isCamera)
                    {
                        JSONHandler.AddCameraToFile(SystemHandler.CurrentProjectDirectory + "\\GameData\\Scenes\\" + SceneName + "\\" + objName, SceneName);
                    }
                }
            }
        }
        void DisposeMenu() //deletes the messagebox when called
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
