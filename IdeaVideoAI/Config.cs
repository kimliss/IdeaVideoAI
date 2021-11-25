using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaVideoAI
{
    public class Config
    {

        private static readonly Lazy<Config> lazy =
           new Lazy<Config>(() => new Config());

        public static Config Instance { get { return lazy.Value; } }

        private Config() { 

            tab3BackgroundVideoFiles = new List<string>();
            tab3ExecCount = 1;

        }

        public int tab3ExecCount;

        //背景视频
        public List<String> tab3BackgroundVideoFiles;

        public string getRandomByTab3BackgroundVideo()
        {
            return tab3BackgroundVideoFiles[new Random().Next(tab3BackgroundVideoFiles.Count())];
        }
    }
}
