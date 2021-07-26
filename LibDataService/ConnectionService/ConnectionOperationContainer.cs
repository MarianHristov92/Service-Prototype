// ///-----------------------------------------------------------------
// ///   Class:          IConnectionOperationContainer
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 07.12.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Flurl.Http;
using LibDataService.DataModels.Callback;
using LibDataService.DataModels.Description;
using LibDataService.Helper;
using System.Diagnostics;
using LibDataService.DataModels.Container;
using LibDataService.Exception;
using LibDataService.Helper;
using LibDataService.Tools;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Crashes;

namespace LibDataService.ConnectionService
{

	public interface IConnectionOperationContainer {

		string Id { get; }

		void PrepairRequest();

		void PerformRequest();

	}

	/// <summary>
	/// This Class cointans contruction and performing of HttpClient (FlurlClient)
	/// </summary>
	public class ConnectionOperationContainer : IConnectionOperationContainer
	{

		#region Events

		public event QueryRemovedEventHandler RunningQueryRemoved;

		#endregion

		#region Attributes

		HttpContent _payload;

		private Lazy<FlurlClient> _client = new Lazy<FlurlClient>(() => new FlurlClient());
		protected IFlurlClient Client => _client.Value; 

		#endregion

		#region Properties

		public IDataDescription DataDescription { get; set; }
		public IDataContainer Parameter { get; set; }
		public IDataContainer Result { get; set; }

		IDataCallback _callback;
		public IDataCallback Callback { 
			get => _callback; 
			set {
				_callback = value;
			} 
		}

		/// <summary>
		/// this method use DomainHelper to get Domain definition as identifier
		/// </summary>
		/// <returns>The identifier.</returns>
		public string Id => DomainHelper.GetDomainAsKey(DataDescription, Parameter);

		#endregion

		#region Public Methods

		/// <summary>
		/// This method client construction and then 
		/// send request.
		/// </summary>
		public void PerformRequest()
		{
            LogForward("PerformRequest");
            if (ConnectionHelper.HasInternet)
            {
                Task.Factory.StartNew(() =>
                {
                    PrepairRequest();
                    var res = SendRequestAsync();
                    Debug.WriteLine(res.AsyncState);
                });
            }
            else
            {
                IConnectionDescription connectionDesc = (DataDescription as IConnectionDescription);
                (connectionDesc as AbstractDescription).ReportError(this, new ExceptionEventArgs(new OperationCanceledException("The operation was canceled due to no internet connection being present.")));
            }
		}

		#endregion

		#region Protected Methods

        private void LogForward(string msg)
        {
            Tools.Log.ForwardLog?.Invoke(this, new Tools.Log.ForwardEventArgs(this.GetType().Name, msg));
        }

