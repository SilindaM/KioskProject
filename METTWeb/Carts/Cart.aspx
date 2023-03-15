<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="MEWeb.Carts.Cart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
  <link href="../Theme/Singular/Custom/home.css" rel="stylesheet" />
  <link href="../Theme/Singular/Custom/customstyles.css" rel="stylesheet" />
  <link href="../Theme/Singular/METTCustomCss/Maintenance/maintenance.css" rel="stylesheet" />
    <style>
   .product-border {
      border-radius: 5px;
      border: 2px solid #DEDEDE;
      padding: 15px;
      margin: 5px;
    }
   .item img {
          width: 100px;
          height: 100px;
         padding: 20px 1px 20px 1px;
}
    div.item {
      vertical-align: top;
      display: inline-block;
      text-align: center;
      padding-bottom: 25px;
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
               
                    var HomeContainer = MainHDiv.Helpers.DivC("tabs-container");
                    {
                        var AssessmentsTab = HomeContainer.Helpers.TabControl();
                        {
                            AssessmentsTab.Style.ClearBoth();
                            AssessmentsTab.AddClass("nav nav-tabs");
                        
                            var HomeContainerTab = AssessmentsTab.AddTab("Catalogy");

                            {
                                var Row = HomeContainerTab.Helpers.DivC("row margin0");
                                {
                                    var RowColLeft = Row.Helpers.DivC("col-md-9 ");
                                    {
                                        var AnotherCardDiv = RowColLeft.Helpers.DivC("ibox float-e-margins paddingBottom");
                                        {
                                            var CardTitleDiv = AnotherCardDiv.Helpers.DivC("ibox-title");
                                            {
                                                CardTitleDiv.Helpers.HTML("<i class='ffa-lg fa-fw pull-left'></i>");
                                                CardTitleDiv.Helpers.HTML().Heading5("Shopping Cart");
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
                                                        // var ProductSection = ColContentDiv.Helpers.BootstrapTableFor<MELib.Products.Product>((c) => c.ProductList, false, false, "");
                                                        var CartList = ColContentDiv.Helpers.BootstrapTableFor<MELib.Carts.CartItem>((c) => c.CartItemList, false, false, "");
                                                        var cart = ColContentDiv.Helpers.BootstrapTableFor<MELib.Carts.Cart>((ca) => ca.cartList, false, false, "");
                                                        {

                                                            var ProductListRow = CartList.FirstRow;
                                                            var CartListRow = CartList.FirstRow;
                                                            ProductListRow.Style.BackgroundColour = "#1000";

                                                            {
                                                                var ProductName = ProductListRow.AddColumn("Product Name");
                                                                {
                                                                    var productNameText = ProductName.Helpers.Span(c => c.ProductName);
                                                                    productNameText.Style.FontSize = "15px";
                                                                    productNameText.Style.Width = "250px";
                                                                    

                                                                }
                                                                var ProductDescription = ProductListRow.AddColumn("Description");
                                                                {
                                                                    var ProductDescriptionText = ProductDescription.Helpers.Span(c => c.ProductDescription);
                                                                    ProductDescriptionText.Style.FontSize = "15px";
                                                                    ProductDescriptionText.Style.Width = "250px";
                                                                }

                                                                var ProductPrice = ProductListRow.AddColumn("Price");
                                                                {
                                                                    var Price = ProductPrice.Helpers.Span(c => "R " + c.Price);
                                                                    Price.Style.FontSize = "15px";
                                                                }
                                                                var ProductImage = ProductListRow.AddColumn("Product Image");
                                                                {
                                                                    ProductImage.Helpers.HTML("<div class='item'>");
                                                                    var Image = ProductImage.Helpers.HTML("<img data-bind=\"attr:{src: $data.ProductImage()} \" class='product-border'/>");
                                                                    Image.Style.FontSize = "15px";
                                                                }
                                                                var CartQuantity = ProductListRow.AddColumn("Quantity");
                                                                {
                                                                    var Quantity = CartQuantity.Helpers.EditorFor(c => c.Quantity);

                                                                    Quantity.Style.FontSize = "15px";
                                                                }
                                                                var Action = ProductListRow.AddColumn("Action");
                                                                {
                                                                    var updateItem = Action.Helpers.Button("Update", Singular.Web.ButtonMainStyle.Success, Singular.Web.ButtonSize.Tiny, Singular.Web.FontAwesomeIcon.refresh);
                                                                    {
                                                                        updateItem.AddBinding(Singular.Web.KnockoutBindingString.click, "UpdateCart($data)");
                                                                        updateItem.AddClass("btn btn-success btn-outline margin-to-10");
                                                                        Action.Style.Width = "100%";
                                                                    }
                                                                    var removeItem = Action.Helpers.Button("Remove", Singular.Web.ButtonMainStyle.Success, Singular.Web.ButtonSize.Tiny, Singular.Web.FontAwesomeIcon.remove);
                                                                    {
                                                                        removeItem.AddBinding(Singular.Web.KnockoutBindingString.click, "DeleteCartItem($data)");
                                                                        removeItem.AddClass("btn btn-danger btn-outline margin-to-10 ");
                                                                        Action.Style.Width = "100%";
                                                                    }
                                                                }

                                                            }
                                                        }
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
                                                        MovieGenreContentDiv.Helpers.HTML().Heading5("R50 Delivery, Collection is F");
                                                        var ReleaseFromDateEditor = MovieGenreContentDiv.Helpers.EditorFor(c => ViewModel.OrderTypeId);
                                                        ReleaseFromDateEditor.AddClass("form-control marginBottom20");

                                                        var FilterBtn = MovieGenreContentDiv.Helpers.Button("Confirm Order", Singular.Web.ButtonMainStyle.Primary, Singular.Web.ButtonSize.Normal, Singular.Web.FontAwesomeIcon.cart_plus);
                                                        {
                                                            FilterBtn.AddBinding(Singular.Web.KnockoutBindingString.click, "CompleteCart($data)");
                                                            FilterBtn.AddClass("btn btn-primary btn-outline marginBottom20");
                                                        }
                                                        var ResetBtn = MovieGenreContentDiv.Helpers.Button("Clear Cart", Singular.Web.ButtonMainStyle.Primary, Singular.Web.ButtonSize.Normal, Singular.Web.FontAwesomeIcon.trash);
                                                        {
                                                            ResetBtn.AddBinding(Singular.Web.KnockoutBindingString.click, "ClearCart($data)");
                                                            ResetBtn.AddClass("btn btn-primary btn-outline marginBottom20");
                                                        }
                                                        var cartSummary = MovieGenreContentDiv.Helpers.DivC("col-md-12");
                                                        {
                                                            
                                                        cartSummary.Helpers.HTML().Heading4("Cart Summary");
                                                            var CartList = cartSummary.Helpers.BootstrapTableFor<MELib.Carts.CartItem>((c) => c.CartItemList, false, false, "");
                                                           
                                                            var cartAmount = MovieGenreContentDiv.Helpers.DivC("col-md-6");
                                                            {
                                                                cartAmount.Helpers.HTML().Heading5("AMOUNT");
                                                                cartAmount.Helpers.Span(c => "R " + c.TotalAmount);
                                                                cartAmount.Style.FontSize = "15px";
                                                            }
                                                            var cartQuantity = MovieGenreContentDiv.Helpers.DivC("col-md-6");
                                                            {
                                                                cartQuantity.Helpers.HTML().Heading5("QUANTITY");
                                                                cartQuantity.Helpers.Span(c => c.totalQuantity + " Items");
                                                                cartQuantity.Style.FontSize = "15px";
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
                    
                             location.reload();

                }
                else {
                    Singular.AddMessage(1, 'Error', result.ErrorText).Fade(2000);
                    
                             location.reload();

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
               
                             location.reload();
              }
              else {
                  Singular.AddMessage(1, 'Error', result.ErrorText).Fade(2000);
                  // MEHelpers.Notification(result.ErrorText, 'center', 'warning', 5000);
               
                             location.reload();
              }
          });
      }
        var DeleteCartItem = function (obj) {
            ViewModel.CallServerMethod("DeleteCartItem", { CartItemID: obj.CartItemID(), ProductId: obj.ProductId(), productCount: obj.Quantity(), CartItemList: ViewModel.CartItemList.Serialise(), ShowLoadingBar: true }, function (result) {
                if (result.Success) {
                    alert('Item Removed Successfully Successfully');
                    Singular.AddMessage(3, 'Save', 'Removed Successfully.').Fade(2000);
                    
                             location.reload();
                }
                else {
                    Singular.AddMessage(1, 'Error', result.ErrorText).Fade(2000);
                    
                             location.reload();
                    // MEHelpers.Notification(result.ErrorText, 'center', 'warning', 5000);
                }
            });
        }
        var CompleteCart = function (obj) {
            ViewModel.CallServerMethod("CompleteCart", { CartItemList: ViewModel.CartItemList.Serialise(), orderTypeId: ViewModel.OrderTypeId(), ShowLoadingBar: true }, function (result) {
            if (result.Success) {
                            alert('Item Removed Successfully Successfully');
                Singular.AddMessage(3, 'Save', 'Removed Successfully.').Fade(2000);
                
                             location.reload();
                        }
                        else {
                            Singular.AddMessage(1, 'Error', result.ErrorText).Fade(2000);
                            // MEHelpers.Notification(result.ErrorText, 'center', 'warning', 5000);
                
                             location.reload();
                        }
                    });
        }
        var ClearCart = function (obj) {
            ViewModel.CallServerMethod("ClearCart", {CartItemList: ViewModel.CartItemList.Serialise(), ShowLoadingBar: true }, function (result) {
            if (result.Success) {
                             alert('Cart Cleared Successfully');
                             Singular.AddMessage(3, 'Save', 'Removed Successfully.').Fade(2000);
                             location.reload();
                        }
                        else {
                            Singular.AddMessage(1, 'Error', result.ErrorText).Fade(2000);
                            // MEHelpers.Notification(result.ErrorText, 'center', 'warning', 5000);
                            // Reload the page
                             location.reload();
                        }
                    });
        }

    </script>
</asp:Content>
