using System.ComponentModel;
using System.Reflection;
using Xabe.FFmpeg;
using Xabe.FFmpeg.Downloader;

namespace IdeaVideoAI
{

    public partial class Form1 : Form
    {

        int curVideoIndex = -1;

        //string[] statusDesc = new string[7] { "读取中", "待标注", "已标注", "已处理", "待去重", "已去重","去重失败" };


        RepeatConfig repeatConfig = new RepeatConfig();

        List<WatermarkVideoItem> waterMarkDatas = new List<WatermarkVideoItem>();

        List<RepeatVideoItem> repeatDatas = new List<RepeatVideoItem>();

        public bool bDrawStart = false;
        public Dictionary<Point, Point> dicPoints = new Dictionary<Point, Point>();
        public Point pointStart = Point.Empty;
        public Point pointContinue = Point.Empty;


        private BackgroundWorker backgroundWorkerCover;
        private BackgroundWorker backgroundWorkerWaterMark;
        private BackgroundWorker backgroundWorkerRepeat;

        public Form1()
        {
            InitializeComponent();

            backgroundWorkerCover = new System.ComponentModel.BackgroundWorker();
            backgroundWorkerCover.WorkerReportsProgress = true;
            backgroundWorkerCover.DoWork += new DoWorkEventHandler(backgroundWorkerCover_DoWork);
            backgroundWorkerCover.ProgressChanged += new ProgressChangedEventHandler(backgroundWorkerCover_ProgressChanged);

            backgroundWorkerWaterMark = new System.ComponentModel.BackgroundWorker();
            backgroundWorkerWaterMark.WorkerReportsProgress = true;
            backgroundWorkerWaterMark.DoWork += new DoWorkEventHandler(backgroundWorkerWaterMark_DoWork);
            backgroundWorkerWaterMark.ProgressChanged += new ProgressChangedEventHandler(backgroundWorkerWaterMark_ProgressChanged);

            backgroundWorkerRepeat = new System.ComponentModel.BackgroundWorker();
            backgroundWorkerRepeat.WorkerReportsProgress = true;
            backgroundWorkerRepeat.DoWork += new DoWorkEventHandler(backgroundWorkerRepeat_DoWork);
            backgroundWorkerRepeat.ProgressChanged += new ProgressChangedEventHandler(backgroundWorkerRepeat_ProgressChanged);

            FFmpeg.SetExecutablesPath(Environment.CurrentDirectory,ffmpegExeutableName: "ffmpeg.exe", ffprobeExecutableName: "ffprobe.exe");
        }

        public void backgroundWorkerCover_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < waterMarkDatas.Count(); i++)
            {
                WatermarkVideoItem videoData = waterMarkDatas[i];

                bool result = Utils.ffmpeg(videoData.coverCmd);
                if (result)
                {
                    videoData.status = VideoStatus.WatermarkDoCoverSuccess;
                }
                else
                {
                    videoData.status = VideoStatus.WatermarkDoCoverError;
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
                curVideoIndex = 0;
                updateWaterMarkDetail(curVideoIndex);

                listView1.Items[curVideoIndex].Selected = true;
                listView1.Focus();
            }
        }

        public void backgroundWorkerWaterMark_DoWork(object sender, DoWorkEventArgs e)
        {

            for (int i = 0; i < waterMarkDatas.Count; i++)
            {
                WatermarkVideoItem videoData = waterMarkDatas[i];

                bool result = Utils.ffmpeg(getClearCmd(videoData));
                if (result)
                {
                    videoData.status = VideoStatus.WatermarkDoSuccess;
                }
                else
                {
                    videoData.status = VideoStatus.WatermarkDoError;
                }


                backgroundWorkerWaterMark.ReportProgress((int)((i + 1.0) / waterMarkDatas.Count() * 100), i);
            }
        }

        public void backgroundWorkerWaterMark_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

            progressBar1.Value = e.ProgressPercentage;

            int index = (int)e.UserState;
            updateItemInListView(index);

