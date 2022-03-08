namespace IdeaVideoAI
{
    public class FileItem
    {

        public FileStatus status;

        public string filePath;

        public string fileName;

        public int width;

        public int height;

        public double framerate;

        public int duration;

        //输出目录
        public string outDir;

        //执行命令
        public string execCmd;

        //批量执行命令
        public List<String> execCmds = new List<string>();

    }

    public enum FileStatus
    {
        WatermarkLoad,WatermarkDoCoverSuccess,WatermarkDoCoverError,WatermarkDoMark,WatermarkDoSuccess,WatermarkDoError,
        RepeatLoad, RepeatDoing,RepeatDoSuccess, RepeatDoError,
        PictureLoad,PictureDoing,PictureDoSuccess,PictureDoError,

        /// <summary>
        /// 初始化
        /// </summary>
        Init,
        /// <summary>
        /// 准备完
        /// </summary>
        Ready, 
        /// <summary>
        /// 处理中
        /// </summary>
        Process, 
        /// <summary>
        /// 完成
        /// </summary>
        Complete, 
        /// <summary>
        /// 失败
        /// </summary>
        Fail
    }
}
