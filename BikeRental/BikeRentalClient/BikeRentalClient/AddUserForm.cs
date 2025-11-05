using BikeRentalClient.UserUtils;
using System.Net.Mail;

namespace BikeRentalClient
{
    public partial class AddUserForm : Form
    {
        private readonly UserService userService;

        TextBox firstnameTextBox = new() { Width = 220 };
        TextBox lastnameTextBox = new() { Width = 220 };
        TextBox emailTextBox = new() { Width = 220 };
        TextBox passwordTextBox = new() { Width = 220 };

        Button okButton = new() { Text = "Add", DialogResult = DialogResult.None, Height = 40, Width = 170 };
        Button cancelButton = new() { Text = "Cancel", DialogResult = DialogResult.Cancel, Height = 40, Width = 170 };

        public AddUserForm(UserService userService)
        {
            this.userService = userService;
            InitializeComponent();

            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false; MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            Width = 400;
            Height = 310;

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

            row("Firstname:", firstnameTextBox);
            row("Lastname:", lastnameTextBox);
            row("Email:", emailTextBox);
            row("Password:", passwordTextBox);

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
            var firstname = firstnameTextBox.Text.Trim();
            var lastname = lastnameTextBox.Text.Trim();
            var email = emailTextBox.Text.Trim();
            var password = passwordTextBox.Text;

            if (string.IsNullOrEmpty(firstname) || string.IsNullOrEmpty(lastname) ||
                string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var addr = new MailAddress(email);
                if (addr.Address != email)
                    throw new Exception();
            }
            catch
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var user = new User
            {
                Firstname = firstname,
                Lastname = lastname,
                Email = email,
                Password = password
            };
            try
            {
                var result = userService.AddUser(user);

                MessageBox.Show(result.Message,
                                result.Success ? "Success" : "Error",
                                MessageBoxButtons.OK,
                                result.Success ? MessageBoxIcon.Information : MessageBoxIcon.Error);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
