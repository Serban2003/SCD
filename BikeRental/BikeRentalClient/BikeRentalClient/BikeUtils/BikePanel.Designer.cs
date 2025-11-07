namespace BikeRentalClient.BikeUtils
{
    partial class BikePanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            yearGroupBox = new GroupBox();
            yearUpNumeric = new NumericUpDown();
            yearDownNumeric = new NumericUpDown();
            applyFiltersButton = new Button();
            dataGridMain = new DataGridView();
            manufacturerFilterComboBox = new ComboBox();
            groupBox1 = new GroupBox();
            priceUpNumeric = new NumericUpDown();
            priceDownNumeric = new NumericUpDown();
            groupBox2 = new GroupBox();
            groupBox3 = new GroupBox();
            flowLayoutPanel2 = new FlowLayoutPanel();
            groupBox4 = new GroupBox();
            availabilityFilterComboBox = new ComboBox();
            panel1 = new Panel();
            rentBikeButton = new Button();
            flowLayoutPanel1 = new FlowLayoutPanel();
            yearGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)yearUpNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)yearDownNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridMain).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)priceUpNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)priceDownNumeric).BeginInit();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            groupBox4.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // yearGroupBox
            // 
            yearGroupBox.AutoSize = true;
            yearGroupBox.Controls.Add(yearUpNumeric);
            yearGroupBox.Controls.Add(yearDownNumeric);
            yearGroupBox.Dock = DockStyle.Top;
            yearGroupBox.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            yearGroupBox.Location = new Point(0, 98);
            yearGroupBox.Margin = new Padding(0);
            yearGroupBox.Name = "yearGroupBox";
            yearGroupBox.Padding = new Padding(5);
            yearGroupBox.Size = new Size(120, 72);
            yearGroupBox.TabIndex = 23;
            yearGroupBox.TabStop = false;
            yearGroupBox.Text = "Year";
            // 
            // yearUpNumeric
            // 
            yearUpNumeric.AutoSize = true;
            yearUpNumeric.Dock = DockStyle.Top;
            yearUpNumeric.Location = new Point(5, 44);
            yearUpNumeric.Margin = new Padding(1, 0, 1, 0);
            yearUpNumeric.Name = "yearUpNumeric";
            yearUpNumeric.Size = new Size(110, 23);
            yearUpNumeric.TabIndex = 19;
            yearUpNumeric.ValueChanged += yearUpNumeric_ValueChanged;
            // 
            // yearDownNumeric
            // 
            yearDownNumeric.AutoSize = true;
            yearDownNumeric.Dock = DockStyle.Top;
            yearDownNumeric.Location = new Point(5, 21);
            yearDownNumeric.Margin = new Padding(1, 0, 1, 0);
            yearDownNumeric.Name = "yearDownNumeric";
            yearDownNumeric.Size = new Size(110, 23);
            yearDownNumeric.TabIndex = 18;
            yearDownNumeric.ValueChanged += yearDownNumeric_ValueChanged;
            // 
            // applyFiltersButton
            // 
            applyFiltersButton.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            applyFiltersButton.Location = new Point(0, 242);
            applyFiltersButton.Margin = new Padding(0);
            applyFiltersButton.Name = "applyFiltersButton";
            applyFiltersButton.Padding = new Padding(2, 0, 2, 0);
            applyFiltersButton.Size = new Size(120, 25);
            applyFiltersButton.TabIndex = 22;
            applyFiltersButton.Text = "Apply filters";
            applyFiltersButton.UseVisualStyleBackColor = true;
            applyFiltersButton.Click += applyFiltersButton_Click;
            // 
            // dataGridMain
            // 
            dataGridMain.AllowUserToAddRows = false;
            dataGridMain.AllowUserToDeleteRows = false;
            dataGridMain.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridMain.Dock = DockStyle.Left;
            dataGridMain.Location = new Point(154, 12);
            dataGridMain.Margin = new Padding(2);
            dataGridMain.Name = "dataGridMain";
            dataGridMain.ReadOnly = true;
            dataGridMain.RowHeadersWidth = 72;
            dataGridMain.Size = new Size(600, 326);
            dataGridMain.TabIndex = 10;
            dataGridMain.SelectionChanged += dataGridMain_SelectionChanged;
            // 
            // manufacturerFilterComboBox
            // 
            manufacturerFilterComboBox.Dock = DockStyle.Top;
            manufacturerFilterComboBox.FormattingEnabled = true;
            manufacturerFilterComboBox.Items.AddRange(new object[] { "All" });
            manufacturerFilterComboBox.Location = new Point(5, 21);
            manufacturerFilterComboBox.Margin = new Padding(2);
            manufacturerFilterComboBox.Name = "manufacturerFilterComboBox";
            manufacturerFilterComboBox.Size = new Size(110, 23);
            manufacturerFilterComboBox.TabIndex = 11;
            // 
            // groupBox1
            // 
            groupBox1.AutoSize = true;
            groupBox1.Controls.Add(priceUpNumeric);
            groupBox1.Controls.Add(priceDownNumeric);
            groupBox1.Dock = DockStyle.Top;
            groupBox1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupBox1.Location = new Point(0, 170);
            groupBox1.Margin = new Padding(0);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(5);
            groupBox1.Size = new Size(120, 72);
            groupBox1.TabIndex = 24;
            groupBox1.TabStop = false;
            groupBox1.Text = "Price";
            // 
            // priceUpNumeric
            // 
            priceUpNumeric.AutoSize = true;
            priceUpNumeric.Dock = DockStyle.Top;
            priceUpNumeric.Location = new Point(5, 44);
            priceUpNumeric.Margin = new Padding(1, 0, 1, 0);
            priceUpNumeric.Name = "priceUpNumeric";
            priceUpNumeric.Size = new Size(110, 23);
            priceUpNumeric.TabIndex = 19;
            priceUpNumeric.ValueChanged += priceUpNumeric_ValueChanged;
            // 
            // priceDownNumeric
            // 
            priceDownNumeric.Dock = DockStyle.Top;
            priceDownNumeric.Location = new Point(5, 21);
            priceDownNumeric.Margin = new Padding(1, 0, 1, 0);
            priceDownNumeric.Name = "priceDownNumeric";
            priceDownNumeric.Size = new Size(110, 23);
            priceDownNumeric.TabIndex = 18;
            priceDownNumeric.ValueChanged += priceDownNumeric_ValueChanged;
            // 
            // groupBox2
            // 
            groupBox2.AutoSize = true;
            groupBox2.Controls.Add(manufacturerFilterComboBox);
            groupBox2.Dock = DockStyle.Top;
            groupBox2.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupBox2.Location = new Point(0, 49);
            groupBox2.Margin = new Padding(0);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(5);
            groupBox2.Size = new Size(120, 49);
            groupBox2.TabIndex = 24;
            groupBox2.TabStop = false;
            groupBox2.Text = "Manufacturer";
            // 
            // groupBox3
            // 
            groupBox3.AutoSize = true;
            groupBox3.Controls.Add(flowLayoutPanel2);
            groupBox3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox3.Location = new Point(12, 12);
            groupBox3.Margin = new Padding(2, 2, 10, 2);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(5);
            groupBox3.Size = new Size(130, 326);
            groupBox3.TabIndex = 25;
            groupBox3.TabStop = false;
            groupBox3.Text = "Filters";
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.AutoSize = true;
            flowLayoutPanel2.Controls.Add(groupBox4);
            flowLayoutPanel2.Controls.Add(groupBox2);
            flowLayoutPanel2.Controls.Add(yearGroupBox);
            flowLayoutPanel2.Controls.Add(groupBox1);
            flowLayoutPanel2.Controls.Add(applyFiltersButton);
            flowLayoutPanel2.Controls.Add(panel1);
            flowLayoutPanel2.Controls.Add(rentBikeButton);
            flowLayoutPanel2.Dock = DockStyle.Fill;
            flowLayoutPanel2.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel2.Location = new Point(5, 21);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new Size(120, 300);
            flowLayoutPanel2.TabIndex = 12;
            // 
            // groupBox4
            // 
            groupBox4.AutoSize = true;
            groupBox4.Controls.Add(availabilityFilterComboBox);
            groupBox4.Dock = DockStyle.Top;
            groupBox4.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupBox4.Location = new Point(0, 0);
            groupBox4.Margin = new Padding(0);
            groupBox4.Name = "groupBox4";
            groupBox4.Padding = new Padding(5);
            groupBox4.Size = new Size(120, 49);
            groupBox4.TabIndex = 25;
            groupBox4.TabStop = false;
            groupBox4.Text = "Availability";
            // 
            // availabilityFilterComboBox
            // 
            availabilityFilterComboBox.Dock = DockStyle.Bottom;
            availabilityFilterComboBox.FormattingEnabled = true;
            availabilityFilterComboBox.Items.AddRange(new object[] { "All" });
            availabilityFilterComboBox.Location = new Point(5, 21);
            availabilityFilterComboBox.Margin = new Padding(2);
            availabilityFilterComboBox.Name = "availabilityFilterComboBox";
            availabilityFilterComboBox.Size = new Size(110, 23);
            availabilityFilterComboBox.TabIndex = 11;
            // 
            // panel1
            // 
            panel1.BackColor = Color.LightGray;
            panel1.Dock = DockStyle.Fill;
            panel1.ForeColor = SystemColors.ControlText;
            panel1.Location = new Point(3, 270);
            panel1.Name = "panel1";
            panel1.Size = new Size(114, 2);
            panel1.TabIndex = 27;
            // 
            // rentBikeButton
            // 
            rentBikeButton.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rentBikeButton.Location = new Point(0, 275);
            rentBikeButton.Margin = new Padding(0);
            rentBikeButton.Name = "rentBikeButton";
            rentBikeButton.Padding = new Padding(2, 0, 2, 0);
            rentBikeButton.Size = new Size(120, 25);
            rentBikeButton.TabIndex = 26;
            rentBikeButton.Text = "Rent bike";
            rentBikeButton.UseVisualStyleBackColor = true;
            rentBikeButton.Click += rentBikeButton_Click;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(groupBox3);
            flowLayoutPanel1.Controls.Add(dataGridMain);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.Location = new Point(0, 0);
            flowLayoutPanel1.Margin = new Padding(2);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Padding = new Padding(10);
            flowLayoutPanel1.Size = new Size(800, 500);
            flowLayoutPanel1.TabIndex = 26;
            // 
            // BikePanel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            Controls.Add(flowLayoutPanel1);
            Margin = new Padding(2);
            Name = "BikePanel";
            Size = new Size(800, 500);
            Load += BikePanel_Load;
            yearGroupBox.ResumeLayout(false);
            yearGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)yearUpNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)yearDownNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridMain).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)priceUpNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)priceDownNumeric).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            groupBox4.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private ComboBox manufacturerFilterComboBox;
        private DataGridView dataGridMain;
        private NumericUpDown yearUpNumeric;
        private NumericUpDown yearDownNumeric;
        private GroupBox yearGroupBox;
        private Button applyFiltersButton;
        private GroupBox groupBox2;
        private GroupBox groupBox1;
        private NumericUpDown priceUpNumeric;
        private NumericUpDown priceDownNumeric;
        private GroupBox groupBox3;
        private FlowLayoutPanel flowLayoutPanel1;
        private FlowLayoutPanel flowLayoutPanel2;
        private GroupBox groupBox4;
        private ComboBox availabilityFilterComboBox;
        private Button rentBikeButton;
        private Panel panel1;
    }
}