            if (index + 1 == waterMarkDatas.Count())
            {
                btnRemoveWatermark.Enabled = true;
            }
        }

        public async void backgroundWorkerRepeat_DoWork(object sender, DoWorkEventArgs e)
        {

            for (int i = 0; i < repeatDatas.Count; i++)
            {
                RepeatVideoItem videoData = repeatDatas[i];

                bool result = Utils.ffmpeg(videoData.repeatCmd);
                if (result)
                {
                    videoData.status = VideoStatus.RepeatDoSuccess;
                }
                else
                {
                    videoData.status = VideoStatus.RepeatDoError;
                }

                backgroundWorkerRepeat.ReportProgress((int)((i + 1.0) / repeatDatas.Count() * 100), i);
            }
        }

        public void backgroundWorkerRepeat_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

            int index = (int)e.UserState;
            updateItemInListView(index);

            if (index + 1 == repeatDatas.Count())
            {
                btnRepeat.Enabled = true;
            }
        }

        /// <summary>
        /// 获取图片大小
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
        /// 加载文件列表
        /// </summary>
        private void loadListView()
        {

            bool isWaterMark = tabControl1.SelectedIndex == 0;
            List<VideoItem> tempDatas = new List<VideoItem>();

            if (isWaterMark)
            {
                waterMarkDatas.ForEach(data => tempDatas.Add(data));
            }
            else
            {
                repeatDatas.ForEach(data => tempDatas.Add(data));
            }

            listView1.BeginUpdate();
            listView1.Items.Clear();

            for (int i = 0; i < tempDatas.Count; i++)
            {

                VideoItem data = tempDatas[i];

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

            bool isWaterMark = tabControl1.SelectedIndex == 0;

            VideoItem videoData;

            if (isWaterMark)
            {
                videoData = waterMarkDatas[index];
            }
            else
            {
                videoData = repeatDatas[index];
            }

            ListViewItem listViewItem = listView1.Items[index];
            listViewItem.SubItems.Clear();
            listViewItem.Text = index + 1 + "-" + videoData.fileName;
            listViewItem.SubItems.Add(Utils.getVideoStatus(videoData.status));

        }

        /// <summary>
        /// 更新去水印 - 图片标注信息
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
                Image dummy = Image.FromFile(Path.Join(videoData.tempCoverDir, videoData.coverName));

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

        /// <summary>
        /// 获取去水印指令
        /// </summary>
        /// <param name="videoData"></param>
        /// <returns></returns>
        private string getClearCmd(WatermarkVideoItem videoData)
        {
            string clearCmd = String.Format(" -y -i \"{0}\" -vf \"", videoData.filePath);
            videoData.waterMarkDatas.ForEach(x =>
            {
                clearCmd += String.Format("delogo=x={0}:y={1}:w={2}:h={3}:show=0,", x.X,
                x.Y, x.W, x.H);
            });
            clearCmd = clearCmd.Remove(clearCmd.Length - 1);
            clearCmd += String.Format("\" \"{0}\"", Path.Join(videoData.tempClearWaterMarkDir, videoData.fileName));
            return clearCmd;
        }

        private void openVideoFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!File.Exists(Path.Join(Environment.CurrentDirectory, "ffmpeg.exe")))
            {
                MessageBox.Show("你还未安装 FFmpeg，请点击帮助菜单中的安装 ffmpeg 选项...");
                return;
            }

            bool isWaterMark = tabControl1.SelectedIndex == 0;

            OpenFileDialog fd = new OpenFileDialog();
            fd.Multiselect = true;
            fd.Title = "Please Select File";
            fd.Filter = "All Video Files|*.mp4;*.mpg;*.mpeg;*.avi;*.rm;*.rmvb;*.mov;*.wmv;*.asf;*.dat;*.asx;*.wvx;*.mpe;*.mpa";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                curVideoIndex = 0;

                if (isWaterMark)
                {
                    waterMarkDatas.Clear();
                }
                else
                {
                    repeatDatas.Clear();
                }


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

                string clearWaterMarkPath = Path.Join(Path.GetDirectoryName(fd.FileName), ".tempVideo");
                string videoCoverPath = Path.Join(Path.GetDirectoryName(fd.FileName), ".tempCover");
                string videoRepeatPath = Path.Join(Path.GetDirectoryName(fd.FileName), ".tempRepeat");

                try
                {
                    if (Directory.Exists(clearWaterMarkPath)) Directory.Delete(clearWaterMarkPath, true);
                    Directory.CreateDirectory(clearWaterMarkPath);
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }

                try
                {
                    if (Directory.Exists(videoCoverPath)) Directory.Delete(videoCoverPath, true);
                    Directory.CreateDirectory(videoCoverPath);
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }

                try
                {
                    if (Directory.Exists(videoRepeatPath)) Directory.Delete(videoRepeatPath, true);
                    Directory.CreateDirectory(videoRepeatPath);
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }


                string[] files = fd.FileNames;

                for (int i = 0; i < files.Length; i++)
                {
                    if (isWaterMark)
                    {
                        WatermarkVideoItem videoData = new WatermarkVideoItem();

                        videoData.filePath = files[i];
                        videoData.fileName = Path.GetFileName(videoData.filePath);

                        videoData.tempCoverDir = videoCoverPath;
                        videoData.tempClearWaterMarkDir = clearWaterMarkPath;

                        videoData.coverName = i + 1 + ".jpg";
                        videoData.coverCmd = String.Format(" -y -ss 00:00:05 -i \"{0}\" -vframes 1 \"{1}\"", videoData.filePath, Path.Join(videoCoverPath, videoData.coverName));
                        videoData.status = VideoStatus.WatermarkLoad;

                        waterMarkDatas.Add(videoData);
                    }
                    else
                    {
                        RepeatVideoItem videoData = new RepeatVideoItem();

                        videoData.filePath = files[i];
                        videoData.fileName = Path.GetFileName(videoData.filePath);
                        videoData.status = VideoStatus.RepeatLoad;

                        videoData.tempRepeatDir = videoRepeatPath;

                        repeatDatas.Add(videoData);
                    }
                }

                loadListView();

                if (isWaterMark) backgroundWorkerCover.RunWorkerAsync();
            }
        }


        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (checkBox1.Checked) button4_Click(null, null);
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
                    curVideoIndex >= 0
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

                    WatermarkVideoItem curVideoData = waterMarkDatas[curVideoIndex];

                    curVideoData.status = VideoStatus.WatermarkDoMark;
                    curVideoData.docPoints.Remove(pointStart);
                    curVideoData.docPoints.Add(pointStart, pointContinue);
                    curVideoData.waterMarkDatas.Add(water);
                }

                pointStart = Point.Empty;
                pointContinue = Point.Empty;
                pictureBox1.Refresh();

                updateItemInListView(curVideoIndex);
                updateWaterMarkDetail(curVideoIndex);
            }
            bDrawStart = false;
        }

        private void btnRemoveWatermark_Click(object sender, EventArgs e)
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
                    btnRemoveWatermark.Enabled = false;
                    this.backgroundWorkerWaterMark.RunWorkerAsync();
                }

                return;
            }

            btnRemoveWatermark.Enabled = false;
            this.backgroundWorkerWaterMark.RunWorkerAsync();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (curVideoIndex == 0)
            {
                MessageBox.Show("First Video...");
            }
            else
            {
                listView1.Items[curVideoIndex].Selected = false;
                updateWaterMarkDetail(--curVideoIndex);
                listView1.Items[curVideoIndex].Selected = true;
                listView1.Focus();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (curVideoIndex == waterMarkDatas.Count() - 1)
            {
                MessageBox.Show("Last Video...");
            }
            else
            {
                if (checkBox2.Checked)
                {
                    WatermarkVideoItem cur = waterMarkDatas[curVideoIndex];
                    WatermarkVideoItem next = waterMarkDatas[curVideoIndex + 1];

                    if (cur.docPoints.Count() > 0 && next.docPoints.Count() <= 0)
                    {
                        next.docPoints = new Dictionary<Point, Point>(cur.docPoints);
                        next.waterMarkDatas = new List<WaterMarkConfig>(cur.waterMarkDatas);
                        next.status = VideoStatus.WatermarkDoMark;
                        updateItemInListView(curVideoIndex + 1);
                    }
                }

                listView1.Items[curVideoIndex].Selected = false;
                updateWaterMarkDetail(++curVideoIndex);
                listView1.Items[curVideoIndex].Selected = true;
                listView1.Focus();

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            WatermarkVideoItem curVideoData = waterMarkDatas[curVideoIndex];

            if (curVideoData.waterMarkDatas.Count > 0)
            {
                curVideoData.waterMarkDatas.RemoveAt(curVideoData.waterMarkDatas.Count - 1);

                if (curVideoData.waterMarkDatas.Count <= 0)
                {
                    curVideoData.status = VideoStatus.WatermarkDoCoverSuccess;
                }
            }

            if (dicPoints.Count() > 0)
            {
                Point key = dicPoints.Last().Key;
                dicPoints.Remove(key);
                pictureBox1.Refresh();
            }

            updateItemInListView(curVideoIndex);
            updateWaterMarkDetail(curVideoIndex);
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

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            int selectIndex = e.ItemIndex;
            curVideoIndex = selectIndex;
            updateWaterMarkDetail(curVideoIndex);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadListView();
        }

        private async void button6_Click(object sender, EventArgs e)
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
            
            btnRepeat.Enabled = false;

            for (int i = 0; i < repeatDatas.Count; i++)
            {
                RepeatVideoItem videoData = repeatDatas[i];

                var mediaInfo = await FFmpeg.GetMediaInfo(videoData.filePath);
                var videoInfo = mediaInfo.VideoStreams.First();
                videoData.width = videoInfo.Width;
                videoData.height = videoInfo.Height;
                videoData.framerate = videoInfo.Framerate;

                updateRepeatCmd(videoData, repeatConfig);
            }

            backgroundWorkerRepeat.RunWorkerAsync();
        }

        /// <summary>
        /// 更新去重 ffmpeg 命令
        /// </summary>
        /// <param name="data"></param>
        /// <param name="repeatConfig"></param>
        public void updateRepeatCmd(RepeatVideoItem data, RepeatConfig repeatConfig)
        {
            int width = data.width;
            double framerate = data.framerate;
            int height = data.height;

            //0: input 1: filterComplex 2: output
            string cmdFormat = " -y {0} -filter_complex \"{1}\"  -map [audio] -map [video] \"{2}\"";

            List<String> inputCmds = new List<string>();
            Dictionary<String, int> ssCmds = new Dictionary<String, int>();
            inputCmds.Add(data.filePath);

            string filterComplex = "[0:v]rotate=0[video];[0:a]atempo=1.0[audio]";

            if (repeatConfig.isSetpts)
            {
                double speed = Utils.nextRandomRangeAndExcluding(repeatConfig.setptsV1, repeatConfig.setptsV2, 2, 1);

                double pts = 3 - 2 * speed;

                filterComplex += string.Format(";[audio]atempo={0}[audio];[video]setpts={1}*PTS[video]", speed, pts);
            }

            if (repeatConfig.isContrast)
            {
                double contrast = Utils.nextRandomRangeAndExcluding((double)repeatConfig.contrastV1, (double)repeatConfig.contrastV2, 2,1);

                filterComplex += String.Format(";[video]eq=contrast={0}[video]", contrast);
            }

            if (repeatConfig.isSaturation)
            {
                double saturation = Utils.nextRandomRangeAndExcluding((double)repeatConfig.saturationV1, (double)repeatConfig.saturationV2, 2,1);

                filterComplex += String.Format(";[video]eq=saturation={0}[video]", saturation);
            }

            if (repeatConfig.isBrightness)
            {
                double brightness = Utils.nextRandomRangeAndExcluding((double)repeatConfig.brightnessV1, (double)repeatConfig.brightnessV2,2,0);

                filterComplex += String.Format(";[video]eq=brightness={0}[video]", brightness);
            }

            if (repeatConfig.isRotate)
            {
                double rotate = Utils.nextRandomRangeAndExcluding((double)repeatConfig.rotateV1, (double)repeatConfig.rotateV2,2, 0);

                filterComplex += String.Format(";[video]rotate={0}*PI/180,zoompan=z={1}:x='iw/2-(iw/zoom/2)':y='ih/2-(ih/zoom/2)':d=1:s={2}x{3}:fps={4}[video]", rotate, repeatConfig.rotateZoomV, width, height,framerate);
            }

            if (repeatConfig.isZoom)
            {
                double zoom = Utils.nextRandomRange((double)repeatConfig.zoomV1, (double)repeatConfig.zoomV2,2);
                filterComplex += String.Format(";[video]zoompan=x='iw/2-(iw/zoom/2)':y='ih/2-(ih/zoom/2)':d=1:s={0}x{1}:fps={2},crop={3}:{4}[video]", data.width, (int)(data.height * zoom),framerate,width, height);
            }

            if (repeatConfig.isShakes)
            {
                double shakes = Utils.nextRandomRangeAndExcluding((double)repeatConfig.shakesV1, (double)repeatConfig.shakesV2,2, 0);
                filterComplex += String.Format(";[video]zoompan=z='if(between(in_time,{0},{1}),min(max(zoom,pzoom)+0.008,1.5),1)':d=1:x='iw/2-(iw/zoom/2)':y='ih/2-(ih/zoom/2)':s={2}x{3}:fps={4}[video]", shakes, shakes + repeatConfig.shakesLength, width, height,framerate);
            }

            if (repeatConfig.isBackAudio)
            {
                string file = repeatConfig.backAudioFiles[new Random().Next(repeatConfig.backAudioFiles.Count)];
                inputCmds.Add(file);

                int backgroundIndex = inputCmds.Count - 1;

                filterComplex += String.Format(";[audio]volume=volume=2[aout0];[{0}:a]volume=volume=1[aout1];[aout1]aloop=loop=-1:size=2e+09,atrim=0:43[aconcat]; [aout0][aconcat]amix=inputs=2:duration=first:dropout_transition=0 [audio]", backgroundIndex);
            }

            if (repeatConfig.isOverlay)
            {
                for (int i = 0; i < repeatConfig.overlayVideoFiles.Count; i++)
                {
                    string file = repeatConfig.overlayVideoFiles[i];
                    inputCmds.Add(file);
                    int overlayIndex = inputCmds.Count - 1;
                    ssCmds.Add(file, Utils.nextRandomRange(0, 10));
                    filterComplex += String.Format(";[{0}:v][0:v]scale2ref=w=iw:h=ih[overlay];[overlay]loop=loop=-1:size=1000[overlay];[video][overlay]overlay=shortest=1[video]", overlayIndex);
                }
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

            string cmd = String.Format(cmdFormat, inputCMD, filterComplex, Path.Join(data.tempRepeatDir, data.fileName));
            data.repeatCmd = cmd;

            if (String.IsNullOrEmpty(tbRepeatLog.Text))
            {
                tbRepeatLog.Text = System.DateTime.Now + "\r\nffmpeg" + cmd;
            }
            else
            {
                tbRepeatLog.Text = System.DateTime.Now + "\r\nffmpeg" + cmd + "\r\n\r\n" + tbRepeatLog.Text;
            }
        }

        private void button7_Click(object sender, EventArgs e)
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

        private void button9_Click(object sender, EventArgs e)
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

        private async void 安装FfmpegToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("关闭弹窗后，开始安装 FFmpeg，安装成功会提示.");
            await FFmpegDownloader.GetLatestVersion(FFmpegVersion.Official);
            MessageBox.Show("FFmpeg 已安装成功...");
        }
    }

}