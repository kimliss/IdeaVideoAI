using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;

namespace IdeaVideoAI
{

    public partial class Form1 : Form
    {

        string ffmpegCmd = "ffmpeg";

        int curVideoIndex = -1;

        string[] statusDesc = new string[4] { "读取中", "待标注", "已标注", "已处理" };

        List<VideoData> datas;

        public bool bDrawStart = false;
        public Dictionary<Point, Point> dicPoints = new Dictionary<Point, Point>();
        public Point pointStart = Point.Empty;
        public Point pointContinue = Point.Empty;


        private BackgroundWorker backgroundWorkerCover = null;
        private BackgroundWorker backgroundWorkerWaterMark = null;

        public Form1()
        {
            InitializeComponent();

            string ffmpegPath = Path.Join(Environment.CurrentDirectory, "ffmpeg.exe");
            if (File.Exists(ffmpegPath)) ffmpegCmd = ffmpegPath;

            datas = new List<VideoData>();


            backgroundWorkerCover = new System.ComponentModel.BackgroundWorker();
            backgroundWorkerCover.WorkerReportsProgress = true;
            backgroundWorkerCover.DoWork += new DoWorkEventHandler(backgroundWorkerCover_DoWork);
            backgroundWorkerCover.ProgressChanged += new ProgressChangedEventHandler(backgroundWorkerCover_ProgressChanged);

            backgroundWorkerWaterMark = new System.ComponentModel.BackgroundWorker();
            backgroundWorkerWaterMark.WorkerReportsProgress = true;
            backgroundWorkerWaterMark.DoWork += new DoWorkEventHandler(backgroundWorkerWaterMark_DoWork);
            backgroundWorkerWaterMark.ProgressChanged += new ProgressChangedEventHandler(backgroundWorkerWaterMark_ProgressChanged);
        }

