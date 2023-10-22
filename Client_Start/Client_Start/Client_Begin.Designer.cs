namespace Client_Start
{
    partial class Client_Begin
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
            this.Broadcasting = new System.Windows.Forms.Button();
            this.Watching = new System.Windows.Forms.Button();
            this.Exit_B = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Broadcasting
            // 
            this.Broadcasting.Location = new System.Drawing.Point(43, 52);
            this.Broadcasting.Name = "Broadcasting";
            this.Broadcasting.Size = new System.Drawing.Size(75, 23);
            this.Broadcasting.TabIndex = 0;
            this.Broadcasting.Text = "송출하기";
            this.Broadcasting.UseVisualStyleBackColor = true;
            this.Broadcasting.Click += new System.EventHandler(this.Broadcasting_Click);
            // 
            // Watching
            // 
            this.Watching.Location = new System.Drawing.Point(168, 52);
            this.Watching.Name = "Watching";
            this.Watching.Size = new System.Drawing.Size(75, 23);
            this.Watching.TabIndex = 1;
            this.Watching.Text = "시청하기";
            this.Watching.UseVisualStyleBackColor = true;
            this.Watching.Click += new System.EventHandler(this.Watching_Click);
            // 
            // Exit_B
            // 
            this.Exit_B.Location = new System.Drawing.Point(107, 123);
            this.Exit_B.Name = "Exit_B";
            this.Exit_B.Size = new System.Drawing.Size(75, 23);
            this.Exit_B.TabIndex = 2;
            this.Exit_B.Text = "종료";
            this.Exit_B.UseVisualStyleBackColor = true;
            this.Exit_B.Click += new System.EventHandler(this.Exit_B_Click);
            // 
            // Client_Begin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.Exit_B);
            this.Controls.Add(this.Watching);
            this.Controls.Add(this.Broadcasting);
            this.Name = "Client_Begin";
            this.Text = "client_tools";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Broadcasting;
        private System.Windows.Forms.Button Watching;
        private System.Windows.Forms.Button Exit_B;
    }
}

