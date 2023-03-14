using Singular.Web.MaintenanceHelpers;

namespace MEWeb.Maintenance
{
    /// <summary>
    /// The Maintenance custom page class
    /// </summary>
    public partial class Maintenance : MEPageBase<MaintenanceVM>
    {
    }

    /// <summary>
    /// The MaintenanceVM ViewModel class
    /// </summary>
    public class MaintenanceVM : StatelessMaintenanceVM
    {
        /// <summary>
        /// Setup the ViewModel
        /// </summary>
        protected override void Setup()
        {
            base.Setup();

            // Add Maintenance pages here.
            MainSection mainSection = AddMainSection("General");
            mainSection.AddMaintenancePage<MELib.Maintenance.MovieGenreList>("Movie Genres");
            mainSection.AddMaintenancePage<MELib.Movies.MovieList>("Movie List");
            mainSection.AddMaintenancePage<MELib.ProductCategory.ProductCategoryList>("Products Categories");
            mainSection.AddMaintenancePage<MELib.Products.ProductList>("Products List");
            mainSection.AddMaintenancePage<MELib.RO.OrderList>("Orders");
            mainSection.AddMaintenancePage<MELib.RO.TransactionList>("Transactions");
            // Add more lists here for maintaining, e.g. Status List, Years or lookup tables used in the project
        }
    }
}
