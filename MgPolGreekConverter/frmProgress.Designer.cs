namespace MgPolGreekConverter
{
    partial class frmProgress
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlButtons = new Panel();
            btnCancel = new Button();
            pnlProgress = new Panel();
            lblFileProgress = new Label();
            progressBar1 = new ProgressBar();
            progressBar2 = new ProgressBar();
            lblFilePart = new Label();
            pnlButtons.SuspendLayout();
            pnlProgress.SuspendLayout();
            SuspendLayout();
            // 
            // pnlButtons
            // 
            pnlButtons.Controls.Add(btnCancel);
            pnlButtons.Dock = DockStyle.Bottom;
            pnlButtons.Location = new Point(0, 151);
            pnlButtons.Name = "pnlButtons";
            pnlButtons.Size = new Size(800, 56);
            pnlButtons.TabIndex = 0;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.Location = new Point(565, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(232, 50);
            btnCancel.TabIndex = 0;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // pnlProgress
            // 
            pnlProgress.Controls.Add(lblFilePart);
            pnlProgress.Controls.Add(progressBar2);
            pnlProgress.Controls.Add(progressBar1);
            pnlProgress.Controls.Add(lblFileProgress);
            pnlProgress.Dock = DockStyle.Fill;
            pnlProgress.Location = new Point(0, 0);
            pnlProgress.Name = "pnlProgress";
            pnlProgress.Size = new Size(800, 151);
            pnlProgress.TabIndex = 1;
            // 
            // lblFileProgress
            // 
            lblFileProgress.AutoSize = true;
            lblFileProgress.Location = new Point(339, 9);
            lblFileProgress.Name = "lblFileProgress";
            lblFileProgress.Size = new Size(88, 25);
            lblFileProgress.TabIndex = 0;
            lblFileProgress.Text = "Files [0/0]";
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(74, 37);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(679, 34);
            progressBar1.TabIndex = 1;
            // 
            // progressBar2
            // 
            progressBar2.Location = new Point(74, 102);
            progressBar2.Name = "progressBar2";
            progressBar2.Size = new Size(679, 34);
            progressBar2.TabIndex = 2;
            // 
            // lblFilePart
            // 
            lblFilePart.AutoSize = true;
            lblFilePart.Location = new Point(339, 74);
            lblFilePart.Name = "lblFilePart";
            lblFilePart.Size = new Size(84, 25);
            lblFilePart.TabIndex = 3;
            lblFilePart.Text = "Part [0/0]";
            // 
            // frmProgress
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 207);
            ControlBox = false;
            Controls.Add(pnlProgress);
            Controls.Add(pnlButtons);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "frmProgress";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MgPolGreek Word Converter";
            Load += frmProgress_Load;
            pnlButtons.ResumeLayout(false);
            pnlProgress.ResumeLayout(false);
            pnlProgress.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlButtons;
        private Button btnCancel;
        private Panel pnlProgress;
        private Label lblFileProgress;
        private Label lblFilePart;
        private ProgressBar progressBar2;
        private ProgressBar progressBar1;
    }
}
