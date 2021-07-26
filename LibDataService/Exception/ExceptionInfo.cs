// // ///-----------------------------------------------------------------
// // ///   Class:          ExceptionInfo.cs
// // ///   Description:    <Description>
// // ///   Author:         ahc                     Date: 20.11.2017
// // ///   Company:        
// // ///   Notes:          <Notes>
// // ///   Revision History:
// // ///-----------------------------------------------------------------
using System;
using Newtonsoft.Json.Linq;

namespace LibDataService.Exception
{
	/// <summary>
	/// Maybe this class should not be extensible. 
	/// The original developer is not sure, if it was important about declaring the class with "sealed". 
	/// Ask "dir" later, when he is still at work and able to invoke his memories :)  
	/// </summary>
	public sealed class ExceptionInfo
	{

		#region Consts

		public const string DefaultReason = "Unknown";
		public const string DefaultReasonCode = "UnknownCode";
		public const string DefaultInternalMessage = "No message found";

		#endregion

		#region Properties

		public string Reason { get; set; }
		public string ReasonCode { get; set; }
		public string InternalMessage { get; set; }
		public ExceptionType Criticality { get; set; }

		#endregion

		#region Constructor

		public ExceptionInfo(ExceptionType criticality = ExceptionType.Unexpectedly)
		{
			Reason = DefaultReason;
			ReasonCode = DefaultReasonCode;
			InternalMessage = DefaultInternalMessage;
			Criticality = ExceptionType.Unexpectedly;

		}

		public ExceptionInfo(string reason = DefaultReason, string reasonCode = DefaultReasonCode, string internalMessage = DefaultInternalMessage, ExceptionType criticality = ExceptionType.Unexpectedly)
		{
			Reason = reason;
			ReasonCode = reasonCode;
			InternalMessage = internalMessage;
			Criticality = criticality;
		}

		#endregion

		#region Public Methods

		public JObject ToJson()
		{
			JObject jOb = new JObject();
			jOb["Reason"] = Reason;
			jOb["ReasonCode"] = ReasonCode;
			jOb["InternalMessage"] = InternalMessage;
			jOb["ExceptionType"] = Criticality.ToString();
			return jOb;
		}

		public ExceptionInfo(JObject jInfo)
		{
			Reason = (string)jInfo["Reason"];
			ReasonCode = (string)jInfo["ReasonCode"];
			InternalMessage = (string)jInfo["InternalMessage"];
			String vCriti = (string)jInfo["ExceptionType"];
			switch (vCriti) {
				case "Warning":
					Criticality = ExceptionType.Warning;
					break;
				case "NoFatal":
					Criticality = ExceptionType.NoFatal;
					break;
				case "Fatal":
					Criticality = ExceptionType.Fatal;
					break;
				case "Unexpectedly":
					Criticality = ExceptionType.Unexpectedly;
					break;
				default:
					Criticality = ExceptionType.Unexpectedly;
					break;
			}
		}

		#endregion
	}
}
