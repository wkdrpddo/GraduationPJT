namespace Broadcaster
{
    partial class Form1
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
            this.messageBox = new System.Windows.Forms.TextBox();
            this.ChatField = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SEND = new System.Windows.Forms.Button();
            this.ViewerList = new System.Windows.Forms.TextBox();
            this.CntButton = new System.Windows.Forms.Button();
            this.FileButton = new System.Windows.Forms.Button();
            this.OFF = new System.Windows.Forms.PictureBox();
            this.waitingPage = new System.Windows.Forms.PictureBox();
            this.ON = new System.Windows.Forms.PictureBox();
            this.Screen = new System.Windows.Forms.PictureBox();
            this.IPtext = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.OFF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.waitingPage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ON)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Screen)).BeginInit();
            this.SuspendLayout();
            // 
            // messageBox
            // 
            this.messageBox.Location = new System.Drawing.Point(886, 519);
            this.messageBox.Multiline = true;
            this.messageBox.Name = "messageBox";
            this.messageBox.ReadOnly = true;
            this.messageBox.Size = new System.Drawing.Size(200, 30);
            this.messageBox.TabIndex = 1;
            this.messageBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("나눔고딕", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(882, 149);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 23);
            this.label1.TabIndex = 3;
            this.label1.Text = "채팅";
            // 
            // SEND
            // 
            this.SEND.Enabled = false;
            this.SEND.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.SEND.Location = new System.Drawing.Point(1095, 519);
            this.SEND.Name = "SEND";
            this.SEND.Size = new System.Drawing.Size(69, 30);
            this.SEND.TabIndex = 4;
            this.SEND.Text = "전 송";
            this.SEND.UseVisualStyleBackColor = true;
            this.SEND.Click += new System.EventHandler(this.send_Click);
            // 
            // ViewerList
            // 
            this.ViewerList.BackColor = System.Drawing.Color.White;
            this.ViewerList.ForeColor = System.Drawing.SystemColors.WindowText;
            this.ViewerList.Location = new System.Drawing.Point(1023, 18);
            this.ViewerList.Multiline = true;
            this.ViewerList.Name = "ViewerList";
            this.ViewerList.ReadOnly = true;
            this.ViewerList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ViewerList.Size = new System.Drawing.Size(141, 128);
            this.ViewerList.TabIndex = 6;
            this.ViewerList.Text = "=== 현재 참여자 ===";
            // 
            // CntButton
            // 
            this.CntButton.Location = new System.Drawing.Point(886, 115);
            this.CntButton.Name = "CntButton";
            this.CntButton.Size = new System.Drawing.Size(74, 31);
            this.CntButton.TabIndex = 7;
            this.CntButton.Text = "START";
            this.CntButton.UseVisualStyleBackColor = true;
            this.CntButton.Click += new System.EventHandler(this.START_Click);
            // 
            // FileButton
            // 
            this.FileButton.Location = new System.Drawing.Point(889, 487);
            this.FileButton.Name = "FileButton";
            this.FileButton.Size = new System.Drawing.Size(100, 25);
            this.FileButton.TabIndex = 9;
            this.FileButton.Text = "파일 전송";
            this.FileButton.UseVisualStyleBackColor = true;
            this.FileButton.Click += new System.EventHandler(this.FileButton_Click);
            // 
            // OFF
            // 
            this.OFF.BackgroundImage = global::B_super.Properties.Resources.OFF;
            this.OFF.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.OFF.InitialImage = null;
            this.OFF.Location = new System.Drawing.Point(886, 18);
            this.OFF.Name = "OFF";
            this.OFF.Size = new System.Drawing.Size(119, 64);
            this.OFF.TabIndex = 11;
            this.OFF.TabStop = false;
            // 
            // waitingPage
            // 
            this.waitingPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.waitingPage.BackgroundImage = global::B_super.Properties.Resources.wating;
            this.waitingPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.waitingPage.Location = new System.Drawing.Point(20, 18);
            this.waitingPage.Name = "waitingPage";
            this.waitingPage.Size = new System.Drawing.Size(853, 531);
            this.waitingPage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.waitingPage.TabIndex = 10;
            this.waitingPage.TabStop = false;
            // 
            // ON
            // 
            this.ON.BackgroundImage = global::B_super.Properties.Resources.ON;
            this.ON.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ON.InitialImage = null;
            this.ON.Location = new System.Drawing.Point(886, 18);
            this.ON.Name = "ON";
            this.ON.Size = new System.Drawing.Size(119, 64);
            this.ON.TabIndex = 5;
            this.ON.TabStop = false;
            // 
            // Screen
            // 
            this.Screen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Screen.Location = new System.Drawing.Point(20, 18);
            this.Screen.Name = "Screen";
            this.Screen.Size = new System.Drawing.Size(853, 531);
            this.Screen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Screen.TabIndex = 0;
            this.Screen.TabStop = false;
            // 
            // IPtext
            // 
            this.IPtext.Location = new System.Drawing.Point(886, 88);
            this.IPtext.Name = "IPtext";
            this.IPtext.Size = new System.Drawing.Size(119, 21);
            this.IPtext.TabIndex = 12;
            this.IPtext.Text = "127.0.0.1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 561);
            this.Controls.Add(this.IPtext);
            this.Controls.Add(this.OFF);
            this.Controls.Add(this.waitingPage);
            this.Controls.Add(this.FileButton);
            this.Controls.Add(this.CntButton);
            this.Controls.Add(this.ViewerList);
            this.Controls.Add(this.ON);
            this.Controls.Add(this.SEND);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ChatField);
            this.Controls.Add(this.messageBox);
            this.Controls.Add(this.Screen);
            this.Name = "Form1";
            this.Text = "Class Assistance";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.OFF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.waitingPage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ON)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Screen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Screen;
        private System.Windows.Forms.TextBox messageBox;
        private System.Windows.Forms.TextBox ChatField;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button SEND;
        private System.Windows.Forms.PictureBox ON;
        private System.Windows.Forms.TextBox ViewerList;
        private System.Windows.Forms.Button CntButton;
        private System.Windows.Forms.Button FileButton;
        private System.Windows.Forms.PictureBox waitingPage;
        private System.Windows.Forms.PictureBox OFF;
        private System.Windows.Forms.TextBox IPtext;
    }
}

