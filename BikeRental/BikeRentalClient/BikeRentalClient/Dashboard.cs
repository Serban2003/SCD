using BikeRentalClient.BikeUtils;
using BikeRentalClient.ManufacturerUtils;
using BikeRentalClient.UserUtils;
using System.Windows.Forms;

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
        }
    }
}
