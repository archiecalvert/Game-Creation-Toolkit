using Game_Creation_Toolkit.Game_Engine.Handlers;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_Creation_Toolkit.Game_Engine.Menus.Home_Menu
{
    public class OpenProject
    {
        public static bool? directoryFound;
        public static string name;
        public static void GetProjectDirectory()
        {
            //Uses the Windows Forms commands to open up the File Explorer
            //This will be used to select specific image files
            FolderBrowserDialog explorer = new FolderBrowserDialog();
            try
            {
                explorer.InitialDirectory = "C:\\Users\\archi\\Documents\\GameMaker";
            }
            catch { }
            if(explorer.ShowDialog() == DialogResult.OK)
            {
                SystemHandler.CurrentProjectDirectory = explorer.SelectedPath + "\\" + Path.GetFileName(explorer.SelectedPath);
                SystemHandler.ProjectName = Path.GetFileName(explorer.SelectedPath);
                directoryFound = true;
            }
            else
            {
                directoryFound = false;
            }

        }
        public static void OpenGameProject()
        {
            Thread thread = new Thread(GetProjectDirectory);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            while (directoryFound == null) { }
            
        }
    }
}
