using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using MELib.Carts;
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
        public MELib.Carts.CartItemList CartItemList { get; set; }
        public MELib.Carts.CartItem CartItem { get; set; }
        public MELib.Carts.Cart Cart { get; set; }
        public bool isActiveInd { get; set; }
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public int CartID { get; set; }
        public String ProductName { get; set; }
        public int ProductQuantity { get; set; }
        public int Quantity { get; set; } 


        /// <summary>
        /// Gets or sets the Movie Genre ID
        /// </summary>
        /// 


        [Singular.DataAnnotations.DropDownWeb(typeof(MELib.RO.ROProductCategoryList), UnselectedText = "Select", ValueMember = "ProductCategoryId", DisplayMember = "ProductCategoryName")]
        [Display(Name = "ProductCategoryName")]
        public int? ProductCategoryId { get; set; }

        public ProductsVM()
        {

        }
        protected override void Setup()
        {
            base.Setup();
            ProductList = MELib.Products.ProductList.GetProductList();
            CartList = MELib.Carts.CartList.GetCartList();
            CartItemList = MELib.Carts.CartItemList.GetCartItemList();
        }

        //filter product category 
        [WebCallable]
        public Result FilterProducts(int ProductCategoryId,int ResetInd)
        {
            Result sr = new Result();
            try
            {
               if(ResetInd == 0)
                {
                    sr.Data = MELib.Products.ProductList.GetProductByCategoryId(ProductCategoryId);
                    sr.Success = true;
                }
                else
                {
                    sr.Data = MELib.Products.ProductList.GetProductList();
                    sr.Success = true;
                }
            }
            catch (Exception e)
            {
                WebError.LogError(e, "Page: LatestReleases.aspx | Method: FilterProducts", $"(int ProductCategoryId, ({ProductCategoryId})");
                sr.Data = e.InnerException;
                sr.ErrorText = "Could not filter movies by category.";
                sr.Success = false;
            }
            return sr;
        }
        //method to subtract qunatity in the database
        public static void ProductSubtract(int productId, int productCount)
        {
            Result sr = new Result();
            try
            {
                //Products
                MELib.Products.ProductList SaveProd = MELib.Products.ProductList.GetProductList(productId);
                //get quantity of specific product
                var stockQuantity = MELib.Products.ProductList.GetProductList().GetItem(productId);
                stockQuantity.ProductQuantity -= productCount;
                stockQuantity.TrySave(typeof(MELib.Products.ProductList));
                SaveProd.Add(stockQuantity);
                sr.Success = true;
            }
            catch (Exception e)
            {
                WebError.LogError(e, "Page: products.aspx | Method: FilterProductCategory", $"(int ProductCategoryId, ({productId})");
                sr.Data = e.InnerException;
                sr.ErrorText = "Could not filter products by category.";
                sr.Success = false;
            }
        }
        public static Result AddToBasket(int ProductID, int productCount, ProductList productlist)
        {
            Result result = new Result();
            try
            {
                // get current loggedin user
                var currentuser = Singular.Security.Security.CurrentIdentity.UserID;

                //Products
                MELib.Products.ProductList SaveProd = MELib.Products.ProductList.GetProductList(ProductID);
                MELib.Products.Product ProdSaveToBasket = SaveProd.GetItem(ProductID);


                // Cart
                MELib.Carts.Cart cart = MELib.Carts.Cart.NewCart();
                MELib.Carts.CartList currentUserCartList = MELib.Carts.CartList.NewCartList();

                // Cart Items
                MELib.Carts.CartItem cartItem = MELib.Carts.CartItem.NewCartItem();
                MELib.Carts.CartItemList cartItemList = MELib.Carts.CartItemList.NewCartItemList();

                //get product by id
                var cartItemExists = MELib.Carts.CartItemList.GetCartItemByProductId(ProductID).FirstOrDefault();
                // MELib.Carts.CartItemList cartListItem = MELib.Carts.CartItemList.GetCartItemList;

                //get the cart of logged in user 
                var cartExists = MELib.Carts.CartList.GetCartByID(currentuser).FirstOrDefault();

                //Check if the cart has quantity
                if (productCount <= 0)
                {
                    result.ErrorText = "Please specify product Quantity to be able to add it to Basket ";
                }
                else
                {
                    // check if the product quantity is greater than quantity to be added in the cart
                    if (ProdSaveToBasket.ProductQuantity >= productCount)
                    {
                        // if the cart user does not have cart create a new cart
                        if (cartExists == null)
                        {
                            // Add new cart
                            cart.Quantity = productCount;
                            cart.IsActiveInd = true;
                            cart.UserID = Singular.Security.Security.CurrentIdentity.UserID;
                            cart.TotalAmount = productCount * ProdSaveToBasket.Price;
                            cart.DateCreated = DateTime.Now;
                            //save to object
                            currentUserCartList.Add(cart);

                            //save to database
                            currentUserCartList.Save();

                            //add to cart item
                            cartItem.ProductId = ProdSaveToBasket.ProductID;
                            cartItem.ProductName = ProdSaveToBasket.ProductName;
                            cartItem.ProductImage = ProdSaveToBasket.ProductImageURL;
                            cartItem.CartID = MELib.Carts.CartList.GetCartByID(currentuser).FirstOrDefault().CartID;
                            cartItem.ProductDescription = ProdSaveToBasket.ProductDescription;
                            cartItem.Price = ProdSaveToBasket.Price;
                            cartItem.Quantity = productCount;
                            cartItem.IsActiveInd = true;
                            cartItem.Value = productCount * ProdSaveToBasket.Price;
                            //save to object
                            cartItemList.Add(cartItem);

                            //save to database
                            cartItemList.Save();
                            //subtract products from the database
                            ProductSubtract(Convert.ToInt32(cartItem.ProductId), productCount);

                        }
                        // if the cart exists, update the cart
                        else
                        {
                            //// if the cart is not active
                            //if (cartExists.IsActiveInd == false)
                            //{

                            //    // Activate the isActive property and update the 
                            //    cartExists.IsActiveInd = true;
                            //    cartExists.Quantity = productCount;
                            //    cartExists.TotalAmount = productCount * ProdSaveToBasket.Price;
                            //    cartExists.DateModified = DateTime.Now;
                            //    //save to object

                            //    //save to database
                            //    cartExists.TrySave(typeof(CartList));

                            //    //add to cart item
                            //    cartItem.ProductId = ProdSaveToBasket.ProductID;
                            //    cartItem.ProductName = ProdSaveToBasket.ProductName;
                            //    cartItem.ProductImage = ProdSaveToBasket.ProductImageURL;
                            //    cartItem.CartID = MELib.Carts.CartList.GetCartByID(currentuser).FirstOrDefault().CartID;
                            //    cartItem.ProductDescription = ProdSaveToBasket.ProductDescription;
                            //    cartItem.Price = ProdSaveToBasket.Price;
                            //    cartItem.Quantity = productCount;
                            //    cartItem.Value = productCount * ProdSaveToBasket.Price;
                            //    //save to object
                            //    cartItemList.Add(cartItem);

                            //    //save to database
                            //    cartItemList.Save();
                            //    //subtract products from the database
                            //    ProductSubtract(Convert.ToInt32(cartItem.ProductId), productCount);
                            //}

                            // check if the cart to be added exists in the cartItem, if exists update the product
                            // alse edit the cart quantity and total amount
                            if (cartItemExists != null && cartExists.IsActiveInd.Equals(true))
                            {
                                // update the cart quantity and total amount
                                cartExists.Quantity += productCount;
                                cartExists.TotalAmount = cartExists.TotalAmount + cartItemExists.Value;
                                
                                currentUserCartList.Add(cartExists);
                                currentUserCartList.Save();

                                // update the cart item
                                cartItemExists.Quantity += productCount;
                                cartItemExists.Value += ProdSaveToBasket.Price * productCount;
                                cartItemList.Add(cartItemExists);
                                cartItemList.Save();


                                //subtract products from the database
                                ProductSubtract(ProductID, productCount);


                            }

                            //if the product does not exist, add it in the cart items
                            else
                            {

                                // update the cart quantity and total amount
                                cartExists.Quantity += productCount;
                                cartExists.TotalAmount += productCount * ProdSaveToBasket.Price;
                                currentUserCartList.Add(cartExists);
                                currentUserCartList.Save();

                                // Add the item in the cartItem
                                cartItem.ProductId = ProdSaveToBasket.ProductID;
                                cartItem.ProductName = ProdSaveToBasket.ProductName;
                                cartItem.ProductImage = ProdSaveToBasket.ProductImageURL;
                                cartItem.CartID = cartExists.CartID;
                                cartItem.ProductDescription = ProdSaveToBasket.ProductDescription;
                                cartItem.Price = ProdSaveToBasket.Price;
                                cartItem.Quantity = productCount;
                                cartItem.IsActiveInd = true;
                                cartItem.Value += ProdSaveToBasket.Price * productCount;

                                //save the cart item
                                cartItemList.Add(cartItem);
                                cartItemList.Save();


                                //subtract products from the database
                                ProductSubtract(ProductID, productCount);

                            }
                        }
                        //saving
                        result.Success = true;
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