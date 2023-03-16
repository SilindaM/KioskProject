using MELib.Accounts;
using MELib.Carts;
using MELib.Orders;
using MELib.Products;
using Singular.Web;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public MELib.Transactions.TransactionList TransactionList { get; set; }
        public MELib.Transactions.TransactionTypeList TransactionTypeList { get; set; }
        public MELib.Carts.CartList cartList { get; set; }
        public MELib.Orders.OrderTypeList orderTypes { get; set; }
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
        public MELib.RO.TransactionTypeList TransactionTypesList { get; set; }


        /// <summary>
        /// Gets or sets the Movie Genre ID
        /// </summary>
        [Singular.DataAnnotations.DropDownWeb(typeof(MELib.RO.ROOrderTypeList), UnselectedText = "Select", ValueMember = "OrderTypeId", DisplayMember = "OrderName")]
        [Display(Name = "OrderName")]
        public int OrderTypeId { get; set; }
        public CartVM()
        {

        }
        protected override void Setup()
        {
            base.Setup();
            orderTypes = MELib.Orders.OrderTypeList.GetOrderTypeList();
            var cartById = MELib.Carts.CartList.GetCartByUserID(Singular.Security.Security.CurrentIdentity.UserID);

            var cartId = cartById.FirstOrDefault().CartID;
            ProductList = MELib.Products.ProductList.GetProductList();

            CartItemList = MELib.Carts.CartItemList.GetCartItemByCartId(cartId);
            TotalAmount = cartById.FirstOrDefault().TotalAmount;
            UpdateAccount = MELib.Accounts.AccountList.GetAccountList();
            Deposit = UpdateAccount.FirstOrDefault();
            cartList = MELib.Carts.CartList.GetCartList();
            TransactionTypeList = MELib.Transactions.TransactionTypeList.GetTransactionTypeList();


            totalQuantity = cartById.FirstOrDefault().Quantity;
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

        public static Result UpdateCart(int CartItemID, int ProductId, int productCount, CartItemList cartItemList)
        {
            //cart
            var cartList = MELib.Carts.CartList.GetCartList();

            var currentuser = Singular.Security.Security.CurrentIdentity.UserID;
            //Products
            MELib.Products.ProductList SaveProd = MELib.Products.ProductList.GetProductList(ProductId);
            MELib.Products.Product products = SaveProd.GetItem(ProductId);

            //get the cart of logged in user 
            var userCart = MELib.Carts.CartList.GetCartByID(currentuser).FirstOrDefault();

            //get the cart of the  current user
            //var cartId = MELib.Carts.CartList.GetCartByUserID(Singular.Security.Security.CurrentIdentity.UserID).FirstOrDefault();


            // get the cart items
           // var cartItems = MELib.Carts.CartItemList.GetCartItemByCartItemId(Convert.ToInt32(CartItemID)).FirstOrDefault();
            var cartItem = MELib.Carts.CartItemList.GetCartItemByCartItemId(CartItemID);
            var cartItems = cartItem.FirstOrDefault();
            // get the cart items
            // var cartItems = cartItem;

            Result result = new Result();
            try
            {
                //get cartQuantity to update
                var oldCartQuantity = cartItems.Quantity + products.ProductQuantity;

                if (productCount > oldCartQuantity/*&& newCartQuantity <= products.ProductQuantity*/)
                // if (productCount > products.ProductQuantity)
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
                    if (cartItems.Quantity > productCount && productCount > 0)
                    {
                        // return the items in the stock
                        ProductAddition(Convert.ToInt32(cartItems.ProductId), newQuantity);
                        //decrease the cartitem 
                        cartItems.Quantity = productCount;
                        //decrease the price of the cart amount
                        cartItems.Value = productCount * products.Price;
                        //save the cart Item
                        cartItemList.Add(cartItems);
                        cartItemList.Save();


                        //decrease the cart quantity
                        userCart.Quantity -= newQuantity;
                        //decrease the cart total amount
                        userCart.TotalAmount -= newQuantity * products.Price;
                        //save the update in the users cart
                        cartList.Add(userCart);
                        cartList.Save();
                    }
                    // if the old cartItem product is less than the new quantity, the subtract the products from the stock, 
                    // also decrease cart Item Quantity
                    else if (cartItems.Quantity < productCount)
                    {


                        //get the new product count in the cart
                        var UpdateQuantity = productCount - cartItems.Quantity;

                        // return the items in the stock
                        ProductSubtraction(Convert.ToInt32(cartItems.ProductId), UpdateQuantity);
                        //increase the cartitem 
                        cartItems.Quantity = productCount;
                        //increase the price of the cart amount
                        cartItems.Value = productCount * products.Price;
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

                    // check if the product is not less than 0
                    else
                    {
                        result.Success = false;
                        result.ErrorText = "Sorry only " + products.ProductQuantity.ToString() + " cannot be 0 or less than";
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
        public static Result CompleteCart(CartItemList CartItemList, int orderTypeId)
        {
            //get the current user
            var currentuser = Singular.Security.Security.CurrentIdentity.UserID;

            //get the cart of logged in user 
            var userCart = MELib.Carts.CartList.GetCartByID(currentuser).FirstOrDefault();

            //create newOrderList
            MELib.Orders.OrderList newOrdersList = MELib.Orders.OrderList.NewOrderList();
            //create single order
            MELib.Orders.Order newOrder = MELib.Orders.Order.NewOrder();

            //create newOrderDetailsList
            MELib.Orders.OrderDetailList newOrderDetailsList = MELib.Orders.OrderDetailList.NewOrderDetailList();
            //create single order
            MELib.Orders.OrderDetail newOrderDetail = MELib.Orders.OrderDetail.NewOrderDetail();


            //new Transaction
            MELib.Transactions.Transaction newTransaction = MELib.Transactions.Transaction.NewTransaction();

            //new Transaction List
            MELib.Transactions.TransactionList transactions = MELib.Transactions.TransactionList.NewTransactionList();

            // 
            MELib.Accounts.AccountList userAccount = MELib.Accounts.AccountList.GetAccountList(currentuser);
            //get the account
            var account = userAccount.FirstOrDefault();

            //transaction type
            var transtype = MELib.Transactions.TransactionTypeList.GetTransactionTypeList().FirstOrDefault();

            // get the cart items
            // var cartItems = MELib.Carts.CartItemList.GetCartItemByCartItemId(Convert.ToInt32(userCart)).ToList();

            // get the order
            MELib.Orders.OrderTypeList orderTypes = MELib.Orders.OrderTypeList.GetOrderTypeById(orderTypeId);

            //transaction types
            var TransactionTypesList = MELib.RO.TransactionTypeList.GetTransactionTypeList().ToList();


            var orderType = orderTypes.FirstOrDefault();

            //cart total amount
            var TotalAmount = userCart.TotalAmount;



            Result result = new Result();
            try
            {
                // check if the orderType its collection or delivery
                if (orderTypeId == 1)
                {
                    TotalAmount = userCart.TotalAmount + orderType.amount;
                }
                // make subtraction from the user's account
                //check if user has sufficient amount, and not a delivery
                if (TotalAmount <= account.Balance && (account.Balance != 0))
                {
                    AmountDeduction(currentuser, TotalAmount);
                    //update the cart
                    userCart.TrySave(typeof(CartList));

                    //Create New Order
                    newOrder.UserID = currentuser;
                    newOrder.OrderedDate = DateTime.Now;
                    newOrder.CartID = userCart.CartID;
                    newOrder.CompletedBy = currentuser;
                    newOrder.OrderTypeId = orderTypeId;

                    // create new transaction
                    newTransaction.TransactionTypeID = 3;
                    newTransaction.UserID = currentuser;
                    newTransaction.CurrentBalance = account.Balance;
                    newTransaction.Description = TransactionTypesList.Select(t => t.TransactionName).LastOrDefault();


                    // check if order Type is Collection or delivery
                    // add the delivery fee to the total Amount
                    if (orderTypeId == 1)
                    {
                        //orderAmount
                        var orderAmount = TotalAmount;

                        newOrder.OrderAmount = orderAmount;
                        newTransaction.Amount = orderAmount;
                        newTransaction.NewBalance = account.Balance - orderAmount;
                    }
                    //if is collection don't add delivery fee
                    else
                    {
                        newOrder.OrderAmount = userCart.TotalAmount;
                        newTransaction.Amount = userCart.TotalAmount;
                        newTransaction.NewBalance = account.Balance - userCart.TotalAmount;
                    }
                    //save transaction
                    transactions.Add(newTransaction);
                    transactions.Save();

                    // newOrder.OrderTypeId =
                    //save to the ordersList
                    newOrdersList.Add(newOrder);
                    newOrdersList.Save();
                    MELib.Orders.Order OrdersList = MELib.Orders.OrderList.GetOrderList().LastOrDefault();
                    var orderid = OrdersList.OrderID;
                    //create order details
                    //get cartItem of currentUser

                    // var cartItemList = MELib.Carts.CartItemList.GetCartItemByCartItemId(userCart).FirstOrDefault();

                    foreach (var od in CartItemList)
                    {
                        newOrderDetail.OrderId = orderid;
                        newOrderDetail.ProductDescription = od.ProductDescription;
                        newOrderDetail.ProductImage = od.ProductImage;
                        newOrderDetail.ProductName = od.ProductName;
                        newOrderDetail.Price = od.Price;
                        newOrderDetail.Quantity = od.Quantity;
                        newOrderDetail.Value = od.Value;
                        newOrderDetail.ProductId = od.ProductId;
                        newOrderDetail.DateCreated = DateTime.Now;


                        //// make the isActive false
                        // CartItemList.ToList().ForEach(x => x.IsActiveInd = false);
                        // CartItemList.Add(od);

                        od.IsActiveInd = false;
                        //CartItemList.Save();
                        //save the order Details
                        newOrderDetailsList.Add(newOrderDetail);
                        newOrderDetailsList.Save();
                        CartItemList.Save();
                    }
                    

                    //update the cart
                    userCart.TotalAmount = 0;
                    userCart.Quantity = 0;
                    userCart.TrySave(typeof(CartList));
                }
                else
                {
                    result.ErrorText = "Insufficient Amount";
                }
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
            var currentUser = MELib.Accounts.AccountList.GetAccountList(userId).FirstOrDefault();
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
            var currentUser = MELib.Accounts.AccountList.GetAccountList(userId).FirstOrDefault();
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
                    ProductAddition(Convert.ToInt32(product.ProductId), product.Quantity);
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
            catch (Exception e)
            {

            }
            return result;
        }

        //// get orderType 
        //[WebCallable]
        //public Result OrderType(int OrderTypeId, int ResetInd)
        //{
        //    Result result = new Result();
        //    try
        //    {
        //        if(ResetInd ==0)
        //        {
        //            MELib.Orders.OrderList OrderList = MELib.Orders.OrderList.Get(OrderTypeId);
        //            result.Data = OrderList;
        //        }
        //        else
        //        {
        //            MELib.Orders.OrderList OrderList = MELib.Orders.OrderList.GetOrderList();
        //            result.Data = OrderList;
        //        }
        //        result.Success = true;
        //    }
        //    catch(Exception e)
        //    {
        //        WebError.LogError(e, "Page: LatestReleases.aspx | Method: FilterMovies", $"(int OrderTypeId, ({OrderTypeId})");
        //        result.Data = e.InnerException;
        //        result.ErrorText = "Could not filter movies by category.";
        //        result.Success = false;
        //    }
        //    return result;
        //}
    }
}
