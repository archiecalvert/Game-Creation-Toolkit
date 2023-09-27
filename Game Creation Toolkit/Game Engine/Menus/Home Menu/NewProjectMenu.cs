using Game_Creation_Toolkit.Classes;
using Game_Creation_Toolkit.Game_Engine.Handlers;
using Game_Creation_Toolkit.Game_Engine.Menus.Editor;
using Game_Creation_Toolkit.Game_Engine.Tools;
using Game_Creation_Toolkit.Game_Engine.Tools.Dotnet;
using Game_Creation_Toolkit.Game_Engine.Tools.NewProject;
using Game_Creation_Toolkit.Game_Engine.UI;
using Game_Creation_Toolkit.Menus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Game_Creation_Toolkit.Game_Engine.Menus.Home_Menu
{
    public class NewProjectMenu : ContentWindow
    {
        public Button CreateBtn;
        public Button CancelBtn;
        public TextField LocationFld;
        public TextField NameFld;
        public NewProjectMenu()
        {
             CreateBtn = new Button(Core._content.Load<Texture2D>("Toolkit/Assets/InitialMenu/Create"), new Vector2(1670, 475), new(0.75f)); //button for creating a project
             CancelBtn = new Button(Core._content.Load<Texture2D>("Toolkit/Assets/InitialMenu/Cancel"), new Vector2(1450, 475), new Vector2(0.75f)); //Button for returning to previous screen
             LocationFld = new TextField(1500, 50, new Vector2(300, 200), "C:\\Users\\archi\\Documents\\GameMaker", Font, Color.White, new Color(96, 96, 96), 1f);//Allows for the directory to be entered
             NameFld = new TextField(1500, 50, new Vector2(300, 125), "TestEmpty", Font, Color.White, new Color(96, 96, 96), 1f);//Allows for the project name to be entered
        }
        private SpriteFont Font = Core._content.Load<SpriteFont>("Toolkit/Fonts/defaultfont");
        public override void Update()
        {
            if (CreateBtn.isClicked)//creates a new monogame project
            {
                CreateBtn.isClicked = false;
                ProjectCreator NewProject = new ProjectCreator(NameFld.Text, LocationFld.Text);
                UnloadWindow();
                MainEditor MainGameEditor = new MainEditor();
                
            }
            if (CancelBtn.isClicked)//goes back to the previous page
            {
                CancelBtn.isClicked = false;
                UnloadWindow();
                InitialMenu Initial = new InitialMenu();
            }
        }
        public override void Draw()
        {
            Core._spriteBatch.DrawString(Font, new string("New Project"), new Vector2(15,15), Color.White, 0f, Vector2.Zero,
                0.35f, SpriteEffects.None, 0);
            Core._spriteBatch.DrawString(Font, new string("Project Name:"), new Vector2(65, 120), Color.White, 0f, Vector2.Zero,
                0.5f, SpriteEffects.None, 0);
            Core._spriteBatch.DrawString(Font, new string("Location:"), new Vector2(137, 200), Color.White, 0f, Vector2.Zero,
                0.5f, SpriteEffects.None, 0);
        }
        public override void UnloadWindow()
        {
            UIHandler.Windows.Remove(this);
            UIHandler.Buttons.Clear();
            UIHandler.TextFields.Clear();
        }
        public override void Initialize()
        {
            Core._graphics.PreferredBackBufferHeight = 560;
            Core._graphics.PreferredBackBufferWidth = 1920;
            Core._graphics.ApplyChanges();
        }
    }
}
