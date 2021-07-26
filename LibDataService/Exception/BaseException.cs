// ///-----------------------------------------------------------------
// ///   Class:          BaseException
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 04.11.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace LibDataService.Exception
{
	public class BaseException : System.Exception, IException
	{
		#region Attributes

		#endregion

		#region Properties

		ExceptionInfo _exceptionInfo;
		public ExceptionInfo Info => _exceptionInfo;

		private List<IException> _exceptionList;
		/// <summary>
		/// This method return List with all occurred Exception. In case this is the first Exception, this method return 
		/// empty list. Arrangement of Exception follow FIFO. 
		/// </summary>
		/// <returns>The exception stack.</returns>
		public List<IException> ExceptionList => _exceptionList;

		/// <summary>
		/// This method retrun first occurred IException. If no exeption in stack this Instace will returned
		/// </summary>
		/// <returns>IException  first occurred IException. </returns>
		public IException OriginException
		{
			get {
				if(_exceptionList?.Count <= 0) return this; 
				return (IException)_exceptionList.GetRange(_exceptionList.Count - 1, 1);
			}

		}

		/// <summary>
		/// This mehtod custom reason or default
		/// default value is Unknown
		/// </summary>
		/// <returns>String costum Reason or defalt value</returns>
		public string Reason => Info.Reason;

		/// <summary>
		/// This mehtod return custom reasonCode or default Code
		/// default value is UnknownCode
		/// </summary>
		/// <returns>String costum ReasonCode or defalt value</returns>
		public String ReasonCode => Info.ReasonCode;

		/// <summary>
		/// This method return ExceptionType as int Value
		/// ExceptionType {
		/// 	Warning = 1,
		///		NoFatal = 2,
		///		Fatal = 3,
		///		Unexpectedly=4
		/// }
		/// Default value is Unexpectedly=4
		/// 
		/// </summary>
		/// <returns>int ExceptionType as int or default value Unexpectedly=4 </returns>
		public ExceptionType Criticality => Info.Criticality;

		/// <summary>
		/// This method return InternalMessage or default message
		/// </summary>
		/// <returns>String given custom Message or default Message "No message found"</returns>
		public string InternalMessage => Info.InternalMessage;

		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="T:LibDataService.BaseException"/> class.
		/// </summary>
		protected BaseException() : base()
		{
			init();
		}


		public BaseException(String message, System.Exception ex) : base(message, ex)
		{
			init();
			_exceptionInfo.Reason = message;
		}

		public BaseException(string message) : base(message)
		{
			init();
			_exceptionInfo.Reason = message;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:LibDataService.BaseException"/> class.
		/// Add Exception Collection from given IException to this and add given IExeption Instance to collection.
		/// Arrangement of Exception follow FIFO. 
		/// </summary>
		/// <param name="exceptionBefor">Exception befor.</param>
		public BaseException(IException preException)
		{
			init();
			_exceptionList = preException.ExceptionList;
			_exceptionList.Add(preException);
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// This mehtod set default value for all attributes
		/// </summary>
		private void init()
		{
			_exceptionInfo = new ExceptionInfo(ExceptionType.Unexpectedly);
			_exceptionList = new List<IException>();
		}

		#endregion

		#region Public Methods

		public JObject ToJson()
		{
			JObject returnJObject = new JObject();
			returnJObject["Info"] = Info.ToJson();
			returnJObject["ExceptionList"] = JArray.FromObject(_exceptionList);
			returnJObject["StackTrace"] = StackTrace;
			return returnJObject;
		}

		#endregion

	}
}
