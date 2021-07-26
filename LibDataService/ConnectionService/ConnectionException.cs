// ///-----------------------------------------------------------------
// ///   Class:          CaoonectionException
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 09.12.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
using System;
using Flurl.Http;
using System.Net;
using System.Diagnostics;
using LibDataService.Exception;

namespace LibDataService.ConnectionService
{
	public class ConnectionException : BaseException
	{
		#region Properties

		protected HttpCall _currentCall;
		public HttpCall CurrentCall {
			get { return _currentCall; }
			set { _currentCall = value; }
		}

		#endregion

		#region Static Methods

		public static ConnectionException ExceptionFromHttpCall(HttpCall call)
		{
			ConnectionException exeption = new ConnectionException(call);
			exeption.Info.Reason = call.Response.StatusCode.ToString();
			exeption.Info.ReasonCode = ((int)call.Response.StatusCode).ToString();
			exeption.CurrentCall = call;
			return exeption;
		}

		public static ConnectionException ExceptionUnexpectedlye(System.Exception ex)
		{
			ConnectionException exeption = new ConnectionException(ex, HttpStatusCode.Conflict);
			exeption.Info.Reason = HttpStatusCode.Conflict.ToString();
			exeption.Info.ReasonCode = ((int)HttpStatusCode.Conflict).ToString();
			return exeption;
		}

		#endregion

		#region Constructor

		private ConnectionException(IException exceptionbefor) : base(exceptionbefor)
		{
		}

		private ConnectionException(System.Exception exceptionbefor, HttpStatusCode cachedExceptionType) : base(cachedExceptionType.ToString(), exceptionbefor)
		{
			Info.Reason = cachedExceptionType.ToString();
			Info.ReasonCode = ((int)cachedExceptionType).ToString();
		}

		private ConnectionException(HttpStatusCode cachedExceptionType)
		{
		}

		public ConnectionException(HttpCall call)
		{
			CurrentCall = call;
			try {
				switch (call.HttpStatus.Value) {
					case HttpStatusCode.Unauthorized:
						Info.Reason = HttpStatusCode.Unauthorized.ToString();
						Info.Criticality = ExceptionType.NoFatal;
						Info.ReasonCode = ((int)HttpStatusCode.Unauthorized).ToString();
						break;
					case HttpStatusCode.InternalServerError:
						Info.Reason = HttpStatusCode.InternalServerError.ToString();
						Info.Criticality = ExceptionType.NoFatal;
						Info.ReasonCode = ((int)HttpStatusCode.InternalServerError).ToString();
						break;
				}
			} catch (System.Exception e) { Debug.WriteLine(e); }
		}
		#endregion

	}
}



