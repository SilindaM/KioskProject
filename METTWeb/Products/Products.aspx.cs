using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Singular.Web;

namespace MEWeb.Products
{
    public partial class Products : MEPageBase<ProductsVM>
    {
    }
    public class ProductsVM : MEStatelessViewModel<ProductsVM>
    {
        public MELib.Products.ProductList ProductList { get; set; }
        public MELib.Carts.CartList CartList { get; set; }
        public MELib.Carts.Cart Cart { get; set; }
        public bool isActiveInd { get; set; }
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public int CartID { get; set; }

        public int Quantity { get; set; } = 0;


        /// <summary>
        /// Gets or sets the Movie Genre ID
        /// </summary>
        [Singular.DataAnnotations.DropDownWeb(typeof(MELib.RO.ROProductCategoryList), UnselectedText = "Select", ValueMember = "MovieGenreID", DisplayMember = "Genre")]
        [Display(Name = "ProductCategory")]
        public int? ProductCategoryId { get; set; }

        public ProductsVM()
        {

        }
        protected override void Setup()
        {
            base.Setup();

            ProductList = MELib.Products.ProductList.GetProductList();
        }
        
        [WebCallable]
        public Result FilterProducts(int ProductCategoryId)
        {
            Result sr = new Result();
            try
            {
                sr.Data = MELib.Products.ProductList.GetProductList(ProductCategoryId);
                sr.Success = true;
            }
            catch (Exception e)
            {
                WebError.LogError(e, "Page: products.aspx | Method: FilterProductCategory", $"(int ProductCategoryId, ({ProductCategoryId})");
                sr.Data = e.InnerException;
                sr.ErrorText = "Could not filter products by category.";
                sr.Success = false;
            }
            return sr;
        }
       public static Result AddToBasket(int ProductID,int productCount, ProductList productlist)
        {
            Result result = new Result();
            try
            {
                var currentuser = Singular.Security.Security.CurrentIdentity.UserID;
                MELib.Products.ProductList products = MELib.Products.ProductList.GetProductID(ProductID);
                MELib.Products.Product  product = products.GetItem(ProductID);
                MELib.Carts.Cart cart = MELib.Carts.Cart.NewCart();
                MELib.Carts.CartList cartExists = MELib.Carts.CartList.GetCartByID(currentuser);

                if (productCount<=0)
                {
                    result.ErrorText = "Please specify product Quantity to be able to add it to Basket ";
                }
                else
                {
                    // check if the cart is not existing
                    if(cartExists == null)
                    {

                    }
                    // if the cart exists
                    else
                    {

                    }
                }

            }
            catch (Exception e)
            {
                result.ErrorText = "Not enough in stock";
                result.Success = false;
            }
            return result;
        }
    }
}

