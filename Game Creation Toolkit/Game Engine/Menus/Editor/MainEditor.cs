using Game_Creation_Toolkit.Classes;
using Game_Creation_Toolkit.Game_Engine.Handlers;
using Game_Creation_Toolkit.Game_Engine.Tools.Dotnet;
using Game_Creation_Toolkit.Game_Engine.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        Button CompileBtn;
        Texture2D BlankTexture = new Texture2D(Core._graphics.GraphicsDevice, 1, 1); //Creates a blank texture
        Vector2 RunCoords = new Vector2(1000, 0); //Coordinates of the run button

        public MainEditor()
        {
            CloseBtn = new Button(Core._content.Load<Texture2D>("Toolkit/Assets/MainEditor/Close/Close2"), new Vector2(2379,0), new Vector2(1f));
            RunBtn = new Button(Core._content.Load<Texture2D>("Toolkit/Assets/MainEditor/Run"), RunCoords, new(1f));
            CompileBtn = new Button(Core._content.Load<Texture2D>("Toolkit/Assets/MainEditor/Compile"), new Vector2(1300,0), new(1f));
            BlankTexture.SetData(new[] { Color.White }); //sets the textures data to white
        }
        public override void Update()
        {
            if (CloseBtn.isHover) //THE RED "X" in the corner. This swaps between the two textures depending on whether the user is hovering over it
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
                Environment.Exit(0); //closes the application
            }
            if (RunBtn.isClicked)
            {
                RunBtn.isClicked = false;
                Console.WriteLine(SystemHandlers.CurrentProjectDirectory);
                ProjectRun.Begin(SystemHandlers.CurrentProjectDirectory); //runs the commands necessary to get the users project running
                
            }
            if (CompileBtn.isClicked)
            {
                CompileBtn.isClicked = false;
                ProjectCompiler.Begin(SystemHandlers.CurrentProjectDirectory);
            }
        }
        public override void Draw()
        {
            Core._spriteBatch.Draw(BlankTexture, new Rectangle(0, 0, 2460, 50), new Color(96,96,96)); //draws the background for the nav bar
        }
        public override void UnloadWindow()
        {
            
        }
        public override void Initialize()
        {
            //Changes the windows size
            Core._graphics.PreferredBackBufferHeight = 1500;
            Core._graphics.PreferredBackBufferWidth = 2460;
            Core._graphics.ApplyChanges();
            
        }
    }
}
