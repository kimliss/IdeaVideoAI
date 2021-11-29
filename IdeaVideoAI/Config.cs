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

            // 加图
            tab3BgVideoFiles = new List<string>();
            tab3ExecCount = 1;

        }

        /// <summary>
        /// 加图
        /// </summary>

        public int tab3ExecCount;
        public int tab3MinStartTime;
        public int tab3MaxStartTime;
        public string tab3FontPictureFile;
        public int tab3OutTime;

        //背景视频
        public List<String> tab3BgVideoFiles;
        public string getRandomByTab3BackgroundVideo()
        {
            return tab3BgVideoFiles[new Random().Next(tab3BgVideoFiles.Count())];
        }
    }
}
