using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Game_Creation_Toolkit.Classes
{
    public abstract class ContentWindow
    {
        public ContentWindow() { }
        public abstract void Draw();
        public abstract void Update();
    }
}