		/// <summary>
		/// this mehtod construct FlurlClient with parameters given 
		/// in instance of IDataDescription buy implementation of
		/// IConnectionDescription (required)
		/// IConnectionAuth (optional)
		/// IConnectionCookie (optional)
		/// IConnectionHeaders (optional)
		/// IConnectionTimeout (optional)
		/// constructed Client will be stored in Property Client
		/// </summary>
		public void PrepairRequest()
		{
            try
            {
                AuthentificationType authType = AuthentificationType.None;
                IConnectionDescription connectionDesc = (DataDescription as IConnectionDescription);
                String targetedUrl;
                /// <summary>
                /// get URL static or with dynamic components
                /// </summary>
                if (connectionDesc.HasParametrizedUrl)
                    targetedUrl = connectionDesc.GetParametrizedUrl(Parameter);
                else
                    targetedUrl = connectionDesc.BaseUrl;

                Client.Url = targetedUrl;
                //Client.Settings.HttpClientFactory = new ModernHttpClientFactory();
                LogForward("Request ID: " + Id + " " + "TargetUrl: " + targetedUrl);

                /// <summary>
                ///if defined  add payload 
                /// </summary>
                if (connectionDesc.HasPayload)
                {
                    if (DataDescription is IConnectionCustomPayload)
                    {
                        _payload = (DataDescription as IConnectionCustomPayload).GetCustomPayload(Parameter);
                    }
                    else
                    {
                        var pairs = connectionDesc.GetPayload(Parameter);
                        _payload = new FormUrlEncodedContent(pairs);
                    }

                    if (_payload != null)
                        LogForward("Request ID: " + Id + " " + "With Payload:" + _payload.ToString());
                }
                /// <summary>
                ///if defined  add autentification type and credentials
                /// </summary>
                if (DataDescription is IConnectionAuth)
                {
                    IConnectionAuth conAuthDescr = (DataDescription as IConnectionAuth);
                    if (conAuthDescr.HasAuthentification)
                    {
                        authType = conAuthDescr.AuthentificationType;
                    }
                    switch (authType)
                    {
                        case AuthentificationType.BasicAuthentification:
                            BasicAuthentification autCredentials = conAuthDescr.GetBasicAuthentification(Parameter);
                            Client.WithBasicAuth(autCredentials.User, autCredentials.Password);
                            LogForward("Request ID: " + Id + " " + "With Basic Auth: " + autCredentials.ToString());
                            break;
                        case AuthentificationType.BearerTokenAuthentification:
                            Client.WithOAuthBearerToken(conAuthDescr.GetBearerToken(Parameter));
                            LogForward("Request ID: " + Id + " " + "With OAuth Bearer Token: " + conAuthDescr.GetBearerToken(Parameter));
                            break;
                        case AuthentificationType.None:
                            break;
                    }
                }

                /// <summary>
                ///if defined  add cookies (static or dynamic dependent DataIn) 
                /// </summary>
                if (DataDescription is IConnectionCookie)
                {
                    IConnectionCookie CookieConnection = (DataDescription as IConnectionCookie);
                    if (CookieConnection.HasCookie())
                    {
                        IList<KeyValuePair<string, string>> cookies = CookieConnection.GetCookies();
                        foreach (KeyValuePair<string, string> cookie in cookies)
                        {
                            Client.WithCookie(cookie.Key, cookie.Value);
                            LogForward("Request ID: " + Id + " " + "Cookie Key: " + cookie.Key + ", Value:" + cookie.Value);
                        }
                    }
                    if (CookieConnection.HasDynamicCookie(Parameter))
                    {
                        IList<KeyValuePair<string, string>> cookies = CookieConnection.GetDynamicCookies(Parameter);
                        if (cookies != null)
                            foreach (KeyValuePair<string, string> cookie in cookies)
                            {
                                Client.WithCookie(cookie.Key, cookie.Value);
                                LogForward("Request ID: " + Id + " " + "Cookie Key: " + cookie.Key + ", Value:" + cookie.Value);
                            }

                    }
                }
                /// <summary>
                ///if defined  add headers (static or dynamic dependent DataIn) 
                /// </summary>
                if (DataDescription is IConnectionHeaders)
                {
                    IConnectionHeaders CookieConnection = (DataDescription as IConnectionHeaders);
                    if (CookieConnection.HasHeader)
                    {

                        List<KeyValuePair<string, string>> headers = CookieConnection.Headers;
                        if (headers != null)
                            foreach (KeyValuePair<string, string> headerItem in headers)
                            {
                                Client.WithHeader(headerItem.Key, headerItem.Value);
                                LogForward("Request ID: " + Id + " " + "Header Key: " + headerItem.Key + ", Value:" + headerItem.Value);
                            }
                    }
                    if (CookieConnection.HasDynamicHeader(Parameter))
                    {
                        List<KeyValuePair<string, string>> headers = CookieConnection.GetDynamicHeaders(Parameter);
                        if (headers != null)
                            foreach (KeyValuePair<string, string> headerItem in headers)
                            {
                                Client.WithHeader(headerItem.Key, headerItem.Value);
                                LogForward("Request ID: " + Id + " " + "Header Key: " + headerItem.Key + ", Value:" + headerItem.Value);
                            }
                    }
                }
                /// <summary>
                /// if defined add custom timeout in sec
                /// </summary>
                if (DataDescription is IConnectionTimeout)
                {
                    int timeOut = (DataDescription as IConnectionTimeout).GetTimeout();
                    Client.WithTimeout(timeOut);
                    LogForward("Request ID: " + Id + " " + "Timeout: " + timeOut);
                }
                else
                    Client.WithTimeout(ConnectionHelper.DefaultTimeout);
            } catch (System.Exception ex) {
                Crashes.TrackError(ex);
                LogForward("Request ID: " + Id + ", Exception while preparing: " + ex.GetType().Name + " - " + ex.Message);
            }
		}

