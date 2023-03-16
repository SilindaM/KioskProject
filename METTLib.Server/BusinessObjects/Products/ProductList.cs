﻿// Generated 15 Mar 2023 15:20 - Singular Systems Object Generator Version 2.2.694
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


namespace MELib.Products
{
    [Serializable]
    public class ProductList
     : SingularBusinessListBase<ProductList, Product>
    {
        #region " Business Methods "

        public Product GetItem(int ProductID)
        {
            foreach (Product child in this)
            {
                if (child.ProductID == ProductID)
                {
                    return child;
                }
            }
            return null;
        }

        public override string ToString()
        {
            return "Products";
        }

        #endregion

        #region " Data Access "

        [Serializable]
        public class Criteria
          : CriteriaBase<Criteria>
        {
            public int? ProductCategoryID = null;
            public int? ProductID = null;
            public bool? IsActiveInd;
            public Criteria()
            {
            }
            public Criteria(int ProductCategoryID)
            {
                ProductCategoryID = ProductCategoryID;
            }

        }

        public static ProductList NewProductList()
        {
            return new ProductList();
        }

        public ProductList()
        {
            // must have parameter-less constructor
        }

        public static ProductList GetProductList()
        {
            return DataPortal.Fetch<ProductList>(new Criteria());
        }
        //get products by category
        public static ProductList GetProductCategory(int? ProductCategoryId)
        {
            return DataPortal.Fetch<ProductList>(new Criteria { ProductCategoryID = ProductCategoryId });
        }


        //get products by id
        public static ProductList GetProductList(int? ProductID)
        {
            return DataPortal.Fetch<ProductList>(new Criteria { ProductID = ProductID });
        }
        //get products by id and is active
        public static ProductList GetProductByCategoryId(int? ProductCategoryId)
        {
            return DataPortal.Fetch<ProductList>(new Criteria { ProductCategoryID = ProductCategoryId });
        }
        
        protected void Fetch(SafeDataReader sdr)
        {
            this.RaiseListChangedEvents = false;
            while (sdr.Read())
            {
                this.Add(Product.GetProduct(sdr));
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
                        cm.CommandText = "GetProcs.getProductList";
                        cm.Parameters.AddWithValue("@ProductID", Singular.Misc.NothingDBNull(crit.ProductID));
                        cm.Parameters.AddWithValue("@ProductCategoryID", Singular.Misc.NothingDBNull(crit.ProductCategoryID));
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