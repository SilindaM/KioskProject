using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MELib.Movies;
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
        public int MovieID { get; set; }
        public MovieVM()
        {
        }
        protected override void Setup()
        {
            base.Setup();
            UserMovieList = MELib.Movies.UserMovieList.GetUserMovieList();
        }
        public Result RentNow(int MovieID)
        {
            Result sr = new Result();
            try
            {
                // Check User Balance   
                decimal Price;
                var AccBalance = MELib.Accounts.AccountList.GetAccountList().Select(c => c.Balance).FirstOrDefault();
                Price = Price = MELib.Movies.MovieList.GetMovieList().Where(c => c.MovieID == MovieID).Select(c => c.Price).FirstOrDefault();
                var NewBalance = AccBalance - Price;
                if (AccBalance >= Price)
                {
                    var newBalance = MELib.Accounts.AccountList.GetAccountList(Singular.Security.Security.CurrentIdentity.UserID).FirstOrDefault();
                    newBalance.UserID = Singular.Security.Security.CurrentIdentity.UserID;
                    newBalance.Balance = NewBalance;
                    newBalance.TrySave(typeof(MELib.Accounts.AccountList));
                    //Insert Data into User Movies  
                    MELib.Movies.UserMovie UserMovie = new MELib.Movies.UserMovie();
                    MELib.Movies.UserMovieList UserMovieList = new MELib.Movies.UserMovieList();
                    UserMovie.MovieID = MovieID;
                    UserMovie.UserID = Singular.Security.Security.CurrentIdentity.UserID;
                    UserMovie.WatchedDate = DateTime.Now;
                    UserMovie.IsActiveInd = true;
                    UserMovieList.Add(UserMovie);
                    UserMovieList.TrySave();

                    ////Insert Data in Transactions   
                    //MELib.Accounts.MovieTransaction MovieTransaction = new MELib.Accounts.MovieTransaction();
                    //MELib.Accounts.MovieTransactionList MovieTransactionList = new MELib.Accounts.MovieTransactionList();
                    //MovieTransaction.MovieID = MovieID;
                    //MovieTransaction.UserID = Singular.Security.Security.CurrentIdentity.UserID;
                    //MovieTransaction.TransactionTypeID = 6;
                    //MovieTransaction.TransactionType = "Rent Movie";
                    //MovieTransaction.Amount = Price;
                    //MovieTransaction.IsActiveInd = true;
                    //MovieTransaction.RentalDate = MovieTransaction.CreatedDate;
                    //MovieTransaction.TrySave(typeof(MELib.Accounts.MovieTransactionList));
                    //sr.Success = true;
                    return sr;
                }
                else
                {
                    sr.Success = false;
                    return sr;
                }
            }
            catch (Exception e)
            {
                sr.Data = e.InnerException;
                sr.Success = false;
                return sr;
            }
        }
    }
}