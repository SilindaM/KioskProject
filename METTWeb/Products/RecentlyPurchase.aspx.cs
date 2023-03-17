using Singular.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MEWeb.Products
{
    public partial class RecentlyPurchase : MEPageBase<RecentlyPurchaseVM>
    {
    }
    public class RecentlyPurchaseVM : MEStatelessViewModel<RecentlyPurchaseVM>
    {
        public MELib.Products.ProductList ProductList { get; set; }
        public MELib.Carts.CartList CartList { get; set; }
        public MELib.Carts.CartItemList CartItemList { get; set; }

        public MELib.Orders.OrderDetailList topselling { get; set; }
        public MELib.Orders.OrderDetailList topsealling { get; set; }

        public int tops { get; set; }

        public RecentlyPurchaseVM()
        {

        }
        protected override void Setup()
        {
           topselling = MELib.Orders.OrderDetailList.GetOrderDetailList();
           var topSellingItem = topselling.OrderByDescending(x => x.Quantity).FirstOrDefault();
           tops = topSellingItem?.Quantity ?? 0;

            //ProductList = MELib.Products.ProductList.GetProductList();
            //var cartById = MELib.Carts.CartList.GetCartByUserID(Singular.Security.Security.CurrentIdentity.UserID);
            //var cartId = cartById.FirstOrDefault().CartID;
            //CartItemList = MELib.Carts.CartItemList.GetCartItemByCartId(cartId);

        }
        //filter product category 
        [WebCallable]
        public Result FilterProducts(int ProductCategoryId, int ResetInd)
        {
            Result sr = new Result();
            try
            {
                if (ResetInd == 0)
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
    }
}
