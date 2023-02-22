﻿// Generated 29 Mar 2019 10:47 - Singular Systems Object Generator Version 2.2.694
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

namespace METTLib.Reports
{
	[Serializable]
	public class ROQuestionnaireAnswerSetAuditResultList
	 : METTReadOnlyListBase<ROQuestionnaireAnswerSetAuditResultList, ROQuestionnaireAnswerSetAuditResult>
	{
		#region " Business Methods "

		public ROQuestionnaireAnswerSetAuditResult GetItem(int QuestionnaireGroupID)
		{
			foreach (ROQuestionnaireAnswerSetAuditResult child in this)
			{
				if (child.QuestionnaireGroupID == QuestionnaireGroupID)
				{
					return child;
				}
			}
			return null;
		}

		public override string ToString()
		{
			return "RO Questionnaire Answer Set Audit Results";
		}

		#endregion

		#region " Data Access "

		[Serializable]
		public class Criteria
			: CriteriaBase<Criteria>
		{
			public int QuestionnaireAnswerSetID { get; set; }
			public Criteria()
			{
			}

		}

		public static ROQuestionnaireAnswerSetAuditResultList NewROQuestionnaireAnswerSetAuditResultList()
		{
			return new ROQuestionnaireAnswerSetAuditResultList();
		}

		public ROQuestionnaireAnswerSetAuditResultList()
		{
			// must have parameter-less constructor
		}

		public static ROQuestionnaireAnswerSetAuditResultList GetROQuestionnaireAnswerSetAuditResultList(int QuestionnaireAnswerSetID)
		{
			return DataPortal.Fetch<ROQuestionnaireAnswerSetAuditResultList>(new Criteria {QuestionnaireAnswerSetID = QuestionnaireAnswerSetID });
		}

		protected void Fetch(SafeDataReader sdr)
		{
			this.RaiseListChangedEvents = false;
			this.IsReadOnly = false;
			while (sdr.Read())
			{
				this.Add(ROQuestionnaireAnswerSetAuditResult.GetROQuestionnaireAnswerSetAuditResult(sdr));
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
						cm.CommandText = "GetProcs.getROQuestionnaireAnswerSetAuditResultList";
						cm.Parameters.AddWithValue("@QuestionnaireAnswerSetID", crit.QuestionnaireAnswerSetID);
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