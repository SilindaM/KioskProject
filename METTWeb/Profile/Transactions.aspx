<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Transactions.aspx.cs" Inherits="MEWeb.Profile.Transactions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
  <!-- Add page specific styles and JavaScript classes below -->
  <link href="../Theme/Singular/Custom/home.css" rel="stylesheet" />
  <link href="../Theme/Singular/Custom/customstyles.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageTitleContent" runat="server">
  <!-- Placeholder not used in this example -->
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
  <%
      using (var h = this.Helpers)
      {
      var MainContent = h.DivC("row pad-top-10");
      {
        var MainContainer = MainContent.Helpers.DivC("col-md-12 p-n-lr");
        {
          var PageContainer = MainContainer.Helpers.DivC("tabs-container");
          {
            var PageTab = PageContainer.Helpers.TabControl();
            {
              PageTab.Style.ClearBoth();
              PageTab.AddClass("nav nav-tabs");
              var ContainerTab = PageTab.AddTab("Transaction List");
              {
                var RowContentDiv = ContainerTab.Helpers.DivC("row");
                {
                  var ColContentDiv = RowContentDiv.Helpers.DivC("col-md-9");
                  {
                    var MoviesDiv = ColContentDiv.Helpers.BootstrapTableFor<MELib.RO.Transaction>((c) => c.TransactionList, false, false, "");
                    {
                      var FirstRow = MoviesDiv.FirstRow;
                      {
                       
                        var MovieDescription = FirstRow.AddColumn("Transaction Name");
                        {
                          var MovieDescriptionText = MovieDescription.Helpers.Span(c => c.Description);
                        }
                        var TransactionAmount = FirstRow.AddColumn("Transaction Amount");
                        {
                          var MovieDescriptionText = TransactionAmount.Helpers.Span(c => c.Amount);
                        }
                        var CurrentBalance = FirstRow.AddColumn("Old Balance");
                        {
                          var MovieDescriptionText = CurrentBalance.Helpers.Span(c => c.CurrentBalance);
                        }
                        var NewBalance = FirstRow.AddColumn("New Balance");
                        {
                          var MovieDescriptionText = NewBalance.Helpers.Span(c => c.NewBalance);
                        }
                      }
                    }
                  }
                  var RowColRight = RowContentDiv.Helpers.DivC("col-md-3");
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
                        var RightRowContentDiv = ContentDiv.Helpers.DivC("row");
                        {
                          var RightColContentDiv = RightRowContentDiv.Helpers.DivC("col-md-12");
                          {
                            var ReleaseFromDateEditor = RightColContentDiv.Helpers.EditorFor(c => ViewModel.TransactionTypeId);
                            ReleaseFromDateEditor.AddClass("form-control marginBottom20 ");

                            var FilterBtn = RightColContentDiv.Helpers.Button("Apply Filter", Singular.Web.ButtonMainStyle.Primary, Singular.Web.ButtonSize.Normal, Singular.Web.FontAwesomeIcon.None);
                            {
                              FilterBtn.AddBinding(Singular.Web.KnockoutBindingString.click, "FilterTransactions($data)");
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
         var FilterTransactions = function (obj) {
      ViewModel.CallServerMethod('FilterTransactions', { TransactionTypeId: obj.TransactionTypeId(), ResetInd: 0, ShowLoadingBar: true }, function (result) {
        if (result.Success) {
            MEHelpers.Notification("Products filtered successfully.", 'center', 'info', 1000);
            ViewModel.TransactionList.Set(result.Data);
        }
        else {
          MEHelpers.Notification(result.ErrorText, 'center', 'warning', 5000);
        }
      })
    };

    var FilterReset = function (obj) {
      ViewModel.CallServerMethod('FilterTransactions', { TransactionTypeId: obj.TransactionTypeId(), ResetInd: 1, ShowLoadingBar: true }, function (result) {
        if (result.Success) {
          MEHelpers.Notification("Products reset successfully.", 'center', 'info', 1000);
            ViewModel.TransactionList.Set(result.Data);
          // Set Drop Down to 'Select'
        }
        else {
          MEHelpers.Notification(result.ErrorText, 'center', 'warning', 5000);
        }
      })
    };


  </script>
</asp:Content>
