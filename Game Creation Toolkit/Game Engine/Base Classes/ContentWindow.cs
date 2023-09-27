using Game_Creation_Toolkit.Game_Engine.Handlers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Game_Creation_Toolkit.Classes
{
    public abstract class ContentWindow //this class is a base class which other classes will derrive variables and methods from
    {
        public ContentWindow()
        {
            UIHandler.Windows.Add(this); //adds the content window to a list in the UIHandler class to allow for automatic update and draw method calls
            Initialize();
        }
        public abstract void Draw(); //makes it a requirement that all derrived classes have a draw method
        public abstract void Update(); //makes it a requirement that all derrived classes have an update method
        public abstract void UnloadWindow();//makes it a requirement that all derrived classes have an unload method
        public virtual void Initialize() { return; }//allows for an initialize method to be called
    }
}
