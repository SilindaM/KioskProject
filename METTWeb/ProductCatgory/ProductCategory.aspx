<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductCategory.aspx.cs" Inherits="MEWeb.ProductCategory.ProductCategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
  <script type="text/javascript" src="../Scripts/JSLINQ.js"></script>
  <link href="../Theme/Singular/Custom/home.css" rel="stylesheet" />
  <link href="../Theme/Singular/Custom/customstyles.css" rel="stylesheet" />
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
              var HomeContainerTab = AssessmentsTab.AddTab("Editable Table");
              {
                var Row = HomeContainerTab.Helpers.DivC("row margin0");
                {
                  var RowCol = Row.Helpers.DivC("col-md-12");
                  {
                    RowCol.Helpers.HTML().Heading2("Editable Table");

                    var ProductCategoriesList = RowCol.Helpers.TableFor<MELib.ProductCategory.ProductCategoryList>((c) => c.ProductCategoryList, true, true);
                    {
                      ProductCategoriesList.AddClass("table table-striped table-bordered table-hover");
                      var ProductCategoriesListRow = ProductCategoriesList.FirstRow;
                      {
                                              
                        var ProductCategoryID = ProductCategoriesList.AddColumn(c => c.ProductCategoryID);
                        {
                          ProductCategoryID.Style.Width = "200px";
                        }
                        var ProductCategoryName = ProductCategoriesList.AddColumn(c => c.MovieTitle);
                        {
                          ProductCategoryName.Style.Width = "300px";
                        }
                        var ProductCategoryDescription = ProductCategoriesList.AddColumn(c => c.Description);
                        var CreatedDate = ProductCategoriesList.AddColumn(c => c.CreatedDate);
                        {
                          CreatedDate.Style.Width = "175px";
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
    Singular.OnPageLoad(function () 
    });
  </script>
</asp:Content>
