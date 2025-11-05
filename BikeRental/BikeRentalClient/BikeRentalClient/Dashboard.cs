using BikeRentalClient.BikeUtils;
using BikeRentalClient.ManufacturerUtils;
using BikeRentalClient.UserUtils;
using System.Diagnostics;


namespace BikeRentalClient
{
    public partial class Dashboard : Form
    {
        private readonly BikeService bikeService;
        private readonly ManufacturerService manufacturerService;
        private readonly UserService userService;
        public Dashboard(BikeService bikeService, ManufacturerService manufacturerService, UserService userService)
        {
            InitializeComponent();
            this.userService = userService;
            this.manufacturerService = manufacturerService;
            this.bikeService = bikeService;

            bikeAvailabilityFilterComboBox.Items.Clear();
            bikeAvailabilityFilterComboBox.Items.AddRange(new object[] { "All", "Available", "Rented", "Maintenance" });
            bikeAvailabilityFilterComboBox.SelectedIndex = 0; // Default to "All"

        }
        private void Dashboard_Load(object sender, EventArgs e)
        {
            if (TablesList.Items.Count > 0)
                TablesList.SelectedIndex = 0; // selects "Bikes"

            if (dataGridMain.Rows.Count > 0)
                dataGridMain.Rows[0].Selected = true;
        }

        private void TablesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedTable = TablesList.SelectedItem?.ToString();
            dataGridMain.AutoGenerateColumns = true;

            if (string.Equals(selectedTable, "Bikes", StringComparison.OrdinalIgnoreCase))
            {
                dataGridMain.DataSource = bikeService.GetAllBikes();
                ToggleBikeFilterUI(true);
            }
            else
            {
                ToggleBikeFilterUI(false);
                if (selectedTable == "Manufacturers") { dataGridMain.DataSource = manufacturerService.GetAllManufacturers(); }
                else if (selectedTable == "Users") { dataGridMain.DataSource = userService.GetAllUsers(); }
            }
        }

        private void BikeAvailabilityFilterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Bike> result = new List<Bike>();
            switch (bikeAvailabilityFilterComboBox.SelectedItem?.ToString())
            {
                case "Available":
                    result = bikeService.GetFilteredBikes(Bike.BikeStatus.AVAILABLE);
                    break;
                case "Rented":
                    result = bikeService.GetFilteredBikes(Bike.BikeStatus.RENTED);
                    break;
                case "Maintenance":
                    result = bikeService.GetFilteredBikes(Bike.BikeStatus.MAINTENANCE);
                    break;
                default:
                    result = bikeService.GetAllBikes();
                    break;
            }

            dataGridMain.DataSource = result;
        }
        private void ToggleBikeFilterUI(bool show)
        {
            bikeAvailabilityFilterLabel.Visible = show;
            bikeAvailabilityFilterComboBox.Visible = show;
            showManufacturerButton.Visible = show;
            showRenterButton.Visible = show;
        }

        private void showRenterButton_Click(object sender, EventArgs e)
        {
            var bike = GetSelectedBike();
            if (bike == null || !bike.CurrentRenter_id.HasValue) return;

            var userId = bike.CurrentRenter_id.Value;

            var user = userService.GetUserById(userId);

            if (user == null)
            {
                MessageBox.Show("Current renter not found.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            MessageBox.Show(
                $"User #{user.User_id}\n" +
                $"Name: {user.Firstname} {user.Lastname}\n" +
                $"Email: {user.Email}",
                "Current Renter",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void showManufacturerButton_Click(object sender, EventArgs e)
        {
            var bike = GetSelectedBike();
            if (bike == null || bike.Manufacturer_id <= 0) return;

            var manufacturerId = bike.Manufacturer_id;
            var manufacturer = manufacturerService.GetManufacturerById(manufacturerId);

            if (manufacturer == null)
            {
                MessageBox.Show("Manufacturer not found.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            MessageBox.Show(
                $"Manufacturer #{manufacturer.Manufacturer_id}\n" +
                $"Name: {manufacturer.Name}\n" +
                $"Description: {manufacturer.Description}",
                "Manufacturer Info",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dataGridMain_SelectionChanged(object sender, EventArgs e)
        {
            var bike = GetSelectedBike();
            if (bike == null)
            {
                showManufacturerButton.Enabled = false;
                showRenterButton.Enabled = false;
                return;
            }

            showManufacturerButton.Enabled = bike.Manufacturer_id > 0;
            showRenterButton.Enabled = bike.CurrentRenter_id.HasValue;
        }
        private Bike? GetSelectedBike()
        {
            var row = dataGridMain.CurrentRow;
            return row?.DataBoundItem as Bike;
        }

        private void addBikeButton_Click(object sender, EventArgs e)
        {
            using var dlg = new AddBikeForm(bikeService, manufacturerService);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                // re-load bikes after a successful add
                
            }
        }

        private void addUserButton_Click(object sender, EventArgs e)
        {
            using var dlg = new AddUserForm(userService);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                // re-load bikes after a successful add

            }
        }

        private void addManufacturerButton_Click(object sender, EventArgs e)
        {
            using var dlg = new AddManufacturerForm(manufacturerService);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                // re-load bikes after a successful add

            }
        }
    }
}
