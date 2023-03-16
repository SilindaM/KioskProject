﻿// Generated 23 Feb 2023 14:39 - Singular Systems Object Generator Version 2.2.694
//<auto-generated/>
using System;
using Csla;
using Csla.Serialization;
using Csla.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Singular;
using System.Data;
using System.Data.SqlClient;


namespace MELib.Carts
{
    [Serializable]
    public class CartItemList
     : SingularBusinessListBase<CartItemList, CartItem>
    {
        #region " Business Methods "

        public CartItem GetItem(int CartItemID)
        {
            foreach (CartItem child in this)
            {
                if (child.CartItemID == CartItemID)
                {
                    return child;
                }
            }
            return null;
        }

        public override string ToString()
        {
            return "Cart Items";
        }

        #endregion

        #region " Data Access "

        [Serializable]
        public class Criteria
          : CriteriaBase<Criteria>
        {
            public int? ProductId =null;
            public int? CartId = null;
            public int? CartItemId = null;
            public Criteria()
            {
            }
            public Criteria( int? cartId)
            {
                this.CartId = cartId;
            }

        }

        public static CartItemList NewCartItemList()
        {
            return new CartItemList();
        }

        public CartItemList()
        {
            // must have parameter-less constructor
        }

        public static CartItemList GetCartItemList()
        {
            return DataPortal.Fetch<CartItemList>(new Criteria());
        }
        public static CartItemList GetCartItemByProductId(int ProductId)
        {
            return DataPortal.Fetch<CartItemList>(new Criteria { ProductId = ProductId });
        }

        public static CartItemList GetCartItemByCartId(int? CartId)
        {
            return DataPortal.Fetch<CartItemList>(new Criteria { CartId = CartId });
        }
        public static CartItemList GetCartItemByCartItemId(int? CartItemID)
        {
            return DataPortal.Fetch<CartItemList>(new Criteria { CartItemId = CartItemID });
        }

        protected void Fetch(SafeDataReader sdr)
        {
            this.RaiseListChangedEvents = false;
            while (sdr.Read())
            {
                this.Add(CartItem.GetCartItem(sdr));
            }
            this.RaiseListChangedEvents = true;
        }

        protected override void DataPortal_Fetch(Object criteria)
        {
            Criteria crit = (Criteria)criteria;
            using (SqlConnection cn = new SqlConnection(Singular.Settings.ConnectionString))
            {
                cn.Open();
                try
                {
                    using (SqlCommand cm = cn.CreateCommand())
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandText = "GetProcs.getCartItemList";

                        cm.Parameters.AddWithValue("@ProductId", Singular.Misc.NothingDBNull(crit.ProductId));
                        cm.Parameters.AddWithValue("@CartId", Singular.Misc.NothingDBNull(crit.CartId));
                        cm.Parameters.AddWithValue("@CartItemId", Singular.Misc.NothingDBNull(crit.CartItemId));

                        using (SafeDataReader sdr = new SafeDataReader(cm.ExecuteReader()))
                        {
                            Fetch(sdr);
                        }
                    }
                }
                finally
                {
                    cn.Close();
                }
            }
        }

        #endregion

    }

}