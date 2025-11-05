using BikeRentalClient.ManufacturerUtils;
using BikeRentalClient.UserUtils;

namespace BikeRentalClient
{
    public partial class AddManufacturerForm : Form
    {
        private readonly ManufacturerService manufacturerService;

        TextBox nameTextBox = new() { Width = 220 };
        TextBox descriptionTextBox = new() {
            Multiline = true,
            ScrollBars = ScrollBars.Vertical,
            WordWrap = true,
            Width = 250,
            Height = 140
        };

        Button okButton = new() { Text = "Add", DialogResult = DialogResult.None, Height = 40, Width = 190 };
        Button cancelButton = new() { Text = "Cancel", DialogResult = DialogResult.Cancel, Height = 40, Width = 190 };

        public AddManufacturerForm(ManufacturerService manufacturerService)
        {
            this.manufacturerService = manufacturerService;
            InitializeComponent();

            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false; MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            Width = 440;
            Height = 330;

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

            row("Name:", nameTextBox);
            row("Description:", descriptionTextBox);

            var buttons = new FlowLayoutPanel { FlowDirection = FlowDirection.RightToLeft, Dock = DockStyle.Bottom, Padding = new Padding(6), AutoSize = true };
            buttons.Controls.Add(okButton);
            buttons.Controls.Add(cancelButton);

            layout.AutoSize = true;
            layout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            layout.Dock = DockStyle.Top;
            Controls.Add(layout);
            Controls.Add(buttons);

            okButton.Click += OkButton_Click;
            AcceptButton = okButton;
            CancelButton = cancelButton;
        }

        private void OkButton_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nameTextBox.Text) || string.IsNullOrWhiteSpace(descriptionTextBox.Text))
            {
                MessageBox.Show("Please enter a name and a description.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var manufacturer = new Manufacturer
            {
                Name = nameTextBox.Text.Trim(),
                Description = descriptionTextBox.Text.Trim()
            };

            try
            {
                var result = manufacturerService.AddManufacturer(manufacturer);

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
