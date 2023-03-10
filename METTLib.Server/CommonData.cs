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
                    return RegisterList<MELib.RO.ROProductCategoryList>(Misc.ContextType.Application, c => ROProductCategoryList, () => { return MELib.RO.ROProductCategoryList.GetROProductCategoryList(); });
                }
            }
            /// <summary>
            /// Gets cached ROMovieGenreList
            /// </summary>
            public RO.ROOrderTypeList ROOrderTypeList
            {
                get
                {
                    return RegisterList<MELib.RO.ROOrderTypeList>(Misc.ContextType.Application, c => ROOrderTypeList, () => { return MELib.RO.ROOrderTypeList.GetROOrderTypeList(); });
                }
            }
            /// <summary>
            /// Gets cached ROMovieGenreList
            /// </summary>
            public RO.TransactionTypeList TransactionTypeList
            {
                get
                {
                    return RegisterList<MELib.RO.TransactionTypeList>(Misc.ContextType.Application, c => TransactionTypeList, () => { return MELib.RO.TransactionTypeList.GetTransactionTypeList(); });
                }
            }

            /// <summary>
            /// Gets cached ROMovieGenreList
            /// </summary>
            public RO.TransactionList TransactionList
            {
                get
                {
                    return RegisterList<MELib.RO.TransactionList>(Misc.ContextType.Application, c => TransactionList, () => { return MELib.RO.TransactionList.GetTransactionList(); });
                }
            }
            /// <summary>
            /// Gets cached ROMovieGenreList
            /// </summary>
            public ProductCategory.ProductCategoryList ProductCategoryList
            {
                get
                {
                    return RegisterList<MELib.ProductCategory.ProductCategoryList>(Misc.ContextType.Application, c => ProductCategoryList, () => { return MELib.ProductCategory.ProductCategoryList.GetProductCategoryList(); });
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
