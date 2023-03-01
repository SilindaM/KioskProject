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
    public class CartItem
     : SingularBusinessBase<CartItem>
    {
        #region " Properties and Methods "

        #region " Properties "

        public static PropertyInfo<int> CartItemIDProperty = RegisterProperty<int>(c => c.CartItemID, "ID", 0);
        /// <summary>
        /// Gets the ID value
        /// </summary>
        [Display(AutoGenerateField = false), Key]
        public int CartItemID
        {
            get { return GetProperty(CartItemIDProperty); }
        }

        public static PropertyInfo<int?> ProductIdProperty = RegisterProperty<int?>(c => c.ProductId, "Product", null);
        /// <summary>
        /// Gets and sets the Product value
        /// </summary>
        [Display(Name = "Product", Description = ""),
        Required(ErrorMessage = "Product required")]
        public int? ProductId
        {
            get { return GetProperty(ProductIdProperty); }
            set { SetProperty(ProductIdProperty, value); }
        }

        public static PropertyInfo<String> ProductNameProperty = RegisterProperty<String>(c => c.ProductName, "Product Name", "");
        /// <summary>
        /// Gets and sets the Product Name value
        /// </summary>
        [Display(Name = "Product Name", Description = ""),
        StringLength(50, ErrorMessage = "Product Name cannot be more than 50 characters")]
        public String ProductName
        {
            get { return GetProperty(ProductNameProperty); }
            set { SetProperty(ProductNameProperty, value); }
        }

        public static PropertyInfo<String> ProductImageProperty = RegisterProperty<String>(c => c.ProductImage, "Product Image", "");
        /// <summary>
        /// Gets and sets the Product Image value
        /// </summary>
        [Display(Name = "Product Image", Description = ""),
        StringLength(50, ErrorMessage = "Product Image cannot be more than 50 characters")]
        public String ProductImage
        {
            get { return GetProperty(ProductImageProperty); }
            set { SetProperty(ProductImageProperty, value); }
        }

        public static PropertyInfo<String> ProductDescriptionProperty = RegisterProperty<String>(c => c.ProductDescription, "Product Description", "");
        /// <summary>
        /// Gets and sets the Product Description value
        /// </summary>
        [Display(Name = "Product Description", Description = ""),
        StringLength(50, ErrorMessage = "Product Description cannot be more than 50 characters")]
        public String ProductDescription
        {
            get { return GetProperty(ProductDescriptionProperty); }
            set { SetProperty(ProductDescriptionProperty, value); }
        }

        public static PropertyInfo<int?> CartIDProperty = RegisterProperty<int?>(c => c.CartID, "Cart", null);
        /// <summary>
        /// Gets and sets the Cart value
        /// </summary>
        [Display(Name = "Cart", Description = ""),
        Required(ErrorMessage = "Cart required")]
        public int? CartID
        {
            get { return GetProperty(CartIDProperty); }
            set { SetProperty(CartIDProperty, value); }
        }

        public static PropertyInfo<Decimal> PriceProperty = RegisterProperty<Decimal>(c => c.Price, "Price", 0D);
        /// <summary>
        /// Gets and sets the Price value
        /// </summary>
        [Display(Name = "Price", Description = ""),
        Required(ErrorMessage = "Price required")]
        public Decimal Price
        {
            get { return GetProperty(PriceProperty); }
            set { SetProperty(PriceProperty, value); }
        }

        public static PropertyInfo<int> QuantityProperty = RegisterProperty<int>(c => c.Quantity, "Quantity", 0);
        /// <summary>
        /// Gets and sets the Quantity value
        /// </summary>
        [Display(Name = "Quantity", Description = ""),
        Required(ErrorMessage = "Quantity required")]
        public int Quantity
        {
            get { return GetProperty(QuantityProperty); }
            set { SetProperty(QuantityProperty, value); }
        }

        public static PropertyInfo<Decimal> ValueProperty = RegisterProperty<Decimal>(c => c.Value, "Value", 0D);
        /// <summary>
        /// Gets and sets the Value value
        /// </summary>
        [Display(Name = "Value", Description = ""),
        Required(ErrorMessage = "Value required")]
        public Decimal Value
        {
            get { return GetProperty(ValueProperty); }
            set { SetProperty(ValueProperty, value); }
        }

        #endregion

        #region " Methods "

        protected override object GetIdValue()
        {
            return GetProperty(CartItemIDProperty);
        }

        public override string ToString()
        {
            if (this.ProductName.Length == 0)
            {
                if (this.IsNew)
                {
                    return String.Format("New {0}", "Cart Item");
                }
                else
                {
                    return String.Format("Blank {0}", "Cart Item");
                }
            }
            else
            {
                return this.ProductName;
            }
        }

        #endregion

        #endregion

        #region " Validation Rules "

        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();
        }

        #endregion

        #region " Data Access & Factory Methods "

        protected override void OnCreate()
        {
            // This is called when a new object is created
            // Set any variables here, not in the constructor or NewCartItem() method.
        }

        public static CartItem NewCartItem()
        {
            return DataPortal.CreateChild<CartItem>();
        }

        public CartItem()
        {
            MarkAsChild();
        }

        internal static CartItem GetCartItem(SafeDataReader dr)
        {
            var c = new CartItem();
            c.Fetch(dr);
            return c;
        }

        protected void Fetch(SafeDataReader sdr)
        {
            using (BypassPropertyChecks)
            {
                int i = 0;
                LoadProperty(CartItemIDProperty, sdr.GetInt32(i++));
                LoadProperty(ProductIdProperty, Singular.Misc.ZeroNothing(sdr.GetInt32(i++)));
                LoadProperty(ProductNameProperty, sdr.GetString(i++));
                LoadProperty(ProductImageProperty, sdr.GetString(i++));
                LoadProperty(ProductDescriptionProperty, sdr.GetString(i++));
                LoadProperty(CartIDProperty, Singular.Misc.ZeroNothing(sdr.GetInt32(i++)));
                LoadProperty(PriceProperty, sdr.GetDecimal(i++));
                LoadProperty(QuantityProperty, sdr.GetInt32(i++));
                LoadProperty(ValueProperty, sdr.GetDecimal(i++));
            }

            MarkAsChild();
            MarkOld();
            BusinessRules.CheckRules();
        }

        protected override Action<SqlCommand> SetupSaveCommand(SqlCommand cm)
        {
            AddPrimaryKeyParam(cm, CartItemIDProperty);

            cm.Parameters.AddWithValue("@ProductId", GetProperty(ProductIdProperty));
            cm.Parameters.AddWithValue("@ProductName", GetProperty(ProductNameProperty));
            cm.Parameters.AddWithValue("@ProductImage", GetProperty(ProductImageProperty));
            cm.Parameters.AddWithValue("@ProductDescription", GetProperty(ProductDescriptionProperty));
            cm.Parameters.AddWithValue("@CartID", GetProperty(CartIDProperty));
            cm.Parameters.AddWithValue("@Price", GetProperty(PriceProperty));
            cm.Parameters.AddWithValue("@Quantity", GetProperty(QuantityProperty));
            cm.Parameters.AddWithValue("@Value", GetProperty(ValueProperty));

            return (scm) =>
            {
                // Post Save
                if (this.IsNew)
                {
                    LoadProperty(CartItemIDProperty, scm.Parameters["@CartItemID"].Value);
                }
            };
        }

        protected override void SaveChildren()
        {
            // No Children
        }

        protected override void SetupDeleteCommand(SqlCommand cm)
        {
            cm.Parameters.AddWithValue("@CartItemID", GetProperty(CartItemIDProperty));
        }

        #endregion

    }

}