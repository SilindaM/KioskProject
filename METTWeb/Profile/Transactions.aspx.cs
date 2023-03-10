using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MELib.Transactions;
using Singular.Web;

namespace MEWeb.Profile
{
  public partial class Transactions : MEPageBase<TransactionsVM>
  {
  }
  public class TransactionsVM : MEStatelessViewModel<TransactionsVM>
  {
        /// <summary>
        /// Gets or sets the Movie Genre ID
        /// </summary>
        [Singular.DataAnnotations.DropDownWeb(typeof(MELib.RO.TransactionTypeList), UnselectedText = "Select", ValueMember = "TransactionTypeID", DisplayMember = "TransactionName")]
        [Display(Name = "TransactionName")]
        public int? TransactionTypeId { get; set; }

        public TransactionList TransactionList { get; set; }
        public MELib.Transactions.TransactionTypeList TransactionTypeList { get; set; }
        public TransactionsVM()
        {

        }
    protected override void Setup()
    {
      base.Setup();
      TransactionList = MELib.Transactions.TransactionList.GetTransactionList();
    }
        [WebCallable]
        public Result FilterTransactions(int TransactionTypeId, int ResetInd)
        {
            Result sr = new Result();
            try
            {
                if (ResetInd == 0)
                {
                    MELib.RO.TransactionList TransactionList = MELib.RO.TransactionList.GetTransactionTransId(TransactionTypeId);
                    sr.Data = TransactionList;
                }
                else
                {
                    MELib.RO.TransactionList TransactionList = MELib.RO.TransactionList.GetTransactionList();
                    sr.Data = TransactionList;
                }
                sr.Success = true;
            }
            catch (Exception e)
            {
                WebError.LogError(e, "Page: LatestReleases.aspx | Method: FilterMovies", $"(int TransactionTypeId, ({TransactionTypeId})");
                sr.Data = e.InnerException;
                sr.ErrorText = "Could not filter movies by category.";
                sr.Success = false;
            }
            return sr;
        }
    }
}

