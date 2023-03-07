﻿// Generated 07 Mar 2023 08:34 - Singular Systems Object Generator Version 2.2.694
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
    public class OrderDetailList
     : SingularBusinessListBase<OrderDetailList, OrderDetail>
    {
        #region " Business Methods "

        public OrderDetail GetItem(int OrderDetailId)
        {
            foreach (OrderDetail child in this)
            {
                if (child.OrderDetailId == OrderDetailId)
                {
                    return child;
                }
            }
            return null;
        }

        public override string ToString()
        {
            return "Order Details";
        }

        #endregion

        #region " Data Access "

        [Serializable]
        public class Criteria
          : CriteriaBase<Criteria>
        {
            public Criteria()
            {
            }

        }

        public static OrderDetailList NewOrderDetailList()
        {
            return new OrderDetailList();
        }

        public OrderDetailList()
        {
            // must have parameter-less constructor
        }

        public static OrderDetailList GetOrderDetailList()
        {
            return DataPortal.Fetch<OrderDetailList>(new Criteria());
        }

        protected void Fetch(SafeDataReader sdr)
        {
            this.RaiseListChangedEvents = false;
            while (sdr.Read())
            {
                this.Add(OrderDetail.GetOrderDetail(sdr));
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
                        cm.CommandText = "GetProcs.getOrderDetailList";
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