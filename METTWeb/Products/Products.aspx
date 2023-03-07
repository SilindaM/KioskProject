﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="MEWeb.Products.Products"%>

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
           {
               var PanelContainer = MainHDiv.Helpers.DivC("col-md-12 p-n-lr");
               {
                   var HomeContainer = PanelContainer.Helpers.DivC("tabs-container");
                   {
                       var AssessmentsTab = HomeContainer.Helpers.TabControl();
                       {
                           AssessmentsTab.Style.ClearBoth();
                           AssessmentsTab.AddClass("nav nav-tabs");

                           var HomeContainerTab = AssessmentsTab.AddTab("Products Table");

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
                                               CardTitleDiv.Helpers.HTML().Heading5("Products List");
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
                                                       var ProductSection = ColContentDiv.Helpers.BootstrapTableFor<MELib.Products.Product>((c) => c.ProductList, false, false, "");
                                                       var CartList = ColContentDiv.Helpers.BootstrapTableFor<MELib.Carts.Cart>((ca) => ca.Cart, false, false, "");
                                                       {

                                                           var ProductListRow = ProductSection.FirstRow;
                                                           var CartListRow = CartList.FirstRow;
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
                                                                   var Quantity = CartQuantity.Helpers.EditorFor(c => c.ProductQuantity);
                                                                   Quantity.Style.Width = "40px";
                                                               }

                                                               var Action = ProductListRow.AddColumn("Purchase");
                                                               {
                                                                   var BuyBtn = Action.Helpers.Button("Add To Basket", Singular.Web.ButtonMainStyle.Success, Singular.Web.ButtonSize.Tiny, Singular.Web.FontAwesomeIcon.cart_arrow_down);
                                                                   {
                                                                       BuyBtn.AddBinding(Singular.Web.KnockoutBindingString.click, "AddToBasket($data)");
                                                                       BuyBtn.AddClass("btn btn-primary btn-outline margin-to-10");
                                                                       Action.Style.Width = "60px";
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

                                       var AnotherCardDivs = RowColRight.Helpers.DivC("ibox float-e-margins paddingBottom");
                                       {
                                           var CardTitleDivs = AnotherCardDivs.Helpers.DivC("ibox-title");
                                           {
                                               CardTitleDivs.Helpers.HTML("<i class='ffa-lg fa-fw pull-left'></i>");
                                               CardTitleDivs.Helpers.HTML().Heading5("Filter Criteria");
                                           }
                                           var CardTitleToolsDivs = CardTitleDivs.Helpers.DivC("ibox-tools");
                                           {
                                               var aToolsTag = CardTitleToolsDivs.Helpers.HTMLTag("a");
                                               aToolsTag.AddClass("collapse-link");
                                               {
                                                   var iToolsTag = aToolsTag.Helpers.HTMLTag("i");
                                                   iToolsTag.AddClass("fa fa-chevron-up");
                                               }
                                           }
                                           var ContentDivs = AnotherCardDivs.Helpers.DivC("ibox-content");
                                           {
                                               var RightRowContentDiv = ContentDivs.Helpers.DivC("row");
                                               {
                                                   var RightColContentDiv = RightRowContentDiv.Helpers.DivC("col-md-12");
                                                   {
                                                       var ViewCart = RightColContentDiv.Helpers.Button("View Cart", Singular.Web.ButtonMainStyle.Primary, Singular.Web.ButtonSize.Normal, Singular.Web.FontAwesomeIcon.shoppingBasket);
                                                       {
                                                           
                                                                   ViewCart.Style.Width = "100%";
                                                                   ViewCart.AddClass("form-control marginBottom20 ");
                                                           var ViewCartButton =  ViewCart.Helpers.Button("Reset", Singular.Web.ButtonMainStyle.Primary, Singular.Web.ButtonSize.Normal, Singular.Web.FontAwesomeIcon.None);
                                                           {
                                                                   ViewCartButton.AddBinding(Singular.Web.KnockoutBindingString.click, "ViewBasket()");
                                                                   ViewCartButton.AddClass("btn btn-success");
                                                                   ViewCartButton.Style.Width = "100%";
                                                                   ViewCartButton.AddClass("form-control marginBottom20 ");
                                                           }
                                                       }

                                                       var ReleaseFromDateLAbel = RightColContentDiv.Helpers.LabelFor(c => ViewModel.ProductCategoryId);
                                                       var ReleaseFromDateEditor = RightColContentDiv.Helpers.EditorFor(c => ViewModel.ProductCategoryId);
                                                       ReleaseFromDateEditor.AddClass("form-control marginBottom20 ");

                                                       var FilterBtn = RightColContentDiv.Helpers.Button("Apply Filter", Singular.Web.ButtonMainStyle.Primary, Singular.Web.ButtonSize.Normal, Singular.Web.FontAwesomeIcon.None);
                                                       {
                                                           FilterBtn.AddBinding(Singular.Web.KnockoutBindingString.click, "FilterMo($data)");
                                                           FilterBtn.AddClass("btn btn-primary btn-outline");
                                                       }
                                                       var ResetBtn = RightColContentDiv.Helpers.Button("Reset", Singular.Web.ButtonMainStyle.Primary, Singular.Web.ButtonSize.Normal, Singular.Web.FontAwesomeIcon.None);
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
       }
  %>
 
  <script type="text/javascript">
    // Place page specific JavaScript here or in a JS file and include in the HeadContent section
    Singular.OnPageLoad(function () {
      $("#menuItem1").addClass('active');
      $("#menuItem1 > ul").addClass('in');
    });

      var AddToBasket = function (obj) {
          ViewModel.CallServerMethod("AddToBasket", { ProductID: obj.ProductID(), productCount: obj.ProductQuantity(),ProductList: ViewModel.ProductList.Serialise() ,ShowLoadingBar: true }, function (result) {
              if (result.Success) {
                  alert('Added To Cart Successfully');
                   Singular.AddMessage(3, 'Save', 'Added Successfully.').Fade(2000);
              }
              else {
                  Singular.AddMessage(1, 'Error', result.ErrorText).Fade(2000);
                  // MEHelpers.Notification(result.ErrorText, 'center', 'warning', 5000);
              }
          });
      }
      var ViewBasket = function () {
          window.location = '../Carts/Cart.aspx';
      }
      var FilterProducts = function (obj) {
      ViewModel.CallServerMethod('FilterProducts', { ProductCategoryId: obj.ProductCategoryId(), ShowLoadingBar: true }, function (result) {
        if (result.Success) {
          MEHelpers.Notification("Movies filtered successfully.", 'center', 'info', 1000);
          ViewModel.MovieList.Set(result.Data);
        }
        else {
          MEHelpers.Notification(result.ErrorText, 'center', 'warning', 5000);
        }
      })
    };
      
  </script>

</asp:Content>