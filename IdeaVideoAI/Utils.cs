using Xabe.FFmpeg;

namespace IdeaVideoAI
{
    public static class Utils
    {

        public static string getVideoStatus(FileStatus videoStatus)
        {
            switch (videoStatus)
            {
                case FileStatus.WatermarkLoad:
                    return "待读取封面";
                case FileStatus.WatermarkDoCoverSuccess:
                    return "已读取封面";
                case FileStatus.WatermarkDoCoverError:
                    return "读取封面失败";
                case FileStatus.WatermarkDoMark:
                    return "已标注";
                case FileStatus.WatermarkDoSuccess:
                    return "去水印成功";
                case FileStatus.WatermarkDoError:
                    return "去水印失败";
                case FileStatus.RepeatLoad:
                    return "待去重";
                case FileStatus.RepeatDoing:
                    return "正在去重";
                case FileStatus.RepeatDoSuccess:
                    return "去重成功";
                case FileStatus.RepeatDoError:
                    return "去重失败";
                case FileStatus.PictureLoad:
                    return "待合成";
                case FileStatus.PictureDoing:
                    return "合成中";
                case FileStatus.PictureDoSuccess:
                    return "合成成功";
                case FileStatus.PictureDoError:
                    return "合成失败";
                case FileStatus.Init:
                    return "已加载";
                case FileStatus.Ready:
                    return "已准备";
                case FileStatus.Process:
                    return "处理中";
                case FileStatus.Complete:
                    return "处理完成";
                case FileStatus.Fail:
                    return "处理失败";

            }
            return "未知";
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

        public static bool IsRealImage(string path)
        {
            try
            {
                Image img = Image.FromFile(path);
                Console.WriteLine("\nIt is a real image");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("\nIt is a fate image");
                return false;
            }
        }
    }
}
