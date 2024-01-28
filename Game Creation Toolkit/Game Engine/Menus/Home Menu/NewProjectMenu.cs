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
using System.IO;
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
        Texture2D BlankTexture = new Texture2D(Core._graphics.GraphicsDevice, 1, 1); //Creates a blank texture

        public NewProjectMenu()
        {
            Color BackgroundField = new Color(192, 192, 192);
            BlankTexture.SetData(new[] { Color.White });
            CreateBtn = new Button(Core._content.Load<Texture2D>("Toolkit/Assets/InitialMenu/Create"), new Vector2(1670, 305), new(0.75f)); //button for creating a project (475)
             CancelBtn = new Button(Core._content.Load<Texture2D>("Toolkit/Assets/InitialMenu/Cancel"), new Vector2(1450, 305), new Vector2(0.75f)); //Button for returning to previous screen
             LocationFld = new TextField(1500, 50, new Vector2(300, 200), "C:\\Users\\archi\\Documents\\GameMaker", Font, Color.Black, BackgroundField, 0.5f);//Allows for the directory to be entered
             NameFld = new TextField(1500, 50, new Vector2(300, 125), "NewProject1", Font, Color.Black, BackgroundField, 0.5f);//Allows for the project name to be entered
        }
        private SpriteFont Font = Core._content.Load<SpriteFont>("Toolkit/Fonts/defaultfont");
        public override void Update()
        {
            if (CreateBtn.isClicked)//creates a new monogame project
            {
                CreateBtn.isClicked = false;
                //Validates the name entered by the user
                if (!ValidateName(NameFld.Text))
                {
                    ErrorMessage ErrorBox = new ErrorMessage(710, 100, 500, 220);
                    ErrorBox.Title = "Error";
                    ErrorBox.Text = "The name field is invalid.";
                    return;
                }
                //Validates the location entered by the user
                else if (!ValidateLocation(LocationFld.Text))
                {
                    ErrorMessage ErrorBox = new ErrorMessage(710, 100, 500, 220);
                    ErrorBox.Title = "Error";
                    ErrorBox.Text = "The location field is invalid.";
                    return;
                }
                //Checks to see whether the location doesnt already exists
                foreach(string file in Directory.GetDirectories(LocationFld.Text))
                {
                    string temp = LocationFld.Text + "\\" + NameFld.Text;
                    if (file == LocationFld.Text + "\\" + NameFld.Text)
                    {
                        ErrorMessage ErrorBox = new ErrorMessage(710, 100, 500, 220);
                        ErrorBox.Title = "Error";
                        ErrorBox.Text = "This Project already exists. \nPlease choose another name.";
                        return;
                    }
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
            int barWidth = 7;
            Core._spriteBatch.DrawString(Font, new string("Create a New Project"), new(20, 17), Color.White, 0f, Vector2.Zero, 0.45f, SpriteEffects.None, 1f);//title

            Core._spriteBatch.DrawString(Font, new string("Project Name:"), new Vector2(65, 130), Color.Black, 0f, Vector2.Zero,
                0.5f, SpriteEffects.None, 0);
            Core._spriteBatch.DrawString(Font, new string("Location:"), new Vector2(137, 210), Color.Black, 0f, Vector2.Zero,
                0.5f, SpriteEffects.None, 0);
            Core._spriteBatch.Draw(BlankTexture, new Rectangle(barWidth, barWidth, 1920 - barWidth * 2, 45), Core.NavColour);//new Color(128,128,128));
            Core.DrawAccent(new Rectangle(0, 0, 1920, 410), 7, 0.9f);

        }
        public override void UnloadWindow()
        {
            UIHandler.Windows.Remove(this); //clears the current windows on screen
            UIHandler.Buttons.Clear(); //Removes all the buttons from the screen
            UIHandler.TextFields.Clear(); //Removes all the text fields from the screen
        }
        public override void Initialize()
        {
            Core.ChangeWindowSize(1920, 410);
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