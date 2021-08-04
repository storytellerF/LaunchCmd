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

            StreamReader streamReader = new StreamReader("../../../launch.json");
            config = JsonConvert.DeserializeObject<Config>(streamReader.ReadToEnd());

            List<SearchPath> searchResults = config.data;
            var startup = config.startup;
            if (config.javaHome == null || config.javaHome.Trim().Length == 0) {
                config.javaHome = "C:\\Program Files\\Java\\jdk-12.0.1\\bin\\java.exe";
            }

            streamReader.Close();
            streamReader.Dispose();
            if (startup != null || startup.Count > 0) {
                foreach (var item in startup) {
                    HandleCmd(item, searchResults);
                }
            }
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
        private static bool HandleCmd(string cmd, IEnumerable<SearchPath> searchResults) {
            string[] pp = cmd.Split(' ');

            try {
                IEnumerable<SearchPath> enumerable = searchResults.Where(t => t.Name.Equals(pp[0]));
                string filePath = enumerable.Select(t => t.Path).First();
                if (string.IsNullOrWhiteSpace(filePath)) {
                    return true;
                }
                string type = enumerable.Select(t => t.Type).First();
                if (!string.IsNullOrWhiteSpace(type)) {
                    if (type.Equals("ex")) {
                        FileInfo fileInfo = new FileInfo(filePath);
                        Process.Start("Explorer.exe", "/select," + fileInfo.FullName);
                        return true;
                    } else if (type.Equals("cmd")) {
                        Process process = new Process {
                            StartInfo =
                                {
                                    FileName = "cmd.exe",
                                    Arguments = "/c "+filePath,
                                    UseShellExecute = true,
                                    CreateNoWindow = false,
                                    WorkingDirectory=enumerable.Select(t=>t.Working).First()
                                }

                        };
                        process.Start();
                        return true;
                    }
                }

                LaunchCmd(filePath);
                if (pp.Length > 1) {
                    if (pp[1].Trim().Equals("-q")) {
                        return false;
                    }
                }

            } catch (Exception e) {
                Console.WriteLine("Main:" + e.Message);
            }
            return true;
        }
        private static void LaunchCmd(string path) {
            //if (!File.Exists(path)) {
            //    if (path.Contains("\\\\") || path.Contains("//")) {
            //        Console.WriteLine("替换");
            //        path = path.Replace("//", "/");
            //        path = path.Replace("\\\\", "\\");
            //        Console.Write(path);
            //        if (!File.Exists(path)) {
            //            Console.WriteLine("文件不存在");
            //            return;
            //        }
            //    }

            //}

            string extension = new FileInfo(path).Extension;
            if (extension.Equals("jar")) {
                try {
                    string arguments = " -jar \"" + path + "\"";
                    Console.WriteLine(arguments);
                    Process p = new Process {
                        StartInfo =
                        {
                            FileName = config.javaHome,
                            Arguments = arguments,
                            UseShellExecute = false,
                            RedirectStandardInput = true,
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,
                            CreateNoWindow = true
                        }
                    };
                    bool s = p.Start();
                    Console.WriteLine(s);
                } catch (Exception e) {
                    Console.WriteLine("Launch:" + e.Message);
                }
                return;
            } else {
                Process process = Process.Start(path);
                if (process == null) {
                    Console.WriteLine("启动失败");
                }
            }

        }
    }
}
