namespace WechatToolApp.App
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.timerWeixinAppend = new System.Windows.Forms.Timer(this.components);
            this.buttonStartWeixin = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // timerWeixinAppend
            // 
            this.timerWeixinAppend.Interval = 500;
            this.timerWeixinAppend.Tick += new System.EventHandler(this.mutexHandleCloseTimer_Tick);
            // 
            // buttonStartWeixin
            // 
            this.buttonStartWeixin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonStartWeixin.Location = new System.Drawing.Point(0, 0);
            this.buttonStartWeixin.Name = "buttonStartWeixin";
            this.buttonStartWeixin.Size = new System.Drawing.Size(304, 141);
            this.buttonStartWeixin.TabIndex = 1000;
            this.buttonStartWeixin.Text = "启 动(0/0)";
            this.buttonStartWeixin.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 141);
            this.Controls.Add(this.buttonStartWeixin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "微信加开 wxjishu@吾爱破解论坛";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMultiInstance_Close);
            this.Load += new System.EventHandler(this.FormMultiInstance_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timerWeixinAppend;
        private System.Windows.Forms.Button buttonStartWeixin;
    }
}

