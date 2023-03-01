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
    public class Cart
     : SingularBusinessBase<Cart>
    {
        #region " Properties and Methods "

        #region " Properties "

        public static PropertyInfo<int> CartIDProperty = RegisterProperty<int>(c => c.CartID, "ID", 0);
        /// <summary>
        /// Gets the ID value
        /// </summary>
        [Display(AutoGenerateField = false), Key]
        public int CartID
        {
            get { return GetProperty(CartIDProperty); }
        }

        public static PropertyInfo<DateTime> DateCreatedProperty = RegisterProperty<DateTime>(c => c.DateCreated, "Date Created");
        /// <summary>
        /// Gets and sets the Date Created value
        /// </summary>
        [Display(Name = "Date Created", Description = ""),
        Required(ErrorMessage = "Date Created required")]
        public DateTime DateCreated
        {
            get
            {
                return GetProperty(DateCreatedProperty);
            }
            set
            {
                SetProperty(DateCreatedProperty, value);
            }
        }

        public static PropertyInfo<Boolean> IsActiveIndProperty = RegisterProperty<Boolean>(c => c.IsActiveInd, "Is Active", false);
        /// <summary>
        /// Gets and sets the Is Active value
        /// </summary>
        [Display(Name = "Is Active", Description = ""),
        Required(ErrorMessage = "Is Active required")]
        public Boolean IsActiveInd
        {
            get { return GetProperty(IsActiveIndProperty); }
            set { SetProperty(IsActiveIndProperty, value); }
        }

        public static PropertyInfo<int> ModifiedByProperty = RegisterProperty<int>(c => c.ModifiedBy, "Modified By", 0);
        /// <summary>
        /// Gets the Modified By value
        /// </summary>
        [Display(AutoGenerateField = false)]
        public int ModifiedBy
        {
            get { return GetProperty(ModifiedByProperty); }
        }

        public static PropertyInfo<DateTime?> DateModifiedProperty = RegisterProperty<DateTime?>(c => c.DateModified, "Date Modified");
        /// <summary>
        /// Gets and sets the Date Modified value
        /// </summary>
        [Display(Name = "Date Modified", Description = "")]
        public DateTime? DateModified
        {
            get
            {
                return GetProperty(DateModifiedProperty);
            }
            set
            {
                SetProperty(DateModifiedProperty, value);
            }
        }

        public static PropertyInfo<int> CreatedByProperty = RegisterProperty<int>(c => c.CreatedBy, "Created By", 0);
        /// <summary>
        /// Gets the Created By value
        /// </summary>
        [Display(AutoGenerateField = false)]
        public int CreatedBy
        {
            get { return GetProperty(CreatedByProperty); }
        }

        public static PropertyInfo<DateTime?> DeleteDateProperty = RegisterProperty<DateTime?>(c => c.DeleteDate, "Delete Date");
        /// <summary>
        /// Gets and sets the Delete Date value
        /// </summary>
        [Display(Name = "Delete Date", Description = "")]
        public DateTime? DeleteDate
        {
            get
            {
                return GetProperty(DeleteDateProperty);
            }
            set
            {
                SetProperty(DeleteDateProperty, value);
            }
        }

        public static PropertyInfo<int> DeleteByProperty = RegisterProperty<int>(c => c.DeleteBy, "Delete By", 0);
        /// <summary>
        /// Gets and sets the Delete By value
        /// </summary>
        [Display(Name = "Delete By", Description = "")]
        public int DeleteBy
        {
            get { return GetProperty(DeleteByProperty); }
            set { SetProperty(DeleteByProperty, value); }
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

        public static PropertyInfo<Decimal> TotalAmountProperty = RegisterProperty<Decimal>(c => c.TotalAmount, "Total Amount", 0D);
        /// <summary>
        /// Gets and sets the Total Amount value
        /// </summary>
        [Display(Name = "Total Amount", Description = ""),
        Required(ErrorMessage = "Total Amount required")]
        public Decimal TotalAmount
        {
            get { return GetProperty(TotalAmountProperty); }
            set { SetProperty(TotalAmountProperty, value); }
        }

        public static PropertyInfo<int?> UserIDProperty = RegisterProperty<int?>(c => c.UserID, "User", null);
        /// <summary>
        /// Gets and sets the User value
        /// </summary>
        [Display(Name = "User", Description = ""),
        Required(ErrorMessage = "User required")]
        public int? UserID
        {
            get { return GetProperty(UserIDProperty); }
            set { SetProperty(UserIDProperty, value); }
        }

        #endregion

        #region " Methods "

        protected override object GetIdValue()
        {
            return GetProperty(CartIDProperty);
        }

        public override string ToString()
        {
            if (this.CartID.ToString().Length == 0)
            {
                if (this.IsNew)
                {
                    return String.Format("New {0}", "Cart");
                }
                else
                {
                    return String.Format("Blank {0}", "Cart");
                }
            }
            else
            {
                return this.CartID.ToString();
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
            // Set any variables here, not in the constructor or NewCart() method.
        }

        public static Cart NewCart()
        {
            return DataPortal.CreateChild<Cart>();
        }

        public Cart()
        {
            MarkAsChild();
        }

        internal static Cart GetCart(SafeDataReader dr)
        {
            var c = new Cart();
            c.Fetch(dr);
            return c;
        }

        protected void Fetch(SafeDataReader sdr)
        {
            using (BypassPropertyChecks)
            {
                int i = 0;
                LoadProperty(CartIDProperty, sdr.GetInt32(i++));
                LoadProperty(DateCreatedProperty, sdr.GetValue(i++));
                LoadProperty(IsActiveIndProperty, sdr.GetBoolean(i++));
                LoadProperty(ModifiedByProperty, sdr.GetInt32(i++));
                LoadProperty(DateModifiedProperty, sdr.GetValue(i++));
                LoadProperty(CreatedByProperty, sdr.GetInt32(i++));
                LoadProperty(DeleteDateProperty, sdr.GetValue(i++));
                LoadProperty(DeleteByProperty, sdr.GetInt32(i++));
                LoadProperty(QuantityProperty, sdr.GetInt32(i++));
                LoadProperty(TotalAmountProperty, sdr.GetDecimal(i++));
                LoadProperty(UserIDProperty, Singular.Misc.ZeroNothing(sdr.GetInt32(i++)));
            }

            MarkAsChild();
            MarkOld();
            BusinessRules.CheckRules();
        }

        protected override Action<SqlCommand> SetupSaveCommand(SqlCommand cm)
        {
            LoadProperty(ModifiedByProperty, Settings.CurrentUser.UserID);

            AddPrimaryKeyParam(cm, CartIDProperty);

            cm.Parameters.AddWithValue("@DateCreated", DateCreated);
            cm.Parameters.AddWithValue("@IsActiveInd", GetProperty(IsActiveIndProperty));
            cm.Parameters.AddWithValue("@ModifiedBy", GetProperty(ModifiedByProperty));
            cm.Parameters.AddWithValue("@DateModified", Singular.Misc.NothingDBNull(DateModified));
            cm.Parameters.AddWithValue("@DeleteDate", Singular.Misc.NothingDBNull(DeleteDate));
            cm.Parameters.AddWithValue("@DeleteBy", GetProperty(DeleteByProperty));
            cm.Parameters.AddWithValue("@Quantity", GetProperty(QuantityProperty));
            cm.Parameters.AddWithValue("@TotalAmount", GetProperty(TotalAmountProperty));
            cm.Parameters.AddWithValue("@UserID", GetProperty(UserIDProperty));

            return (scm) =>
            {
                // Post Save
                if (this.IsNew)
                {
                    LoadProperty(CartIDProperty, scm.Parameters["@CartID"].Value);
                }
            };
        }

        protected override void SaveChildren()
        {
            // No Children
        }

        protected override void SetupDeleteCommand(SqlCommand cm)
        {
            cm.Parameters.AddWithValue("@CartID", GetProperty(CartIDProperty));
        }

        public static implicit operator int(Cart v)
        {
            throw new NotImplementedException();
        }

        #endregion

    }

}