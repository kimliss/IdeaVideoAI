using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaVideoAI
{
    public static class Utils
    {

        /// <summary>
        /// 获取随机数
        /// </summary>
        /// <param name="minimum"></param>
        /// <param name="maximum"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static double nextRandomRange(double minimum, double maximum,int len)
        {
            Random rand = new Random();
            double value =rand.NextDouble() * (maximum - minimum) + minimum;

            return Math.Round(value, len);
        }
        /// <summary>
        /// 获取随机数 (默认取 1 位小数)
        /// </summary>
        /// <param name="minimum"></param>
        /// <param name="maximum"></param>
        /// <returns></returns>
        public static double nextRandomRange(double minimum, double maximum)
        {
            return nextRandomRange(minimum, maximum, 1);
        }

        public static void execCmd(string cmd, bool isShow)
        {
            var startInfo = new ProcessStartInfo
            {
                WindowStyle = isShow ? ProcessWindowStyle.Normal : ProcessWindowStyle.Hidden,
                FileName = "cmd.exe",
                Arguments = "/C" + cmd,
                UseShellExecute = true
            };
            var process = new Process
            {
                StartInfo = startInfo
            };
            process.Start();
            process.WaitForExit();
        }

        public static void execCmd(string cmd)
        {
            execCmd(cmd, false);
        }

    }
}