		/// <summary>
		/// this method send asynchron depend on selected RequestType 
		/// the request. Result or Error will be returned to Callback if it is set
		/// after Request copmplete perform delOperationComplete if it is set
		/// </summary>
		protected async Task SendRequestAsync()
		{
			HttpResponseMessage responseMessage = null;
			IConnectionDescription connectionDesc = (DataDescription as IConnectionDescription);
			try
			{
				switch (connectionDesc.RequestType)
				{
					case RequestType.GET:
						responseMessage = await Client.GetAsync();
						break;
					case RequestType.POST:
						responseMessage = await Client.PostAsync(_payload);
						break;
					case RequestType.DELETE:
                        if(_payload==null)
						    responseMessage = await Client.DeleteAsync();
                        else
                            responseMessage = await Client.SendAsync(HttpMethod.Delete, _payload);
						break;
					case RequestType.PUT:
						responseMessage = await Client.PutAsync(_payload);
						break;
					case RequestType.PATCH:
						responseMessage = await Client.PatchAsync(_payload);
						break;
					case RequestType.HEAD:
						responseMessage = await Client.HeadAsync();
						break;
				}
				RunningQueryRemoved?.Invoke(Id, Client);
                connectionDesc.AnalyseResponse(responseMessage, Callback);
			}
			catch (System.Exception e)
			{
				if (e is FlurlHttpException)
				{
					var ex = (e as FlurlHttpException);
					if (ex.Call.HttpStatus == null)
					{
                        var basEx = new ConnectionException(ex.Call);
                        (connectionDesc as AbstractDescription).ReportError(this, new ExceptionConnectionEventArgs(ex, ex.Call));
						Callback.OnObtainError(basEx);
					}
					else
					{
                        (connectionDesc as AbstractDescription).ReportError(this, new ExceptionConnectionEventArgs(ex,ex.Call));
						connectionDesc.AnalyseResponse((e as FlurlHttpException).Call, Callback);
					}
                }else
                {
					(connectionDesc as AbstractDescription).ReportError(this, new ExceptionEventArgs(e));
                }
				RunningQueryRemoved?.Invoke(Id, Client);
                Crashes.TrackError(e);
                LogForward("SendRequestAsync: " + e.GetType().Name + " - " + e.Message);
			}

		}

		#endregion

		#region Private / Inline Methods

		/// <summary>
		/// this method check is IConnectionPriorityProcessing implemented
		/// return false or static or dynamic priority flag
		/// </summary>
		/// <returns><c>true</c>, if priority flag was static or dynamic set, <c>false</c> otherwise.</returns>
		internal bool HasPriorityFlag()
		{
			bool priorityFlag = false;
			if (DataDescription is IConnectionPriorityProcessing)
			{
				IConnectionPriorityProcessing prioCont = (DataDescription as IConnectionPriorityProcessing);
				bool priorityStatic = prioCont.HasPriority();
				bool priorityDyn = prioCont.HasPriority(Parameter);
				if (priorityStatic || priorityDyn)
					priorityFlag = true;
			}
			return priorityFlag;
		}

		#endregion
	}

