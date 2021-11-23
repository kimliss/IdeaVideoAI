using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaVideoAI
{
    public class VideoItem
    {

        public string filePath;

        public string fileName;

        public VideoStatus status;

        public int width;

        public int height;

        public double framerate;

        public int duration;

    }

    public enum VideoStatus
    {
        WatermarkLoad,WatermarkDoCoverSuccess,WatermarkDoCoverError,WatermarkDoMark,WatermarkDoSuccess,WatermarkDoError,
        RepeatLoad, RepeatDoing,RepeatDoSuccess, RepeatDoError
    }
}
