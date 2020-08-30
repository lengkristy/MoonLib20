namespace MQClient
{
    partial class LoginForm
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.skinLabel1 = new CCWin.SkinControl.SkinLabel();
            this.StbUserName = new CCWin.SkinControl.SkinTextBox();
            this.slabelps = new CCWin.SkinControl.SkinLabel();
            this.StbPassword = new CCWin.SkinControl.SkinTextBox();
            this.SBtnLogin = new CCWin.SkinControl.SkinButton();
            this.SBtnCancel = new CCWin.SkinControl.SkinButton();
            this.SBtnSetting = new CCWin.SkinControl.SkinButton();
            this.SuspendLayout();
            // 
            // skinLabel1
            // 
            this.skinLabel1.AutoSize = true;
            this.skinLabel1.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel1.BorderColor = System.Drawing.Color.White;
            this.skinLabel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel1.Location = new System.Drawing.Point(31, 50);
            this.skinLabel1.Name = "skinLabel1";
            this.skinLabel1.Size = new System.Drawing.Size(44, 17);
            this.skinLabel1.TabIndex = 0;
            this.skinLabel1.Text = "账号：";
            // 
            // StbUserName
            // 
            this.StbUserName.BackColor = System.Drawing.Color.Transparent;
            this.StbUserName.DownBack = null;
            this.StbUserName.Icon = null;
            this.StbUserName.IconIsButton = false;
            this.StbUserName.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.StbUserName.IsPasswordChat = '\0';
            this.StbUserName.IsSystemPasswordChar = false;
            this.StbUserName.Lines = new string[0];
            this.StbUserName.Location = new System.Drawing.Point(81, 45);
            this.StbUserName.Margin = new System.Windows.Forms.Padding(0);
            this.StbUserName.MaxLength = 32767;
            this.StbUserName.MinimumSize = new System.Drawing.Size(28, 28);
            this.StbUserName.MouseBack = null;
            this.StbUserName.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.StbUserName.Multiline = false;
            this.StbUserName.Name = "StbUserName";
            this.StbUserName.NormlBack = null;
            this.StbUserName.Padding = new System.Windows.Forms.Padding(5);
            this.StbUserName.ReadOnly = false;
            this.StbUserName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.StbUserName.Size = new System.Drawing.Size(298, 28);
            // 
            // 
            // 
            this.StbUserName.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.StbUserName.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StbUserName.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.StbUserName.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.StbUserName.SkinTxt.Name = "BaseText";
            this.StbUserName.SkinTxt.Size = new System.Drawing.Size(288, 18);
            this.StbUserName.SkinTxt.TabIndex = 0;
            this.StbUserName.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.StbUserName.SkinTxt.WaterText = "";
            this.StbUserName.TabIndex = 1;
            this.StbUserName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.StbUserName.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.StbUserName.WaterText = "";
            this.StbUserName.WordWrap = true;
            // 
            // slabelps
            // 
            this.slabelps.AutoSize = true;
            this.slabelps.BackColor = System.Drawing.Color.Transparent;
            this.slabelps.BorderColor = System.Drawing.Color.White;
            this.slabelps.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.slabelps.Location = new System.Drawing.Point(31, 127);
            this.slabelps.Name = "slabelps";
            this.slabelps.Size = new System.Drawing.Size(44, 17);
            this.slabelps.TabIndex = 0;
            this.slabelps.Text = "密码：";
            // 
            // StbPassword
            // 
            this.StbPassword.BackColor = System.Drawing.Color.Transparent;
            this.StbPassword.DownBack = null;
            this.StbPassword.Icon = null;
            this.StbPassword.IconIsButton = false;
            this.StbPassword.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.StbPassword.IsPasswordChat = '\0';
            this.StbPassword.IsSystemPasswordChar = false;
            this.StbPassword.Lines = new string[0];
            this.StbPassword.Location = new System.Drawing.Point(81, 121);
            this.StbPassword.Margin = new System.Windows.Forms.Padding(0);
            this.StbPassword.MaxLength = 32767;
            this.StbPassword.MinimumSize = new System.Drawing.Size(28, 28);
            this.StbPassword.MouseBack = null;
            this.StbPassword.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.StbPassword.Multiline = false;
            this.StbPassword.Name = "StbPassword";
            this.StbPassword.NormlBack = null;
            this.StbPassword.Padding = new System.Windows.Forms.Padding(5);
            this.StbPassword.ReadOnly = false;
            this.StbPassword.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.StbPassword.Size = new System.Drawing.Size(298, 28);
            // 
            // 
            // 
            this.StbPassword.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.StbPassword.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StbPassword.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.StbPassword.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.StbPassword.SkinTxt.Name = "BaseText";
            this.StbPassword.SkinTxt.Size = new System.Drawing.Size(288, 18);
            this.StbPassword.SkinTxt.TabIndex = 0;
            this.StbPassword.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.StbPassword.SkinTxt.WaterText = "";
            this.StbPassword.TabIndex = 2;
            this.StbPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.StbPassword.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.StbPassword.WaterText = "";
            this.StbPassword.WordWrap = true;
            // 
            // SBtnLogin
            // 
            this.SBtnLogin.BackColor = System.Drawing.Color.Transparent;
            this.SBtnLogin.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.SBtnLogin.DownBack = null;
            this.SBtnLogin.Location = new System.Drawing.Point(88, 198);
            this.SBtnLogin.MouseBack = null;
            this.SBtnLogin.Name = "SBtnLogin";
            this.SBtnLogin.NormlBack = null;
            this.SBtnLogin.Size = new System.Drawing.Size(75, 23);
            this.SBtnLogin.TabIndex = 3;
            this.SBtnLogin.Text = "登陆";
            this.SBtnLogin.UseVisualStyleBackColor = false;
            this.SBtnLogin.Click += new System.EventHandler(this.SBtnLogin_Click);
            // 
            // SBtnCancel
            // 
            this.SBtnCancel.BackColor = System.Drawing.Color.Transparent;
            this.SBtnCancel.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.SBtnCancel.DownBack = null;
            this.SBtnCancel.Location = new System.Drawing.Point(213, 198);
            this.SBtnCancel.MouseBack = null;
            this.SBtnCancel.Name = "SBtnCancel";
            this.SBtnCancel.NormlBack = null;
            this.SBtnCancel.Size = new System.Drawing.Size(75, 23);
            this.SBtnCancel.TabIndex = 3;
            this.SBtnCancel.Text = "取消";
            this.SBtnCancel.UseVisualStyleBackColor = false;
            // 
            // SBtnSetting
            // 
            this.SBtnSetting.BackColor = System.Drawing.Color.Transparent;
            this.SBtnSetting.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.SBtnSetting.DownBack = null;
            this.SBtnSetting.Location = new System.Drawing.Point(370, 248);
            this.SBtnSetting.MouseBack = null;
            this.SBtnSetting.Name = "SBtnSetting";
            this.SBtnSetting.NormlBack = null;
            this.SBtnSetting.Size = new System.Drawing.Size(41, 23);
            this.SBtnSetting.TabIndex = 3;
            this.SBtnSetting.Text = "设置";
            this.SBtnSetting.UseVisualStyleBackColor = false;
            this.SBtnSetting.Click += new System.EventHandler(this.SBtnSetting_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 319);
            this.Controls.Add(this.SBtnSetting);
            this.Controls.Add(this.SBtnCancel);
            this.Controls.Add(this.SBtnLogin);
            this.Controls.Add(this.StbPassword);
            this.Controls.Add(this.StbUserName);
            this.Controls.Add(this.slabelps);
            this.Controls.Add(this.skinLabel1);
            this.MaximizeBox = false;
            this.Name = "LoginForm";
            this.Text = "登陆";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CCWin.SkinControl.SkinLabel skinLabel1;
        private CCWin.SkinControl.SkinTextBox StbUserName;
        private CCWin.SkinControl.SkinLabel slabelps;
        private CCWin.SkinControl.SkinTextBox StbPassword;
        private CCWin.SkinControl.SkinButton SBtnLogin;
        private CCWin.SkinControl.SkinButton SBtnCancel;
        private CCWin.SkinControl.SkinButton SBtnSetting;
    }
}

