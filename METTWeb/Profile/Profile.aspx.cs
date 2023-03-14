using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MELib.Movies;
using Singular.Web;

namespace MEWeb.Profile
{
  public partial class Profile : MEPageBase<ProfileVM>
  {
  }
  public class ProfileVM : MEStatelessViewModel<ProfileVM>
  {
        public string LoggedInUserName { get; set; }


        public MELib.Accounts.AccountList UserAccountList { get; set; }
        public MELib.Accounts.Account UserAccount { get; set; }

        

        public MELib.RO.User User { get; set; }
        public MELib.RO.UserList UserList { get; set; }
        public int UserID { get; set; }

        public ProfileVM()
        {

        }
    protected override void Setup()
    {
      base.Setup();
            UserID = Singular.Security.Security.CurrentIdentity.UserID;
            // On page load initiate/set your data/variables and or properties here
            // Should pass in criteria for the specific user that is viewing the page, however using current identity
            //   UserMovieList = MELib.Movies.UserMovieList.GetUserMovieList();
            UserAccountList = MELib.Accounts.AccountList.GetAccountList();
            
            UserList = MELib.RO.UserList.GetROUserListID(UserID);


            //.FirstOrDefault();
            User = UserList.FirstOrDefault();
            UserAccount = UserAccountList.FirstOrDefault();

               
            LoggedInUserName = Singular.Security.Security.CurrentIdentity.UserName;
        }

        /// <summary>
        /// This is a very basic example of a WebCallable method
        /// </summary>
        /// <param name="SomeReferenceID"></param>
        /// <returns></returns>
        [Singular.Web.WebCallable(LoggedInOnly = true)]
        public static Singular.Web.Result GetSomeData(int SomeReferenceID)
        {
            Result sr = new Result();
            try
            {
                // Perform some action here and return the result
                // sr.Data = "";
                sr.Success = true;
            }
            catch (Exception e)
            {
                sr.Data = e.InnerException;
                sr.Success = false;
            }
            return sr;
        }
    }
}

