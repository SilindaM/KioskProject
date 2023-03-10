<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="MEWeb.Orders.Orders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <link href="../Theme/Singular/Custom/home.css" rel="stylesheet" />
  <link href="../Theme/Singular/Custom/customstyles.css" rel="stylesheet" />
  <link href="../Theme/Singular/METTCustomCss/Maintenance/maintenance.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <%
        using (var h = this.Helpers)
        {
            var MainHDiv = h.DivC("row pad-top-10");
            var PanelContainer = MainHDiv.Helpers.DivC("col-md-12 p-n-lr");
            {
                var HomeContainer = PanelContainer.Helpers.DivC("tabs-container");
                {
                    var AssessmentsTab = HomeContainer.Helpers.TabControl();
                    {
                        AssessmentsTab.Style.ClearBoth();
                        AssessmentsTab.AddClass("nav nav-tabs");
                        var HomeContainerTab = AssessmentsTab.AddTab("Parent / Child Tables");
                        {
                            var Row = HomeContainerTab.Helpers.DivC("row margin0");
                            {
                                var RowCol = Row.Helpers.DivC("col-md-9");
                                {

                                    #region Orders

                                    var OrdersDiv = RowCol.Helpers.Div();
                                    {
                                        var Orders = OrdersDiv.Helpers.BootstrapTableFor<MELib.RO.Order>((c) => c.OrderList, false, false, "", true);
                                        var OrdersFirstRow = Orders.FirstRow;
                                        {
                                            var OrderID = OrdersFirstRow.AddReadOnlyColumn(c => c.OrderID);
                                            {
                                                OrderID.HeaderText = "Order ID";
                                            }
                                            var OrderType = OrdersFirstRow.AddReadOnlyColumn(c => c.OrderTypeId);
                                            {
                                                OrderType.HeaderText = "Order Type";
                                            }
                                            var OrderDate = OrdersFirstRow.AddReadOnlyColumn(c => c.OrderedDate);
                                            {
                                                OrderDate.HeaderText = "Order Date";
                                            }
                                            var OrderAmount = OrdersFirstRow.AddReadOnlyColumn(c => c.OrderAmount);
                                            {
                                                OrderAmount.HeaderText = "Order Amount";
                                            }
                                        }
                                        var OrderDetails = Orders.AddChildTable<MELib.RO.OrderDetail>((c) => c.OrderDetailList, false, false, "");
                                        {
                                            var OrderDetailsFirstRow = OrderDetails.FirstRow;
                                            {
                                                var OrderDetailsId = OrderDetailsFirstRow.AddReadOnlyColumn(c => c.OrderDetailId);
                                                {
                                                    OrderDetailsId.HeaderText = "OrderDetails Id";
                                                }
                                                var ProductName = OrderDetailsFirstRow.AddReadOnlyColumn(c => c.ProductName);
                                                {
                                                    ProductName.HeaderText = "ProductName";
                                                }
                                                var ProductDescription = OrderDetailsFirstRow.AddReadOnlyColumn(c => c.ProductDescription);
                                                {
                                                    ProductDescription.HeaderText = "ProductDescription";
                                                }
                                                var Price = OrderDetailsFirstRow.AddReadOnlyColumn(c => c.Price);
                                                {
                                                    Price.HeaderText = "Price";
                                                }
                                                var Quantity = OrderDetailsFirstRow.AddReadOnlyColumn(c => c.Quantity);
                                                {
                                                    Quantity.HeaderText = "Quantity";
                                                }
                                                var Value = OrderDetailsFirstRow.AddReadOnlyColumn(c => c.Value);
                                                {
                                                    Value.HeaderText = "Value";
                                                }
                                                var ProductId = OrderDetailsFirstRow.AddReadOnlyColumn(c => c.ProductId);
                                                {
                                                    ProductId.HeaderText = "ProductId";
                                                }
                                            }
                                        }
                                    }
                                }
                                var RowColRight = Row.Helpers.DivC("col-md-3");
                                {

                                    var AnotherCardDiv = RowColRight.Helpers.DivC("ibox float-e-margins paddingBottom");
                                    {
                                        var CardTitleDiv = AnotherCardDiv.Helpers.DivC("ibox-title");
                                        {
                                            CardTitleDiv.Helpers.HTML("<i class='ffa-lg fa-fw pull-left'></i>");
                                            CardTitleDiv.Helpers.HTML().Heading5("Filter Criteria");
                                        }
                                        var CardTitleToolsDiv = CardTitleDiv.Helpers.DivC("ibox-tools");
                                        {
                                            var aToolsTag = CardTitleToolsDiv.Helpers.HTMLTag("a");
                                            aToolsTag.AddClass("collapse-link");
                                            {
                                                var iToolsTag = aToolsTag.Helpers.HTMLTag("i");
                                                iToolsTag.AddClass("fa fa-chevron-up");
                                            }
                                        }
                                        var ContentDiv = AnotherCardDiv.Helpers.DivC("ibox-content");
                                        {
                                            var RowContentDiv = ContentDiv.Helpers.DivC("row");
                                            {
                                             
                                                var MovieGenreContentDiv = RowContentDiv.Helpers.DivC("col-md-12");
                                                {

                                                    MovieGenreContentDiv.Helpers.LabelFor(c => ViewModel.OrderTypeId);
                                                    var ReleaseFromDateEditor = MovieGenreContentDiv.Helpers.EditorFor(c => ViewModel.OrderTypeId);
                                                    ReleaseFromDateEditor.AddClass("form-control marginBottom20 ");

                                                    var FilterBtn = MovieGenreContentDiv.Helpers.Button("Apply Filter", Singular.Web.ButtonMainStyle.Primary, Singular.Web.ButtonSize.Normal, Singular.Web.FontAwesomeIcon.None);
                                                    {
                                                        FilterBtn.AddBinding(Singular.Web.KnockoutBindingString.click, "FilterOrders($data)");
                                                        FilterBtn.AddClass("btn btn-primary btn-outline");
                                                    }
                                                    var ResetBtn = MovieGenreContentDiv.Helpers.Button("Reset", Singular.Web.ButtonMainStyle.Primary, Singular.Web.ButtonSize.Normal, Singular.Web.FontAwesomeIcon.None);
                                                    {
                                                        ResetBtn.AddBinding(Singular.Web.KnockoutBindingString.click, "FilterReset($data)");
                                                        ResetBtn.AddClass("btn btn-primary btn-outline");
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion
        %>
    
  <script type="text/javascript">
    // Place page specific JavaScript here or in a JS file and include in the HeadContent section
    Singular.OnPageLoad(function () {
      $("#menuItem1").addClass('active');
      $("#menuItem1 > ul").addClass('in');
    });

     
      var FilterOrders = function (obj) {
      ViewModel.CallServerMethod('FilterOrders', { OrderTypeId: obj.OrderTypeId(), ResetInd: 0, ShowLoadingBar: true }, function (result) {
        if (result.Success) {
            MEHelpers.Notification("Products filtered successfully.", 'center', 'info', 1000);
            ViewModel.OrderList.Set(result.Data);
        }
        else {
          MEHelpers.Notification(result.ErrorText, 'center', 'warning', 5000);
        }
      })
    };

    var FilterReset = function (obj) {
      ViewModel.CallServerMethod('FilterOrders', { OrderTypeId: obj.OrderTypeId(), ResetInd: 1, ShowLoadingBar: true }, function (result) {
        if (result.Success) {
          MEHelpers.Notification("Products reset successfully.", 'center', 'info', 1000);
            ViewModel.OrderList.Set(result.Data);
          // Set Drop Down to 'Select'
        }
        else {
          MEHelpers.Notification(result.ErrorText, 'center', 'warning', 5000);
        }
      })
    };

      
  </script>
</asp:Content>
