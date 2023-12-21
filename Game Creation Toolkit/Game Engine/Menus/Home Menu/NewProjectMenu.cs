using Game_Creation_Toolkit.Classes;
using Game_Creation_Toolkit.Game_Engine.Handlers;
using Game_Creation_Toolkit.Game_Engine.Menus.Editor;
using Game_Creation_Toolkit.Game_Engine.Menus.MessageBoxes;
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
using System.Threading;
using System.Windows;

namespace Game_Creation_Toolkit.Game_Engine.Menus.Home_Menu
{
    public class NewProjectMenu : ContentWindow
    {
        Button CreateBtn; //Button for creating the new project
        Button CancelBtn; //Button for going back to the previous menu
        TextField LocationFld; //Text field to allow for the directory to be entered by the user
        TextField NameFld; //Text field to allow for the user to choose the projects name
        public NewProjectMenu()
        {
             CreateBtn = new Button(Core._content.Load<Texture2D>("Toolkit/Assets/InitialMenu/Create"), new Vector2(1670, 305), new(0.75f)); //button for creating a project (475)
             CancelBtn = new Button(Core._content.Load<Texture2D>("Toolkit/Assets/InitialMenu/Cancel"), new Vector2(1450, 305), new Vector2(0.75f)); //Button for returning to previous screen
             LocationFld = new TextField(1500, 50, new Vector2(300, 200), "C:\\Users\\archi\\Documents\\GameMaker", Font, Color.White, new Color(96, 96, 96), 0.5f);//Allows for the directory to be entered
             NameFld = new TextField(1500, 50, new Vector2(300, 125), "NewProject1", Font, Color.White, new Color(96, 96, 96), 0.5f);//Allows for the project name to be entered
        }
        private SpriteFont Font = Core._content.Load<SpriteFont>("Toolkit/Fonts/defaultfont");
        public override void Update()
        {
            if (CreateBtn.isClicked)//creates a new monogame project
            {
                CreateBtn.isClicked = false;
                if (!ValidateName(NameFld.Text))
                {
                    ErrorMessage ErrorBox = new ErrorMessage(710, 100, 500, 220);
                    ErrorBox.Title = "Error";
                    ErrorBox.Text = "The name field is invalid.";
                    return;
                }
                else if (!ValidateLocation(LocationFld.Text))
                {
                    ErrorMessage ErrorBox = new ErrorMessage(710, 100, 500, 220);
                    ErrorBox.Title = "Error";
                    ErrorBox.Text = "The location field is invalid.";
                    return;
                }
                ProjectCreator NewProject = new ProjectCreator(NameFld.Text, LocationFld.Text); //runs all the commands for creating a new project
                UnloadWindow(); //Unloads the current menu
                MainEditor MainGameEditor = new MainEditor(); //Opens the project editor
            }
            if (CancelBtn.isClicked)//goes back to the previous page
            {
                CancelBtn.isClicked = false;
                UnloadWindow();
                InitialMenu Initial = new InitialMenu(); //Goes back to the previous menu
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
            UIHandler.Windows.Remove(this); //clears the current windows on screen
            UIHandler.Buttons.Clear(); //Removes all the buttons from the screen
            UIHandler.TextFields.Clear(); //Removes all the text fields from the screen
        }
        public override void Initialize()
        {
            Core._graphics.PreferredBackBufferHeight = 410; //Height of the window
            Core._graphics.PreferredBackBufferWidth = 1920; //Width of the window
            Core._graphics.ApplyChanges();
        }
        bool ValidateName(string name)
        {
            if (name == null || name == "") return false; //checks to see whether the parameter is empty
            List<string> forbiddenNames = new List<string> {
                "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9", "COM0",
                "LPT1", "LPT2", "LPT3", "LPT4", "LPT5", "LPT6", "LPT7", "LPT8", "LPT9", "LPT0",
                "CON", "PRN", "AUX", "NUL", "Game"
            }; //list of all the invalid file names
            List<char> forbiddenChar = new List<char> { '<', '>', ':', '"', '/', '\\', '|', '?', '*'}; //list of all the invalid characters
            foreach(string test in forbiddenNames) //checks to see if the name of the file matches any of the invalid names
            {
                if (name == test) return false;
            }
            foreach(char test in forbiddenChar) //checks to see if the name contains any invalid characters
            {
                if(name.Contains(test)) return false;
            }
            return true; //if the name doesn't contain any invalid data, then the program can continue
        }
        bool ValidateLocation(string location)
        {
            if (System.IO.Directory.Exists(location)) //Checks to see if the location exists
            {
                return true;
            }
            else
            {
                Console.WriteLine("Error! The location: \""+LocationFld.Text + "\" doesn't exist.");
                return false;
            }
        }
    }
}