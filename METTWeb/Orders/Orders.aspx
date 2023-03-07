<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="MEWeb.Orders.Orders" %>
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
            var AssessmentsTab = HomeContainer.Helpers.TabControl();
            {
              AssessmentsTab.Style.ClearBoth();
              AssessmentsTab.AddClass("nav nav-tabs");
              var HomeContainerTab = AssessmentsTab.AddTab("Parent / Child Tables");
              {
                var Row = HomeContainerTab.Helpers.DivC("row margin0");
                {
                  var RowCol = Row.Helpers.DivC("col-md-12");
                  {
                    RowCol.Helpers.HTML().Heading2("Parent / Child Tables");
                    RowCol.Helpers.HTMLTag("p").HTML = "This page shows an example of a Parent/Child table. Create your tables in your database and use the CSLA extension to create your classes.";
                    RowCol.Helpers.HTMLTag("p").HTML = "Tables used in this example, [Categories] and [SubCategories]. Once your CSLA Parent/Child objects have been created, add the [IsExpanded] property to your parent class e.g. [Categories] in order for the + - column to show on the parent row/category.";
                    RowCol.Helpers.HTML("<pre><code>[Singular.DataAnnotations.AlwaysClean, Singular.DataAnnotations.ExpandOptions(Singular.DataAnnotations.ExpandOptions.RenderChildrenModeType.OnExpand)]");
                    RowCol.Helpers.HTML("<br>public Boolean IsExpanded { get; set; }</code></pre><br>");
                    RowCol.Helpers.HTML().Heading3("Example");
                    #region Categories

                    var CategoriesDiv = RowCol.Helpers.Div();
                    {
                      var Categoies = CategoriesDiv.Helpers.BootstrapTableFor<MELib.Orders.Order>((c) => c.OrderList, false, false, "", true);
                      var CategoriesFirstRow = Categoies.FirstRow;
                      {
                        var CategoryName = CategoriesFirstRow.AddReadOnlyColumn(c => c.OrderAmount);
                        {
                          CategoryName.HeaderText = "Threat Category";
                        }
                      }
                      var SubCategories = Categoies.AddChildTable<MELib.Orders.OrderDetail>((c) => c.Orde, false, false, "");
                      {
                        var SubCategoriesFirstRow = SubCategories.FirstRow;
                        {
                          var SubCategoryName = SubCategoriesFirstRow.AddReadOnlyColumn(c => c.SubCategoryName);
                          {
                          }
                          var SubCategoryDescription = SubCategoriesFirstRow.AddReadOnlyColumn(c => c.SubCategoryDescription);
                          {
                            SubCategoryDescription.HeaderText = "Description";
                          }
                        }
                        var EditCol = SubCategoriesFirstRow.AddColumn("Action");
                        {
                          // Specify the column width - this can be done in various ways, one example below;
                          EditCol.Attributes.Add("style", "width:150px;text-align: center;");

                          // Add Action Buttons Here
                          var EditBtn = EditCol.Helpers.Button("Edit", Singular.Web.ButtonMainStyle.Primary, Singular.Web.ButtonSize.Normal, Singular.Web.FontAwesomeIcon.None);
                          {
                            EditBtn.AddBinding(Singular.Web.KnockoutBindingString.click, "DoSomething($data)");
                            EditBtn.AddClass("btn btn-primary btn-outline");
                          }

                          var DeleteBtn = EditCol.Helpers.Button("Delete", Singular.Web.ButtonMainStyle.Primary, Singular.Web.ButtonSize.Normal, Singular.Web.FontAwesomeIcon.None);
                          {
                            DeleteBtn.AddBinding(Singular.Web.KnockoutBindingString.click, "DoSomething($data)");
                            DeleteBtn.AddClass("btn btn-primary btn-outline");
                          }
                        }

                      }
                    }
                  }
                  #endregion
                }
              }
            }
          }
        }
      }
       }
       %>
</asp:Content>