        public void backgroundWorkerCover_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < datas.Count(); i++)
            {
                VideoData videoData = datas[i];

                execCmd(videoData.coverCmd);

                videoData.status = statusDesc[1];

                backgroundWorkerCover.ReportProgress((int)((i + 1.0) / datas.Count() * 100), i);
            }
        }

        public void backgroundWorkerCover_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int index = (int)e.UserState;
            updateItemInListView(index);

            if(index == 0)
            {
                curVideoIndex = 0;
                updateVideoDetail(curVideoIndex);

                listView1.Items[curVideoIndex].Selected = true;
                listView1.Focus();
            }
        }

        public void backgroundWorkerWaterMark_DoWork(object sender, DoWorkEventArgs e)
        {

            for (int i = 0; i < datas.Count; i++)
            {
                VideoData videoData = datas[i];

                execCmd(getClearCmd(videoData));

                videoData.status = statusDesc[3];

                backgroundWorkerWaterMark.ReportProgress((int)((i + 1.0) / datas.Count() * 100), i);
            }
        }

        public void backgroundWorkerWaterMark_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

            progressBar1.Value = e.ProgressPercentage;

            int index = (int) e.UserState;
            updateItemInListView(index);

            if(index + 1 == datas.Count())
            {
                button2.Enabled = true;
            }
        }


        public Rectangle GetPictureBoxZoomSize(PictureBox p_PictureBox)
        {
            if (p_PictureBox != null)
            {
                PropertyInfo _ImageRectanglePropert = p_PictureBox.GetType().GetProperty("ImageRectangle", BindingFlags.Instance | BindingFlags.NonPublic);
                return (Rectangle)_ImageRectanglePropert.GetValue(p_PictureBox, null);
            }
            return new Rectangle(0, 0, 0, 0);
        }

        private void openVideoFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Multiselect = true;
            fd.Title = "Please Select File";
            fd.Filter = "All Media Files|*.wav;*.aac;*.wma;*.wmv;*.avi;*.mpg;*.mpeg;*.m1v;*.mp2;*.mp3;*.mpa;*.mpe;*.m3u;*.mp4;*.mov;*.3g2;*.3gp2;*.3gp;*.3gpp;*.m4a;*.cda;*.aif;*.aifc;*.aiff;*.mid;*.midi;*.rmi;*.mkv;*.WAV;*.AAC;*.WMA;*.WMV;*.AVI;*.MPG;*.MPEG;*.M1V;*.MP2;*.MP3;*.MPA;*.MPE;*.M3U;*.MP4;*.MOV;*.3G2;*.3GP2;*.3GP;*.3GPP;*.M4A;*.CDA;*.AIF;*.AIFC;*.AIFF;*.MID;*.MIDI;*.RMI;*.MKV";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                curVideoIndex = 0;

                datas.Clear();
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

                string clearWaterMarkPath = Path.Join(Path.GetDirectoryName(fd.FileName), "tempClearWaterMark");
                string videoCoverPath = Path.Join(Path.GetDirectoryName(fd.FileName), "tempVideoCover");

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
                    MessageBox.Show(ex.Message);
                }


                string[] files = fd.FileNames;

                for (int i = 0; i < files.Length; i++)
                {
                    VideoData videoData = new VideoData();

                    videoData.tempCoverDir = videoCoverPath;
                    videoData.tempClearWaterMarkDir = clearWaterMarkPath;

                    videoData.filePath = files[i];
                    videoData.fileName = Path.GetFileName(videoData.filePath);
                    videoData.coverName = i + 1 + ".jpg";
                    videoData.coverCmd = String.Format("{0} -ss 00:00:05 -y -i \"{1}\" -vframes 1 \"{2}\"", ffmpegCmd, videoData.filePath, Path.Join(videoCoverPath, videoData.coverName));
                    videoData.status = statusDesc[0];

                    datas.Add(videoData);
                }

                loadListView();

                backgroundWorkerCover.RunWorkerAsync();
            }
        }

        private void execCmd(String cmd)
        {
            execCmd(cmd, false);
        }

        private void execCmd(string cmd, bool isShow)
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

        private void loadListView()
        {

            listView1.BeginUpdate();

            listView1.Items.Clear();

            for (int i = 0; i < datas.Count(); i++)
            {

                VideoData data = datas[i];

                ListViewItem listViewItem = new ListViewItem();

                listViewItem.Text = i + 1 + "-" + data.fileName;
                listViewItem.SubItems.Add(data.status);

                listView1.Items.Add(listViewItem);
            }

            listView1.EndUpdate();
        }

        private void updateItemInListView(int index)
        {
            if (index < 0) return;

            VideoData videoData = datas[index];

            ListViewItem listViewItem = listView1.Items[index];
            listViewItem.SubItems.Clear();
            listViewItem.Text = index + 1 + "-" + videoData.fileName;
            listViewItem.SubItems.Add(videoData.status);
        }

        private void updateVideoDetail(int index)
        {
            if (index < 0) return;

            VideoData videoData = datas[index];

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

                if (pointStart.X >= startX && pointStart.X <= endX && pointStart.Y >= startY && pointStart.Y <= endY &&
                    pointContinue.X >= startX && pointContinue.X <= endX && pointContinue.Y >= startY && pointContinue.Y <= endY &&
                    pointContinue.X >= pointStart.X && pointContinue.Y >= pointStart.Y &&
                    curVideoIndex >= 0
                   )
                {
                    dicPoints.Add(pointStart, pointContinue);

                    WaterMarkData water = new WaterMarkData();

                    int sx = (int)((pointStart.X - startX) / zoomImageRect.Width * image.Width);
                    int sy = (int)((pointStart.Y - startY) / zoomImageRect.Height * image.Height);

                    int ex = (int)((pointContinue.X - startX) / zoomImageRect.Width * image.Width);
                    int ey = (int)((pointContinue.Y - startY) / zoomImageRect.Height * image.Height);

                    water.X = sx;
                    water.Y = sy;
                    water.W = ex - sx;
                    water.H = ey - sy;

                    VideoData curVideoData = datas[curVideoIndex];

                    curVideoData.status = statusDesc[2];
                    curVideoData.docPoints.Remove(pointStart);
                    curVideoData.docPoints.Add(pointStart, pointContinue);
                    curVideoData.waterMarkDatas.Add(water);
                }

                pointStart = Point.Empty;
                pointContinue = Point.Empty;
                pictureBox1.Refresh();

                updateItemInListView(curVideoIndex);
                updateVideoDetail(curVideoIndex);
            }
            bDrawStart = false;
        }

        private string getClearCmd(VideoData videoData)
        {
            string clearCmd = String.Format("{0} -y -i \"{1}\" -vf \"", ffmpegCmd, videoData.filePath);
            videoData.waterMarkDatas.ForEach(x =>
            {
                clearCmd += String.Format("delogo=x={0}:y={1}:w={2}:h={3}:show=0,", x.X,
                x.Y, x.W, x.H);
            });
            clearCmd = clearCmd.Remove(clearCmd.Length - 1);
            clearCmd += String.Format("\" \"{0}\"", Path.Join(videoData.tempClearWaterMarkDir, videoData.fileName));
            return clearCmd;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            VideoData curVideoData = datas[curVideoIndex];

            if (curVideoData.waterMarkDatas.Count() <= 0)
            {
                MessageBox.Show("请先标注水印，再操作");
                return;
            }

            execCmd(getClearCmd(curVideoData), true);

            curVideoData.status = statusDesc[2];

            updateItemInListView(curVideoIndex);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int markCount = 0;
            List<String> unMarks = new List<String>();
            for (int i = 0; i < datas.Count; i++)
            {
                VideoData videoData = datas[i];
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
                    button2.Enabled = false;
                    this.backgroundWorkerWaterMark.RunWorkerAsync();
                }

                return;
            }

            button2.Enabled = false;
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
                updateVideoDetail(--curVideoIndex);
                listView1.Items[curVideoIndex].Selected = true;
                listView1.Focus();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (curVideoIndex == datas.Count() - 1)
            {
                MessageBox.Show("Last Video...");
            }
            else
            {
                if (checkBox2.Checked)
                {
                    VideoData cur = datas[curVideoIndex];
                    VideoData next = datas[curVideoIndex + 1];

                    if (cur.docPoints.Count() > 0 && next.docPoints.Count() <= 0)
                    {
                        next.docPoints = new Dictionary<Point, Point>(cur.docPoints);
                        next.waterMarkDatas = new List<WaterMarkData>(cur.waterMarkDatas);
                        next.status = statusDesc[2];
                        updateItemInListView(curVideoIndex + 1);
                    }
                }

                listView1.Items[curVideoIndex].Selected = false;
                updateVideoDetail(++curVideoIndex);
                listView1.Items[curVideoIndex].Selected = true;
                listView1.Focus();

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            VideoData curVideoData = datas[curVideoIndex];

            if (curVideoData.waterMarkDatas.Count > 0)
            {
                curVideoData.waterMarkDatas.RemoveAt(curVideoData.waterMarkDatas.Count - 1);

                if (curVideoData.waterMarkDatas.Count <= 0)
                {
                    curVideoData.status = statusDesc[1];
                }
            }

            if (dicPoints.Count() > 0)
            {
                Point key = dicPoints.Last().Key;
                dicPoints.Remove(key);
                pictureBox1.Refresh();
            }

            updateItemInListView(curVideoIndex);
            updateVideoDetail(curVideoIndex);
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
            updateVideoDetail(curVideoIndex);
        }
    }

    class VideoData
    {
        public string filePath;

        public string fileName;

        public string coverName;

        public string coverCmd;

        public string tempCoverDir;

        public string tempClearWaterMarkDir;

        public string status;

        public Dictionary<Point, Point> docPoints = new Dictionary<Point, Point>();

        public List<WaterMarkData> waterMarkDatas = new List<WaterMarkData>();
    }

    class WaterMarkData
    {
        public int X = -1;
        public int Y = -1;
        public int W = -1;
        public int H = -1;
    }
}