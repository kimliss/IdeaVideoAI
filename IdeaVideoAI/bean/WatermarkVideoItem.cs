using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaVideoAI
{
    public class WatermarkVideoItem : VideoItem
    {
        public string coverName;

        public string coverCmd;

        public string outDirIsCover;

        public Dictionary<Point, Point> docPoints = new Dictionary<Point, Point>();

        public List<WaterMarkConfig> waterMarkDatas = new List<WaterMarkConfig>();

        public string getExecCmd()
        {
            execCmd = String.Format(" -y -i \"{0}\" -vf \"", filePath);
            waterMarkDatas.ForEach(x =>
            {
                execCmd += String.Format("delogo=x={0}:y={1}:w={2}:h={3}:show=0,", x.X,
                x.Y, x.W, x.H);
            });
            execCmd = execCmd.Remove(execCmd.Length - 1);
            execCmd += String.Format("\" \"{0}\"", Path.Join(outDir, fileName));
            return execCmd;
        }
    }
}
