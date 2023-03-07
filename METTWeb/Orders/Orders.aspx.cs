using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MEWeb.Orders
{
    public partial class Orders : MEPageBase<OrdersVM>
    {
    }
    public class OrdersVM : MEStatelessViewModel<OrdersVM>
    {
        public MELib.Orders.OrderList OrderList { get; set; }
        public MELib.Orders.OrderDetailList OrderDetailList { get; set; }
        

        public OrdersVM()
        {

        }
        protected override void Setup()
        {
            base.Setup();
            OrderList = MELib.Orders.OrderList.GetOrderList();
        }
    }
}
