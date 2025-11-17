using BikeRentalClient.ManufacturerUtils;
using BikeRentalClient.UserUtils;

namespace BikeRentalClient.BikeUtils
{
    public partial class BikePanel : UserControl
    {
        private readonly BikeService bikeService;
        private readonly UserService userService;
        private readonly ManufacturerService manufacturerService;
        private Dictionary<int, string> manufacturerById = new();
        private List<Bike> bikeList = new();
        private HashSet<int> userRentedBikeIdList = new();
        private bool settingUpPrices;
        private bool settingUpYears;
        private Label emptyLabel;

        private bool IsOwnedByLoggedUser(Bike b) =>
            userService.LoggedUser != null &&
            b.Status == Bike.BikeStatus.RENTED &&
            userRentedBikeIdList.Contains(b.Bike_id);

        public BikePanel(UserService userService, BikeService bikeService, ManufacturerService manufacturerService)
        {
            this.userService = userService;
            this.bikeService = bikeService;
            this.manufacturerService = manufacturerService;
            InitializeComponent();

            availabilityFilterComboBox.Items.Clear();
            availabilityFilterComboBox.Items.AddRange(new object[] { "All", "Available", "Rented", "Maintenance" });
            availabilityFilterComboBox.SelectedIndex = 0; // Default to "All"

            LoadManufacturers();
            LoadBikes();


            RefreshUserRentedList();

            SetPriceRange(bikeList);
            SetYearRange(bikeList);

            dataGridMain.AutoGenerateColumns = false;
            dataGridMain.Columns.Clear();

            dataGridMain.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Bike_id",
                HeaderText = "ID",
                Width = 60
            });

