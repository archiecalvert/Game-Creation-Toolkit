using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Creation_Toolkit.Game_Engine.Tools.Dotnet
{
    public class ProjectBuilder
    {
        public ProjectBuilder(string Directory)
        {
            string[] commands = new string[5];
            commands[0] = "cd\\";
            commands[1] = "cd " + Directory;
            commands[2] = "dotnet build";
            commands[3] = "dotnet publish --sc -p:PublishReadyToRun=true -p:PublishSingleFile=true";
            commands[4] = "EXIT";
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            foreach (string command in commands)
            {
                process.StandardInput.WriteLine(command);
            }
            process.StandardInput.Close();
            Console.WriteLine("Script Run! Published file should've been created.");
            
            
        }
        
    }
}
