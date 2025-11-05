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
            TablesList = new ListBox();
            dataGridMain = new DataGridView();
            bikeAvailabilityFilterComboBox = new ComboBox();
            bikeAvailabilityFilterLabel = new Label();
            showRenterButton = new Button();
            showManufacturerButton = new Button();
            actionsGroupBox = new GroupBox();
            addBikeButton = new Button();
            addManufacturerButton = new Button();
            addUserButton = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridMain).BeginInit();
            actionsGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // TablesList
            // 
            TablesList.FormattingEnabled = true;
            TablesList.ItemHeight = 30;
            TablesList.Items.AddRange(new object[] { "Bikes", "Manufacturers", "Users" });
            TablesList.Location = new Point(12, 12);
            TablesList.Margin = new Padding(10);
            TablesList.Name = "TablesList";
            TablesList.Size = new Size(239, 94);
            TablesList.TabIndex = 0;
            TablesList.SelectedIndexChanged += TablesList_SelectedIndexChanged;
            // 
            // dataGridMain
            // 
            dataGridMain.AllowUserToAddRows = false;
            dataGridMain.AllowUserToDeleteRows = false;
            dataGridMain.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridMain.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridMain.Location = new Point(264, 12);
            dataGridMain.Name = "dataGridMain";
            dataGridMain.ReadOnly = true;
            dataGridMain.RowHeadersWidth = 72;
            dataGridMain.Size = new Size(924, 651);
            dataGridMain.TabIndex = 1;
            dataGridMain.SelectionChanged += dataGridMain_SelectionChanged;
            // 
            // bikeAvailabilityFilterComboBox
            // 
            bikeAvailabilityFilterComboBox.FormattingEnabled = true;
            bikeAvailabilityFilterComboBox.Location = new Point(12, 171);
            bikeAvailabilityFilterComboBox.Name = "bikeAvailabilityFilterComboBox";
            bikeAvailabilityFilterComboBox.Size = new Size(239, 38);
            bikeAvailabilityFilterComboBox.TabIndex = 2;
            bikeAvailabilityFilterComboBox.SelectedIndexChanged += BikeAvailabilityFilterComboBox_SelectedIndexChanged;
            // 
            // bikeAvailabilityFilterLabel
            // 
            bikeAvailabilityFilterLabel.AutoSize = true;
            bikeAvailabilityFilterLabel.Location = new Point(12, 138);
            bikeAvailabilityFilterLabel.Name = "bikeAvailabilityFilterLabel";
            bikeAvailabilityFilterLabel.Size = new Size(118, 30);
            bikeAvailabilityFilterLabel.TabIndex = 3;
            bikeAvailabilityFilterLabel.Text = "Availability:";
            // 
            // showRenterButton
            // 
            showRenterButton.Location = new Point(12, 261);
            showRenterButton.Name = "showRenterButton";
            showRenterButton.Size = new Size(239, 40);
            showRenterButton.TabIndex = 5;
            showRenterButton.Text = "Show Renter Details";
            showRenterButton.UseVisualStyleBackColor = true;
            showRenterButton.Click += showRenterButton_Click;
            // 
            // showManufacturerButton
            // 
            showManufacturerButton.Location = new Point(12, 215);
            showManufacturerButton.Name = "showManufacturerButton";
            showManufacturerButton.Size = new Size(239, 40);
            showManufacturerButton.TabIndex = 4;
            showManufacturerButton.Text = "Show Manufacturer";
            showManufacturerButton.UseVisualStyleBackColor = true;
            showManufacturerButton.Click += showManufacturerButton_Click;
            // 
            // actionsGroupBox
            // 
            actionsGroupBox.Controls.Add(addBikeButton);
            actionsGroupBox.Controls.Add(addManufacturerButton);
            actionsGroupBox.Controls.Add(addUserButton);
            actionsGroupBox.Location = new Point(12, 486);
            actionsGroupBox.Name = "actionsGroupBox";
            actionsGroupBox.Size = new Size(239, 177);
            actionsGroupBox.TabIndex = 6;
            actionsGroupBox.TabStop = false;
            actionsGroupBox.Text = "Actions";
            // 
            // addBikeButton
            // 
            addBikeButton.Location = new Point(6, 34);
            addBikeButton.Name = "addBikeButton";
            addBikeButton.Size = new Size(227, 40);
            addBikeButton.TabIndex = 2;
            addBikeButton.Text = "Add Bike";
            addBikeButton.UseVisualStyleBackColor = true;
            addBikeButton.Click += addBikeButton_Click;
            // 
            // addManufacturerButton
            // 
            addManufacturerButton.Location = new Point(6, 80);
            addManufacturerButton.Name = "addManufacturerButton";
            addManufacturerButton.Size = new Size(227, 40);
            addManufacturerButton.TabIndex = 1;
            addManufacturerButton.Text = "Add Manufacturer";
            addManufacturerButton.UseVisualStyleBackColor = true;
            addManufacturerButton.Click += addManufacturerButton_Click;
            // 
            // addUserButton
            // 
            addUserButton.Location = new Point(6, 126);
            addUserButton.Name = "addUserButton";
            addUserButton.Size = new Size(227, 40);
            addUserButton.TabIndex = 0;
            addUserButton.Text = "Add User";
            addUserButton.UseVisualStyleBackColor = true;
            addUserButton.Click += addUserButton_Click;
            // 
            // Dashboard
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1200, 675);
            Controls.Add(actionsGroupBox);
            Controls.Add(showRenterButton);
            Controls.Add(showManufacturerButton);
            Controls.Add(bikeAvailabilityFilterLabel);
            Controls.Add(bikeAvailabilityFilterComboBox);
            Controls.Add(dataGridMain);
            Controls.Add(TablesList);
            Margin = new Padding(4);
            Name = "Dashboard";
            Text = "Dashboard";
            Load += Dashboard_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridMain).EndInit();
            actionsGroupBox.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox TablesList;
        private DataGridView dataGridMain;
        private ComboBox bikeAvailabilityFilterComboBox;
        private Label bikeAvailabilityFilterLabel;
        private Button showRenterButton;
        private Button showManufacturerButton;
        private GroupBox actionsGroupBox;
        private Button addBikeButton;
        private Button addManufacturerButton;
        private Button addUserButton;
    }
}
