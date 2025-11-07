namespace BikeRentalClient.UserUtils
{
    public partial class LoginForm : Form
    {
        private readonly UserService userService;
        public User? LoggedInUser { get; private set; }
        public LoginForm(UserService userService)
        {
            this.userService = userService;
            InitializeComponent();

            tabControl.SelectedIndexChanged += (s, e) =>
            {
                AcceptButton = tabControl.SelectedTab.Text == "Register" ? registerButton : loginButton;
            };
            AcceptButton = loginButton; // start on Login
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            string email = loginEmailTextBox.Text.Trim();
            string password = loginPasswordTextBox.Text;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both email and password.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var (ok, msg, user) = userService.Login(email, password);
                if (!ok)
                {
                    MessageBox.Show(msg, "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                LoggedInUser = user!;
                DialogResult = DialogResult.OK;

                loginEmailTextBox.Clear();
                loginPasswordTextBox.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            string first = registerFirstnameTextBox.Text.Trim();
            string last = registerLastnameTextBox.Text.Trim();
            string email = registerEmailTextBox.Text.Trim();
            string password = registerPasswordTextBox.Text;

            if (string.IsNullOrWhiteSpace(first) ||
                string.IsNullOrWhiteSpace(last) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please fill in all fields.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var newUser = new User
                {
                    Firstname = first,
                    Lastname = last,
                    Email = email,
                    Password = password
                };

                var (ok, msg, createdUser) = userService.Register(newUser);
                if (!ok)
                {
                    MessageBox.Show(msg, "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show(msg, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoggedInUser = createdUser!;
                DialogResult = DialogResult.OK;

                registerFirstnameTextBox.Clear();
                registerLastnameTextBox.Clear();
                registerEmailTextBox.Clear();
                loginPasswordTextBox.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
