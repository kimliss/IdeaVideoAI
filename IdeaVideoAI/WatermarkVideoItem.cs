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

        public string tempCoverDir;

        public string tempClearWaterMarkDir;

        public Dictionary<Point, Point> docPoints = new Dictionary<Point, Point>();

        public List<WaterMarkConfig> waterMarkDatas = new List<WaterMarkConfig>();
    }
}
