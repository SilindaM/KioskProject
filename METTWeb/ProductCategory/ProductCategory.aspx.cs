using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MEWeb.ProductCategory
{
    public partial class ProductCategory : MEPageBase<ProductCategoryVM>
    {
    }
    public class ProductCategoryVM : MEStatelessViewModel<ProductCategoryVM>
    {
        public ProductCategoryVM()
        {

        }
        protected override void Setup()
        {
            base.Setup();
        }
    }
}

