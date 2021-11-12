using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaVideoAI
{
    public class RepeatConfig
    {
        //生抽视频数
        public int outVideoCount = 1;

        //是否随机速率
        public bool isSetpts = false;

        public double setptsV1;

        public double setptsV2;


        //是否随机对比度
        public bool isContrast = false;

        public double contrastV1;

        public double contrastV2;


        //是否随机饱和度
        public bool isSaturation = false;

        public double saturationV1;

        public double saturationV2;


        //是否随机亮度
        public bool isBrightness = false;

        public double brightnessV1;

        public double brightnessV2;


        //是否随机拉伸
        public bool isZoom = false;

        public double zoomV1;

        public double zoomV2;


        //是否随机旋转
        public bool isRotate = false;

        public double rotateV1;

        public double rotateV2;

        public double rotateZoomV;


        //是否抖动
        public bool isShakes = false;

        public double shakesV1;

        public double shakesV2;

        public double shakesLength;


        //是否添加背景音乐
        public bool isBackAudio = true;

        //背景音乐
        public List<String> backAudioFiles = new List<string>();


        //是否叠加视频
        public bool isOverlay = true;

        //叠加视频
        public List<String> overlayVideoFiles = new List<string>();
    }
}
