using BikeRentalClient.BikeUtils;
using BikeRentalClient.ManufacturerUtils;
using BikeRentalClient.UserUtils;
using static BikeRentalClient.BikeUtils.Bike;

namespace BikeRentalClient
{
    public partial class AddBikeForm : Form
    {
        private readonly BikeService bikeService;
        private readonly ManufacturerService manufacturerService;

        TextBox modelTextBox = new() { Width = 220 };
        NumericUpDown yearNumeric = new() { Minimum = 2010, Maximum = 2026, Value = 2025 };
        NumericUpDown priceNumeric = new() { Minimum = 10, Maximum = 200, DecimalPlaces = 2, Increment = 1, Width = 120 };
        ComboBox statusComboBox = new() { DropDownStyle = ComboBoxStyle.DropDownList, Width = 220 };
        ComboBox manufacturerComboBox = new() { DropDownStyle = ComboBoxStyle.DropDownList, Width = 220 };

        Button okButton = new() { Text = "Add", DialogResult = DialogResult.None, Height = 40, Width = 190 };
        Button cancelButton = new() { Text = "Cancel", DialogResult = DialogResult.Cancel, Height = 40, Width = 190 };

        public AddBikeForm(BikeService bikeService, ManufacturerService manufacturerService)
        {
            this.bikeService = bikeService;
            this.manufacturerService = manufacturerService;
            InitializeComponent();

            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false; MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            Width = 440;
            Height = 360;

            statusComboBox.Items.AddRange(Enum.GetNames(typeof(BikeStatus)));
            statusComboBox.SelectedIndex = 0;

            var layout = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(12), ColumnCount = 2 };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            void row(string label, Control ctl)
            {
                layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                layout.Controls.Add(new Label { Text = label, AutoSize = true, Anchor = AnchorStyles.Left, Padding = new Padding(0, 6, 0, 0) }, 0, layout.RowCount);
                layout.Controls.Add(ctl, 1, layout.RowCount);
                layout.RowCount++;
            }

            row("Manufacturer:", manufacturerComboBox);
            row("Model:", modelTextBox);
            row("Year:", yearNumeric);
            row("Price:", priceNumeric);
            row("Status:", statusComboBox);

            var buttons = new FlowLayoutPanel { FlowDirection = FlowDirection.RightToLeft, Dock = DockStyle.Bottom, Padding = new Padding(6), AutoSize = true };
            buttons.Controls.Add(okButton);
            buttons.Controls.Add(cancelButton);

            layout.AutoSize = true;
            layout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            layout.Dock = DockStyle.Top;
            Controls.Add(layout);
            Controls.Add(buttons);

            Load += AddBikeForm_Load;
            okButton.Click += OkButton_Click;
            AcceptButton = okButton;
            CancelButton = cancelButton;
        }
        private void AddBikeForm_Load(object? sender, EventArgs e)
        { 
            var manufacturers = manufacturerService.GetAllManufacturers(); // implement if not present
            manufacturerComboBox.DataSource = manufacturers;
            manufacturerComboBox.DisplayMember = "Name";
            manufacturerComboBox.ValueMember = "Manufacturer_id";
        }

        private void OkButton_Click(object? sender, EventArgs e)
        {
            if (manufacturerComboBox.SelectedValue == null || string.IsNullOrWhiteSpace(modelTextBox.Text))
            {
                MessageBox.Show("Please select a manufacturer and enter a model.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var bike = new Bike
            {
                Manufacturer_id = (int )manufacturerComboBox.SelectedValue,
                Model = modelTextBox.Text.Trim(),
                Year = (int) yearNumeric.Value,
                Price = (float) priceNumeric.Value,
                Status = Enum.Parse<BikeStatus>(statusComboBox.SelectedItem!.ToString()!)
            };

            try
            {
                var result = bikeService.AddBike(bike);

                MessageBox.Show(result.Message,
                                result.Success ? "Success" : "Error",
                                MessageBoxButtons.OK,
                                result.Success ? MessageBoxIcon.Information : MessageBoxIcon.Error);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
