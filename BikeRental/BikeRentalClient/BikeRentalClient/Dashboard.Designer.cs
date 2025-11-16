namespace BikeRentalClient
{
    partial class Dashboard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dashboard));
            menuStrip = new MenuStrip();
            bikesMenuItem = new ToolStripMenuItem();
            manufacturersMenuItem = new ToolStripMenuItem();
            loginMenuItem = new ToolStripMenuItem();
            logoutMenuItem = new ToolStripMenuItem();
            statusUserLabel = new ToolStripTextBox();
            mainPanel = new Panel();
            menuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.AutoSize = false;
            menuStrip.ImageScalingSize = new Size(28, 28);
            menuStrip.Items.AddRange(new ToolStripItem[] { bikesMenuItem, manufacturersMenuItem, loginMenuItem, logoutMenuItem, statusUserLabel });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Padding = new Padding(4, 1, 0, 1);
            menuStrip.Size = new Size(1057, 26);
            menuStrip.TabIndex = 7;
            menuStrip.Text = "menuStrip1";
            menuStrip.ItemClicked += menuStrip_ItemClicked;
            // 
            // bikesMenuItem
            // 
            bikesMenuItem.Name = "bikesMenuItem";
            bikesMenuItem.Size = new Size(46, 24);
            bikesMenuItem.Text = "Bikes";
            // 
            // manufacturersMenuItem
            // 
            manufacturersMenuItem.Name = "manufacturersMenuItem";
            manufacturersMenuItem.Size = new Size(96, 24);
            manufacturersMenuItem.Text = "Manufacturers";
            // 
            // loginMenuItem
            // 
            loginMenuItem.Name = "loginMenuItem";
            loginMenuItem.Size = new Size(49, 24);
            loginMenuItem.Text = "Login";
            // 
            // logoutMenuItem
            // 
            logoutMenuItem.Name = "logoutMenuItem";
            logoutMenuItem.Size = new Size(57, 24);
            logoutMenuItem.Text = "Logout";
            // 
            // statusUserLabel
            // 
            statusUserLabel.Alignment = ToolStripItemAlignment.Right;
            statusUserLabel.BackColor = Color.White;
            statusUserLabel.BorderStyle = BorderStyle.None;
            statusUserLabel.Enabled = false;
            statusUserLabel.Margin = new Padding(1, 0, 15, 0);
            statusUserLabel.Name = "statusUserLabel";
            statusUserLabel.Overflow = ToolStripItemOverflow.Never;
            statusUserLabel.ReadOnly = true;
            statusUserLabel.Size = new Size(160, 24);
            statusUserLabel.Text = "User Status";
            statusUserLabel.TextBoxTextAlign = HorizontalAlignment.Right;
            // 
            // mainPanel
            // 
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.Location = new Point(0, 26);
            mainPanel.Margin = new Padding(2);
            mainPanel.Name = "mainPanel";
            mainPanel.Size = new Size(1057, 392);
            mainPanel.TabIndex = 8;
            // 
            // Dashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1057, 418);
            Controls.Add(mainPanel);
            Controls.Add(menuStrip);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip;
            Margin = new Padding(2);
            Name = "Dashboard";
            Text = "Dashboard";
            Load += Dashboard_Load;
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private MenuStrip menuStrip;
        private ToolStripMenuItem bikesMenuItem;
        private ToolStripMenuItem manufacturersMenuItem;
        private ToolStripMenuItem loginMenuItem;
        private Panel mainPanel;
        private ToolStripMenuItem logoutMenuItem;
        private ToolStripTextBox statusUserLabel;
    }
}