            dataGridMain.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Manufacturer",
                HeaderText = "Manufacturer",
                Width = 200
            });
            dataGridMain.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Model",
                HeaderText = "Model",
                Width = 250
            });
            dataGridMain.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Year",
                HeaderText = "Year",
                Width = 80
            });
            dataGridMain.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Price",
                HeaderText = "Price/h",
                DefaultCellStyle = { Format = "C2" },  // currency
                Width = 100
            });
            dataGridMain.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Status",
                HeaderText = "Status",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dataGridMain.Width = 885;
            dataGridMain.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridMain.Font, FontStyle.Bold);
            dataGridMain.ReadOnly = true;
            dataGridMain.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridMain.RowHeadersVisible = false;
            dataGridMain.AllowUserToAddRows = false;
            dataGridMain.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            dataGridMain.AdvancedColumnHeadersBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None; // disable default line
            dataGridMain.DataBindingComplete += (s, e) => UpdateRowStyles();

            dataGridMain.Paint += (s, e) =>
            {
                int headerBottom = dataGridMain.ColumnHeadersHeight - 1;

                using (Pen pen = new Pen(Color.Black, 3))
                {
                    e.Graphics.DrawLine(pen, 0, headerBottom, dataGridMain.Width, headerBottom);
                }
            };
            dataGridMain.DataSource = bikeList;
            WireFormatting();

            emptyLabel = new Label
            {
                Text = "No bikes found matching your filters.",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font(Font.FontFamily, 11, FontStyle.Italic),
                ForeColor = Color.Gray,
                Visible = false
            };
            dataGridMain.Controls.Add(emptyLabel);
            emptyLabel.BringToFront();
        }
        private void UpdateRowStyles()
        {
            foreach (DataGridViewRow row in dataGridMain.Rows)
            {
                if (row.DataBoundItem is not Bike b) continue;

                // reset
                row.DefaultCellStyle.BackColor = Color.Empty;
                row.DefaultCellStyle.ForeColor = Color.Empty;
                row.DefaultCellStyle.SelectionBackColor = Color.Empty;
                row.DefaultCellStyle.SelectionForeColor = Color.Empty;

                // highlight only if rented by the logged user
                if (IsOwnedByLoggedUser(b))
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(144, 238, 144);
                    row.DefaultCellStyle.ForeColor = Color.Black;
                    row.DefaultCellStyle.SelectionBackColor = Color.MediumSeaGreen;
                    row.DefaultCellStyle.SelectionForeColor = Color.White;
                }
            }

            dataGridMain.Invalidate(); // one repaint after bulk style change
        }

        private void LoadManufacturers()
        {
            var (okM, msgM, manufacturers) = manufacturerService.GetAllManufacturers();
            if (!okM)
            {
                MessageBox.Show(msgM, "Manufacturers error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                manufacturers = new List<Manufacturer>();
            }
            manufacturerById = manufacturers.ToDictionary(m => m.Manufacturer_id, m => m.Name);

            var allManufacturers = new List<Manufacturer>
            {
                new Manufacturer { Manufacturer_id = 0, Name = "All" }
            };
            allManufacturers.AddRange(manufacturers);

            manufacturerFilterComboBox.DataSource = allManufacturers;
            manufacturerFilterComboBox.DisplayMember = "Name";
            manufacturerFilterComboBox.ValueMember = "Manufacturer_id";
            manufacturerFilterComboBox.SelectedIndex = 0; // default to "All"
        }

        private void LoadBikes()
        {
            var (okB, msgB, bikes) = bikeService.GetAllBikes();
            if (!okB)
            {
                MessageBox.Show(msgB, "Bikes error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bikes = new List<Bike>();
            }
            bikeList = bikes;
        }

        private void WireFormatting()
        {
            dataGridMain.CellFormatting += (s, e) =>
            {
                if (dataGridMain.Columns[e.ColumnIndex].Name == "Manufacturer")
                {
                    if (dataGridMain.Rows[e.RowIndex].DataBoundItem is Bike b &&
                        manufacturerById.TryGetValue(b.Manufacturer_id, out var name))
                    {
                        e.Value = name;
                        e.FormattingApplied = true;
                    }
                }
                else if (dataGridMain.Columns[e.ColumnIndex].DataPropertyName == "Status")
                {
                    if (e.Value is Bike.BikeStatus st)
                    {
                        e.Value = st switch
                        {
                            Bike.BikeStatus.AVAILABLE => "Available",
                            Bike.BikeStatus.RENTED => "Rented",
                            Bike.BikeStatus.MAINTENANCE => "Maintenance",
                            _ => st.ToString()
                        };
                        e.FormattingApplied = true;
                    }
                }
            };
        }

        private void BikePanel_Load(object sender, EventArgs e)
        {
            if (dataGridMain.Rows.Count > 0)
                dataGridMain.Rows[0].Selected = true;
        }

        private void applyFiltersButton_Click(object sender, EventArgs e)
        {
            ApplyAllFilters();
        }

        private void ApplyAllFilters()
        {
            IEnumerable<Bike> filtered = bikeList;

            // --- 1️⃣ Availability filter ---
            switch (availabilityFilterComboBox.SelectedItem?.ToString())
            {
                case "Available":
                    filtered = filtered.Where(b => b.Status == Bike.BikeStatus.AVAILABLE);
                    break;
                case "Rented":
                    filtered = filtered.Where(b => b.Status == Bike.BikeStatus.RENTED);
                    break;
                case "Maintenance":
                    filtered = filtered.Where(b => b.Status == Bike.BikeStatus.MAINTENANCE);
                    break;
                    // “All” → no filter
            }

            if (manufacturerFilterComboBox.SelectedValue is int manuId && manuId != 0)
                filtered = filtered.Where(b => b.Manufacturer_id == manuId);

            int yearMin = (int)yearDownNumeric.Value;
            int yearMax = (int)yearUpNumeric.Value;
            filtered = filtered.Where(b => b.Year >= yearMin && b.Year <= yearMax);

            decimal priceMin = priceDownNumeric.Value;
            decimal priceMax = priceUpNumeric.Value;
            filtered = filtered.Where(b => (decimal)b.Price >= priceMin && (decimal)b.Price <= priceMax);

            var resultList = filtered.ToList();
            dataGridMain.DataSource = resultList;
            OnUserChanged();

            emptyLabel.Visible = resultList.Count == 0;
        }

        public void OnUserChanged()
        {
            RefreshUserRentedList();
            dataGridMain.Refresh();
            dataGridMain_SelectionChanged(this, EventArgs.Empty);
            UpdateRowStyles();
        }

        private void SetYearRange(List<Bike> bikes)
        {
            settingUpYears = true;
            try
            {
                if (bikes == null || bikes.Count == 0)
                {
                    yearDownNumeric.Minimum = 0;
                    yearDownNumeric.Maximum = 0;
                    yearUpNumeric.Minimum = 0;
                    yearUpNumeric.Maximum = 0;

                    yearDownNumeric.Value = 0;
                    yearUpNumeric.Value = 0;
                    return;
                }

                int minYear = bikes.Min(b => b.Year);
                int maxYear = bikes.Max(b => b.Year);
                if (minYear > maxYear) (minYear, maxYear) = (maxYear, minYear);


                yearDownNumeric.Minimum = minYear;
                yearDownNumeric.Maximum = maxYear;
                yearUpNumeric.Minimum = minYear;
                yearUpNumeric.Maximum = maxYear;

                yearDownNumeric.Value = minYear;
                yearUpNumeric.Value = maxYear;

                yearUpNumeric.Minimum = yearDownNumeric.Value;
                yearDownNumeric.Maximum = yearUpNumeric.Value;
            }
            finally
            {
                settingUpYears = false;
            }
        }

        private void SetPriceRange(List<Bike> bikes)
        {
            settingUpPrices = true;
            try
            {
                if (bikes == null || bikes.Count == 0)
                {
                    priceDownNumeric.Minimum = 0;
                    priceDownNumeric.Maximum = 0;
                    priceUpNumeric.Minimum = 0;
                    priceUpNumeric.Maximum = 0;
                    priceDownNumeric.Value = 0;
                    priceUpNumeric.Value = 0;
                    return;
                }

                float minPriceF = bikes.Min(b => b.Price);
                float maxPriceF = bikes.Max(b => b.Price);

                decimal minPrice = Convert.ToDecimal(minPriceF);
                decimal maxPrice = Convert.ToDecimal(maxPriceF);

                if (minPrice > maxPrice) (minPrice, maxPrice) = (maxPrice, minPrice);

                priceDownNumeric.DecimalPlaces = 2;
                priceUpNumeric.DecimalPlaces = 2;
                priceDownNumeric.Increment = 0.5m;
                priceUpNumeric.Increment = 0.5m;

                priceDownNumeric.Minimum = minPrice;
                priceDownNumeric.Maximum = maxPrice;
                priceUpNumeric.Minimum = minPrice;
                priceUpNumeric.Maximum = maxPrice;

                priceDownNumeric.Value = minPrice;
                priceUpNumeric.Value = maxPrice;

                priceUpNumeric.Minimum = priceDownNumeric.Value;
                priceDownNumeric.Maximum = priceUpNumeric.Value;
            }
            finally
            {
                settingUpPrices = false;
            }
        }


        private void yearDownNumeric_ValueChanged(object sender, EventArgs e)
        {
            if (settingUpYears) return;

            if (yearDownNumeric.Value > yearUpNumeric.Value)
                yearDownNumeric.Value = yearUpNumeric.Value;

            yearUpNumeric.Minimum = yearDownNumeric.Value;
        }

        private void yearUpNumeric_ValueChanged(object sender, EventArgs e)
        {
            if (settingUpYears) return;

            if (yearUpNumeric.Value < yearDownNumeric.Value)
                yearUpNumeric.Value = yearDownNumeric.Value;

            yearDownNumeric.Maximum = yearUpNumeric.Value;
        }

        private void priceDownNumeric_ValueChanged(object sender, EventArgs e)
        {
            if (settingUpPrices) return;

            if (priceDownNumeric.Value > priceUpNumeric.Value)
                priceDownNumeric.Value = priceUpNumeric.Value;

            priceUpNumeric.Minimum = priceDownNumeric.Value;
        }

        private void priceUpNumeric_ValueChanged(object sender, EventArgs e)
        {
            if (settingUpPrices) return;

            if (priceUpNumeric.Value < priceDownNumeric.Value)
                priceUpNumeric.Value = priceDownNumeric.Value;

            priceDownNumeric.Maximum = priceUpNumeric.Value;
        }

        private void dataGridMain_SelectionChanged(object sender, EventArgs e)
        {
            rentBikeButton.Enabled = false;
            rentBikeButton.Text = "Rent bike";

            if (dataGridMain.CurrentRow?.DataBoundItem is Bike b)
            {
                if (b.Status == Bike.BikeStatus.AVAILABLE)
                {
                    rentBikeButton.Enabled = true;
                    rentBikeButton.Text = "Rent bike";
                }
                else if (IsOwnedByLoggedUser(b))
                {
                    rentBikeButton.Enabled = true;
                    rentBikeButton.Text = "Return bike";
                }
            }
        }

        private void rentBikeButton_Click(object sender, EventArgs e)
        {
            if (dataGridMain.CurrentRow?.DataBoundItem is not Bike bike)
                return;

            if (IsOwnedByLoggedUser(bike))
            {
                // --- UNRENT FLOW ---
                try
                {
                    if (bike.RentDate == null)
                    {
                        MessageBox.Show("Cannot determine rental start time. Please contact support.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    TimeSpan duration = DateTime.UtcNow - bike.RentDate.Value;
                    if (duration.TotalMinutes < 0) duration = TimeSpan.Zero;

                    decimal hours = (decimal)(duration.TotalMinutes / 60.0);
                    decimal amount = Math.Round((decimal)bike.Price * hours, 2, MidpointRounding.AwayFromZero);

                    manufacturerById.TryGetValue(bike.Manufacturer_id, out var manufacturerName);
                    string label = $"{(manufacturerName ?? "Unknown")} {bike.Model}";

                    var confirm = MessageBox.Show(
                        $"Return \"{label}\"?\n\n" +
                        $"Rental time: {Math.Floor(duration.TotalHours)}h {duration.Minutes}m\n" +
                        $"Rate: {bike.Price:C2}/h\n" +
                        $"Amount owed: {amount:C2}\n\n" +
                        "Do you want to finish the rental now?",
                        "Finish rental",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2);

                    if (confirm != DialogResult.Yes) return;

                    bike.Status = Bike.BikeStatus.AVAILABLE;
                    bike.CurrentRenter_id = null;
                    bike.RentDate = null;

                    var (ok, msg) = bikeService.UpdateBike(bike);

                    if (!ok)
                    {
                        MessageBox.Show($"Failed to finish the rental.\n\n{msg}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    RefetchAndRebind(bike.Bike_id);
                    MessageBox.Show($"Rental finished.\nTotal to pay: {amount:C2}", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error while finishing rental: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return;
            }

            // --- RENT FLOW ---
            if (bike.Status != Bike.BikeStatus.AVAILABLE)
            {
                MessageBox.Show("This bike is not available.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (userService.LoggedUser == null)
            {
                MessageBox.Show("You need to be logged in in order to rent a bike!", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            manufacturerById.TryGetValue(bike.Manufacturer_id, out var manufacturerName2);
            string label2 = $"{(manufacturerName2 ?? "Unknown")} {bike.Model}";

            var confirmRent = MessageBox.Show($"Rent \"{label2}\" for {bike.Price:C2} per hour?",
                "Confirm rent",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);

            if (confirmRent != DialogResult.Yes) return;

            try
            {
                int renterId = userService.LoggedUser.User_id;

                bike.Status = Bike.BikeStatus.RENTED;
                bike.CurrentRenter_id = renterId;
                bike.RentDate = DateTime.UtcNow;

                var (ok, msg) = bikeService.UpdateBike(bike);

                if (!ok)
                {
                    MessageBox.Show($"Failed to rent a bike.\n\n{msg}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                RefetchAndRebind(bike.Bike_id);
                MessageBox.Show("Bike rented successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while renting: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void RefetchAndRebind(int selectedId)
        {
            LoadBikes();
            OnUserChanged();

            foreach (DataGridViewRow row in dataGridMain.Rows)
            {
                if (row.DataBoundItem is Bike b && b.Bike_id == selectedId)
                {
                    row.Selected = true;
                    dataGridMain.CurrentCell = row.Cells[0];
                    dataGridMain.FirstDisplayedScrollingRowIndex = row.Index;
                    return;
                }
            }
        }
        private void RefreshUserRentedList()
        {
            userRentedBikeIdList.Clear();

            if (userService.LoggedUser == null) return;

            var (_, _, list) = bikeService.GetRentedBikesByUser(userService.LoggedUser.User_id);
            if (list != null && list.Count > 0)
                userRentedBikeIdList = list.Select(b => b.Bike_id).ToHashSet();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            LoadBikes();
            OnUserChanged();
        }
    }
}
