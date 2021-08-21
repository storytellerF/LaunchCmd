using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaunchCmd {
    class LaunchConfig {
        public string Name { get; set; }
        public string Path { get; set; }
        /// <summary>
        /// 如果是ex 代表使用explore 打开文件夹
        /// 如果是
        /// </summary>
        public string Type { get; set; }
        public string Working { get; set; }
    }
}
