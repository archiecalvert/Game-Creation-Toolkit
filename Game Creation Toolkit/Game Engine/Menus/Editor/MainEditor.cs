using Game_Creation_Toolkit.Classes;
using Game_Creation_Toolkit.Game_Engine.Handlers;
using Game_Creation_Toolkit.Game_Engine.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Creation_Toolkit.Game_Engine.Menus.Editor
{
    public class MainEditor : ContentWindow
    {
        Button CloseBtn;
        Button RunBtn;
        Texture2D BlankTexture = new Texture2D(Core._graphics.GraphicsDevice, 1, 1);
        Texture2D HoverTexture = Core._content.Load<Texture2D>("Toolkit/Assets/MainEditor/HoverTexture");
        Vector2 RunCoords = new Vector2(1000,0);
        public MainEditor()
        {
            CloseBtn = new Button(Core._content.Load<Texture2D>("Toolkit/Assets/MainEditor/Close/Close2"), new Vector2(2379,0), new Vector2(1f));
            RunBtn = new Button(Core._content.Load<Texture2D>("Toolkit/Assets/MainEditor/Run"), RunCoords, new(1f));
            BlankTexture.SetData(new[] { Color.White });
        }
        public override void Update()
        {
            if (CloseBtn.isHover)
            {
                CloseBtn.texture = Core._content.Load<Texture2D>("Toolkit/Assets/MainEditor/Close/Close1");
            }
            else
            {
                CloseBtn.texture = Core._content.Load<Texture2D>("Toolkit/Assets/MainEditor/Close/Close2");
            }
            if(CloseBtn.isClicked)
            {
                CloseBtn.isClicked = false;
                Environment.Exit(0);
            }
            if (RunBtn.isClicked)
            {
                RunBtn.isClicked = false;
                Console.WriteLine(SystemHandlers.CurrentProjectDirectory);         
                Process process = new Process();
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.Start();
                process.StandardInput.WriteLine("cd\\");
                process.StandardInput.WriteLine("cd "+SystemHandlers.CurrentProjectDirectory);
                process.StandardInput.WriteLine("dotnet run");
                process.StandardInput.WriteLine("EXIT");
                process.StandardInput.Close();
            }
        }
        public override void Draw()
        {
            Core._spriteBatch.Draw(BlankTexture, new Rectangle(0, 0, 2460, 50), new Color(96,96,96));
            if(RunBtn.isHover)
            {
                Core._spriteBatch.Draw(HoverTexture, new Rectangle((int)RunCoords.X, (int)RunCoords.Y, 191, 50), Color.Gray);
            }
        }
        public override void UnloadWindow()
        {
            
        }
        public override void Initialize()
        {
            Core._graphics.PreferredBackBufferHeight = 1500;
            Core._graphics.PreferredBackBufferWidth = 2460;
            Core._graphics.ApplyChanges();
            
        }
    }
}
