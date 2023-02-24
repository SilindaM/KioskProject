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
        public MELib.Carts.CartItemList CartItemList { get; set; }
        public MELib.Carts.CartItem CartItem { get; set; }
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
            CartItemList = MELib.Carts.CartItemList.GetCartItemList();
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
                var cartItemExists = MELib.Carts.CartItemList.GetCartItemList(ProductID).FirstOrDefault();
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
                            cartItem.CartID = cart.CartID;
                            cartItem.ProductDescription = ProdSaveToBasket.ProductDescription;
                            cartItem.Price = ProdSaveToBasket.Price;
                            cartItem.Quantity = ProdSaveToBasket.ProductQuantity;
                            cartItem.Value = ProdSaveToBasket.Price * ProdSaveToBasket.ProductQuantity;

                            //save to object
                            cartItemList.Add(cartItem);
                        
                            //save to database
                            cartItemList.Save();

                        }
                        // if the cart exists update the cart
                        else
                        {
                            // if the cart is not active
                            if (cartExists.IsActiveInd == false) { 

                                        // add to cart
                                        cartExists.UserID = cartExists.UserID;
                                        cart.IsActiveInd = cartExists.IsActiveInd;
                                        cart.UserID = cartExists.UserID;
                                        cart.TotalAmount += productCount * ProdSaveToBasket.Price;
                                        cart.DateCreated = cartExists.DateCreated;
                                        cart.DateModified = DateTime.Now;
                                        cart.Quantity = cartExists.Quantity + productCount;
                                        currentUserCartList.Add(cart);
                                        currentUserCartList.Save();
                            }
                            // check if the cart to be added exists in the cartItem, if exists update the product
                            // alse edit the cart quantity and total amount
                            if(cartItemExists != null)
                            {
                                // update the cart item
                                cartItemExists.Quantity = cartItemExists.Quantity  +productCount;
                                cartItemExists.Value += ProdSaveToBasket.Price * productCount;
                                cartItemList.Add(cartItemExists);
                                cartItemList.Save();

                                // update the cart quantity and total amount
                                cartExists.Quantity += cartItemExists.Quantity;
                                cartExists.TotalAmount = cartExists.TotalAmount + cartItemExists.Value;
                                currentUserCartList.Add(cartExists);
                                currentUserCartList.Save();
                            }

                            //if the product does not exist add it, in the cart items
                            else
                            {
                                cartItem.ProductId = ProdSaveToBasket.ProductID;
                                cartItem.ProductName = ProdSaveToBasket.ProductName;
                                cartItem.ProductImage = ProdSaveToBasket.ProductImageURL;
                                cartItem.CartID = cartExists.CartID;
                                cartItem.ProductDescription = ProdSaveToBasket.ProductDescription;
                                cartItem.Price = ProdSaveToBasket.Price;
                                cartItem.Quantity = productCount;
                                cartItem.Value += ProdSaveToBasket.Price * productCount;
                                cartItemList.Add(cartItem);
                                cartItemList.Save();
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
