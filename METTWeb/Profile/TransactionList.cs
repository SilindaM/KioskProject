﻿// Generated 20 Feb 2023 15:45 - Singular Systems Object Generator Version 2.2.694
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


namespace MEWeb.Transactions
{
    [Serializable]
    public class TransactionList
     : SingularBusinessListBase<TransactionList, Transaction>
    {
        #region " Business Methods "

        public Transaction GetItem(int TransactionID)
        {
            foreach (Transaction child in this)
            {
                if (child.TransactionID == TransactionID)
                {
                    return child;
                }
            }
            return null;
        }

        public override string ToString()
        {
            return "Transactions";
        }

        #endregion

        #region " Data Access "

        [Serializable]
        public class Criteria
          : CriteriaBase<Criteria>
        {
            public int? UserID = null;
            public Criteria()
            {
            }

            public Criteria(int userId)
            {
                this.UserID = userId;
            }

        }

        public static TransactionList NewTransactionList()
        {
            return new TransactionList();
        }

        public TransactionList()
        {
            // must have parameter-less constructor
        }

        public static TransactionList GetTransactionList()
        {
            return DataPortal.Fetch<TransactionList>(new Criteria());
        }

        public static TransactionList GetTransactionByUserId(int userID)
        {
            return DataPortal.Fetch<TransactionList>(new Criteria { UserID = userID});
        }

        protected void Fetch(SafeDataReader sdr)
        {
            this.RaiseListChangedEvents = false;
            while (sdr.Read())
            {
                this.Add(Transaction.GetTransaction(sdr));
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
                        cm.CommandText = "GetProcs.getTransactionList";
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