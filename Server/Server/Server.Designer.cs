namespace Server
{
    partial class Server
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.start_bnt = new System.Windows.Forms.Button();
            this.end_btn = new System.Windows.Forms.Button();
            this.state_label = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.ScreenOP = new System.Windows.Forms.ToolStripStatusLabel();
            this.TextOP = new System.Windows.Forms.ToolStripStatusLabel();
            this.FileOP = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // start_bnt
            // 
            this.start_bnt.Font = new System.Drawing.Font("돋움", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.start_bnt.Location = new System.Drawing.Point(36, 79);
            this.start_bnt.Name = "start_bnt";
            this.start_bnt.Size = new System.Drawing.Size(75, 23);
            this.start_bnt.TabIndex = 0;
            this.start_bnt.Text = "Start";
            this.start_bnt.UseVisualStyleBackColor = true;
            this.start_bnt.Click += new System.EventHandler(this.start_bnt_Click);
            // 
            // end_btn
            // 
            this.end_btn.Enabled = false;
            this.end_btn.Font = new System.Drawing.Font("돋움", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.end_btn.Location = new System.Drawing.Point(162, 79);
            this.end_btn.Name = "end_btn";
            this.end_btn.Size = new System.Drawing.Size(75, 23);
            this.end_btn.TabIndex = 1;
            this.end_btn.Text = "End";
            this.end_btn.UseVisualStyleBackColor = true;
            this.end_btn.Click += new System.EventHandler(this.end_btn_Click);
            // 
            // state_label
            // 
            this.state_label.AutoSize = true;
            this.state_label.Font = new System.Drawing.Font("나눔고딕", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.state_label.Location = new System.Drawing.Point(74, 34);
            this.state_label.Name = "state_label";
            this.state_label.Size = new System.Drawing.Size(122, 23);
            this.state_label.TabIndex = 2;
            this.state_label.Text = "Server Close";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ScreenOP,
            this.TextOP,
            this.FileOP});
            this.statusStrip1.Location = new System.Drawing.Point(0, 117);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(284, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // ScreenOP
            // 
            this.ScreenOP.Name = "ScreenOP";
            this.ScreenOP.Size = new System.Drawing.Size(62, 17);
            this.ScreenOP.Text = "Prog_Start";
            // 
            // TextOP
            // 
            this.TextOP.Name = "TextOP";
            this.TextOP.Size = new System.Drawing.Size(62, 17);
            this.TextOP.Text = "Prog_Start";
            // 
            // FileOP
            // 
            this.FileOP.Name = "FileOP";
            this.FileOP.Size = new System.Drawing.Size(62, 17);
            this.FileOP.Text = "Prog_Start";
            // 
            // Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 139);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.state_label);
            this.Controls.Add(this.end_btn);
            this.Controls.Add(this.start_bnt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Server";
            this.Text = "Server";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button start_bnt;
        private System.Windows.Forms.Button end_btn;
        private System.Windows.Forms.Label state_label;
        private System.Windows.Forms.StatusStrip statusStrip1;
        public System.Windows.Forms.ToolStripStatusLabel ScreenOP;
        public System.Windows.Forms.ToolStripStatusLabel TextOP;
        public System.Windows.Forms.ToolStripStatusLabel FileOP;
    }
}

