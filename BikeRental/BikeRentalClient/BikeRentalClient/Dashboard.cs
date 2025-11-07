using BikeRentalClient.BikeUtils;
using BikeRentalClient.ManufacturerUtils;
using BikeRentalClient.UserUtils;

namespace BikeRentalClient
{
    public partial class Dashboard : Form
    {
        private readonly BikeService bikeService;
        private readonly ManufacturerService manufacturerService;
        private readonly UserService userService;
        private BikePanel bikePanel;
        private ManufacturerPanel manufacturerPanel;
        public Dashboard(BikeService bikeService, ManufacturerService manufacturerService, UserService userService)
        {
            InitializeComponent();
            this.userService = userService;
            this.manufacturerService = manufacturerService;
            this.bikeService = bikeService;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            bikePanel = new BikePanel(userService, bikeService, manufacturerService) { Dock = DockStyle.Fill };
            manufacturerPanel = new ManufacturerPanel(manufacturerService) { Dock = DockStyle.Fill };
            UpdateAuthMenu();
        }

        private void UpdateAuthMenu()
        {
            bool signedIn = userService.LoggedUser != null;
            loginMenuItem.Visible = !signedIn;
            logoutMenuItem.Visible = signedIn;

            statusUserLabel.BackColor = ColorTranslator.FromHtml("#f7f7f7");
            statusUserLabel.Text = signedIn ? $"Signed in as {userService.LoggedUser!.Firstname} {userService.LoggedUser!.Lastname}" : "Not signed in";
        }
        private void menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == null)
                return;

            switch (e.ClickedItem.Name)
            {
                case "bikesMenuItem":
                    mainPanel.Controls.Clear();
                    mainPanel.Controls.Add(bikePanel);
                    break;

                case "manufacturersMenuItem":
                    mainPanel.Controls.Clear();
                    mainPanel.Controls.Add(manufacturerPanel);
                    break;

                case "loginMenuItem":
                    LoginForm loginForm = new LoginForm(userService);
                    loginForm = new LoginForm(userService);

                    if (loginForm.ShowDialog() != DialogResult.OK)
                        return; 
                    
                    userService.LoggedUser = loginForm.LoggedInUser!;
                    UpdateAuthMenu();
                    bikePanel?.OnUserChanged();
                    MessageBox.Show($"Welcome, {userService.LoggedUser.Firstname} {userService.LoggedUser.Lastname}!", "Login succesful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;

                case "logoutMenuItem":
                    userService.LoggedUser = null;
                    UpdateAuthMenu();
                    bikePanel?.OnUserChanged();
                    MessageBox.Show(
                        "You have been logged out.",
                        "Logout",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation
                    );
                    break;
            }
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(bikePanel);
        }
    }
}
