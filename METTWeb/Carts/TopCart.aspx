<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TopCart.aspx.cs" Inherits="MEWeb.Carts.TopCart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
      <!-- Add page specific styles and JavaScript classes below -->
  <link href="../Theme/Singular/Custom/home.css" rel="stylesheet" />
  <link href="../Theme/Singular/Custom/customstyles.css" rel="stylesheet" />
  <style>
    .product-border {
      border-radius: 5px;
      border: 2px solid #DEDEDE;
      padding: 15px;
      margin: 5px;
    }
   .item img {
          width: 200px;
          height: 200px;
         padding: 20px 1px 20px 1px;
}
    div.item {
      vertical-align: top;
      display: inline-block;
      text-align: center;
      padding-bottom: 25px;
    }

    .caption {
      display: block;
      padding-bottom: 5px;
      font-size : 20px;
      background : #000000;
      margin-right : 30px;
      border-radius: 5px;
      margin-left : 30px;
    }
    .caption,text
    {
        padding-left : -50px;
        padding-right : -10px;
    }
    WatchBtn
    {
      margin-right : 5px;
      margin-left : 5px;

    }
  </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <%
        using (var h = this.Helpers)
        {
            var MainHDiv = h.DivC("row pad-top-10");
            {
                var PanelContainer = MainHDiv.Helpers.DivC("col-md-12 p-n-lr");
                {
                    var HomeContainer = PanelContainer.Helpers.DivC("tabs-container");
                    {
                        var AssessmentsTab = HomeContainer.Helpers.TabControl();
                        {
                            AssessmentsTab.Style.ClearBoth();
                            AssessmentsTab.AddClass("nav nav-tabs");
                            var HomeContainerTab = AssessmentsTab.AddTab("Home");
                            {
                                var Row = HomeContainerTab.Helpers.DivC("row margin0");
                                {
                                    var RowColLeft = Row.Helpers.DivC("col-md-9");
                                    {
                                        var AnotherCardDiv = RowColLeft.Helpers.DivC("ibox float-e-margins paddingBottom");
                                        {
                                            var CardTitleDiv = AnotherCardDiv.Helpers.DivC("ibox-title");
                                            {
                                                CardTitleDiv.Helpers.HTML("<i class='ffa-lg fa-fw pull-left'></i>");
                                                CardTitleDiv.Helpers.HTML().Heading5("Latest Releases");
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

                                                    var ColContentDiv = RowContentDiv.Helpers.DivC("col-md-12");
                                                    {
                                                        var MoviesWatchedDiv = ColContentDiv.Helpers.ForEach<MELib.Carts.CartItem>(c => c.CartItemList);
                                                        {

                                                            // Using Knockout Binding
                                                            // <img width="16px" height="16px" data-bind="attr:{src: imagePath}" />
                                                            MoviesWatchedDiv.Helpers.HTML("<div class='item'>");
                                                            MoviesWatchedDiv.Helpers.HTML("<img data-bind=\"attr:{src: $data.ProductImage()} \" class='product-border'/>");
                                                            
                                                            MoviesWatchedDiv.Helpers.HTML("<span data-bind=\"text: $data.ProductName() + '    R' +$data.Price()\" class='caption'></span>");
                                                             MoviesWatchedDiv.Helpers.HTML("QUANTITY"+"<span data-bind=\"text : $data.Quantity()\" class='caption' contenteditable='true'></span>");
                                                           //  MoviesWatchedDiv.Helpers.EditorFor(c => c.Quantity);
                                                            
                                                        }
                                                        var actionButton = MoviesWatchedDiv.Helpers.DivC("col-md-12");
                                                        {
                                                            var update = actionButton.Helpers.DivC("col-md-6");
                                                            {
                                                                    var WatchBtn = update.Helpers.Button("Update", Singular.Web.ButtonMainStyle.Primary, Singular.Web.ButtonSize.Normal, Singular.Web.FontAwesomeIcon.upload);
                                                                    {
                                                                        WatchBtn.AddBinding(Singular.Web.KnockoutBindingString.click, "AddToBasket($data)");
                                                                        WatchBtn.AddClass("btn btn-primary");
                                                                        WatchBtn.AddClass("btn btn-primary btn-block");
                                                                    }
                                                            }
                                                            var delete = actionButton.Helpers.DivC("col-md-6");
                                                            {
                                                                    var WatchBtn = delete.Helpers.Button("Remove", Singular.Web.ButtonMainStyle.Primary, Singular.Web.ButtonSize.Normal, Singular.Web.FontAwesomeIcon.trash_o);
                                                                    {
                                                                        WatchBtn.AddBinding(Singular.Web.KnockoutBindingString.click, "AddToBasket($data)");
                                                                        WatchBtn.AddClass("btn btn-primary");
                                                                        WatchBtn.AddClass("btn btn-primary btn-block");
                                                                    }
                                                            }
                                                        }

                                                        MoviesWatchedDiv.Helpers.HTML("</div>");
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

                                                        MovieGenreContentDiv.Helpers.HTML().Heading5("Select Delivery Method");
                                                        var ReleaseFromDateEditor = MovieGenreContentDiv.Helpers.EditorFor(c => ViewModel.OrderTypeId);
                                                        ReleaseFromDateEditor.AddClass("form-control marginBottom20");

                                                        var FilterBtn = MovieGenreContentDiv.Helpers.Button("Confirm Order", Singular.Web.ButtonMainStyle.Primary, Singular.Web.ButtonSize.Normal, Singular.Web.FontAwesomeIcon.cart_plus);
                                                        {
                                                            FilterBtn.AddBinding(Singular.Web.KnockoutBindingString.click, "FilterProducts($data)");
                                                            FilterBtn.AddClass("btn btn-primary btn-outline marginBottom20");
                                                        }
                                                        var ResetBtn = MovieGenreContentDiv.Helpers.Button("Clear Cart", Singular.Web.ButtonMainStyle.Primary, Singular.Web.ButtonSize.Normal, Singular.Web.FontAwesomeIcon.trash);
                                                        {
                                                            ResetBtn.AddBinding(Singular.Web.KnockoutBindingString.click, "FilterReset($data)");
                                                            ResetBtn.AddClass("btn btn-primary btn-outline marginBottom20");
                                                        }
                                                        var cartSummary = MovieGenreContentDiv.Helpers.DivC("col-md-12");
                                                        {
                                                            var cartAmount = MovieGenreContentDiv.Helpers.DivC("col-md-6");
                                                            {
                                                                cartAmount.Helpers.HTML().Heading4("Total");
                                                                cartAmount.Helpers.LabelFor(c => ViewModel.totalQuantity.ToString());
                                                            }
                                                            var cartQuantity = MovieGenreContentDiv.Helpers.DivC("col-md-6");
                                                            {
                                                                cartQuantity.Helpers.HTML().Heading4("Quantity");
                                                                cartQuantity.Helpers.LabelFor(ca => ViewModel.TotalAmount);
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
            }
        }
    %>
    <script type="text/javascript">

        var deleteCartItem = function (obj) {
            console.log(obj);
            ViewModel.CallServerMethod("deleteCartItem", { productId: obj.productId, productCount: obj.productCount, ShowLoadingBar: true }, function (result) {
                if (result.Success) {
                    alert('Deleted From Cart Successfully');
                    Singular.AddMessage(3, 'Save', 'Added Successfully.').Fade(2000);

                }
                else {
                    Singular.AddMessage(1, 'Error', result.ErrorText).Fade(2000);
                    // MEHelpers.Notification(result.ErrorText, 'center', 'warning', 5000);
                }
            });
        }
      var UpdateCart = function (obj)
      {
          ViewModel.CallServerMethod("UpdateCart", { CartItemID: obj.CartItemID(), ProductId: obj.ProductId(), productCount: obj.Quantity(), CartItemList: ViewModel.CartItemList.Serialise(), ShowLoadingBar: true }, function (result) {
           if (result.Success) {
                  alert('Cart Updated  Successfully');
                   Singular.AddMessage(3, 'Save', 'Updated Successfully.').Fade(2000);
              }
              else {
                  Singular.AddMessage(1, 'Error', result.ErrorText).Fade(2000);
                  // MEHelpers.Notification(result.ErrorText, 'center', 'warning', 5000);
              }
          });
      }
        var DeleteCartItem = function (obj) {
            ViewModel.CallServerMethod("DeleteCartItem", { CartItemID: obj.CartItemID(), ProductId: obj.ProductId(), productCount: obj.Quantity(), CartItemList: ViewModel.CartItemList.Serialise(), ShowLoadingBar: true }, function (result) {
                if (result.Success) {
                    alert('Item Removed Successfully Successfully');
                    Singular.AddMessage(3, 'Save', 'Removed Successfully.').Fade(2000);
                }
                else {
                    Singular.AddMessage(1, 'Error', result.ErrorText).Fade(2000);
                    // MEHelpers.Notification(result.ErrorText, 'center', 'warning', 5000);
                }
            });
        }
        var CompleteCart = function (obj) {
            ViewModel.CallServerMethod("CompleteCart", { CartItemList: ViewModel.CartItemList.Serialise(), orderTypeId: ViewModel.OrderTypeId(), ShowLoadingBar: true }, function (result) {
            if (result.Success) {
                            alert('Item Removed Successfully Successfully');
                            Singular.AddMessage(3, 'Save', 'Removed Successfully.').Fade(2000);
                        }
                        else {
                            Singular.AddMessage(1, 'Error', result.ErrorText).Fade(2000);
                            // MEHelpers.Notification(result.ErrorText, 'center', 'warning', 5000);
                        }
                    });
        }
        var ClearCart = function (obj) {
            ViewModel.CallServerMethod("ClearCart", {CartItemList: ViewModel.CartItemList.Serialise(), ShowLoadingBar: true }, function (result) {
            if (result.Success) {
                            alert('Cart Cleared Successfully');
                            Singular.AddMessage(3, 'Save', 'Removed Successfully.').Fade(2000);
                        }
                        else {
                            Singular.AddMessage(1, 'Error', result.ErrorText).Fade(2000);
                            // MEHelpers.Notification(result.ErrorText, 'center', 'warning', 5000);
                        }
                    });
        }

    </script>

</asp:Content>
