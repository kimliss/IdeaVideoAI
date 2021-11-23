using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xabe.FFmpeg;

namespace IdeaVideoAI
{
    public static class Utils
    {

        public static string getVideoStatus(VideoStatus videoStatus)
        {
            switch (videoStatus)
            {
                case VideoStatus.WatermarkLoad:
                    return "待读取封面";
                case VideoStatus.WatermarkDoCoverSuccess:
                    return "已读取封面";
                case VideoStatus.WatermarkDoCoverError:
                    return "读取封面失败";
                case VideoStatus.WatermarkDoMark:
                    return "已标注";
                case VideoStatus.WatermarkDoSuccess:
                    return "去水印成功";
                case VideoStatus.WatermarkDoError:
                    return "去水印失败";
                case VideoStatus.RepeatLoad:
                    return "待去重";
                case VideoStatus.RepeatDoing:
                    return "正在去重";
                case VideoStatus.RepeatDoSuccess:
                    return "去重成功";
                case VideoStatus.RepeatDoError:
                    return "去重失败";

            }
            return "";
        }

        public static int nextRandomRange(int minimum, int maximum)
        {
            Random rand = new Random();
            int value = rand.Next(minimum,maximum);

            return value;
        }

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

        /// <summary>
        /// 获取随机数 (默认取 1 位小数)
        /// </summary>
        /// <param name="minimum"></param>
        /// <param name="maximum"></param>
        /// <returns></returns>
        public static double nextRandomRangeAndExcluding(double minimum, double maximum, int len, double excluding)
        {

            if (minimum == maximum && minimum == excluding)
            {
                throw new ArgumentException("excluding");
            }

            double value = nextRandomRange(minimum, maximum, len);

            while (value == excluding)
            {
                value = nextRandomRange(minimum, maximum, len);
            }

            return value;
        }

        /// <summary>
        /// 获取随机数 (默认取 1 位小数)
        /// </summary>
        /// <param name="minimum"></param>
        /// <param name="maximum"></param>
        /// <returns></returns>
        public static double nextRandomRangeAndExcluding(double minimum, double maximum, double excluding)
        {
            return nextRandomRangeAndExcluding(minimum, maximum, 1, excluding);
        }

        public static bool ffmpeg(string arguments)
        {
            try
            {
                IConversionResult conversionResult = FFmpeg.Conversions.New()
                    .Start(arguments)
                    .GetAwaiter()
                    .GetResult();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

    }
}
