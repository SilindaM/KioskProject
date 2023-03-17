using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MELib.Accounts;
using Singular.Web;

namespace MEWeb.Movies
{
    public partial class Movie : MEPageBase<MovieVM>
    {
    }
    public class MovieVM : MEStatelessViewModel<MovieVM>
    {

        public MELib.Movies.MovieList MovieList { get; set; }
        public MELib.Movies.UserMovieList UserMovieList { get; set; }
        public MELib.Transactions.TransactionList TransactionList { get; set; }
        public MELib.Transactions.TransactionTypeList TransactionTypeList { get; set; }
        public MELib.RO.TransactionTypeList TransactionTypesList { get; set; }
        


        public int MovieID { get; set; }
        public MovieVM()
        {

        }
        protected override void Setup()
        {
            base.Setup();
            MovieID = System.Convert.ToInt32(Page.Request.QueryString[0]);
            UserMovieList = MELib.Movies.UserMovieList.GetUserMovieList();
            MovieList = MELib.Movies.MovieList.GetMovieList(null, MovieID);
            TransactionTypeList = MELib.Transactions.TransactionTypeList.GetTransactionTypeList();



        }
        [WebCallable]
        public Result RentNow(int MovieID)
        {
            Result result = new Result();
            try
            {
                //get current user
                var currentuser = Singular.Security.Security.CurrentIdentity.UserID;

                // Check User Balance
                decimal Price=0;
                var AccountList = MELib.Accounts.AccountList.GetAccountList(currentuser);
                var userBalance = AccountList.FirstOrDefault();
                
                 Price = MELib.Movies.MovieList.GetMovieList().Where(c => c.MovieID == MovieID).Select(c => c.Price).FirstOrDefault();


                if(userBalance.Balance >= Price)
                {

                    AmountDeduction(currentuser, Price);
                    
                    //Insert Data into User Movies
                    MELib.Movies.UserMovie UserMovie = new MELib.Movies.UserMovie();
                    MELib.Movies.UserMovieList UserMovieList = new MELib.Movies.UserMovieList();

                    UserMovie.MovieID = MovieID;
                    UserMovie.UserID = Singular.Security.Security.CurrentIdentity.UserID;
                    UserMovie.WatchedDate = DateTime.Now;
                    UserMovie.IsActiveInd = true;
                    UserMovieList.Add(UserMovie);
                    UserMovieList.TrySave();



                    //transaction types
                    var TransactionTypesList = MELib.RO.TransactionTypeList.GetTransactionTypeList().ToList();
                    //new Transaction
                    MELib.Transactions.Transaction newTransaction = MELib.Transactions.Transaction.NewTransaction();

                    //new Transaction List
                    MELib.Transactions.TransactionList transactions = MELib.Transactions.TransactionList.NewTransactionList();

                    // create new transaction
                    newTransaction.TransactionTypeID = 1;
                    newTransaction.UserID = currentuser;
                    newTransaction.CurrentBalance = userBalance.Balance;
                    newTransaction.Description = 
                    newTransaction.Description = TransactionTypesList.Select(t => t.TransactionName).FirstOrDefault();
                    newTransaction.CurrentBalance = userBalance.Balance;
                    newTransaction.NewBalance = userBalance.Balance - Price;
                    newTransaction.Amount = Price;
                    transactions.Add(newTransaction);
                    transactions.Save();

                    result.Success = true;
                }
                else
                {
                    result.Success = false;
                    return result;
                }
            }
            catch (Exception e)
            {
                result.Data = e.InnerException;
                result.Success = false;
            }
            return result;
        }
        // method to deduct amount
        [WebCallable]
        public static Result AmountDeduction(int userId, decimal Amount)
        {
            //get the current user 
            var currentUser = MELib.Accounts.AccountList.GetAccountList(userId).FirstOrDefault();
            //  currentUser.UserID = Singular.Security.Security.CurrentIdentity.UserID;

            Result result = new Result();
            try
            {
                //check if the amount to be deducted is greater than account balance
                if (currentUser.Balance >= Amount)
                {
                    currentUser.Balance -= Amount;
                    currentUser.TrySave(typeof(AccountList));
                }
                else
                {
                    result.ErrorText = "Insufficient Amount";
                }
            }
            catch (Exception e)
            {
                result.Data = e.InnerException;
                result.Success = false;

            }
            return result;
        }
    }
}
