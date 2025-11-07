namespace BikeRentalClient.UserUtils
{
    partial class LoginForm
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
            tabControl = new TabControl();
            loginTabPage = new TabPage();
            flowLayoutPanel1 = new FlowLayoutPanel();
            label1 = new Label();
            label2 = new Label();
            loginEmailTextBox = new TextBox();
            label3 = new Label();
            loginPasswordTextBox = new TextBox();
            loginButton = new Button();
            registerTabPage = new TabPage();
            flowLayoutPanel2 = new FlowLayoutPanel();
            label4 = new Label();
            label5 = new Label();
            registerFirstnameTextBox = new TextBox();
            label6 = new Label();
            registerLastnameTextBox = new TextBox();
            label7 = new Label();
            registerEmailTextBox = new TextBox();
            label8 = new Label();
            registerPasswordTextBox = new TextBox();
            registerButton = new Button();
            tabControl.SuspendLayout();
            loginTabPage.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            registerTabPage.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl
            // 
            tabControl.Controls.Add(loginTabPage);
            tabControl.Controls.Add(registerTabPage);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Location = new Point(0, 0);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(237, 299);
            tabControl.TabIndex = 0;
            // 
            // loginTabPage
            // 
            loginTabPage.Controls.Add(flowLayoutPanel1);
            loginTabPage.Location = new Point(4, 24);
            loginTabPage.Name = "loginTabPage";
            loginTabPage.Padding = new Padding(10);
            loginTabPage.Size = new Size(229, 285);
            loginTabPage.TabIndex = 0;
            loginTabPage.Text = "Login";
            loginTabPage.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(label1);
            flowLayoutPanel1.Controls.Add(label2);
            flowLayoutPanel1.Controls.Add(loginEmailTextBox);
            flowLayoutPanel1.Controls.Add(label3);
            flowLayoutPanel1.Controls.Add(loginPasswordTextBox);
            flowLayoutPanel1.Controls.Add(loginButton);
            flowLayoutPanel1.Dock = DockStyle.Left;
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Location = new Point(10, 10);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Padding = new Padding(2);
            flowLayoutPanel1.Size = new Size(211, 265);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Segoe UI", 14.1428576F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(5, 2);
            label1.Margin = new Padding(3, 0, 3, 10);
            label1.Name = "label1";
            label1.Size = new Size(200, 25);
            label1.TabIndex = 0;
            label1.Text = "Login";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(5, 37);
            label2.Name = "label2";
            label2.Size = new Size(36, 15);
            label2.TabIndex = 1;
            label2.Text = "Email";
            // 
            // loginEmailTextBox
            // 
            loginEmailTextBox.Location = new Point(5, 55);
            loginEmailTextBox.Margin = new Padding(3, 3, 3, 5);
            loginEmailTextBox.Name = "loginEmailTextBox";
            loginEmailTextBox.Size = new Size(200, 23);
            loginEmailTextBox.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(5, 83);
            label3.Name = "label3";
            label3.Size = new Size(57, 15);
            label3.TabIndex = 3;
            label3.Text = "Password";
            // 
            // loginPasswordTextBox
            // 
            loginPasswordTextBox.Location = new Point(5, 101);
            loginPasswordTextBox.Name = "loginPasswordTextBox";
            loginPasswordTextBox.PasswordChar = '*';
            loginPasswordTextBox.Size = new Size(200, 23);
            loginPasswordTextBox.TabIndex = 4;
            loginPasswordTextBox.UseSystemPasswordChar = true;
            // 
            // loginButton
            // 
            loginButton.Dock = DockStyle.Fill;
            loginButton.Location = new Point(5, 130);
            loginButton.Name = "loginButton";
            loginButton.Size = new Size(200, 23);
            loginButton.TabIndex = 1;
            loginButton.Text = "Login";
            loginButton.UseVisualStyleBackColor = true;
            loginButton.Click += loginButton_Click;
            // 
            // registerTabPage
            // 
            registerTabPage.Controls.Add(flowLayoutPanel2);
            registerTabPage.Location = new Point(4, 24);
            registerTabPage.Name = "registerTabPage";
            registerTabPage.Padding = new Padding(10);
            registerTabPage.Size = new Size(229, 271);
            registerTabPage.TabIndex = 1;
            registerTabPage.Text = "Register";
            registerTabPage.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.Controls.Add(label4);
            flowLayoutPanel2.Controls.Add(label5);
            flowLayoutPanel2.Controls.Add(registerFirstnameTextBox);
            flowLayoutPanel2.Controls.Add(label6);
            flowLayoutPanel2.Controls.Add(registerLastnameTextBox);
            flowLayoutPanel2.Controls.Add(label7);
            flowLayoutPanel2.Controls.Add(registerEmailTextBox);
            flowLayoutPanel2.Controls.Add(label8);
            flowLayoutPanel2.Controls.Add(registerPasswordTextBox);
            flowLayoutPanel2.Controls.Add(registerButton);
            flowLayoutPanel2.Dock = DockStyle.Left;
            flowLayoutPanel2.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel2.Location = new Point(10, 10);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Padding = new Padding(2);
            flowLayoutPanel2.Size = new Size(210, 251);
            flowLayoutPanel2.TabIndex = 1;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Dock = DockStyle.Fill;
            label4.Font = new Font("Segoe UI", 14.1428576F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(5, 2);
            label4.Margin = new Padding(3, 0, 3, 10);
            label4.Name = "label4";
            label4.Size = new Size(200, 25);
            label4.TabIndex = 0;
            label4.Text = "Register";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(5, 37);
            label5.Name = "label5";
            label5.Size = new Size(59, 15);
            label5.TabIndex = 1;
            label5.Text = "Firstname";
            // 
            // registerFirstnameTextBox
            // 
            registerFirstnameTextBox.Dock = DockStyle.Fill;
            registerFirstnameTextBox.Location = new Point(5, 55);
            registerFirstnameTextBox.Margin = new Padding(3, 3, 3, 5);
            registerFirstnameTextBox.Name = "registerFirstnameTextBox";
            registerFirstnameTextBox.Size = new Size(200, 23);
            registerFirstnameTextBox.TabIndex = 2;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(5, 83);
            label6.Name = "label6";
            label6.Size = new Size(58, 15);
            label6.TabIndex = 3;
            label6.Text = "Lastname";
            // 
            // registerLastnameTextBox
            // 
            registerLastnameTextBox.Location = new Point(5, 101);
            registerLastnameTextBox.Margin = new Padding(3, 3, 3, 5);
            registerLastnameTextBox.Name = "registerLastnameTextBox";
            registerLastnameTextBox.Size = new Size(200, 23);
            registerLastnameTextBox.TabIndex = 4;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(5, 129);
            label7.Name = "label7";
            label7.Size = new Size(36, 15);
            label7.TabIndex = 5;
            label7.Text = "Email";
            // 
            // registerEmailTextBox
            // 
            registerEmailTextBox.Location = new Point(5, 147);
            registerEmailTextBox.Margin = new Padding(3, 3, 3, 5);
            registerEmailTextBox.Name = "registerEmailTextBox";
            registerEmailTextBox.Size = new Size(200, 23);
            registerEmailTextBox.TabIndex = 6;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(5, 175);
            label8.Name = "label8";
            label8.Size = new Size(57, 15);
            label8.TabIndex = 7;
            label8.Text = "Password";
            // 
            // registerPasswordTextBox
            // 
            registerPasswordTextBox.Location = new Point(5, 193);
            registerPasswordTextBox.Name = "registerPasswordTextBox";
            registerPasswordTextBox.PasswordChar = '*';
            registerPasswordTextBox.Size = new Size(200, 23);
            registerPasswordTextBox.TabIndex = 8;
            registerPasswordTextBox.UseSystemPasswordChar = true;
            // 
            // registerButton
            // 
            registerButton.Dock = DockStyle.Fill;
            registerButton.Location = new Point(5, 222);
            registerButton.Name = "registerButton";
            registerButton.Size = new Size(200, 23);
            registerButton.TabIndex = 1;
            registerButton.Text = "Register";
            registerButton.UseVisualStyleBackColor = true;
            registerButton.Click += registerButton_Click;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(237, 299);
            Controls.Add(tabControl);
            Name = "LoginForm";
            Text = "Account";
            tabControl.ResumeLayout(false);
            loginTabPage.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            registerTabPage.ResumeLayout(false);
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl;
        private TabPage loginTabPage;
        private TabPage registerTabPage;
        private FlowLayoutPanel flowLayoutPanel1;
        private Label label1;
        private Label label2;
        private TextBox loginEmailTextBox;
        private Label label3;
        private TextBox loginPasswordTextBox;
        private Button loginButton;
        private FlowLayoutPanel flowLayoutPanel2;
        private Label label4;
        private Label label5;
        private TextBox registerFirstnameTextBox;
        private Label label6;
        private TextBox registerLastnameTextBox;
        private Button registerButton;
        private Label label7;
        private TextBox registerEmailTextBox;
        private Label label8;
        private TextBox registerPasswordTextBox;
    }
}