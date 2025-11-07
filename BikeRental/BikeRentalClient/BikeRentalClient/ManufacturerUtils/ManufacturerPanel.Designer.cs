namespace BikeRentalClient.ManufacturerUtils
{
    partial class ManufacturerPanel
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
            flowLayoutPanel1 = new FlowLayoutPanel();
            manufacturerDataGrid = new DataGridView();
            flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)manufacturerDataGrid).BeginInit();
            SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(manufacturerDataGrid);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.Location = new Point(0, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Padding = new Padding(10);
            flowLayoutPanel1.Size = new Size(489, 335);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // manufacturerDataGrid
            // 
            manufacturerDataGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            manufacturerDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            manufacturerDataGrid.Location = new Point(13, 13);
            manufacturerDataGrid.Name = "manufacturerDataGrid";
            manufacturerDataGrid.Size = new Size(463, 308);
            manufacturerDataGrid.TabIndex = 0;
            // 
            // ManufacturerPanel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(flowLayoutPanel1);
            Name = "ManufacturerPanel";
            Size = new Size(489, 335);
            flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)manufacturerDataGrid).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel flowLayoutPanel1;
        private DataGridView manufacturerDataGrid;
    }
}
