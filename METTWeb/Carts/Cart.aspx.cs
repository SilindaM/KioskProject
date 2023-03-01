using MELib.Carts;
using MELib.Products;
using Singular.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MEWeb.Carts
{
    public partial class Cart : MEPageBase<CartVM>
    {
    }
    public class CartVM : MEStatelessViewModel<CartVM>
    {
        public MELib.Carts.CartItemList CartItemList { get; set; }
        public MELib.Products.ProductList ProductList { get; set; }
        public MELib.Carts.CartList CartList { get; set; }
        public MELib.Carts.CartItem CartItem { get; set; }
        public MELib.Carts.Cart Cart { get; set; }
        public decimal TotalAmount { get; set; } 
        public int totalQuantity { get; set; } 
        public int ItemQuantity { get; set; }
        public int ProductID { get; set; }


        public int testRotal { get; set; } = 0;
        public CartVM()
        {

        }
        protected override void Setup()
        {
            base.Setup();
            
            CartList = MELib.Carts.CartList.GetCartByUserID(Singular.Security.Security.CurrentIdentity.UserID);

            var cartId = CartList.FirstOrDefault().CartID;
            ProductList = MELib.Products.ProductList.GetProductList();
            CartItemList = MELib.Carts.CartItemList.GetCartItemByCartId(cartId);
            TotalAmount = CartList.FirstOrDefault().TotalAmount;
     
          //  totalQuantity = CartList.FirstOrDefault().Quantity;
            //ItemQuantity = CartItemList.FirstOrDefault().Quantity;

        }
        //method to subtract qunatity in the database
        public static void ProductAddition(int ProductID, int productCount)
        {
            Result result = new Result();
            try
            {

                //Products
                MELib.Products.ProductList SaveProd = MELib.Products.ProductList.GetProductList(ProductID);
                //get quantity of specific product
                var stockQuantity = MELib.Products.ProductList.GetProductList().GetItem(ProductID);
                stockQuantity.ProductQuantity += productCount;
                stockQuantity.TrySave(typeof(MELib.Products.ProductList));
                SaveProd.Add(stockQuantity);
                result.Success = true;
            }
            catch (Exception e)
            {
                WebError.LogError(e, "Page: products.aspx | Method: FilterProductCategory", $"(int ProductCategoryId, ({ProductID})");
                result.Data = e.InnerException;
                result.ErrorText = "Could not filter products by category.";
                result.Success = false;
            }
        }
        //method to subtract quantity in the database
        public static void ProductSubtraction(int ProductID, int productCount)
        {
            Result result = new Result();
            try
            {

                //Products
                MELib.Products.ProductList SaveProd = MELib.Products.ProductList.GetProductList(ProductID);
                //get quantity of specific product
                var stockQuantity = MELib.Products.ProductList.GetProductList().GetItem(ProductID);
                stockQuantity.ProductQuantity -= productCount;
                stockQuantity.TrySave(typeof(MELib.Products.ProductList));
                SaveProd.Add(stockQuantity);
                result.Success = true;
            }
            catch (Exception e)
            {
                WebError.LogError(e, "Page: products.aspx | Method: FilterProductCategory", $"(int ProductCategoryId, ({ProductID})");
                result.Data = e.InnerException;
                result.ErrorText = "Could not filter products by category.";
                result.Success = false;
            }
        }
        // method to delete from cart

        [WebCallable]
        public static Result deleteCartItem(int ProductID, int productCount)
        {
            ProductID = 1;
            Result result = new Result();
            // get current loggedin user
            var currentuser = Singular.Security.Security.CurrentIdentity.UserID;

            //Products
           var SaveProd = MELib.Products.ProductList.GetProductList(ProductID);
         
            //get the cart of logged in user 
            var userCart = MELib.Carts.CartList.GetCartByID(currentuser).FirstOrDefault();

            //get the cart of the  current user
            var cartId = MELib.Carts.CartList.GetCartByUserID(Singular.Security.Security.CurrentIdentity.UserID).FirstOrDefault();

            // get the cart items
            var cartItems = MELib.Carts.CartItemList.GetCartItemByCartId(Convert.ToInt32(SaveProd)).FirstOrDefault();
            try
            {
                // return the items in the stock
                ProductAddition(Convert.ToInt32(cartItems.ProductId), Convert.ToInt32(cartItems.Quantity));
                //make userCartQuantity 0
                userCart.Quantity = 0;


                result.Success = true;
            }
            catch (Exception e)
            {
                WebError.LogError(e, "Page: products.aspx | Method: FilterProductCategory", $"(int ProductCategoryId, ({ProductID})");
                result.Data = e.InnerException;
                result.ErrorText = "Could not filter products by category.";
                result.Success = false;
            }
            return result;
        }

        // clear cart
        public static void clearUserCart(int userId)
        {
            Result result = new Result();
            try
            {

            }
            catch (Exception e)
            {
            }
        }

        // update cart
        
        public static Result UpdateCart(int ProductId, int productCount, CartItemList cartItemList, ProductList productlist)
        {
            var currentuser = Singular.Security.Security.CurrentIdentity.UserID;

            //Products
            var products = MELib.Products.ProductList.GetProductList(ProductId).FirstOrDefault();

            //get the cart of logged in user 
            var userCart = MELib.Carts.CartList.GetCartByID(currentuser).FirstOrDefault();

            //get the cart of the  current user
            var cartId = MELib.Carts.CartList.GetCartByUserID(Singular.Security.Security.CurrentIdentity.UserID).FirstOrDefault();

            // get the cart items
            var cartItems = MELib.Carts.CartItemList.GetCartItemByCartId(Convert.ToInt32(cartId.CartID)).FirstOrDefault();

            Result result = new Result();
            try
            {
                if (cartItems.Quantity > products.ProductQuantity)
                {
                    result.Success = false;
                    result.ErrorText = "Sorry only " + products.ProductQuantity.ToString() + " left In Stock";
                }
                else
                {

                    //get the new product count in the cart
                    var newQuantity = cartItems.Quantity - productCount;

                    // if the old cartItem product is greater than the new quantity,  then return the products to the stock, 
                    // also decrease cart Item Quantity
                    if (cartItems.Quantity > productCount)
                    {


                        // return the items in the stock
                        ProductAddition(Convert.ToInt32(cartItems.ProductId), Convert.ToInt32(cartItems.Quantity));
                        //decrease the cartitem 
                        cartItems.Quantity -= productCount;
                        //increase the amount of products to be returned
                        cartItems.Value = productCount * products.Price;
                        //decrease the cart quantity
                        cartId.Quantity -= cartItems.Quantity;
                        //decrease the price of the cart amount
                        cartItems.Value = productCount * products.Price;
                    }
                    // if the old cartItem product is less than the new quantity, the subtract the products from the stock, 
                    // also decrease cart Item Quantity
                    else if (cartItems.Quantity < productCount)
                    {


                        // subtract from the stock
                        ProductSubtraction(Convert.ToInt32(cartItems.ProductId), Convert.ToInt32(cartItems.Quantity));
                        //increament the cartItems
                        cartItems.Quantity += productCount;
                        //increase the amount of products to be returned
                        cartItems.Value = productCount * products.Price;
                        //increase the cart quantity
                        cartId.Quantity += cartItems.Quantity;
                        //increase the amount of products to be returned
                        cartItems.Value = productCount * products.Price;
                    }
                }
            }
            catch (Exception e)
            {
                result.ErrorText = "Failed To Update";
                result.Success = false;
            }
            return result;
        }
        
        public static Result ConfirmCartItems()
        {
            Result result = new Result();
            try
            {

            }
            catch (Exception e)
            {

            }
            return result;
        }
    }
}

