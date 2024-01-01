using Game_Creation_Toolkit.Game_Engine.Handlers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Creation_Toolkit.Game_Engine.Tools.Dotnet
{
    public class ProjectRun
    {
        static public void Begin(string Directory)
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.CreateNoWindow = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            process.StandardInput.WriteLine("cd\\"); //Goes to the root directory
            process.StandardInput.WriteLine("cd \"" + Directory + "\""); //changes the directory to the projects
            process.StandardInput.WriteLine("dotnet run"); //runs their monogame project
            process.StandardInput.WriteLine("EXIT"); //exits the command line
            process.WaitForExit();
            process.Close();
        }
    }
}
