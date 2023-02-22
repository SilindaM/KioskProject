using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Singular.Web;
using Singular;
using MELib.Accounts;
using MELib.Transactions;

namespace MEWeb.Profile
{
  public partial class DepositFunds : MEPageBase<DepositFundsVM>
  {
  }
    public class DepositFundsVM : MEStatelessViewModel<DepositFundsVM>
    {
        public MELib.Accounts.AccountList DepositAccount { get; set; }
        public MELib.Accounts.Account Deposit { get; set; }
        public MELib.Transactions.TransactionList Transaction { get; set; }
        public int? AccountID { get; set; }

        public decimal Money { get; set; }

        public DepositFundsVM()
        {

        }
        protected override void Setup()
        {
            base.Setup();
            DepositAccount = MELib.Accounts.AccountList.GetAccountList();
            Deposit = DepositAccount.FirstOrDefault();

        }
        [WebCallable]
        public Result SaveBalance(AccountList Account)
        {
            Result result = new Result();
            var currentUser = MELib.Accounts.AccountList.GetAccountByID(Singular.Security.Security.CurrentIdentity.UserID).FirstOrDefault();
            currentUser.UserID = Singular.Security.Security.CurrentIdentity.UserID;
            currentUser.Balance += Account.FirstOrDefault().Balance;
            currentUser.TrySave(typeof(AccountList));
            return result;

        }


    }
}

