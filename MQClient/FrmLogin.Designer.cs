namespace MQClient
{
    partial class FrmLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            this.pnlTx = new CCWin.SkinControl.SkinPanel();
            this.pnlImgTx = new CCWin.SkinControl.SkinPanel();
            this.userName = new CCWin.SkinControl.SkinTextBox();
            this.password = new CCWin.SkinControl.SkinTextBox();
            this.rememberPassword = new CCWin.SkinControl.SkinCheckBox();
            this.btnLogin = new CCWin.SkinControl.SkinButton();
            this.pnlTx.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTx
            // 
            this.pnlTx.BackColor = System.Drawing.Color.Transparent;
            this.pnlTx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pnlTx.Controls.Add(this.pnlImgTx);
            this.pnlTx.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.pnlTx.DownBack = null;
            this.pnlTx.Location = new System.Drawing.Point(42, 195);
            this.pnlTx.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTx.MouseBack = null;
            this.pnlTx.Name = "pnlTx";
            this.pnlTx.NormlBack = null;
            this.pnlTx.Size = new System.Drawing.Size(82, 82);
            this.pnlTx.TabIndex = 13;
            // 
            // pnlImgTx
            // 
            this.pnlImgTx.BackColor = System.Drawing.Color.Transparent;
            this.pnlImgTx.BackgroundImage = global::MQClient.Properties.Resources._4;
            this.pnlImgTx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlImgTx.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.pnlImgTx.DownBack = null;
            this.pnlImgTx.Location = new System.Drawing.Point(1, 1);
            this.pnlImgTx.Margin = new System.Windows.Forms.Padding(0);
            this.pnlImgTx.MouseBack = null;
            this.pnlImgTx.Name = "pnlImgTx";
            this.pnlImgTx.NormlBack = null;
            this.pnlImgTx.Radius = 4;
            this.pnlImgTx.RoundStyle = CCWin.SkinClass.RoundStyle.All;
            this.pnlImgTx.Size = new System.Drawing.Size(80, 80);
            this.pnlImgTx.TabIndex = 12;
            // 
            // userName
            // 
            this.userName.BackColor = System.Drawing.Color.Transparent;
            this.userName.DownBack = null;
            this.userName.Icon = null;
            this.userName.IconIsButton = false;
            this.userName.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.userName.IsPasswordChat = '\0';
            this.userName.IsSystemPasswordChar = false;
            this.userName.Lines = new string[0];
            this.userName.Location = new System.Drawing.Point(133, 196);
            this.userName.Margin = new System.Windows.Forms.Padding(0);
            this.userName.MaxLength = 32767;
            this.userName.MinimumSize = new System.Drawing.Size(28, 28);
            this.userName.MouseBack = ((System.Drawing.Bitmap)(resources.GetObject("userName.MouseBack")));
            this.userName.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.userName.Multiline = true;
            this.userName.Name = "userName";
            this.userName.NormlBack = ((System.Drawing.Bitmap)(resources.GetObject("userName.NormlBack")));
            this.userName.Padding = new System.Windows.Forms.Padding(5);
            this.userName.ReadOnly = false;
            this.userName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.userName.Size = new System.Drawing.Size(194, 30);
            // 
            // 
            // 
            this.userName.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.userName.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userName.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.userName.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.userName.SkinTxt.Multiline = true;
            this.userName.SkinTxt.Name = "BaseText";
            this.userName.SkinTxt.Size = new System.Drawing.Size(184, 20);
            this.userName.SkinTxt.TabIndex = 0;
            this.userName.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.userName.SkinTxt.WaterText = "账号/手机/邮箱";
            this.userName.TabIndex = 39;
            this.userName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.userName.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.userName.WaterText = "账号/手机/邮箱";
            this.userName.WordWrap = true;
            // 
            // password
            // 
            this.password.BackColor = System.Drawing.Color.Transparent;
            this.password.DownBack = null;
            this.password.Icon = null;
            this.password.IconIsButton = true;
            this.password.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.password.IsPasswordChat = '●';
            this.password.IsSystemPasswordChar = false;
            this.password.Lines = new string[0];
            this.password.Location = new System.Drawing.Point(133, 226);
            this.password.Margin = new System.Windows.Forms.Padding(0);
            this.password.MaxLength = 32767;
            this.password.MinimumSize = new System.Drawing.Size(0, 28);
            this.password.MouseBack = ((System.Drawing.Bitmap)(resources.GetObject("password.MouseBack")));
            this.password.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.password.Multiline = true;
            this.password.Name = "password";
            this.password.NormlBack = ((System.Drawing.Bitmap)(resources.GetObject("password.NormlBack")));
            this.password.Padding = new System.Windows.Forms.Padding(5);
            this.password.ReadOnly = false;
            this.password.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.password.Size = new System.Drawing.Size(194, 30);
            // 
            // 
            // 
            this.password.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.password.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.password.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.password.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.password.SkinTxt.Multiline = true;
            this.password.SkinTxt.Name = "BaseText";
            this.password.SkinTxt.PasswordChar = '●';
            this.password.SkinTxt.Size = new System.Drawing.Size(184, 20);
            this.password.SkinTxt.TabIndex = 0;
            this.password.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.password.SkinTxt.WaterText = "密码";
            this.password.TabIndex = 40;
            this.password.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.password.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.password.WaterText = "密码";
            this.password.WordWrap = true;
            // 
            // rememberPassword
            // 
            this.rememberPassword.AutoSize = true;
            this.rememberPassword.BackColor = System.Drawing.Color.Transparent;
            this.rememberPassword.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.rememberPassword.DefaultCheckButtonWidth = 17;
            this.rememberPassword.DownBack = null;
            this.rememberPassword.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rememberPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(101)))), ((int)(((byte)(101)))));
            this.rememberPassword.LightEffect = false;
            this.rememberPassword.Location = new System.Drawing.Point(133, 256);
            this.rememberPassword.MouseBack = null;
            this.rememberPassword.Name = "rememberPassword";
            this.rememberPassword.NormlBack = null;
            this.rememberPassword.SelectedDownBack = null;
            this.rememberPassword.SelectedMouseBack = null;
            this.rememberPassword.SelectedNormlBack = null;
            this.rememberPassword.Size = new System.Drawing.Size(75, 21);
            this.rememberPassword.TabIndex = 1;
            this.rememberPassword.Text = "记住密码";
            this.rememberPassword.UseVisualStyleBackColor = false;
            // 
            // btnLogin
            // 
            this.btnLogin.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnLogin.BackColor = System.Drawing.Color.Transparent;
            this.btnLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnLogin.BackRectangle = new System.Drawing.Rectangle(50, 23, 50, 23);
            this.btnLogin.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(21)))), ((int)(((byte)(26)))));
            this.btnLogin.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.btnLogin.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnLogin.DownBack = ((System.Drawing.Image)(resources.GetObject("btnLogin.DownBack")));
            this.btnLogin.DrawType = CCWin.SkinControl.DrawStyle.Img;
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(133, 288);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(0);
            this.btnLogin.MouseBack = ((System.Drawing.Image)(resources.GetObject("btnLogin.MouseBack")));
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.NormlBack = ((System.Drawing.Image)(resources.GetObject("btnLogin.NormlBack")));
            this.btnLogin.Size = new System.Drawing.Size(194, 30);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.Text = "登  录";
            this.btnLogin.UseVisualStyleBackColor = false;
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(242)))), ((int)(((byte)(249)))));
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackShade = false;
            this.BackToColor = false;
            this.CanResize = false;
            this.CaptionHeight = 30;
            this.ClientSize = new System.Drawing.Size(430, 330);
            this.CloseBoxSize = new System.Drawing.Size(30, 30);
            this.CloseDownBack = ((System.Drawing.Image)(resources.GetObject("$this.CloseDownBack")));
            this.CloseMouseBack = ((System.Drawing.Image)(resources.GetObject("$this.CloseMouseBack")));
            this.CloseNormlBack = ((System.Drawing.Image)(resources.GetObject("$this.CloseNormlBack")));
            this.ControlBoxOffset = new System.Drawing.Point(0, 0);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.rememberPassword);
            this.Controls.Add(this.password);
            this.Controls.Add(this.userName);
            this.Controls.Add(this.pnlTx);
            this.DropBack = false;
            this.EffectCaption = CCWin.TitleType.None;
            this.MaxDownBack = ((System.Drawing.Image)(resources.GetObject("$this.MaxDownBack")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(430, 330);
            this.MaxMouseBack = ((System.Drawing.Image)(resources.GetObject("$this.MaxMouseBack")));
            this.MaxNormlBack = ((System.Drawing.Image)(resources.GetObject("$this.MaxNormlBack")));
            this.MaxSize = new System.Drawing.Size(30, 30);
            this.MiniDownBack = ((System.Drawing.Image)(resources.GetObject("$this.MiniDownBack")));
            this.MiniMouseBack = ((System.Drawing.Image)(resources.GetObject("$this.MiniMouseBack")));
            this.MinimumSize = new System.Drawing.Size(430, 330);
            this.MiniNormlBack = ((System.Drawing.Image)(resources.GetObject("$this.MiniNormlBack")));
            this.MiniSize = new System.Drawing.Size(30, 30);
            this.MobileApi = false;
            this.Name = "FrmLogin";
            this.Radius = 2;
            this.RestoreDownBack = ((System.Drawing.Image)(resources.GetObject("$this.RestoreDownBack")));
            this.RestoreMouseBack = ((System.Drawing.Image)(resources.GetObject("$this.RestoreMouseBack")));
            this.RestoreNormlBack = ((System.Drawing.Image)(resources.GetObject("$this.RestoreNormlBack")));
            this.ShadowWidth = 6;
            this.ShowDrawIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmLogin";
            this.TopMost = true;
            this.pnlTx.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CCWin.SkinControl.SkinPanel pnlTx;
        private CCWin.SkinControl.SkinPanel pnlImgTx;
        private CCWin.SkinControl.SkinTextBox userName;
        private CCWin.SkinControl.SkinTextBox password;
        private CCWin.SkinControl.SkinCheckBox rememberPassword;
        private CCWin.SkinControl.SkinButton btnLogin;

    }
}