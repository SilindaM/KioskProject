<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="MEWeb.Carts.Cart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
                        var currentCart = HomeContainer.Helpers.DivC("col-md-12 text-right");
                        {
                            var btnView = currentCart.Helpers.Button("${`quantity`}", Singular.Web.ButtonMainStyle.NoStyle, Singular.Web.ButtonSize.Normal, Singular.Web.FontAwesomeIcon.shoppingBasket);
                            {
                                btnView.AddClass("btn btn-success btn-outline");
                                btnView.AddBinding(Singular.Web.KnockoutBindingString.click, "ViewBasket()");
                            }
                        }
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
                                                            // var CartListRow = CartList.FirstRow;
                                                            ProductListRow.Style.BackgroundColour = "#1000";

                                                            {
                                                                var ProductTitle = ProductListRow.AddColumn("Product Name");
                                                                {
                                                                    var productNameText = ProductTitle.Helpers.Span(c => c.ProductName);
                                                                    productNameText.Style.FontSize = "15px";
                                                                }
                                                                var ProductDescription = ProductListRow.AddColumn("Description");
                                                                {
                                                                    var ProductDescriptionText = ProductDescription.Helpers.Span(c => c.ProductDescription);
                                                                    ProductDescriptionText.Style.FontSize = "15px";
                                                                }

                                                                var ProductPrice = ProductListRow.AddColumn("Price");
                                                                {
                                                                    var Price = ProductPrice.Helpers.Span(c => "R " + c.Price);
                                                                    Price.Style.FontSize = "15px";
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
                                    {   var ContentDiv = RowColRight.Helpers.DivC("ibox-content");
                                        {
                                            var RightRowContentDiv = ContentDiv.Helpers.DivC("row");
                                            {
                                                var RightColContentDiv = RightRowContentDiv.Helpers.DivC("col-md-12");
                                                {
                                                    //var TotalOrder = RightColContentDiv.Helpers.EditorFor(c => ViewModel.Quantu);
                                                    //TotalOrder.AddClass("form-control marginBottom20 ");

                                                    //var orderQuantity = RightColContentDiv.Helpers.EditorFor(c => ViewModel.ItemQuantity);
                                                    //orderQuantity.AddClass("form-control marginBottom20 ");

                                                    var CompleteBtn = RightColContentDiv.Helpers.Button("Confirm ", Singular.Web.ButtonMainStyle.Primary, Singular.Web.ButtonSize.Normal, Singular.Web.FontAwesomeIcon.save);
                                                    {
                                                        CompleteBtn.AddBinding(Singular.Web.KnockoutBindingString.click, "FilterMovies($data)");
                                                        CompleteBtn.AddClass("btn btn-success btn-outline");
                                                    }
                                                    var cancel = RightColContentDiv.Helpers.Button("Clear ", Singular.Web.ButtonMainStyle.Primary, Singular.Web.ButtonSize.Normal, Singular.Web.FontAwesomeIcon.remove);
                                                    {
                                                        cancel.AddBinding(Singular.Web.KnockoutBindingString.click, "FilterMovies($data)");
                                                        cancel.AddClass("btn btn-primary btn-outline");
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
    </script>
</asp:Content>
