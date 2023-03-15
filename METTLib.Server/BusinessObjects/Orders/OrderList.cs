﻿// Generated 08 Mar 2023 10:58 - Singular Systems Object Generator Version 2.2.694
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


namespace MELib.Orders
{
    [Serializable]
    public class OrderList
     : SingularBusinessListBase<OrderList, Order>
    {
        #region " Business Methods "

        public Order GetItem(int OrderID)
        {
            foreach (Order child in this)
            {
                if (child.OrderID == OrderID)
                {
                    return child;
                }
            }
            return null;
        }

        public override string ToString()
        {
            return "Orders";
        }

        #endregion

        #region " Data Access "

        [Serializable]
        public class Criteria
          : CriteriaBase<Criteria>
        {
            public int? OrderTypeID = null;
            public int? UserID = null;
            public Criteria()
            {
            }

        }

        public static OrderList NewOrderList()
        {
            return new OrderList();
        }

        public OrderList()
        {
            // must have parameter-less constructor
        }

        public static OrderList GetOrderList()
        {
            return DataPortal.Fetch<OrderList>(new Criteria());
        }
        public static OrderList GetOrderByType(int OrderTypeId)
        {
            return DataPortal.Fetch<OrderList>(new Criteria { OrderTypeID = OrderTypeId });
        }
        public static OrderList GetOrderByUserId(int UserId)
        {
            return DataPortal.Fetch<OrderList>(new Criteria { UserID = UserId });
        }

        protected void Fetch(SafeDataReader sdr)
        {
            this.RaiseListChangedEvents = false;
            while (sdr.Read())
            {
                this.Add(Order.GetOrder(sdr));
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
                        cm.CommandText = "GetProcs.getOrderList";
                        cm.Parameters.AddWithValue("@OrderTypeID", Singular.Misc.NothingDBNull(crit.OrderTypeID));
                        cm.Parameters.AddWithValue("@UserID", Singular.Misc.NothingDBNull(crit.UserID));
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