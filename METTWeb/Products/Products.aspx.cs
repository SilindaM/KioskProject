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
            CartList = MELib.Carts.CartList.GetCartList();
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

                // get data for specific product
                var products = MELib.Products.ProductList.GetProductList(ProductID).FirstOrDefault();
                // var amount = products

               // MELib.Products.Product  product = Product.GetItem(ProductID);
                MELib.Carts.Cart cart = MELib.Carts.Cart.NewCart();
                var cartExists = MELib.Carts.CartList.GetCartByID(currentuser).FirstOrDefault();

                //Check if the cart has quantity
                if (productCount<=0)
                {
                    result.ErrorText = "Please specify product Quantity to be able to add it to Basket ";
                }
                else
                {
                    // check if the product quantity is greater than quantity to be added in the cart
                    if(products.ProductQuantity >= productCount)
                    {
                        // check if the cart is not existing
                        if (cartExists != null)
                        {
                            cart.TotalAmount = productCount;
                            cart.ProductID = ProductID;
                            cart.IsActiveInd = true;
                            cart.UserID = Singular.Security.Security.CurrentIdentity.UserID;
                            cart.TotalAmount = productCount * products.Price;
                            cart.TrySave(typeof(MELib.Carts.CartList));
                            result.Success = true;
                        }
                        // if the cart exists update the cart
                        else
                        {
                            cartExists.UserID = cartExists.UserID;

                        }
                    }
                    else
                    {
                        result.ErrorText = "No Enough Quantity In Stock";
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

