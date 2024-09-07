namespace Com.Youlaiyouqu.WeixinAppend
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
            this.numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // timerWeixinAppend
            // 
            this.timerWeixinAppend.Interval = 500;
            this.timerWeixinAppend.Tick += new System.EventHandler(this.TryWeixinAppend);
            // 
            // buttonStartWeixin
            // 
            this.buttonStartWeixin.Font = new System.Drawing.Font("宋体", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonStartWeixin.Location = new System.Drawing.Point(137, 12);
            this.buttonStartWeixin.Name = "buttonStartWeixin";
            this.buttonStartWeixin.Size = new System.Drawing.Size(122, 117);
            this.buttonStartWeixin.TabIndex = 1000;
            this.buttonStartWeixin.Text = "启 动(0/0)";
            this.buttonStartWeixin.UseVisualStyleBackColor = true;
            this.buttonStartWeixin.Click += new System.EventHandler(this.buttonStartWeixin_Click);
            // 
            // numericUpDown
            // 
            this.numericUpDown.Font = new System.Drawing.Font("宋体", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.numericUpDown.Increment = new decimal(new int[] {
            3,
            0,
            0,
            65536});
            this.numericUpDown.Location = new System.Drawing.Point(12, 12);
            this.numericUpDown.Name = "numericUpDown";
            this.numericUpDown.Size = new System.Drawing.Size(117, 117);
            this.numericUpDown.TabIndex = 1001;
            this.numericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDown.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 146);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(247, 306);
            this.richTextBox1.TabIndex = 1002;
            this.richTextBox1.Text = "直接在文件名加启动数，如“微信+2.exe”，即可更改启动数\n一，启动数设为0，不会触发自动打开微信，需手动调整数字（滚轮）后点启动按钮。\n二，启动数设为1，软件" +
    "启动后自动打开一个微信客户端\n三，启动数设为2,3,...，软件启动后，自动打开指定个数的微信客户端，并重新排版窗口，使其相互不遮挡，如果当前已有打开的微信客户" +
    "端，则只会打开一个新的微信客户端";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(272, 146);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.numericUpDown);
            this.Controls.Add(this.buttonStartWeixin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "微信加开 wxjishu@吾爱";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMultiInstance_Close);
            this.Load += new System.EventHandler(this.FormMultiInstance_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timerWeixinAppend;
        private System.Windows.Forms.Button buttonStartWeixin;
        private System.Windows.Forms.NumericUpDown numericUpDown;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}

