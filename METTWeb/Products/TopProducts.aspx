 <%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TopProducts.aspx.cs" Inherits="MEWeb.Products.TopProducts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
  <!-- Add page specific styles and JavaScript classes below -->
  <link href="../Theme/Singular/Custom/home.css" rel="stylesheet" />
  <link href="../Theme/Singular/Custom/customstyles.css" rel="stylesheet" />
  <style>
    .product-border {
      border-radius: 5px;
      border: 2px solid #332626;
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
      border : #000000;
      margin-right : 5px;
      border-radius: 5px;
      margin-left : 5px;
    }
    WatchBtn
    {
      margin-right : 5px;
      margin-left : 5px;

    }
  </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageTitleContent" runat="server">
  <!-- Placeholder not used in this example -->
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
                                                      var MoviesWatchedDiv = ColContentDiv.Helpers.ForEach<MELib.Products.Product>(c => c.ProductList);
                                                      {

                                                          // Using Knockout Binding
                                                          // <img width="16px" height="16px" data-bind="attr:{src: imagePath}" />
                                                          MoviesWatchedDiv.Helpers.HTML("<div class='item'>");
                                                          MoviesWatchedDiv.Helpers.HTML("<img data-bind=\"attr:{src: $data.ProductImageURL()} \" class='product-border'/>");
                                                          MoviesWatchedDiv.Helpers.HTML("<span data-bind=\"text: $data.ProductName() + '  R' +  $data.Price()\" class='caption'></span>");
                                                          // MoviesWatchedDiv.Helpers.HTML("<span data-bind=\"text: $data.MovieGenreID() \"  class='caption'></span>");

                                                      }
                                                      var WatchBtn = MoviesWatchedDiv.Helpers.Button("Add To Cart", Singular.Web.ButtonMainStyle.Primary, Singular.Web.ButtonSize.Normal, Singular.Web.FontAwesomeIcon.shoppingCart);
                                                      {
                                                          WatchBtn.AddBinding(Singular.Web.KnockoutBindingString.click, "AddToBasket($data)");
                                                          WatchBtn.AddClass("btn btn-success btn-block");
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
                                                   var ColContentDiv = RowContentDiv.Helpers.DivC("col-md-12");
                                                   {
                                                               var FilterBtn = ColContentDiv.Helpers.Button("View Cart", Singular.Web.ButtonMainStyle.Primary, Singular.Web.ButtonSize.Normal, Singular.Web.FontAwesomeIcon.cart_arrow_down);
                                                               {
                                                                   FilterBtn.AddBinding(Singular.Web.KnockoutBindingString.click, "ViewCart()");
                                                                   FilterBtn.AddClass("btn btn-primary btn-outline  marginBottom20  btn-block ");
                                                               }
                                                   }
                                                   var MovieGenreContentDiv = RowContentDiv.Helpers.DivC("col-md-12");
                                                   {
                                                       var ReleaseFromDateEditor = MovieGenreContentDiv.Helpers.EditorFor(c => ViewModel.ProductCategoryId);
                                                       ReleaseFromDateEditor.AddClass("form-control marginBottom20 ");

                                                       var FilterBtn = MovieGenreContentDiv.Helpers.Button("Apply Filter", Singular.Web.ButtonMainStyle.Primary, Singular.Web.ButtonSize.Normal, Singular.Web.FontAwesomeIcon.None);
                                                       {
                                                           FilterBtn.AddBinding(Singular.Web.KnockoutBindingString.click, "FilterProducts($data)");
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
      }
  %>
  <script type="text/javascript">
    // Place page specific JavaScript here or in a JS file and include in the HeadContent section
    Singular.OnPageLoad(function () {
      $("#menuItem1").addClass('active');
      $("#menuItem1 > ul").addClass('in');
    });
      
      var AddToBasket = function (obj) {
          ViewModel.CallServerMethod("AddToBasket", { ProductID: obj.ProductID(), productCount: ViewModel.productCount(), ProductList: ViewModel.ProductList.Serialise(), ShowLoadingBar: true }, function (result) {
              if (result.Success) {
                 MEHelpers.Notification("Product Added TO Cart", 'center', 'warning', 5000);
                   Singular.AddMessage(3, 'Save', 'Added Successfully.').Fade(2000);
              }
              else {
                  Singular.AddMessage(1, 'Error', result.ErrorText).Fade(2000);
                 MEHelpers.Notification(result.ErrorText, 'center', 'warning', 5000);
              }
          });
      }
      var ViewCart = function () {
          window.location = '../Carts/Cart.aspx';
      }
      var FilterProducts = function (obj) {
      ViewModel.CallServerMethod('FilterProducts', { ProductCategoryId: obj.ProductCategoryId(), ResetInd: 0, ShowLoadingBar: true }, function (result) {
        if (result.Success) {
            MEHelpers.Notification("Products filtered successfully.", 'center', 'info', 1000);
            ViewModel.ProductList.Set(result.Data);
        }
        else {
          MEHelpers.Notification(result.ErrorText, 'center', 'warning', 5000);
        }
      })
    };

    var FilterReset = function (obj) {
      ViewModel.CallServerMethod('FilterProducts', { ProductCategoryId: obj.ProductCategoryId(), ResetInd: 1, ShowLoadingBar: true }, function (result) {
        if (result.Success) {
          MEHelpers.Notification("Products reset successfully.", 'center', 'info', 1000);
            ViewModel.ProductList.Set(result.Data);
          // Set Drop Down to 'Select'
        }
        else {
          MEHelpers.Notification(result.ErrorText, 'center', 'warning', 5000);
        }
      })
    };

  </script>
</asp:Content>