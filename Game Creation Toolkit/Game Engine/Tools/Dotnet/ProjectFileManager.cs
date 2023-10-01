using Game_Creation_Toolkit.Game_Engine.Handlers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Creation_Toolkit.Game_Engine.Tools.Dotnet
{
    public class ProjectFileManager
    {
        //this class is used to insert a new itemgroup so that the file passed in as an arguement will be included in the compilation and build process
        public static void AddFileToProject(string csproj, string FileDirectory)
        {
            string newData;
            using (StreamReader sr = new StreamReader(csproj)) //this will read all of the text in the .csproj file
            {
                string csFile = sr.ReadToEnd();
                int location = FindSpace(csFile); //finds the location where the new itemgroup can be inserted into the project file
                if (location != -1)
                {
                    string textBefore = "";
                    for (int i = 0; i < location; i++) //stores all the data before the part where it will be inserted
                    {
                        textBefore += csFile[i];
                    }
                    string textAfter = "";
                    for (int i = location; i < csFile.Length; i++) //stores all the data after where the part will be inserted
                    {
                        textAfter += csFile[i];
                    }
                    string include = "<ItemGroup>\r\n    <None Update=\"" + FileDirectory + "\">\r\n      <CopyToOutputDirectory>Always</CopyToOutputDirectory>\r\n    </None>\r\n  </ItemGroup>\r\n  ";
                    newData = textBefore + include + textAfter; //inserts the new item group
                    sr.Close();
                    using (StreamWriter sw = new StreamWriter(csproj)) //writes this new data to the project file
                    {
                        sw.WriteLine(newData);
                        sw.Close();            
                    }
                }
                sr.Close();
            }
        }
        static int FindSpace(string file) //locates the most appropriate space to include the new itemgroup
        {
            bool keepReading = true;
            int index = 0;
            while (keepReading || index>file.Length)
            {
                if (file.Substring(index, 16) == "</PropertyGroup>")
                {
                    if(file.Substring(index + 17, 30).Contains("<ItemGroup>"))
                    {
                        for(int i = 0; i < 30; i++)
                        {
                            if (file[i+index+17] == '<')
                            {
                                return i + index + 17;
                            }
                        }
                    }
                }
                index++;
            }
            return -1;
        }
    }
    
}
