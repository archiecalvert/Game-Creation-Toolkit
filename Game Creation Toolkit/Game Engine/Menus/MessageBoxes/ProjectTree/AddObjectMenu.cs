using Game_Creation_Toolkit.Game_Engine.Handlers;
using Game_Creation_Toolkit.Game_Engine.Menus.MessageBoxes.ProjectTree;
using Game_Creation_Toolkit.Game_Engine.Tools.Dotnet;
using Game_Creation_Toolkit.Game_Engine.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Game_Creation_Toolkit.Game_Engine.Menus.MessageBoxes
{
    public class AddObjectMenu : MessageBox
    {
        Rectangle Bounds;
        TextField NameField;
        SpriteFont Font = Core._content.Load<SpriteFont>("Toolkit/Fonts/defaultfont"); //loads in the default font for the application
        Button CloseBtn;
        Texture2D BlankTexture;
        Button SceneBtn;
        Button GameObjBtn;
        List<Button> Buttons = new List<Button>();

        public AddObjectMenu(int x, int y, int width, int height)
        {
            BlankTexture = new Texture2D(Core._graphics.GraphicsDevice, 1, 1);
            BlankTexture.SetData(new[] { Color.White });
            Bounds = new Rectangle(x, y, width, height);
            NameField = new TextField(width - 200, 50, new Vector2(x + 175, y + 100), "", Font, Color.Black, Color.White, 0.5f);
            NameField.layerDepth = 0.51f;
            CloseBtn = new Button(Core._content.Load<Texture2D>("Toolkit/Assets/MessageBox/Cancel"),
                new Vector2(x + (width / 2) - (0.6f * Core._content.Load<Texture2D>("Toolkit/Assets/InitialMenu/Cancel").Width / 2), y + height - 60),
                new(0.6f));
            Buttons.Add(CloseBtn);
            SceneBtn = new Button(Core._content.Load<Texture2D>("Toolkit/Assets/MessageBox/AddObjectMenu/Scene"),
                new Vector2(NameField.FieldBounds.X, NameField.FieldBounds.Y) + new Vector2(-120, 150),
                new(0.6f));
            Buttons.Add(SceneBtn);
            GameObjBtn = new Button(Core._content.Load<Texture2D>("Toolkit/Assets/MessageBox/AddObjectMenu/GameObject"),
                new Vector2(NameField.FieldBounds.X, NameField.FieldBounds.Y) + new Vector2(60, 150),
                new(0.6f));
            Buttons.Add(GameObjBtn);
        }
        public override void Draw()
        {
            DrawBackground(Bounds); //draws the background 2 rectangles that is present in each message box.
            Core._spriteBatch.Draw(texture: BlankTexture,
                position: new(NameField.FieldBounds.X - 3, NameField.FieldBounds.Y - 3),
                null,
                color: new Color(180, 180, 180),                            //Draws the border of the message box
                rotation: 0,
                origin: Vector2.Zero,
                scale: new Vector2(NameField.FieldBounds.Width + 6, NameField.FieldBounds.Height + 6),
                SpriteEffects.None,
                layerDepth: 0.5f);
            Core._spriteBatch.DrawString(spriteFont: Font,
                text: "Name:",
                position: new Vector2(NameField.FieldBounds.X, NameField.FieldBounds.Y) + new Vector2(-120, 0),
                color: Color.Black,
                rotation: 0f,                                           //Draws the description of the message box
                origin: Vector2.Zero,
                scale: 0.5f,
                SpriteEffects.None,
                layerDepth: 0.5f);
            Core._spriteBatch.DrawString(spriteFont: Font,
                text: "Type of Object",
                position: new Vector2(NameField.FieldBounds.X, NameField.FieldBounds.Y) + new Vector2(-120, 100),
                color: Color.Black,
                rotation: 0f,                                           //Draws the description of the message box
                origin: Vector2.Zero,
                scale: 0.4f,
                SpriteEffects.None,
                layerDepth: 0.5f);
        }
        public override void Update()
        {
            //These update calls won't be automatically called in UIHandler as "inFocus = false" so they are skipped directly over
            NameField.Update(); //Update the name field
            foreach (Button button in Buttons)
            {
                button.Update();
            }
            if (CloseBtn.isClicked)
            {
                CloseBtn.isClicked = false;
                DisposeMenu();
            }
            if (SceneBtn.isClicked)
            {
                SceneBtn.isClicked = false;
                if(NameField.Text != "")
                {
                    ProjectFileManager.AddScene(NameField.Text);
                    DisposeMenu();
                }
            }
            if (GameObjBtn.isClicked)
            {
                GameObjBtn.isClicked = false;
                if(NameField.Text != "")
                {

                    DisposeMenu();
                    GameObjectMenu GameObjectMenu = new GameObjectMenu((2460 - 1100)/2,(1500-750)/2,1100,750, NameField.Text);
                    
                }
            }
        }
        void DisposeMenu()
        {
            //Removes all UI elements when cancel is pressed.
            Exit();
            foreach (Button btn in Buttons)
            {
                UIHandler.Buttons.Remove(btn);
            }
            UIHandler.TextFields.Remove(NameField);
        }
    }

}
