using Game_Creation_Toolkit.Game_Engine.Handlers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Creation_Toolkit.Game_Engine.Tools.NewProject
{
    public class ProjectCreator
    {
        public ProjectCreator(string Name, string Directory)
        {
            //https://www.youtube.com/watch?v=HeHR9q-IWF8
            string[] commands = new string[8];
            commands[0] = "cd\\";
            commands[1] = "cd " + Directory;
            commands[2] = "Mkdir " + Name;
            commands[3] = "cd " + Name;
            commands[4] = "dotnet new sln";
            commands[5] = "dotnet new install Monogame.Templates.CSharp";
            commands[6] = "dotnet new mgdesktopgl -o " + Name;
            commands[7] = "EXIT";
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            foreach(string command in commands)
            {
                process.StandardInput.WriteLine(command);
            }
            process.StandardInput.Close();
            Console.WriteLine("Script Run");
            SystemHandlers.CurrentProjectDirectory = Directory+"\\"+Name+"\\"+Name;
        }
        
    }
}
