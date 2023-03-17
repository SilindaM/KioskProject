using MELib.RO;
using Singular.Web;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace MEWeb.Orders
{
    public partial class Orders : MEPageBase<OrdersVM>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
    public class OrdersVM : MEStatelessViewModel<OrdersVM>
    {
        public OrderList OrderList { get; set; }
        public OrderDetailList OrdersDetailsList { get; set; }

        [Singular.DataAnnotations.DropDownWeb(typeof(MELib.RO.ROOrderTypeList), UnselectedText = "Select", ValueMember = "OrderTypeId", DisplayMember = "OrderName")]
        [Display(Name = "OrderName")]
        public int? OrderTypeId { get; set; }

        public OrdersVM()
        {

        }

        protected override void Setup()
        {
            base.Setup();

            OrderList = MELib.RO.OrderList.GetOrderList();
        }
        //filter product category 
        [WebCallable]
        public Result FilterOrders(int OrderTypeId, int ResetInd)
        {
            Result sr = new Result();
            try
            {
                if (ResetInd == 0)
                {
                    sr.Data = MELib.Orders.OrderList.GetOrderByType(OrderTypeId);
                    sr.Success = true;
                }
                else
                {
                    sr.Data = MELib.RO.OrderList.GetOrderList();
                    sr.Success = true;
                }
            }
            catch (Exception e)
            {
                WebError.LogError(e, "Page: Orders.aspx | Method: FilterProducts", $"(int OrderTypeId, ({OrderTypeId})");
                sr.Data = e.InnerException;
                sr.ErrorText = "Could not filter Orders by type.";
                sr.Success = false;
            }
            return sr;
        }
    }
}
