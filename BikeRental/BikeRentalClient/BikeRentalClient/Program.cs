using BikeRentalClient.UserUtils;
using BikeRentalClient.ManufacturerUtils;
using BikeRentalClient.BikeUtils;

namespace BikeRentalClient
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            var httpClient = new HttpClient();

            var bikeService = new BikeService(httpClient);
            var manufacturerService = new ManufacturerService(httpClient);
            var userService = new UserService(httpClient);

            bikeService.CreateConnection();
            manufacturerService.CreateConnection();
            userService.CreateConnection();

            Application.Run(new Dashboard(bikeService, manufacturerService, userService)); // TODO: pass the services to the form
        }
    }
}