using System.Diagnostics;
using System.IO;

namespace LaunchCmd.launch {
    class RegularLaunch : ILaunch {
        public Process launch(LaunchConfig searchPath, Config config) {
            string path = searchPath.Path;
            string extension = new FileInfo(path).Extension;
            if (extension.Equals("jar")) {
                return new JavaLaunch().launch(searchPath, config);
            } else {
                Process p = new Process {
                    StartInfo =
                                {
                    FileName = path

                }
                };
                return p;
            }

        }
    }
}