	/// <summary>
	/// This Class cointans contruction and performing of HttpClient (FlurlClient)
	/// </summary>
	public class ConnectionOperationContainer<TResult, TParameter> : IConnectionOperationContainer
		where TResult : class, IDataContainer
		where TParameter : class, IDataContainer
	{

		#region Events

		public event QueryRemovedEventHandler RunningQueryRemoved;

		#endregion

		#region Attributes

		HttpContent _payload;

		private Lazy<FlurlClient> _client = new Lazy<FlurlClient>(() => new FlurlClient());
		protected IFlurlClient Client => _client.Value; 

		#endregion

		#region Properties

		public IDataDescription DataDescription { get; set; }
		public TParameter Parameter { get; set; }
		public TResult Result { get; set; }

		IDataCallback<TResult> _callback;
		public IDataCallback<TResult> Callback { 
			get => _callback; 
			set {
				_callback = value;
			} 
		}

		/// <summary>
		/// this method use DomainHelper to get Domain definition as identifier
		/// </summary>
		/// <returns>The identifier.</returns>
		public string Id => DomainHelper.GetDomainAsKey(DataDescription, Parameter);

		#endregion

		#region Public Methods

		/// <summary>
		/// This method client construction and then 
		/// send request.
		/// </summary>
		public void PerformRequest()
		{
            LogForward("PerformRequest");
            if (ConnectionHelper.HasInternet)
            {
                Task.Factory.StartNew(() =>
                {
                    PrepairRequest();
                    var res = SendRequestAsync();
                    Debug.WriteLine(res.AsyncState);
                });
            }
            else
            {
                IConnectionDescription connectionDesc = (DataDescription as IConnectionDescription);
                (connectionDesc as AbstractDescription).ReportError(this, new ExceptionEventArgs(new OperationCanceledException("The operation was canceled due to no internet connection being present.")));
            }
        }

		#endregion

		#region Protected Methods

        private void LogForward(string msg)
        {
            Tools.Log.ForwardLog?.Invoke(this, new Tools.Log.ForwardEventArgs(this.GetType().Name, msg));
        }

		/// <summary>
		/// this mehtod construct FlurlClient with parameters given 
		/// in instance of IDataDescription buy implementation of
		/// IConnectionDescription (required)
		/// IConnectionAuth (optional)
		/// IConnectionCookie (optional)
		/// IConnectionHeaders (optional)
		/// IConnectionTimeout (optional)
		/// constructed Client will be stored in Property Client
		/// </summary>
		public void PrepairRequest()
		{
			AuthentificationType authType = AuthentificationType.None;
			IConnectionDescription connectionDesc = (DataDescription as IConnectionDescription);
			String targetedUrl;
			/// <summary>
			/// get URL static or with dynamic components
			/// </summary>
			if (connectionDesc.HasParametrizedUrl)
				targetedUrl = connectionDesc.GetParametrizedUrl(Parameter);
			else
				targetedUrl = connectionDesc.BaseUrl;

			Client.Url = targetedUrl;
			//Client.Settings.HttpClientFactory = new ModernHttpClientFactory();
            LogForward("Request ID: " + Id + " " + "TargetUrl: " + targetedUrl);

			/// <summary>
			///if defined  add payload 
			/// </summary>
			if (connectionDesc.HasPayload)
			{
				if (DataDescription is IConnectionCustomPayload)
				{
					_payload = (DataDescription as IConnectionCustomPayload).GetCustomPayload(Parameter);
				}
				else
				{
					var pairs = connectionDesc.GetPayload(Parameter);
					_payload = new FormUrlEncodedContent(pairs);
				}

                if (_payload != null)
                    LogForward("Request ID: " + Id + " " + "With Payload:" + _payload.ToString());
			}
			/// <summary>
			///if defined  add autentification type and credentials
			/// </summary>
			if (DataDescription is IConnectionAuth)
			{
				IConnectionAuth conAuthDescr = (DataDescription as IConnectionAuth);
				if (conAuthDescr.HasAuthentification)
				{
					authType = conAuthDescr.AuthentificationType;
				}
				switch (authType)
				{
					case AuthentificationType.BasicAuthentification:
						BasicAuthentification autCredentials = conAuthDescr.GetBasicAuthentification(Parameter);
						Client.WithBasicAuth(autCredentials.User, autCredentials.Password);
                        LogForward("Request ID: " + Id + " " + "With Basic Auth: " + autCredentials.ToString());
						break;
					case AuthentificationType.BearerTokenAuthentification:
						Client.WithOAuthBearerToken(conAuthDescr.GetBearerToken(Parameter));
                        LogForward("Request ID: " + Id + " " + "With OAuth Bearer Token: " + conAuthDescr.GetBearerToken(Parameter));
						break;
					case AuthentificationType.None:
						break;
				}
			}

			/// <summary>
			///if defined  add cookies (static or dynamic dependent DataIn) 
			/// </summary>
			if (DataDescription is IConnectionCookie)
			{
				IConnectionCookie CookieConnection = (DataDescription as IConnectionCookie);
				if (CookieConnection.HasCookie())
				{
					IList<KeyValuePair<string, string>> cookies = CookieConnection.GetCookies();
					foreach (KeyValuePair<string, string> cookie in cookies)
					{
						Client.WithCookie(cookie.Key, cookie.Value);
                        LogForward("Request ID: " + Id + " " + "Cookie Key: " + cookie.Key + ", Value:" + cookie.Value);
					}
				}
				if (CookieConnection.HasDynamicCookie(Parameter))
				{
					IList<KeyValuePair<string, string>> cookies = CookieConnection.GetDynamicCookies(Parameter);
					if (cookies != null)
						foreach (KeyValuePair<string, string> cookie in cookies)
					{
						Client.WithCookie(cookie.Key, cookie.Value);
                        LogForward("Request ID: " + Id + " " + "Cookie Key: " + cookie.Key + ", Value:" + cookie.Value);
					}

				}
			}
			/// <summary>
			///if defined  add headers (static or dynamic dependent DataIn) 
			/// </summary>
			if (DataDescription is IConnectionHeaders)
			{
				IConnectionHeaders CookieConnection = (DataDescription as IConnectionHeaders);
				if (CookieConnection.HasHeader)
				{

					List<KeyValuePair<string, string>> headers = CookieConnection.Headers;
					if (headers != null)
						foreach (KeyValuePair<string, string> headerItem in headers)
					{
						Client.WithHeader(headerItem.Key, headerItem.Value);
                        LogForward("Request ID: " + Id + " " + "Header Key: " + headerItem.Key + ", Value:" + headerItem.Value);
					}
				}
				if (CookieConnection.HasDynamicHeader(Parameter))
				{
					List<KeyValuePair<string, string>> headers = CookieConnection.GetDynamicHeaders(Parameter);
					if (headers != null)
						foreach (KeyValuePair<string, string> headerItem in headers)
					{
						Client.WithHeader(headerItem.Key, headerItem.Value);
                        LogForward("Request ID: " + Id + " " + "Header Key: " + headerItem.Key + ", Value:" + headerItem.Value);
					}
				}
			}
            /// <summary>
            /// if defined add custom timeout in sec
            /// </summary>
            if (DataDescription is IConnectionTimeout)
            {
                int timeOut = (DataDescription as IConnectionTimeout).GetTimeout();
                Client.WithTimeout(timeOut);
                LogForward("Request ID: " + Id + " " + "Timeout: " + timeOut);
            }
            else
                Client.WithTimeout(ConnectionHelper.DefaultTimeout);
		}

		/// <summary>
		/// this method send asynchron depend on selected RequestType 
		/// the request. Result or Error will be returned to Callback if it is set
		/// after Request copmplete perform delOperationComplete if it is set
		/// </summary>
		protected async Task SendRequestAsync()
		{
			HttpResponseMessage responseMessage = null;
			IConnectionDescription connectionDesc = (DataDescription as IConnectionDescription);
			try
			{
				switch (connectionDesc.RequestType)
				{
					case RequestType.GET:
						responseMessage = await Client.GetAsync();
						break;
					case RequestType.POST:
						responseMessage = await Client.PostAsync(_payload);
						break;
					case RequestType.DELETE:
                        if (_payload == null)
                            responseMessage = await Client.DeleteAsync();
                        else
                            responseMessage = await Client.SendAsync(HttpMethod.Delete, _payload);
						break;
					case RequestType.PUT:
						responseMessage = await Client.PutAsync(_payload);
						break;
					case RequestType.PATCH:
						responseMessage = await Client.PatchAsync(_payload);
						break;
					case RequestType.HEAD:
						responseMessage = await Client.HeadAsync();
						break;
				}
				RunningQueryRemoved?.Invoke(Id, Client);
                connectionDesc.AnalyseResponse(responseMessage, Callback);
			}
			catch (System.Exception e)
			{
				if (e is FlurlHttpException)
				{
                    var description = (DataDescription as AbstractDescription);
					var ex = (e as FlurlHttpException);
					if (ex.Call.HttpStatus == null)
					{
                        var basEx = new ConnectionException(ex.Call);
                        description.ReportError(this, new ExceptionConnectionEventArgs(ex.Call));
						Callback.OnObtainError(basEx);
					}
					else
					{
                        description.ReportError(this, new  ExceptionConnectionEventArgs(ex.Call));
						connectionDesc.AnalyseResponse((e as FlurlHttpException).Call, Callback);
					}
                }
                else
                {
					(DataDescription as AbstractDescription).ReportError(this,  new ExceptionEventArgs(e));
                }
				RunningQueryRemoved?.Invoke(Id, Client);
                Crashes.TrackError(e);
                LogForward("SendRequestAsync: " + e.GetType().Name + " - " + e.Message);
			}

		}

		#endregion

		#region Private / Inline Methods

		/// <summary>
		/// this method check is IConnectionPriorityProcessing implemented
		/// return false or static or dynamic priority flag
		/// </summary>
		/// <returns><c>true</c>, if priority flag was static or dynamic set, <c>false</c> otherwise.</returns>
		internal bool HasPriorityFlag()
		{
			bool priorityFlag = false;
			if (DataDescription is IConnectionPriorityProcessing)
			{
				IConnectionPriorityProcessing prioCont = (DataDescription as IConnectionPriorityProcessing);
				bool priorityStatic = prioCont.HasPriority();
				bool priorityDyn = prioCont.HasPriority(Parameter);
				if (priorityStatic || priorityDyn)
					priorityFlag = true;
			}
			return priorityFlag;
		}

		#endregion
	}

}