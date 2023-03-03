using MELib.Accounts;
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
        public MELib.Carts.CartList cartList { get; set; }
        public MELib.Carts.CartItem CartItem { get; set; }
        public MELib.Carts.Cart Cart { get; set; }
        public MELib.Orders.OrderList OrderList { get; set; }
        public decimal TotalAmount { get; set; }
        public int totalQuantity { get; set; }
        public int ItemQuantity { get; set; }
        public int ProductID { get; set; }
        public MELib.Accounts.AccountList UpdateAccount { get; set; }
        public MELib.Accounts.Account Deposit { get; set; }
        public int? AccountID { get; set; }


        public int testRotal { get; set; } = 0;
        public CartVM()
        {

        }
        protected override void Setup()
        {
            base.Setup();
            var cartById = MELib.Carts.CartList.GetCartByUserID(Singular.Security.Security.CurrentIdentity.UserID);

            var cartId = cartById.FirstOrDefault().CartID;
            ProductList = MELib.Products.ProductList.GetProductList();

            CartItemList = MELib.Carts.CartItemList.GetCartItemByCartId(cartId);
            TotalAmount = cartById.FirstOrDefault().TotalAmount;
            UpdateAccount = MELib.Accounts.AccountList.GetAccountList();
            Deposit = UpdateAccount.FirstOrDefault();
            cartList = MELib.Carts.CartList.GetCartList();


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
        
        public static Result DeleteCartItem(int CartItemID, int ProductId, int productCount, CartItemList cartItemList)
        {
            //cart
            var cartList = MELib.Carts.CartList.GetCartList();

            var currentuser = Singular.Security.Security.CurrentIdentity.UserID;

            //Products
            var products = MELib.Products.ProductList.GetProductList(ProductId).FirstOrDefault();

            //get the cart of logged in user 
            var userCart = MELib.Carts.CartList.GetCartByID(currentuser).FirstOrDefault();

            //get the cart of the  current user
            //var cartId = MELib.Carts.CartList.GetCartByUserID(Singular.Security.Security.CurrentIdentity.UserID).FirstOrDefault();

            // get the cart items
            var cartItems = MELib.Carts.CartItemList.GetCartItemByCartItemId(Convert.ToInt32(CartItemID)).FirstOrDefault();
            // get the cart items
            // var cartItems = cartItem;


            Result result = new Result();
            try
            {
                // return the items in the stock
                ProductAddition(Convert.ToInt32(cartItems.ProductId), Convert.ToInt32(cartItems.Quantity));
                // update quantity in the User's cart
                userCart.Quantity -= cartItems.Quantity;
                //update the total amount in the user's cart
                userCart.TotalAmount -= cartItems.Value;

                //update in the database of the user's cart
                cartList.Add(userCart);
                cartList.Save();


                //remove item from the db
                cartItemList.Remove(cartItems);
                cartItemList.Save();


                result.Success = true;
            }
            catch (Exception e)
            {
                WebError.LogError(e, "Page: products.aspx | Method: FilterProductCategory", $"(int ProductCategoryId, ({ProductId})");
                result.Data = e.InnerException;
                result.ErrorText = "Could not filter products by category.";
                result.Success = false;
            }
            return result;
        }

        // update cart

        public static Result UpdateCart(int CartItemID,int ProductId, int productCount, CartItemList cartItemList)
        {
            //cart
           var  cartList = MELib.Carts.CartList.GetCartList();

            var currentuser = Singular.Security.Security.CurrentIdentity.UserID;

            //Products
            var products = MELib.Products.ProductList.GetProductList(ProductId).FirstOrDefault();

            //get the cart of logged in user 
            var userCart = MELib.Carts.CartList.GetCartByID(currentuser).FirstOrDefault();

            //get the cart of the  current user
            //var cartId = MELib.Carts.CartList.GetCartByUserID(Singular.Security.Security.CurrentIdentity.UserID).FirstOrDefault();

            // get the cart items
            var cartItems = MELib.Carts.CartItemList.GetCartItemByCartItemId(Convert.ToInt32(CartItemID)).FirstOrDefault();
            // get the cart items
            // var cartItems = cartItem;

            Result result = new Result();
            try
            {
                //get cartQuantity to update
                var cartQuantity = cartItems.Quantity - productCount;

                if (cartItems.Quantity > products.ProductQuantity && cartQuantity<=cartItems.Quantity)
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
                        ProductAddition(Convert.ToInt32(cartItems.ProductId), newQuantity);
                        //decrease the cartitem 
                        cartItems.Quantity -= productCount;
                        //decrease the price of the cart amount
                        cartItems.Value -= productCount * products.Price;
                        //save the cart Item
                        cartItemList.Add(cartItems);
                        cartItemList.Save();


                        //decrease the cart quantity
                        userCart.Quantity -= cartItems.Quantity;
                        //decrease the cart total amount
                        userCart.TotalAmount -= productCount * products.Price;
                        //save the update in the users cart
                        cartList.Add(userCart);
                        cartList.Save();
                    }
                    // if the old cartItem product is less than the new quantity, the subtract the products from the stock, 
                    // also decrease cart Item Quantity
                    else if (cartItems.Quantity < productCount)
                    {


                        //get the new product count in the cart
                        var UpdateQuantity = productCount - cartItems.Quantity ;

                        // return the items in the stock
                            ProductSubtraction(Convert.ToInt32(cartItems.ProductId), UpdateQuantity);
                            //increase the cartitem 
                            cartItems.Quantity += UpdateQuantity;
                            //increase the price of the cart amount
                            cartItems.Value += UpdateQuantity * products.Price;
                            //save the cart Item
                            cartItemList.Add(cartItems);
                            cartItemList.Save();


                            //increase the cart quantity
                            userCart.Quantity += UpdateQuantity;
                            //increase the cart total amount
                            userCart.TotalAmount += UpdateQuantity * products.Price;
                            //save the update in the users cart
                            cartList.Add(userCart);
                            cartList.Save();
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
        //confirm the cart
        public static Result CompleteCart(CartItemList CartItemList)
        {
            //get the current user
            var currentuser = Singular.Security.Security.CurrentIdentity.UserID;
            
            //get the cart of logged in user 
            var userCart = MELib.Carts.CartList.GetCartByID(currentuser).FirstOrDefault();

            //create newOrderList
            MELib.Orders.OrderList newOrdersList = MELib.Orders.OrderList.NewOrderList();
            //create single order
            MELib.Orders.Order newOrder = MELib.Orders.Order.NewOrder();

            // get the cart items
            // var cartItems = MELib.Carts.CartItemList.GetCartItemByCartItemId(Convert.ToInt32(CartItemID)).FirstOrDefault();

            //cart total amount
            var TotalAmount = userCart.TotalAmount;

            Result result = new Result();
            try
            {
                // make subtraction from the user's account
                AmountDeduction(currentuser, TotalAmount);
                //update the cart
                userCart.TrySave(typeof(CartList));

                //Create New Order
                newOrder.UserID =currentuser;
                newOrder.OrderedDate = DateTime.Now;
                newOrder.CartID = userCart.CartID;
                newOrder.CompletedBy = currentuser;
                newOrder.OrderAmount = TotalAmount;
                //save to the ordersList
                newOrdersList.Add(newOrder);
                newOrdersList.Save();
            }
            catch (Exception e)
            {

            }
            return result;
        }
        // method to deduct amount
        [WebCallable]
        public static Result AmountDeduction(int userId, decimal Amount)
        {
            //get the current user
            var currentUser = MELib.Accounts.AccountList.GetAccountByID(userId).FirstOrDefault();
            //  currentUser.UserID = Singular.Security.Security.CurrentIdentity.UserID;

            Result result = new Result();
            try
            {
                //check if the amount to be deducted is greater than account balance
                if (currentUser.Balance >= Amount)
                {
                    currentUser.Balance -= Amount;
                    currentUser.TrySave(typeof(AccountList));
                }
                else
                {
                    result.ErrorText = "Insufficient Amount";
                }

            }
            catch (Exception e)
            {
                result.Data = e.InnerException;
                result.Success = false;

            }
            return result;
        }
        // method to deduct amount
        [WebCallable]
        public static Result AmountAddition(int userId, decimal Amount)
        {
            //get the current user
            var currentUser = MELib.Accounts.AccountList.GetAccountByID(userId).FirstOrDefault();
            //  currentUser.UserID = Singular.Security.Security.CurrentIdentity.UserID;

            Result result = new Result();
            try
            {
                    currentUser.Balance += Amount;
                    currentUser.TrySave(typeof(AccountList));

            }
            catch (Exception e)
            {
                result.Data = e.InnerException;
                result.Success = false;

            }
            return result;
        }


        //remove all items in cart
        public static Result ClearCart(CartItemList CartItemList)
        {
            
            //get the current user
            var currentuser = Singular.Security.Security.CurrentIdentity.UserID;

            //get the cart of logged in user 
            var userCart = MELib.Carts.CartList.GetCartByID(currentuser).FirstOrDefault();

            // get the cart items
            var cartItems = MELib.Carts.CartItemList.GetCartItemByCartItemId(Convert.ToInt32(userCart.CartID)).FirstOrDefault();

            //cart total amount
            var TotalAmount = userCart.TotalAmount;
            Result result = new Result();
            try
            {
                // loop through the itemlist
                foreach (var product in CartItemList)
                {
                    // return each product
                    ProductAddition(Convert.ToInt32(product.ProductId),product.Quantity);
                }
                // return the amount
                AmountAddition(currentuser, userCart.TotalAmount);
                // clear cart item
                CartItemList.Clear();
                //save the cart item, cartItem now is empty;
                CartItemList.Save();

                //update the cart
                userCart.TotalAmount = 0;
                userCart.Quantity = 0;
                userCart.TrySave(typeof(CartList));

            }
            catch(Exception e)
            {

            }
            return result;
        }
    }
}

