<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RecentlyPurchase.aspx.cs" Inherits="MEWeb.Products.RecentlyPurchase" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitleContent" runat="server">
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
      background : #ffffff;
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
                                    var RowColRight = Row.Helpers.DivC("col-md-12");
                                    {
                                        var AnotherCardDiv = RowColRight.Helpers.DivC("ibox float-e-margins paddingBottom");
                                        {
                                            var CardTitleDiv = AnotherCardDiv.Helpers.DivC("ibox-title");
                                            {
                                                CardTitleDiv.Helpers.HTML("<i class='ffa-lg fa-fw pull-left'></i>");
                                                CardTitleDiv.Helpers.HTML().Heading5("Recently Bought Products");
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
                                                        var MoviesWatchedDiv = ColContentDiv.Helpers.ForEach<MELib.Orders.OrderDetailList>(c => c.OrderDetailList);
                                                        {

                                                            // Using Knockout Binding
                                                            // <img width="16px" height="16px" data-bind="attr:{src: imagePath}" />
                                                            MoviesWatchedDiv.Helpers.HTML("<div class='item'>");
                                                            MoviesWatchedDiv.Helpers.HTML("<img data-bind=\"attr:{src: $data.ProductImage()} \" class='product-border'/>");
                                                            MoviesWatchedDiv.Helpers.HTML("<span data-bind=\"text: $data.ProductName() + '  R' +  $data.Price()\" class='caption'></span>");
                                                            // MoviesWatchedDiv.Helpers.HTML("<span data-bind=\"text: $data.MovieGenreID() \"  class='caption'></span>");

                                                        }
                                                        var WatchBtn = MoviesWatchedDiv.Helpers.Button("Buy Again", Singular.Web.ButtonMainStyle.Primary, Singular.Web.ButtonSize.Normal, Singular.Web.FontAwesomeIcon.None);
                                                        {
                                                            WatchBtn.AddBinding(Singular.Web.KnockoutBindingString.click, "BuyAgain()");
                                                            WatchBtn.AddClass("btn btn-success btn-block outline-none");
                                                        }
                                                        MoviesWatchedDiv.Helpers.HTML("</div>");
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
    %> <script type="text/javascript">
    // Place page specific JavaScript here or in a JS file and include in the HeadContent section
    Singular.OnPageLoad(function () {
      $("#menuItem1").addClass('active');
      $("#menuItem1 > ul").addClass('in');
    });
      var BuyAgain = function () {
                 window.location = '../Products/TopProducts.aspx';
              }
  </script>
</asp:Content>

