using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MEWeb.Products
{
  public partial class TopProducts : MEPageBase<TopProductsVM>
  {
  }
  public class TopProductsVM : MEStatelessViewModel<TopProductsVM>
    {
        public MELib.Products.ProductList ProductList { get; set; }
        public int ProductID { get; set; }

        public String ProductName { get; set; }

        public TopProductsVM()
    {

    }
    protected override void Setup()
    {
      base.Setup();
      ProductList = MELib.Products.ProductList.GetProductList();
    }
  }
}

