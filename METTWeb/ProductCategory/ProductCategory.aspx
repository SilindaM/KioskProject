<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductCategory.aspx.cs" Inherits="MEWeb.ProductCategory.ProductCategory" %>
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
                        var Row = HomeContainer.Helpers.DivC("row margin0");
                        {
                            var RowCol = Row.Helpers.DivC("col-md-12");
                            {
                                  RowCol.Helpers.HTML().Heading2("Product Categories");
                            }
                        }

                    }
                }
            }

        }
    %>
</asp:Content>
