using Microsoft.VisualStudio.TestTools.UnitTesting;
using IdeaVideoAI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaVideoAI.Tests
{
    [TestClass()]
    public class UtilsTests
    {
        [TestMethod()]
        public void ffmpegTest()
        {

            bool result = Utils.ffmpeg("-y  -i \"D:\\Test\\0.mp4\"  -filter_complex \"[0:v]setpts=PTS-STARTPTS[video];[0:a]atempo=1.0[audio];[audio2]atempo=0.93[audio];[video]setpts=1.14*PTS[video]\"  -map [audio] -map [video] \"D:\\Test\\.tempRepeat\\0.mp4\"");

            Assert.IsTrue(result);
        }
    }
}