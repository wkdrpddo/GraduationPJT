namespace Viewer
{
    partial class Viewer
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
            this.ScreenView = new System.Windows.Forms.PictureBox();
            this.ON = new System.Windows.Forms.PictureBox();
            this.SendButton = new System.Windows.Forms.Button();
            this.TypeField = new System.Windows.Forms.TextBox();
            this.ChatField = new System.Windows.Forms.TextBox();
            this.ViewerList = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CntButton = new System.Windows.Forms.Button();
            this.OFF = new System.Windows.Forms.PictureBox();
            this.CapButton = new System.Windows.Forms.Button();
            this.WatingPage = new System.Windows.Forms.PictureBox();
            this.IPtext = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.ScreenView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ON)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OFF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WatingPage)).BeginInit();
            this.SuspendLayout();
            // 
            // ScreenView
            // 
            this.ScreenView.BackColor = System.Drawing.Color.White;
            this.ScreenView.Location = new System.Drawing.Point(20, 18);
            this.ScreenView.Name = "ScreenView";
            this.ScreenView.Size = new System.Drawing.Size(853, 531);
            this.ScreenView.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ScreenView.TabIndex = 0;
            this.ScreenView.TabStop = false;
            // 
            // ON
            // 
            this.ON.BackgroundImage = global::Viewer.Properties.Resources.ON;
            this.ON.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ON.Location = new System.Drawing.Point(886, 18);
            this.ON.Name = "ON";
            this.ON.Size = new System.Drawing.Size(119, 64);
            this.ON.TabIndex = 1;
            this.ON.TabStop = false;
            this.ON.Visible = false;
            // 
            // SendButton
            // 
            this.SendButton.Enabled = false;
            this.SendButton.Location = new System.Drawing.Point(1095, 519);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(69, 30);
            this.SendButton.TabIndex = 2;
            this.SendButton.Text = "전 송";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // TypeField
            // 
            this.TypeField.BackColor = System.Drawing.SystemColors.Control;
            this.TypeField.Location = new System.Drawing.Point(886, 519);
            this.TypeField.Multiline = true;
            this.TypeField.Name = "TypeField";
            this.TypeField.ReadOnly = true;
            this.TypeField.Size = new System.Drawing.Size(200, 30);
            this.TypeField.TabIndex = 3;
            this.TypeField.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TypeField_KeyPress);
            // 
            // ChatField
            // 
            this.ChatField.BackColor = System.Drawing.Color.White;
            this.ChatField.Location = new System.Drawing.Point(886, 175);
            this.ChatField.Multiline = true;
            this.ChatField.Name = "ChatField";
            this.ChatField.ReadOnly = true;
            this.ChatField.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ChatField.Size = new System.Drawing.Size(278, 300);
            this.ChatField.TabIndex = 2;
            // 
            // ViewerList
            // 
            this.ViewerList.BackColor = System.Drawing.Color.White;
            this.ViewerList.Location = new System.Drawing.Point(1023, 18);
            this.ViewerList.Multiline = true;
            this.ViewerList.Name = "ViewerList";
            this.ViewerList.ReadOnly = true;
            this.ViewerList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ViewerList.Size = new System.Drawing.Size(141, 128);
            this.ViewerList.TabIndex = 6;
            this.ViewerList.Text = "=== 현재 참여자 ===";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("나눔고딕", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(882, 149);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 23);
            this.label1.TabIndex = 6;
            this.label1.Text = "채팅";
            // 
            // CntButton
            // 
            this.CntButton.Location = new System.Drawing.Point(886, 115);
            this.CntButton.Name = "CntButton";
            this.CntButton.Size = new System.Drawing.Size(74, 31);
            this.CntButton.TabIndex = 7;
            this.CntButton.Text = "연 결";
            this.CntButton.UseVisualStyleBackColor = true;
            this.CntButton.Click += new System.EventHandler(this.CntButton_Click);
            // 
            // OFF
            // 
            this.OFF.BackgroundImage = global::Viewer.Properties.Resources.OFF;
            this.OFF.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.OFF.Location = new System.Drawing.Point(886, 18);
            this.OFF.Name = "OFF";
            this.OFF.Size = new System.Drawing.Size(119, 64);
            this.OFF.TabIndex = 8;
            this.OFF.TabStop = false;
            // 
            // CapButton
            // 
            this.CapButton.Location = new System.Drawing.Point(889, 487);
            this.CapButton.Name = "CapButton";
            this.CapButton.Size = new System.Drawing.Size(100, 25);
            this.CapButton.TabIndex = 9;
            this.CapButton.Text = "Capture";
            this.CapButton.UseVisualStyleBackColor = true;
            this.CapButton.Click += new System.EventHandler(this.CapButton_Click);
            // 
            // WatingPage
            // 
            this.WatingPage.BackColor = System.Drawing.Color.White;
            this.WatingPage.BackgroundImage = global::Viewer.Properties.Resources.wating;
            this.WatingPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.WatingPage.ErrorImage = null;
            this.WatingPage.Location = new System.Drawing.Point(20, 18);
            this.WatingPage.Name = "WatingPage";
            this.WatingPage.Size = new System.Drawing.Size(853, 531);
            this.WatingPage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.WatingPage.TabIndex = 0;
            this.WatingPage.TabStop = false;
            // 
            // IPtext
            // 
            this.IPtext.Location = new System.Drawing.Point(886, 88);
            this.IPtext.Name = "IPtext";
            this.IPtext.Size = new System.Drawing.Size(119, 21);
            this.IPtext.TabIndex = 10;
            this.IPtext.Text = "127.0.0.1";
            // 
            // Viewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 561);
            this.Controls.Add(this.IPtext);
            this.Controls.Add(this.CapButton);
            this.Controls.Add(this.OFF);
            this.Controls.Add(this.CntButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ViewerList);
            this.Controls.Add(this.ChatField);
            this.Controls.Add(this.TypeField);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.ON);
            this.Controls.Add(this.WatingPage);
            this.Controls.Add(this.ScreenView);
            this.Name = "Viewer";
            this.Text = "Class Viewer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Viewer_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.ScreenView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ON)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OFF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WatingPage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox ScreenView;
        private System.Windows.Forms.PictureBox ON;
        private System.Windows.Forms.Button SendButton;
        private System.Windows.Forms.TextBox TypeField;
        private System.Windows.Forms.TextBox ChatField;
        private System.Windows.Forms.TextBox ViewerList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button CntButton;
        private System.Windows.Forms.PictureBox OFF;
        private System.Windows.Forms.Button CapButton;
        private System.Windows.Forms.PictureBox WatingPage;
        private System.Windows.Forms.TextBox IPtext;
    }
}

