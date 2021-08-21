using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
namespace LaunchCmd {
    internal static class Program {
        static Config config;
        private static void Main() {

            StreamReader streamReader = new("../../../../launch.json");
          
            config = JsonConvert.DeserializeObject<Config>(streamReader.ReadToEnd());

            List<LaunchConfig> searchResults = config.data;
            var startup = config.startup;
            if (config.javaHome == null || config.javaHome.Trim().Length == 0) {
                config.javaHome = "C:\\Program Files\\Java\\jdk-12.0.1\\bin\\java.exe";
            }

            streamReader.Close();
            streamReader.Dispose();
            
            while (true) {
                string cmd = Console.ReadLine();
                if (cmd == null) continue;
                if (cmd.Equals("Q")) {
                    break;
                }
                if (cmd.Equals("P")) {
                    foreach (var item in startup) {
                        Console.WriteLine(item);
                    }
                    foreach (var item in searchResults) {
                        Console.WriteLine(item.Name + " " + item.Path);
                    }

                    continue;
                }
                if (cmd.Equals("S")) {
                    foreach (var item in startup) {
                        HandleCmd(item, searchResults);
                    }
                    continue;
                }
                if (cmd.Length <= 1) {
                    continue;
                }
                bool b = HandleCmd(cmd, searchResults);
                if (b) {
                    continue;
                } else {
                    break;
                }
            }
            Console.WriteLine("closing······");
        }
        private static bool HandleCmd(string cmd, IEnumerable<LaunchConfig> searchResults) {
            string[] pp = cmd.Split(' ');
            try {
                IEnumerable<LaunchConfig> enumerable = searchResults.Where(t => t.Name.Equals(pp[0]));
                LaunchConfig searchPath= enumerable.First();
                if (searchPath==null) {
                    return true;
                }
                ILaunch launch= LaunchFactory.Get(searchPath);
                Process process= launch.launch(searchPath, config);
                bool r= process.Start();
                Console.WriteLine(r);
                
                if (pp.Length > 1) {
                    if (pp[1].Trim().Equals("-q")) {
                        return false;
                    }
                }

            } catch (Exception e) {
                Console.WriteLine(e.ToString());
                Console.WriteLine("Main:" + e.Message);
            }
            return true;
        }
    }
}
