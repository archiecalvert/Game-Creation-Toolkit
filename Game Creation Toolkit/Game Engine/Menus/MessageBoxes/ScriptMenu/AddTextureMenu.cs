using Assimp;
using Game_Creation_Toolkit.Game_Engine.Base_Classes;
using Game_Creation_Toolkit.Game_Engine.Handlers;
using Game_Creation_Toolkit.Game_Engine.Menus.Editor;
using Game_Creation_Toolkit.Game_Engine.Scripts;
using Game_Creation_Toolkit.Game_Engine.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Button = Game_Creation_Toolkit.Game_Engine.UI.Button;
using MessageBox = Game_Creation_Toolkit.Game_Engine.UI.MessageBox;
using Scene = Game_Creation_Toolkit.Game_Engine.Scripts.Scene;

namespace Game_Creation_Toolkit.Game_Engine.Menus.MessageBoxes.ScriptMenu
{
    public class AddTextureMenu : MessageBox
    {
        Rectangle Bounds;
        Button CancelBtn;
        Button SelectTextureBtn;
        Button CreateBtn;
        Texture2D PreviewTexture;
        List<Button> Buttons = new List<Button>();
        string directory;
        public AddTextureMenu()
        {
            Bounds = new Rectangle((2460 - 1100) / 2, (1500 - 750) / 2, 1100, 750);
            CancelBtn = new Button(Core._content.Load<Texture2D>("Toolkit/Assets/MessageBox/Cancel"),
                new Vector2(Bounds.X + (Bounds.Width * 2/ 5) - (0.6f * Core._content.Load<Texture2D>("Toolkit/Assets/InitialMenu/Cancel").Width / 2), Bounds.Y + Bounds.Height - 60),
                new(0.6f));
            SelectTextureBtn = new Button(Core._content.Load<Texture2D>("Toolkit/Assets/MessageBox/AddScriptMenu/SelectTexture"),
                new Vector2(Bounds.X + 55, Bounds.Y + 130),
                new(0.6f));
            CreateBtn = new Button(Core._content.Load<Texture2D>("Toolkit/Assets/MessageBox/AddScriptMenu/Create"),
                new Vector2(Bounds.X + (Bounds.Width * 3/ 5) - (0.6f * Core._content.Load<Texture2D>("Toolkit/Assets/InitialMenu/Cancel").Width / 2), Bounds.Y + Bounds.Height - 60),
                new(0.6f));
            Buttons.Add(CreateBtn);
            Buttons.Add(SelectTextureBtn);
            Buttons.Add(CancelBtn);
        }
        public override void Update()
        {
            foreach (var button in Buttons)
            {
                //manually updates the buttons
                button.Update();
            }
            if (CancelBtn.isClicked)
            {
                CancelBtn.isClicked = false;
                DisposeMenu();
            }
            if (SelectTextureBtn.isClicked)
            {
                SelectTextureBtn.isClicked = false;
                //Starts the file explorer in a new thread so that the appartment state can be different to the main thread
                Thread thread = new Thread(OpenExplorer);
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
            }
            if (CreateBtn.isClicked)
            {
                CreateBtn.isClicked = false;
                JSONHandler.AddTextureToFile(MainEditor.ScriptMenu.CurrentItemDirectory, directory);
                bool HasCoordinateScript = false;
                foreach (string line in File.ReadLines(MainEditor.ScriptMenu.CurrentItemDirectory + "\\object.dat"))
                {
                    dynamic obj = JsonConvert.DeserializeObject(line);
                    if ((string)obj["id"] == "coordinate")
                    {
                        HasCoordinateScript = true;
                    }
                }
                if (!HasCoordinateScript)
                {
                    JSONHandler.AddCoordinatesToFile(MainEditor.ScriptMenu.CurrentItemDirectory, directory);
                }
                DisposeMenu();
                string SceneName = MainEditor.ScriptMenu.CurrentItemDirectory.Substring(SystemHandler.CurrentProjectDirectory.Length + new string("\\GameData\\Scenes\\").Length).Split("\\")[0];
                MainEditor.ScriptMenu.ReloadFlag = true;
                //reloads all of the scripts in the game objects so that the texture script will be added correctly to the object
                foreach(Scene scene in ObjectHandler.SceneData)
                {
                    if(scene.id == SceneName)
                    {
                        foreach(GameObject GameObject in scene.GameObjects)
                        {
                            GameObject.ReloadScripts();
                        }
                    }
                }
            }
        }
        public override void Draw()
        {
            DrawBackground(Bounds);
            if (PreviewTexture != null)
            {
                //if the texture isnt blank, it will draw a preview to the screen with the lines below
                Rectangle PreviewBounds;
                PreviewBounds = new Rectangle(Bounds.X + 55, Bounds.Y + 200, Bounds.Width - 110, 300);
                Core._spriteBatch.Draw(PreviewTexture, PreviewBounds, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None,layerDepth: 1f);
            }
        }
        void DisposeMenu() //deletes the message box window
        {
            Exit();
            foreach (var btn in Buttons)
            {
                UIHandler.Buttons.Remove(btn);
            }
        }
        void OpenExplorer()
        {
            //Uses the Windows Forms commands to open up the File Explorer
            //This will be used to select specific image files
            OpenFileDialog explorer = new OpenFileDialog();
            explorer.Title = "Select texture file";
            explorer.Filter = "png files (*.png)|*.png"; //selects the only accepted files
            explorer.CheckFileExists = true;
            explorer.CheckPathExists = true;
            if (explorer.ShowDialog() == DialogResult.OK)
            {
                //if an image has been selected...
                directory = explorer.FileName;
                PreviewTexture = Texture2D.FromFile(Core._graphics.GraphicsDevice, directory);
            }
        }
    }
}
    
