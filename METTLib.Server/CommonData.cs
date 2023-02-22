using System;
using Singular.CommonData;

namespace MELib
{
  public class CommonData : CommonDataBase<MELib.CommonData.MECachedLists>
  {
    [Serializable]
    public class MECachedLists : CommonDataBase<MECachedLists>.CachedLists
    {
      /// <summary>
      /// Gets cached ROUserList
      /// </summary>
      public MELib.RO.ROUserList ROUserList
      {
        get
        {
          return RegisterList<MELib.RO.ROUserList>(Misc.ContextType.Application, c => c.ROUserList, () => { return MELib.RO.ROUserList.GetROUserList(); });
        }
      }
              /// <summary>
              /// Gets cached ROMovieGenreList
              /// </summary>
              public RO.ROMovieGenreList ROMovieGenreList
              {
                get
                {
                  return RegisterList<MELib.RO.ROMovieGenreList>(Misc.ContextType.Application, c => c.ROMovieGenreList, () => { return MELib.RO.ROMovieGenreList.GetROMovieGenreList(); });
                }
              }
            /// <summary>
            /// Gets cached ROMovieGenreList
            /// </summary>
            public RO.ROProductCategoryList ROProductCategoryList
            {
                get
                {
                    return RegisterList<MELib.RO.ROProductCategoryList>(Misc.ContextType.Application,c => ROProductCategoryList, () => { return MELib.RO.ROProductCategoryList.GetROProductCategoryList(); });
                }
            }
        }
    }

  public class Enums
  {
		public enum AuditedInd
		{
			Yes = 1,
			No = 0
		}
    public enum DeletedInd
    {
      Yes = 1,
      No = 0
    }
  }
}
