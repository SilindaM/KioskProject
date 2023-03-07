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

            //new Transaction
            MELib.Transactions.Transaction newTransaction = MELib.Transactions.Transaction.NewTransaction();

            //new Transaction List
            MELib.Transactions.TransactionList transactions = MELib.Transactions.TransactionList.NewTransactionList();

            Result result = new Result();
            var currentUser = MELib.Accounts.AccountList.GetAccountByID(Singular.Security.Security.CurrentIdentity.UserID).FirstOrDefault();
            currentUser.UserID = Singular.Security.Security.CurrentIdentity.UserID;
            currentUser.Balance += Account.FirstOrDefault().Balance;


            //create a new Transaction
            // create new transaction
            newTransaction.TransactionTypeID = 2;
            newTransaction.UserID = currentUser.UserID;
            newTransaction.CurrentBalance = currentUser.Balance;
            newTransaction.NewBalance = currentUser.Balance + Account.FirstOrDefault().Balance;
            //save the new transaction
            transactions.Add(newTransaction);
            transactions.Save();

            //save the user Account

            currentUser.TrySave(typeof(AccountList));

            return result;

        }


    }
}

