using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using Xabe.FFmpeg;
using Xabe.FFmpeg.Downloader;

namespace IdeaVideoAI
{

    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();

            FFmpeg.SetExecutablesPath(Path.Join(Environment.CurrentDirectory, "ffmpeg"), ffmpegExeutableName: "ffmpeg.exe", ffprobeExecutableName: "ffprobe.exe");

            initRemoveWatermark();

            initRemoveRepeat();

            initOverlayPicture();
        }

        /// <summary>
        /// 选择文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openVideoFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!File.Exists(Path.Join(Environment.CurrentDirectory, "ffmpeg/ffmpeg.exe")))
            {
                MessageBox.Show("你还未安装 FFmpeg，请点击帮助菜单中的安装 ffmpeg 选项...");
                return;
            }


            OpenFileDialog fd = new OpenFileDialog();
            fd.Multiselect = true;
            fd.Title = "Please Select File";

            switch (tabControl1.SelectedIndex)
            {
                case 2:
                    fd.Filter = "All Image Files|*.jpg;*.png;*.jpeg";
                    break;
                default:
                    fd.Filter = "Image Or Video Files|*.jpg;*.png;*.jpeg;*.mp4;*.mpg;*.mpeg;*.avi;*.rm;*.rmvb;*.mov;*.wmv;*.asf;*.dat;*.asx;*.wvx;*.mpe;*.mpa";
                    break;
            }

            if (fd.ShowDialog() == DialogResult.OK)
            {
                tbLog.Text = "";

                string videoCoverPath = Path.Join(Path.GetDirectoryName(fd.FileName), ".tempVideoCover_IdeaVideoAI");
                string delWaterMarkPath = Path.Join(Path.GetDirectoryName(fd.FileName), ".tempDelWaterMark_IdeaVideoAI");
                string repeatVideoPath = Path.Join(Path.GetDirectoryName(fd.FileName), ".tempRepeatVideo_IdeaVideoAI");
                string pictureVideoPath = Path.Join(Path.GetDirectoryName(fd.FileName), ".tempPictureVideo_IdeaVideoAI");


                switch (tabControl1.SelectedIndex)
                {
                    case 0:
                        curSelectIndexAtWatermarkDatas = 0;

                        waterMarkDatas.Clear();
                        Directory.CreateDirectory(delWaterMarkPath);
                        Directory.CreateDirectory(videoCoverPath);

                        dicPoints.Clear();
                        pointStart = Point.Empty;
                        pointContinue = Point.Empty;

                        Image dummy = pictureBox1.Image;
                        if (dummy != null)
                        {
                            pictureBox1.Image = null;
                            pictureBox1.Update();
                            dummy.Dispose();
                        }
                        break;
                    case 1:
                        repeatDatas.Clear();
                        Directory.CreateDirectory(repeatVideoPath);
                        break;
                    case 2:
                        pictureDatas.Clear();
                        Directory.CreateDirectory(pictureVideoPath);
                        break;
                }

                string[] files = fd.FileNames;

                for (int i = 0; i < files.Length; i++)
                {
                    if (tabControl1.SelectedIndex == 0)
                    {
                        WatermarkVideoItem videoData = new WatermarkVideoItem();

                        videoData.filePath = files[i];
                        videoData.fileName = Path.GetFileName(videoData.filePath);

                        videoData.outDirIsCover = videoCoverPath;
                        videoData.outDir = delWaterMarkPath;

                        videoData.coverName = i + 1 + ".jpg";
                        if (Utils.IsRealImage(videoData.filePath))
                        {
                            videoData.coverCmd = String.Format(" -y -i \"{0}\" -c copy \"{1}\"", videoData.filePath, Path.Join(videoCoverPath, videoData.coverName));
                        }
                        else
                        {
                            videoData.coverCmd = String.Format(" -y -ss 00:00:05 -i \"{0}\" -vframes 1 \"{1}\"", videoData.filePath, Path.Join(videoCoverPath, videoData.coverName));
                        }
                        
                        videoData.status = FileStatus.WatermarkLoad;

                        waterMarkDatas.Add(videoData);
                    }
                    else if(tabControl1.SelectedIndex == 1)
                    {
                        var videoData = new FileItem();

                        videoData.filePath = files[i];
                        videoData.fileName = Path.GetFileName(videoData.filePath);
                        videoData.status = FileStatus.RepeatLoad;

                        videoData.outDir = repeatVideoPath;

                        repeatDatas.Add(videoData);
                    }else if(tabControl1.SelectedIndex == 2)
                    {
                        var videoData = new FileItem();

                        videoData.filePath = files[i];
                        videoData.fileName = Path.GetFileName(videoData.filePath);
                        videoData.status = FileStatus.PictureLoad;

                        videoData.outDir = pictureVideoPath;

                        pictureDatas.Add(videoData);
                    }
                }

                loadListView();

                if (tabControl1.SelectedIndex == 0) backgroundWorkerCover.RunWorkerAsync();
            }
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            int selectIndex = e.ItemIndex;
            curSelectIndexAtWatermarkDatas = selectIndex;
            updateWaterMarkDetail(curSelectIndexAtWatermarkDatas);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadListView();
        }

        /// <summary>
        /// 加载文件列表
        /// </summary>
        private void loadListView()
        {

            var isWaterMark = tabControl1.SelectedIndex == 0;
            var tempDatas = new List<FileItem>();

            if (tabControl1.SelectedIndex == 0)
            {
                waterMarkDatas.ForEach(data => tempDatas.Add(data));
            }
            else if(tabControl1.SelectedIndex == 1)
            {
                repeatDatas.ForEach(data => tempDatas.Add(data));
            }else if(tabControl1.SelectedIndex == 2)
            {
                pictureDatas.ForEach(data => tempDatas.Add(data));
            }else
            {
                return;
            }

            listView1.BeginUpdate();
            listView1.Items.Clear();

            for (int i = 0; i < tempDatas.Count; i++)
            {

                FileItem data = tempDatas[i];

                ListViewItem listViewItem = new ListViewItem();

                listViewItem.Text = i + 1 + "-" + data.fileName;
                listViewItem.SubItems.Add(Utils.getVideoStatus(data.status));

                listView1.Items.Add(listViewItem);
            }

            listView1.EndUpdate();
        }

        /// <summary>
        /// 更新单个文件列表项
        /// </summary>
        /// <param name="index"></param>
        private void updateItemInListView(int index)
        {
            if (index < 0) return;

            FileItem videoData;

            if (tabControl1.SelectedIndex == 0)
            {
                videoData = waterMarkDatas[index];
            }
            else if(tabControl1.SelectedIndex == 1)
            {
                videoData = repeatDatas[index];
            }else if(tabControl1.SelectedIndex == 2)
            {
                videoData = pictureDatas[index];
            }
            else
            {
                return;
            }

            ListViewItem listViewItem = listView1.Items[index];
            listViewItem.SubItems.Clear();
            listViewItem.Text = index + 1 + "-" + videoData.fileName;
            listViewItem.SubItems.Add(Utils.getVideoStatus(videoData.status));

        }

        private void recodeLog(string log)
        {
            if (String.IsNullOrEmpty(tbLog.Text))
            {
                tbLog.Text = log;
            }
            else
            {
                tbLog.Text = log + "\r\n\r\n" + tbLog.Text;
            }
        }

        private async void 安装FfmpegToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("关闭弹窗后，开始安装 FFmpeg，等待安装成功提示后再进行操作.");
            await FFmpegDownloader.GetLatestVersion(FFmpegVersion.Full, Path.Join(Environment.CurrentDirectory, "ffmpeg"));
            MessageBox.Show("FFmpeg 已安装成功...");
        }

        /// <summary>
        /// 去水印
        /// </summary>

        private List<WatermarkVideoItem> waterMarkDatas = new List<WatermarkVideoItem>();
        private BackgroundWorker backgroundWorkerCover;
        private BackgroundWorker backgroundWorkerWaterMark;

        int curSelectIndexAtWatermarkDatas = -1;

        public bool bDrawStart = false;
        public Dictionary<Point, Point> dicPoints = new Dictionary<Point, Point>();
        public Point pointStart = Point.Empty;
        public Point pointContinue = Point.Empty;

        public void initRemoveWatermark()
        {
            backgroundWorkerCover = new System.ComponentModel.BackgroundWorker();
            backgroundWorkerCover.WorkerReportsProgress = true;
            backgroundWorkerCover.DoWork += new DoWorkEventHandler(backgroundWorkerCover_DoWork);
            backgroundWorkerCover.ProgressChanged += new ProgressChangedEventHandler(backgroundWorkerCover_ProgressChanged);

            backgroundWorkerWaterMark = new System.ComponentModel.BackgroundWorker();
            backgroundWorkerWaterMark.WorkerReportsProgress = true;
            backgroundWorkerWaterMark.DoWork += new DoWorkEventHandler(backgroundWorkerWaterMark_DoWork);
            backgroundWorkerWaterMark.ProgressChanged += new ProgressChangedEventHandler(backgroundWorkerWaterMark_ProgressChanged);
        }

        /// <summary>
        /// 去水印 - 获取图片大小
        /// </summary>
        /// <param name="p_PictureBox"></param>
        /// <returns></returns>
        public Rectangle GetPictureBoxZoomSize(PictureBox p_PictureBox)
        {
            if (p_PictureBox != null)
            {
                PropertyInfo _ImageRectanglePropert = p_PictureBox.GetType().GetProperty("ImageRectangle", BindingFlags.Instance | BindingFlags.NonPublic);
                return (Rectangle)_ImageRectanglePropert.GetValue(p_PictureBox, null);
            }
            return new Rectangle(0, 0, 0, 0);
        }


        /// <summary>
        /// 去水印 - 更新去水印 - 图片标注信息
        /// </summary>
        /// <param name="index"></param>
        private void updateWaterMarkDetail(int index)
        {
            bool isWaterMark = tabControl1.SelectedIndex == 0;
            if (!isWaterMark || index < 0) return;

            WatermarkVideoItem videoData = waterMarkDatas[index];

            dicPoints = videoData.docPoints;

            try
            {
                Image dummy = Image.FromFile(Path.Join(videoData.outDirIsCover, videoData.coverName));

                pictureBox1.Image = dummy;
                pictureBox1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            string clearCmd = " ";
            videoData.waterMarkDatas.ForEach(x =>
            {
                clearCmd += String.Format("delogo=x={0}:y={1}:w={2}:h={3}:show=0,", x.X,
                x.Y, x.W, x.H);
            });
            clearCmd = clearCmd.Remove(clearCmd.Length - 1);
            textBox1.Text = clearCmd;
        }

        public void backgroundWorkerCover_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < waterMarkDatas.Count(); i++)
            {
                WatermarkVideoItem videoData = waterMarkDatas[i];

                bool result = Utils.ffmpeg(videoData.coverCmd);
                if (result)
                {
                    videoData.status = FileStatus.WatermarkDoCoverSuccess;
                }
                else
                {
                    videoData.status = FileStatus.WatermarkDoCoverError;
                }


                backgroundWorkerCover.ReportProgress((int)((i + 1.0) / waterMarkDatas.Count() * 100), i);
            }
        }

        public void backgroundWorkerCover_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int index = (int)e.UserState;
            updateItemInListView(index);

            if (index == 0)
            {
                curSelectIndexAtWatermarkDatas = 0;
                updateWaterMarkDetail(curSelectIndexAtWatermarkDatas);

                listView1.Items[curSelectIndexAtWatermarkDatas].Selected = true;
                listView1.Focus();
            }
        }

        public void backgroundWorkerWaterMark_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < waterMarkDatas.Count; i++)
            {
                WatermarkVideoItem videoData = waterMarkDatas[i];

                bool result = Utils.ffmpeg(videoData.getExecCmd());
                if (result)
                {
                    videoData.status = FileStatus.WatermarkDoSuccess;
                }
                else
                {
                    videoData.status = FileStatus.WatermarkDoError;
                }


                backgroundWorkerWaterMark.ReportProgress((int)((i + 1.0) / waterMarkDatas.Count() * 100), i);
            }
        }

        public void backgroundWorkerWaterMark_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

            pbProgress.Value = e.ProgressPercentage;

            int index = (int)e.UserState;
            updateItemInListView(index);

            if (index + 1 == waterMarkDatas.Count())
            {
                btnTab1RmWatermark.Enabled = true;
            }
        }

        /// <summary>
        /// 去水印 - 事件
        /// </summary>

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (checkBox1.Checked) btnTab1NextVideo_Click(null, null);
                return;
            }

            if (bDrawStart)
            {
                bDrawStart = false;
            }
            else
            {
                bDrawStart = true;
                pointStart = e.Location;
            }

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) return;

            if (bDrawStart)
            {
                pointContinue = e.Location;
                pictureBox1.Refresh();
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) return;

            if (bDrawStart)
            {
                Rectangle zoomImageRect = GetPictureBoxZoomSize(pictureBox1);
                Image image = pictureBox1.Image;

                float startX = (pictureBox1.Width - zoomImageRect.Width) / 2;
                float startY = (pictureBox1.Height - zoomImageRect.Height) / 2;
                float endX = startX + zoomImageRect.Width;
                float endY = startY + zoomImageRect.Height;

                if (
                    image != null &&
                    pointStart.X >= startX && pointStart.X <= endX && pointStart.Y >= startY && pointStart.Y <= endY &&
                    pointContinue.X >= startX && pointContinue.X <= endX && pointContinue.Y >= startY && pointContinue.Y <= endY &&
                    pointContinue.X >= pointStart.X && pointContinue.Y >= pointStart.Y &&
                    curSelectIndexAtWatermarkDatas >= 0
                   )
                {
                    dicPoints.Add(pointStart, pointContinue);

                    WaterMarkConfig water = new WaterMarkConfig();

                    int sx = (int)((pointStart.X - startX) / zoomImageRect.Width * image.Width);
                    int sy = (int)((pointStart.Y - startY) / zoomImageRect.Height * image.Height);

                    int ex = (int)((pointContinue.X - startX) / zoomImageRect.Width * image.Width);
                    int ey = (int)((pointContinue.Y - startY) / zoomImageRect.Height * image.Height);

                    water.X = sx;
                    water.Y = sy;
                    water.W = ex - sx;
                    water.H = ey - sy;

                    WatermarkVideoItem curVideoData = waterMarkDatas[curSelectIndexAtWatermarkDatas];

                    curVideoData.status = FileStatus.WatermarkDoMark;
                    curVideoData.docPoints.Remove(pointStart);
                    curVideoData.docPoints.Add(pointStart, pointContinue);
                    curVideoData.waterMarkDatas.Add(water);
                }

                pointStart = Point.Empty;
                pointContinue = Point.Empty;
                pictureBox1.Refresh();

                updateItemInListView(curSelectIndexAtWatermarkDatas);
                updateWaterMarkDetail(curSelectIndexAtWatermarkDatas);
            }
            bDrawStart = false;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.LimeGreen);

            pen.Width = 2;

            //实时的画矩形
            if (bDrawStart)
            {
                Graphics g = e.Graphics;
                g.DrawRectangle(pen, pointStart.X, pointStart.Y, pointContinue.X - pointStart.X, pointContinue.Y - pointStart.Y);
            }

            //实时的画之前已经画好的矩形
            foreach (var item in dicPoints)
            {
                Point p1 = item.Key;

                Point p2 = item.Value;

                e.Graphics.DrawRectangle(pen, p1.X, p1.Y, p2.X - p1.X, p2.Y - p1.Y);
            }

            pen.Dispose();
        }

        private void btnTab1PrevVideo_Click(object sender, EventArgs e)
        {

            if (curSelectIndexAtWatermarkDatas == 0)
            {
                MessageBox.Show("First Video...");
            }
            else
            {
                listView1.Items[curSelectIndexAtWatermarkDatas].Selected = false;
                updateWaterMarkDetail(--curSelectIndexAtWatermarkDatas);
                listView1.Items[curSelectIndexAtWatermarkDatas].Selected = true;
                listView1.Focus();
            }
        }

        private void btnTab1NextVideo_Click(object sender, EventArgs e)
        {
            if (curSelectIndexAtWatermarkDatas == waterMarkDatas.Count() - 1)
            {
                MessageBox.Show("Last Video...");
            }
            else
            {
                if (checkBox2.Checked)
                {
                    WatermarkVideoItem cur = waterMarkDatas[curSelectIndexAtWatermarkDatas];
                    WatermarkVideoItem next = waterMarkDatas[curSelectIndexAtWatermarkDatas + 1];

                    if (cur.docPoints.Count() > 0 && next.docPoints.Count() <= 0)
                    {
                        next.docPoints = new Dictionary<Point, Point>(cur.docPoints);
                        next.waterMarkDatas = new List<WaterMarkConfig>(cur.waterMarkDatas);
                        next.status = FileStatus.WatermarkDoMark;
                        updateItemInListView(curSelectIndexAtWatermarkDatas + 1);
                    }
                }

                listView1.Items[curSelectIndexAtWatermarkDatas].Selected = false;
                updateWaterMarkDetail(++curSelectIndexAtWatermarkDatas);
                listView1.Items[curSelectIndexAtWatermarkDatas].Selected = true;
                listView1.Focus();

            }
        }

        private void btnTab1CancelLabel_Click(object sender, EventArgs e)
        {
            WatermarkVideoItem curVideoData = waterMarkDatas[curSelectIndexAtWatermarkDatas];

            if (curVideoData.waterMarkDatas.Count > 0)
            {
                curVideoData.waterMarkDatas.RemoveAt(curVideoData.waterMarkDatas.Count - 1);

                if (curVideoData.waterMarkDatas.Count <= 0)
                {
                    curVideoData.status = FileStatus.WatermarkDoCoverSuccess;
                }
            }

            if (dicPoints.Count() > 0)
            {
                Point key = dicPoints.Last().Key;
                dicPoints.Remove(key);
                pictureBox1.Refresh();
            }

            updateItemInListView(curSelectIndexAtWatermarkDatas);
            updateWaterMarkDetail(curSelectIndexAtWatermarkDatas);
        }
        
        private void btnTab1RmWatermark_Click(object sender, EventArgs e)
        {
            int markCount = 0;
            List<String> unMarks = new List<String>();
            for (int i = 0; i < waterMarkDatas.Count; i++)
            {
                WatermarkVideoItem videoData = waterMarkDatas[i];
                if (videoData.waterMarkDatas.Count > 0)
                {
                    markCount++;
                }
                else
                {
                    unMarks.Add(i + 1 + "");
                }
            }

            if (unMarks.Count() > 0)
            {
                string message = String.Format("第 {0} 个视频, 还未标注水印! 是否继续去水印", String.Join(",", unMarks));
                string caption = "提示";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;
                result = MessageBox.Show(this, message, caption, buttons, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    btnTab1RmWatermark.Enabled = false;
                    this.backgroundWorkerWaterMark.RunWorkerAsync();
                }

                return;
            }

            btnTab1RmWatermark.Enabled = false;
            this.backgroundWorkerWaterMark.RunWorkerAsync();
        }


        /// <summary>
        /// 视频去重 - 随机修改
        /// </summary>

        private List<FileItem> repeatDatas = new List<FileItem>();
        private BackgroundWorker backgroundWorkerRepeat;

        RepeatConfig repeatConfig = new RepeatConfig();

        public void initRemoveRepeat()
        {
            backgroundWorkerRepeat = new System.ComponentModel.BackgroundWorker();
            backgroundWorkerRepeat.WorkerReportsProgress = true;
            backgroundWorkerRepeat.DoWork += new DoWorkEventHandler(backgroundWorkerRepeat_DoWork);
            backgroundWorkerRepeat.ProgressChanged += new ProgressChangedEventHandler(backgroundWorkerRepeat_ProgressChanged);
        }
        public async void backgroundWorkerRepeat_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < repeatDatas.Count; i++)
            {
                FileItem videoData = repeatDatas[i];

                videoData.status = FileStatus.RepeatDoing;
                backgroundWorkerRepeat.ReportProgress((int)((i + 0.0) / repeatDatas.Count() * 100), i);

                bool result = true;
                for (int j = 0; j < videoData.execCmds.Count; j++)
                {
                    if (result)
                    {
                        result = Utils.ffmpeg(videoData.execCmds[j]);
                    }
                }

                if (result)
                {
                    videoData.status = FileStatus.RepeatDoSuccess;
                }
                else
                {
                    videoData.status = FileStatus.RepeatDoError;
                }

                backgroundWorkerRepeat.ReportProgress((int)((i + 1.0) / repeatDatas.Count() * 100), i);
            }
        }

        public void backgroundWorkerRepeat_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbProgress.Value = e.ProgressPercentage;

            int index = (int)e.UserState;
            updateItemInListView(index);

            if (index == repeatDatas.Count() - 1 && repeatDatas[index].status != FileStatus.RepeatDoing)
            {
                btnTab2Repeat.Enabled = true;
            }
        }

        /// <summary>
        /// 去重 - 设置 ffmpeg 命令
        /// </summary>
        /// <param name="data"></param>
        /// <param name="repeatConfig"></param>
        public void updateRepeatCmd(FileItem data, RepeatConfig repeatConfig, int count)
        {
            data.execCmds.Clear();

            int width = data.width;
            int height = data.height;
            double duration = data.duration;
            double framerate = data.framerate;

            for (int j = 0; j < count; j++)
            {
                //0: input 1: filterComplex 2: output
                string cmdFormat = " -y {0} -filter_complex \"{1}\"  -map [audio] -map [video] \"{2}\"";

                List<String> inputCmds = new List<string>();
                Dictionary<String, int> ssCmds = new Dictionary<String, int>();
                inputCmds.Add(data.filePath);

                string filterComplex = "[0:v]rotate=0[video];[0:a]atempo=1.0[audio]";

                if (repeatConfig.isContrast)
                {
                    double contrast = Utils.nextRandomRangeAndExcluding((double)repeatConfig.contrastV1, (double)repeatConfig.contrastV2, 2, 1);

                    filterComplex += String.Format(";[video]eq=contrast={0}[video]", contrast);
                }

                if (repeatConfig.isSaturation)
                {
                    double saturation = Utils.nextRandomRangeAndExcluding((double)repeatConfig.saturationV1, (double)repeatConfig.saturationV2, 2, 1);

                    filterComplex += String.Format(";[video]eq=saturation={0}[video]", saturation);
                }

                if (repeatConfig.isBrightness)
                {
                    double brightness = Utils.nextRandomRangeAndExcluding((double)repeatConfig.brightnessV1, (double)repeatConfig.brightnessV2, 2, 0);

                    filterComplex += String.Format(";[video]eq=brightness={0}[video]", brightness);
                }

                if (repeatConfig.isRotate)
                {
                    double rotate = Utils.nextRandomRangeAndExcluding((double)repeatConfig.rotateV1, (double)repeatConfig.rotateV2, 2, 0);

                    filterComplex += String.Format(";[video]rotate={0}*PI/180,zoompan=z={1}:x='iw/2-(iw/zoom/2)':y='ih/2-(ih/zoom/2)':d=1:s={2}x{3}:fps={4}[video]", rotate, repeatConfig.rotateZoomV, width, height, framerate);
                }

                if (repeatConfig.isZoom)
                {
                    double zoom = Utils.nextRandomRange((double)repeatConfig.zoomV1, (double)repeatConfig.zoomV2, 2);
                    filterComplex += String.Format(";[video]zoompan=x='iw/2-(iw/zoom/2)':y='ih/2-(ih/zoom/2)':d=1:s={0}x{1}:fps={2},crop={3}:{4}[video]", data.width, (int)(data.height * zoom), framerate, width, height);
                }

                if (repeatConfig.isShakes)
                {
                    double shakes = Utils.nextRandomRangeAndExcluding((double)repeatConfig.shakesV1, (double)repeatConfig.shakesV2, 2, 0);
                    filterComplex += String.Format(";[video]zoompan=z='if(between(in_time,{0},{1}),min(max(zoom,pzoom)+0.008,1.5),1)':d=1:x='iw/2-(iw/zoom/2)':y='ih/2-(ih/zoom/2)':s={2}x{3}:fps={4}[video]", shakes, shakes + repeatConfig.shakesLength, width, height, framerate);
                }

                if (repeatConfig.isBackAudio)
                {
                    string file = repeatConfig.backAudioFiles[new Random().Next(repeatConfig.backAudioFiles.Count)];
                    inputCmds.Add(file);

                    int backgroundIndex = inputCmds.Count - 1;

                    filterComplex += String.Format(";[audio]volume=volume=2[aout0];[{0}:a]volume=volume=1[aout1];[aout1]aloop=loop=-1:size=2e+09,atrim=0:43[aconcat];[aout0][aconcat]amix=inputs=2:duration=first:dropout_transition=0 [audio]", backgroundIndex);
                }

                if (repeatConfig.isOverlay)
                {
                    for (int i = 0; i < repeatConfig.overlayVideoFiles.Count; i++)
                    {
                        string file = repeatConfig.overlayVideoFiles[i];
                        inputCmds.Add(file);
                        int overlayIndex = inputCmds.Count - 1;
                        ssCmds.Add(file, Utils.nextRandomRange(0, 10));
                        filterComplex += String.Format(";[{0}:v]scale={1}:{2},loop=loop=-1:size=1000[overlay];[video][overlay]overlay=shortest=1[video]", overlayIndex, width, height);
                    }
                }

                if (repeatConfig.isSetpts)
                {
                    double speed = Utils.nextRandomRangeAndExcluding(repeatConfig.setptsV1, repeatConfig.setptsV2, 2, 1);
                    double pts = (duration / speed / duration);
                    filterComplex += string.Format(";[audio]atempo={0}[audio];[video]setpts={1}*PTS[video]", speed, Math.Round(pts, 2));
                }

                string inputCMD = "";
                inputCmds.ForEach(x =>
                {
                    if (ssCmds.ContainsKey(x))
                    {
                        inputCMD += String.Format(" -ss {0} -i \"{1}\" ", ssCmds[x], x);
                    }
                    else
                    {
                        inputCMD += String.Format(" -i \"{0}\" ", x);
                    }
                });

                string cmd = String.Format(cmdFormat, inputCMD, filterComplex, Path.Join(data.outDir, j + 1 + "__" + data.fileName));
                data.execCmds.Add(cmd);
            }

            string log = System.DateTime.Now + "\r\nffmpeg" + string.Join("\r\nffmpeg", data.execCmds);
            recodeLog(log);
        }
        

        private void btnTab2SelectAudio_Click(object sender, EventArgs e)
        {
            repeatConfig.backAudioFiles.Clear();

            OpenFileDialog fd = new OpenFileDialog();
            fd.Multiselect = true;
            fd.Title = "Please Select File";
            fd.Filter = "All Audio Files|*.mp3;*.wma;*.rm;*.wav;*.mid;*.ape;*.flac";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                repeatConfig.backAudioFiles.AddRange(fd.FileNames);
                lbBackCount.Text = "" + fd.FileNames.Length;
            }
        }

        private void btnTab2SelectVideo_Click(object sender, EventArgs e)
        {
            repeatConfig.overlayVideoFiles.Clear();

            OpenFileDialog fd = new OpenFileDialog();
            fd.Multiselect = true;
            fd.Title = "Please Select File";
            fd.Filter = "All Video Files|*.mpg;*.mpeg;*.avi;*.rm;*.rmvb;*.mov;*.wmv;*.asf;*.dat;*.asx;*.wvx;*.mpe;*.mpa";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                repeatConfig.overlayVideoFiles.AddRange(fd.FileNames);
                lbOverCount.Text = "" + fd.FileNames.Count();
            }
        }

        private async void btnTab2Repeat_Click(object sender, EventArgs e)
        {
            if (repeatDatas.Count <= 0)
            {
                MessageBox.Show("请至少选择一个去重的视频");
                return;
            }

            //是否随机速率
            repeatConfig.isSetpts = cbSetpts.Checked;
            repeatConfig.setptsV1 = (double)nUDSetptsV1.Value;
            repeatConfig.setptsV2 = (double)nUDSetptsV2.Value;

            //是否随机对比度
            repeatConfig.isContrast = cbContrast.Checked;
            repeatConfig.contrastV1 = (double)nUDContrastV1.Value;
            repeatConfig.contrastV2 = (double)nUDContrastV2.Value;


            //是否随机饱和度
            repeatConfig.isSaturation = cbSaturation.Checked;
            repeatConfig.saturationV1 = (double)nUDSaturationV1.Value;
            repeatConfig.saturationV2 = (double)nUDSaturationV2.Value;

            //是否随机亮度
            repeatConfig.isBrightness = cbBrightness.Checked;
            repeatConfig.brightnessV1 = (double)nUDBrightnessV1.Value;
            repeatConfig.brightnessV2 = (double)nUDBrightnessV2.Value;

            //是否旋转
            repeatConfig.isRotate = cbRotate.Checked;
            repeatConfig.rotateV1 = (double)nUDRotateV1.Value;
            repeatConfig.rotateV2 = (double)nUDRotateV2.Value;
            repeatConfig.rotateZoomV = (double)nUDRotateZoom.Value;

            //是否拉伸
            repeatConfig.isZoom = cbZoom.Checked;
            repeatConfig.zoomV1 = (double)nUDZoomV1.Value;
            repeatConfig.zoomV2 = (double)nUDZoomV2.Value;

            //是否抖动
            repeatConfig.isShakes = cBShakes.Checked;
            repeatConfig.shakesV1 = (double)nUDShakesV1.Value;
            repeatConfig.shakesV2 = (double)nUDShakesV2.Value;
            repeatConfig.shakesLength = (double)nUDShakesLength.Value;


            //是否添加随机背景音乐
            repeatConfig.isBackAudio = cbBackground.Checked;

            //是否添加叠加视频
            repeatConfig.isOverlay = cbOverlay.Checked;

            if (!(repeatConfig.isSetpts || repeatConfig.isContrast || repeatConfig.isSaturation || repeatConfig.isBrightness || repeatConfig.isBackAudio || repeatConfig.isOverlay || repeatConfig.isRotate || repeatConfig.isZoom || repeatConfig.isShakes))
            {
                MessageBox.Show("请至少选择一个随机参数");
                return;
            }

            if (repeatConfig.isBackAudio && repeatConfig.backAudioFiles.Count <= 0)
            {
                MessageBox.Show("请至少选择一个背景音乐");
                return;
            }

            if (repeatConfig.isOverlay && repeatConfig.overlayVideoFiles.Count <= 0)
            {
                MessageBox.Show("请至少选择一个叠加视频");
                return;
            }

            btnTab2Repeat.Enabled = false;
            pbProgress.Value = 0;

            int count = (int)nUPRepeatCount.Value;

            for (int i = 0; i < repeatDatas.Count; i++)
            {
                FileItem videoData = repeatDatas[i];

                var mediaInfo = await FFmpeg.GetMediaInfo(videoData.filePath);
                var videoInfo = mediaInfo.VideoStreams.First();
                videoData.width = videoInfo.Width;
                videoData.height = videoInfo.Height;
                videoData.framerate = videoInfo.Framerate;
                videoData.duration = videoInfo.Duration.Seconds;

                updateRepeatCmd(videoData, repeatConfig, count);
            }

            backgroundWorkerRepeat.RunWorkerAsync();
        }

        /// <summary>
        /// 叠加图片
        /// </summary>

        private List<FileItem> pictureDatas = new List<FileItem>();
        private BackgroundWorker backgroundWorkerAddPicture;

        /// <summary>
        /// 叠加图片
        /// </summary>
        public void initOverlayPicture()
        {
            backgroundWorkerAddPicture = new System.ComponentModel.BackgroundWorker();
            backgroundWorkerAddPicture.WorkerReportsProgress = true;
            backgroundWorkerAddPicture.DoWork += new DoWorkEventHandler(backgroundWorkerAddPicture_DoWork);
            backgroundWorkerAddPicture.ProgressChanged += new ProgressChangedEventHandler(backgroundWorkerAddPicture_ProgressChanged);
        }

        public async void backgroundWorkerAddPicture_DoWork(object sender, DoWorkEventArgs e)
        {

            for (int i = 0; i < pictureDatas.Count; i++)
            {
                FileItem videoData = pictureDatas[i];

                videoData.status = FileStatus.PictureDoing;
                backgroundWorkerAddPicture.ReportProgress((int)((i + 0.0) / pictureDatas.Count() * 100), i);

                bool result = true;
                for (int j = 0; j < videoData.execCmds.Count; j++)
                {
                    if (!Utils.ffmpeg(videoData.execCmds[j]))
                    {
                        result = false;
                    }
                }

                if (result)
                {
                    videoData.status = FileStatus.PictureDoSuccess;
                }
                else
                {
                    videoData.status = FileStatus.PictureDoError;
                }

                backgroundWorkerAddPicture.ReportProgress((int)((i + 1.0) / pictureDatas.Count() * 100), i);
            }
        }

        public void backgroundWorkerAddPicture_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

            pbProgress.Value = e.ProgressPercentage;

            int index = (int)e.UserState;
            updateItemInListView(index);

            if (index == pictureDatas.Count() - 1 && pictureDatas[index].status != FileStatus.PictureDoing)
            {
                btnTab3Exec.Enabled = true;
            }
        }

        private void btnTab3BackgroundVideo_Click(object sender, EventArgs e)
        {
            Config.Instance.tab3BgVideoFiles.Clear();

            OpenFileDialog fd = new OpenFileDialog();
            fd.Multiselect = true;
            fd.Title = "Please Select File";
            fd.Filter = "All Video Files|*.mp4;*.mpg;*.mpeg;*.avi;*.rm;*.rmvb;*.mov;*.wmv;*.asf;*.dat;*.asx;*.wvx;*.mpe;*.mpa";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                Config.Instance.tab3BgVideoFiles.AddRange(fd.FileNames);
                labTab3BackgroundCount.Text = "" + fd.FileNames.Count();
            }
        }

        private void btnTab3Exec_Click(object sender, EventArgs e)
        {

            if (pictureDatas.Count <= 0)
            {
                MessageBox.Show("请至少选择一个图片");
                return;
            }

            if(Config.Instance.tab3BgVideoFiles.Count <= 0)
            {
                MessageBox.Show("请至少选择一个背景视频");
                return;
            }

            Config.Instance.tab3ExecCount = (int)nUDTab3ExecCount.Value;

            Config.Instance.tab3MinStartTime = (int)nUDTab3VideoMinStartTime.Value;
            Config.Instance.tab3MaxStartTime = (int)nUDTab3VideoMaxStartTime.Value;
            Config.Instance.tab3OutTime = (int)nUDTab3OutTime.Value;
            Config.Instance.tab3PictureSharpen = cbTab3PictureSharpen.Checked;
            Config.Instance.tab3PictureWidthMax = cbTab3PictureWidthMax.Checked;


            for (int i = 0; i < pictureDatas.Count; i++)
            {
                FileItem item = pictureDatas[i];
                item.execCmds.Clear();

                for(int j = 0; j < Config.Instance.tab3ExecCount; j++)
                {

                    var ss = Utils.nextRandomRange(Config.Instance.tab3MinStartTime, Config.Instance.tab3MaxStartTime);
                    var videoFile = Config.Instance.getRandomByTab3BackgroundVideo();
                    var imageFile = item.filePath;
                    var fontFile = Config.Instance.tab3LogoPictureFile;

                    int xRandom = Utils.nextRandomRange(75, 138);
                    int yRandom = Utils.nextRandomRange(0, 100);


                    var ffmpegFormat = " -y {0} -filter_complex \"{1}\" \"{2}\" ";

                    var tempInput = string.Format(" -ss {0} -t {1} -i \"{2}\" -i \"{3}\" ", ss, Config.Instance.tab3OutTime, videoFile, imageFile);
                    
                    var tempFilter = "[1:v][0:v]scale2ref=w=iw/10*8:h=ow/main_a[over][video]";
                    if (Config.Instance.tab3PictureWidthMax)
                    {
                        tempFilter = "[1:v][0:v]scale2ref=w=iw:h=ow/main_a[over][video]";
                    }


                    var tempOut = Path.Join(item.outDir, j + 1 + "__" + item.fileName + Path.GetExtension(videoFile));

                    if (Config.Instance.tab3PictureSharpen)
                    {
                        tempFilter += ";[over]unsharp=3:3:5[over]";
                    }

                    if (fontFile.Count > 0)
                    {
                        tempInput += string.Format(" -i {0} ", Config.Instance.getRandomByTab3LogoPicture());
                        tempFilter += ";[2:v][over]scale2ref=w=iw:h=ow/main_a[font][over];[over][font]vstack[over]";
                    }

                    if (Config.Instance.tab3PictureWidthMax)
                    {
                        tempFilter += string.Format(";[video][over]overlay=x=W*{0}/1000:y=H/16+H/16*{1}/100", 0, yRandom);
                    }
                    else
                    {
                        tempFilter += string.Format(";[video][over]overlay=x=W*{0}/1000:y=H/16+H/16*{1}/100", xRandom, yRandom);
                    }


                    //" -y -ss {0} -i \"{1}\" -i \"{2}\" -i \"{3}\" -filter_complex \"[0:a]acopy[audio];[1:v][0:v]scale2ref=w=iw/10*8:h=ow/main_a[pic][video];[2:v][pic]scale2ref=w=iw:h=ow/main_a[font][pic];[pic][font]vstack[over];[video][pic]overlay=x=W*{4}/1000:y=H/16+H/16*{5}/100[video]\" -map [audio] -map [video] \"{6}\" "
                    var cmd = String.Format(ffmpegFormat, tempInput, tempFilter, tempOut);
                    item.execCmds.Add(cmd);
                }

                string log = System.DateTime.Now + "\r\nffmpeg" + string.Join("\r\nffmpeg", item.execCmds);
                recodeLog(log);
            }

            btnTab3Exec.Enabled = false;
            pbProgress.Value = 0;
            backgroundWorkerAddPicture.RunWorkerAsync();

        }

        private void btnTab3AddFontPicture_Click(object sender, EventArgs e)
        {

            OpenFileDialog fd = new OpenFileDialog();
            fd.Multiselect = true;
            fd.Title = "Please Select File";
            fd.Filter = "All Image Files|*.jpg;*.png;*.jepg";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                Config.Instance.tab3LogoPictureFile = new List<String>();
                string[] files = fd.FileNames;

                for (int i = 0; i < files.Length; i++)
                {
                    Config.Instance.tab3LogoPictureFile.Add(files[i]);
                }

                labTab3AddFontPicture.Text = "" + files.Length;
            }
        }

        private void 更新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = "https://github.com/itwangxiang/IdeaVideoAI/releases",
                UseShellExecute = true
            });
        }
    }

}