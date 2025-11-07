namespace BikeRentalClient.ManufacturerUtils
{
    public partial class ManufacturerPanel : UserControl
    {
        private readonly ManufacturerService manufacturerService;
        public ManufacturerPanel(ManufacturerService manufacturerService)
        {
            this.manufacturerService = manufacturerService;
            InitializeComponent();

            manufacturerDataGrid.AutoGenerateColumns = false;
            manufacturerDataGrid.Columns.Clear();

            manufacturerDataGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Manufacturer_id",
                HeaderText = "ID",
                Width = 60
            });

            manufacturerDataGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Name",
                HeaderText = "Name",
                Width = 200
            });
            manufacturerDataGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Description",
                HeaderText = "Description",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            
            manufacturerDataGrid.Width = 1130;
            manufacturerDataGrid.Height = 570;
            manufacturerDataGrid.ColumnHeadersDefaultCellStyle.Font = new Font(manufacturerDataGrid.Font, FontStyle.Bold);
            manufacturerDataGrid.ReadOnly = true;
            manufacturerDataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            manufacturerDataGrid.RowHeadersVisible = false;
            manufacturerDataGrid.AllowUserToAddRows = false;
            manufacturerDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            manufacturerDataGrid.AdvancedColumnHeadersBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None; // disable default line
          
            manufacturerDataGrid.Paint += (s, e) =>
            {
                int headerBottom = manufacturerDataGrid.ColumnHeadersHeight - 1;

                using (Pen pen = new Pen(Color.Black, 3))
                {
                    e.Graphics.DrawLine(pen, 0, headerBottom, manufacturerDataGrid.Width, headerBottom);
                }
            };

            var (ok, msg, manufacturers) = manufacturerService.GetAllManufacturers();

            if (!ok)
            {
                MessageBox.Show($"Failed to load manufacturers.\n\n{msg}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                manufacturers = new List<Manufacturer>();
            }
            manufacturerDataGrid.DataSource = manufacturers;
        }
    }
}
