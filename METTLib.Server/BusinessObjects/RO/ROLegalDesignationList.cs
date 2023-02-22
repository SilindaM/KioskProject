﻿// Generated 29 Jun 2018 11:38 - Singular Systems Object Generator Version 2.2.694
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


namespace METTLib.RO
{
  [Serializable]
  public class ROLegalDesignationList
   : METTReadOnlyListBase<ROLegalDesignationList, ROLegalDesignation>
  {
    #region " Business Methods "

    public ROLegalDesignation GetItem(int LegalDesignationID)
    {
      foreach (ROLegalDesignation child in this)
      {
        if (child.LegalDesignationID == LegalDesignationID)
        {
          return child;
        }
      }
      return null;
    }

    public override string ToString()
    {
      return "RO Legal Designations";
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

    public static ROLegalDesignationList NewROLegalDesignationList()
    {
      return new ROLegalDesignationList();
    }

    public ROLegalDesignationList()
    {
      // must have parameter-less constructor
    }

    public static ROLegalDesignationList GetROLegalDesignationList()
    {
      return DataPortal.Fetch<ROLegalDesignationList>(new Criteria());
    }

    protected void Fetch(SafeDataReader sdr)
    {
      this.RaiseListChangedEvents = false;
      this.IsReadOnly = false;
      while (sdr.Read())
      {
        this.Add(ROLegalDesignation.GetROLegalDesignation(sdr));
      }
      this.IsReadOnly = true;
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
            cm.CommandText = "GetProcs.getROLegalDesignationList";
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