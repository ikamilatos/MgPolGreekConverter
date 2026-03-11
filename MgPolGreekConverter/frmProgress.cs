namespace MgPolGreekConverter
{
    public partial class frmProgress : Form
    {
        private static Thread _thread;
        private System.Windows.Forms.Timer progressTimer;

        public frmProgress()
        {
            InitializeComponent();
        }

        private void frmProgress_Load(object sender, EventArgs e)
        {
            progressTimer = new System.Windows.Forms.Timer();
            progressTimer.Interval = 1000; // Update every 100 ms
            progressTimer.Tick += ProgressTimer_Tick;
            progressTimer.Start();

            _thread = new Thread(() =>
            {
                MgConverter.Instance.SetSourceFiles(Program.ConvFiles);
                MgConverter.Instance.ProcessDocs();
                // After processing, close the form
                Invoke(new Action(() =>
                {
                    progressTimer.Stop();
                    this.Close();
                }));
            });
            _thread.IsBackground = true;
            _thread.Start();
        }

        private void ProgressTimer_Tick(object? sender, EventArgs e)
        {
            if (_thread == null)
            {
                lblFileProgress.Text = "No files to process.";
                lblFileProgress.Visible = false;
                lblFilePart.Text = "";
                lblFilePart.Visible = false;
                progressBar1.Value = 0;
                progressBar2.Value = 0;
                progressBar1.Maximum = 0;
                progressBar2.Maximum = 0;
                return;
            }

            lblFileProgress.Text = $"Files [{MgConverter.Instance.FilesProcessed}/{MgConverter.Instance.FilesTotal}]";
            lblFilePart.Text =$"Part [{MgConverter.Instance.FileProgress}/{MgConverter.Instance.FileTotal}]";
            progressBar1.Maximum = MgConverter.Instance.FilesTotal;
            progressBar1.Value = MgConverter.Instance.FilesProcessed;
            progressBar2.Maximum = MgConverter.Instance.FileTotal;
            progressBar2.Value = MgConverter.Instance.FileProgress;
            lblFilePart.Refresh();
            lblFileProgress.Refresh();
            progressBar1.Refresh();
            progressBar2.Refresh();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if(_thread != null && _thread.IsAlive)
            {
                _thread.Abort();
                progressTimer.Stop();
                this.Close();
            }
        }
    }
}
