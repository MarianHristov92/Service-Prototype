// ///-----------------------------------------------------------------
// ///   Class:          AssetException
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 09.11.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
using System;
using LibDataService.CacheService;
using LibDataService.ConnectionService;
using LibDataService.DataModels.Callback;
using LibDataService.DataModels.Container;
using LibDataService.DataModels.Description;
using LibDataService.DataService;
using LibDataService.Exception;

namespace LibDataService.AssetService
{
	/// <summary>
	/// This Class represent basic IAssetManager implementation.
	/// All Get record request's passed to instance of this class will follow this steps:
	/// - Create AssetProcess Container with record request parametters
	/// - Try to fulfill request with Cache Manager
	/// - - In Case Cache Manager can fulfill request. Notify Observer with result and finish process
	/// - Try to fulfill request with Connection Manager
	/// - - In Case Connection Manager return result. 
	/// - - - Cache Result with Cache Manager
	/// - - - Notify Observer with result and finish process.
	/// - Otherwise notify Observer with error message
	/// All Set record request's passed to instance of this class will follow this steps:
	/// - Create AssetProcess Container with record request parametters
	/// - Try to fulfill request with Connection Manager
	/// - - In Case Connection Manager return result.
	/// - - - If Observer exist, notify with result and finish process.
	/// - - If Observer exist notify with error message and finish process.
	/// - Otherwise finish process. (Fire and forget)
	public class AssetManager : IAssetManager 
	{
		#region Events

		public event EventHandler<DataSuccessEventArgs<IDataContainer>> Success;
		public event EventHandler<DataFailEventArgs<IException>> Fail;

		#endregion

		#region Attributes

		protected ICacheManager _cacheManager;
		protected IConnectionManager _connectionManager;

		#endregion

		#region Constructor

		public AssetManager()
		{
			_cacheManager = new CacheManagerDictonary();
			_connectionManager = new ConnectionManagerFlurl();
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// This method try to fulfill Get record request for static domain. 
		/// For Process discription see  Class discription.
		/// </summary>
		/// <param name="dataDescriptionInstance">RecordEndpot description instance.</param>
		/// <param name="callback">Callback to notify Observer.</param>
		public void RequestData(IDataDescription dataDescriptionInstance, IDataCallback callback)
		{
			try {
				AssetProcessContainer processContainer = new AssetProcessContainer();
                processContainer.ID = dataDescriptionInstance.ID;
                processContainer.Source = dataDescriptionInstance.Source;
				processContainer.ExternCallback = callback;
				processContainer.DataDescriptionInstance = dataDescriptionInstance;
				processContainer.AssetManager = this;
				processContainer.SetActionRead();
				_cacheManager.RequestData(dataDescriptionInstance, processContainer);
			} catch (System.Exception ex) {
				AssetException assetErr = new AssetException(ExceptionInfo.DefaultReason, ex);
				PerformErrorHandling(assetErr, callback);
			}
		}

		/// <summary>
		/// This method try to fulfill Get record request for static domain. 
		/// For Process discription see  Class discription.
		/// </summary>
		/// <param name="dataDescriptionInstance">RecordEndpot description instance.</param>
		/// <param name="callback">Callback to notify Observer.</param>
		public void RequestData<TResult, TParameter>(IDataDescription dataDescriptionInstance, IDataCallback<TResult> callback)
			where TResult : class, IDataContainer
			where TParameter : class, IDataContainer
		{
			try {
				AssetProcessContainer<TResult, TParameter> processContainer = new AssetProcessContainer<TResult, TParameter>();
                processContainer.ID = dataDescriptionInstance.ID;
                processContainer.Source = dataDescriptionInstance.Source;
				processContainer.ExternCallback = callback;
				processContainer.DataDescriptionInstance = dataDescriptionInstance;
				processContainer.AssetManager = this;
				processContainer.SetActionRead();
				_cacheManager.RequestData<TResult,TParameter>(dataDescriptionInstance, processContainer);
			} catch (System.Exception ex) {
				AssetException assetErr = new AssetException(ExceptionInfo.DefaultReason, ex);
				PerformErrorHandling(assetErr, callback);
			}
		}

		/// <summary>
		/// This method try to fulfill Get record request for dynamic domain. 
		/// For Process discription see Class discription.
		/// </summary>
		/// <param name="dataDescriptionInstance">RecordEndpot description instance.</param>
		/// <param name="data"> contains additional indentifiers and parameters for request</param>
		/// <param name="callback">Callback to notify Observer.</param>
		public void RequestData(IDataDescription dataDescriptionInstance, IDataContainer data, IDataCallback callback)
		{
			try {
				AssetProcessContainer processContainer = new AssetProcessContainer();
                processContainer.ID = dataDescriptionInstance.ID;
                processContainer.Source = dataDescriptionInstance.Source;
				processContainer.ExternCallback = callback;
				processContainer.DataDescriptionInstance = dataDescriptionInstance;
				processContainer.Parameter = data;
				processContainer.AssetManager = this;
				processContainer.SetActionRead();
				_cacheManager.RequestData(dataDescriptionInstance, data, processContainer);
			} catch (System.Exception ex) {
				AssetException assetErr = new AssetException(ExceptionInfo.DefaultReason, ex);
				PerformErrorHandling(assetErr, callback);
			}
		}

		/// <summary>
		/// This method try to fulfill Get record request for dynamic domain. 
		/// For Process discription see Class discription.
		/// </summary>
		/// <param name="dataDescriptionInstance">RecordEndpot description instance.</param>
		/// <param name="data"> contains additional indentifiers and parameters for request</param>
		/// <param name="callback">Callback to notify Observer.</param>
		public void RequestData<TResult, TParameter>(IDataDescription dataDescriptionInstance, TParameter data, IDataCallback<TResult> callback)
			where TResult : class, IDataContainer
			where TParameter : class, IDataContainer
		{
			try {
				AssetProcessContainer<TResult, TParameter> processContainer = new AssetProcessContainer<TResult, TParameter>();
                processContainer.ID = dataDescriptionInstance.ID;
                processContainer.Source = dataDescriptionInstance.Source;
				processContainer.ExternCallback = callback;
				processContainer.DataDescriptionInstance = dataDescriptionInstance;
				processContainer.Parameter = data;
				processContainer.AssetManager = this;
				processContainer.SetActionRead();
				_cacheManager.RequestData(dataDescriptionInstance, data, processContainer);
			} catch (System.Exception ex) {
				AssetException assetErr = new AssetException(ExceptionInfo.DefaultReason, ex);
				PerformErrorHandling(assetErr, callback);
			}
		}

		/// <summary>
		/// This method try to fulfill Set record request for static or dynamic domain. (Fire and forget)
		/// For Process discription see Class discription.
		/// </summary>
		/// <param name="dataDescriptionInstance">RecordEndpot description instance.</param>
		/// <param name="data"> contains record to send and may include additional indentifiers and parameters for request</param>
		public void SaveData(IDataDescription dataDescriptionInstance, IDataContainer data)
		{
			try {
                AssetProcessContainer processContainer = new AssetProcessContainer();
                processContainer.ID = dataDescriptionInstance.ID;
                processContainer.Source = dataDescriptionInstance.Source;
                processContainer.ExternCallback = null;
                processContainer.DataDescriptionInstance = dataDescriptionInstance;
				// TODO: Check this command for inconsistency
                processContainer.Parameter = data;
                processContainer.AssetManager = this;
                processContainer.SetActionWrite();
				_connectionManager.SaveData(dataDescriptionInstance, data);
			} catch (System.Exception ex) {
				AssetException assetEx = new AssetException(ExceptionInfo.DefaultReason, ex);
				PerformErrorHandling(assetEx, null);
			}

		}

		/// <summary>
		/// This method try to fulfill Set record request for static or dynamic domain. With Observer notifying 
		/// For Process discription see Class discription.
		/// </summary>
		/// <param name="dataDescriptionInstance">Data description instance.</param>
		/// <param name="data"> contains record to send and may include additional indentifiers and parameters for request</param>
		/// <param name="callback">Callback to notify Observer</param>
		public void SaveData(IDataDescription dataDescriptionInstance, IDataContainer data, IDataCallback callback)
		{
			try {
				AssetProcessContainer processContainer = new AssetProcessContainer();
                processContainer.ID = dataDescriptionInstance.ID;
                processContainer.Source = dataDescriptionInstance.Source;
				processContainer.ExternCallback = callback;
				processContainer.DataDescriptionInstance = dataDescriptionInstance;
				// TODO: Check this command for inconsistency
				processContainer.Parameter = data;
				processContainer.AssetManager = this;
				processContainer.SetActionWrite();
				_connectionManager.SaveData(dataDescriptionInstance, data, processContainer);
			} catch (System.Exception ex) {
				AssetException assetEx = new AssetException(ExceptionInfo.DefaultReason, ex);
				PerformErrorHandling(assetEx, callback);
			}
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// This method will be called after cache process step (AssetProcessContainer.ProcessSteps) complete.
		/// In this method run case distinction for request direction (read,write)
		/// and performe relevant method.
		/// </summary>
		/// <param name="assetProcesContainer">Asset proces container.</param>
		public void PostCachingStep(AssetProcessContainer assetProcessContainer)
		{
			if (assetProcessContainer.HasException()) {
				if (ShouldHandelException(assetProcessContainer.Exception)) {
					PerformErrorHandling(assetProcessContainer.Exception, assetProcessContainer.ExternCallback);
					return;
				}
				assetProcessContainer.Exception = null;
			}

			switch (assetProcessContainer.CurrentAction) {
				case AssetProcessContainer.ActionType.Read:
					PostGetCaching(assetProcessContainer);
					break;
				case AssetProcessContainer.ActionType.Write:
					PostSetCaching(assetProcessContainer);
					break;
				default:
					PostGetCaching(assetProcessContainer);
					break;
			}
		}

		/// <summary>
		/// This method will be called after cache process step (AssetProcessContainer.ProcessSteps) complete.
		/// In this method run case distinction for request direction (read,write)
		/// and performe relevant method.
		/// </summary>
		/// <param name="assetProcesContainer">Asset proces container.</param>
		public void PostCachingStep<TResult, TParameter>(AssetProcessContainer<TResult, TParameter> assetProcessContainer)
			where TResult : class, IDataContainer
			where TParameter : class, IDataContainer
		{
			if (assetProcessContainer.HasException()) {
				if (ShouldHandelException(assetProcessContainer.Exception)) {
					PerformErrorHandling(assetProcessContainer.Exception, assetProcessContainer.ExternCallback);
					return;
				}
				assetProcessContainer.Exception = null;
			}

			switch (assetProcessContainer.CurrentAction) {
				case AssetProcessContainer<TResult, TParameter>.ActionType.Read:
					PostGetCaching(assetProcessContainer);
					break;
				case AssetProcessContainer<TResult, TParameter>.ActionType.Write:
					PostSetCaching<TResult, TParameter>(assetProcessContainer);
					break;
				default:
					PostGetCaching(assetProcessContainer);
					break;
			}
		}

		/// <summary>
		/// This method will called after cache process step completed in case of Get Request.
		/// This method analyse cache process result.  (No special content validation)
		/// If the request could be fulfilled, notify Observer with result and finish the process.
		/// Otherwise swich process step to Cloud and pass the request to Connection Manager.
		/// </summary>
		/// <param name="assetProcesContainer">Asset proces container.</param>
		protected void PostGetCaching(AssetProcessContainer assetProcesContainer)
		{
			IDataCallback externalCallback = assetProcesContainer.ExternCallback;
			if (assetProcesContainer.HasData) {
                Success?.Invoke(this, new DataSuccessEventArgs<IDataContainer>(assetProcesContainer.Result, assetProcesContainer.ID, assetProcesContainer.Source));
				//externalCallback.OnObtainData(assetProcesContainer.Result);
				return;
			}
			assetProcesContainer.SetProcessStepCloud();
			if (assetProcesContainer.Parameter != null) {
				_connectionManager.RequestData(assetProcesContainer.DataDescriptionInstance, assetProcesContainer.Parameter, assetProcesContainer);
			} else {
				_connectionManager.RequestData(assetProcesContainer.DataDescriptionInstance, assetProcesContainer);
			}
		}

		/// <summary>
		/// This method will called after cache process step completed in case of Get Request.
		/// This method analyse cache process result.  (No special content validation)
		/// If the request could be fulfilled, notify Observer with result and finish the process.
		/// Otherwise swich process step to Cloud and pass the request to Connection Manager.
		/// </summary>
		/// <param name="assetProcesContainer">Asset proces container.</param>
		protected void PostGetCaching<TResult, TParameter>(AssetProcessContainer<TResult, TParameter> assetProcesContainer)
			where TResult : class, IDataContainer
			where TParameter : class, IDataContainer
		{
			IDataCallback<TResult> externalCallback = assetProcesContainer.ExternCallback;
			if (assetProcesContainer.HasData) {
                Success?.Invoke(this, new DataSuccessEventArgs<IDataContainer>(assetProcesContainer.Result, assetProcesContainer.ID, assetProcesContainer.Source));
				//externalCallback.OnObtainData(assetProcesContainer.Result);
				return;
			}
			assetProcesContainer.SetProcessStepCloud();
			if (assetProcesContainer.Parameter != null) {
				_connectionManager.RequestData(assetProcesContainer.DataDescriptionInstance, assetProcesContainer.Parameter, assetProcesContainer);
			} else {
				_connectionManager.RequestData<TResult, TParameter>(assetProcesContainer.DataDescriptionInstance, assetProcesContainer);
			}
		}

		/// <summary>
		/// This method will called after cache process step completed in case of Set Request.
		/// !!!!!!! The caching of set request result is currently not provided !!!!!!!!!!
		/// This method analyse cache process result. (No special content validation)
		/// If the request could be fulfilled, notify Observer with result and finish the process.
		/// Otherwise swich process step to Cloud and pass the request to Connection Manager.
		/// </summary>
		/// <param name="assetProcessContainer">Asset proces container.</param>
		protected void PostSetCaching(AssetProcessContainer assetProcessContainer)
		{
			IDataCallback externalCallback = assetProcessContainer.ExternCallback;
			if (assetProcessContainer.HasData) {
                Success?.Invoke(this, new DataSuccessEventArgs<IDataContainer>(assetProcessContainer.Result, assetProcessContainer.ID, assetProcessContainer.Source));
				//externalCallback?.OnObtainData(assetProcessContainer.Result);
				return;
			}               
			//externalCallback.onObtainError ();
			assetProcessContainer.SetProcessStepCloud();
			//TODO hier muss die fehlerrückgbe noch umgesetzt werden
			if (assetProcessContainer.Parameter != null) {
				_connectionManager.RequestData(assetProcessContainer.DataDescriptionInstance, assetProcessContainer.Parameter, assetProcessContainer);
			} else {
				_connectionManager.RequestData(assetProcessContainer.DataDescriptionInstance, assetProcessContainer);
			}
		}

		/// <summary>
		/// This method will called after cache process step completed in case of Set Request.
		/// !!!!!!! The caching of set request result is currently not provided !!!!!!!!!!
		/// This method analyse cache process result. (No special content validation)
		/// If the request could be fulfilled, notify Observer with result and finish the process.
		/// Otherwise swich process step to Cloud and pass the request to Connection Manager.
		/// </summary>
		/// <param name="assetProcessContainer">Asset proces container.</param>
		protected void PostSetCaching<TResult, TParameter>(AssetProcessContainer<TResult, TParameter> assetProcessContainer)
			where TResult : class, IDataContainer
			where TParameter : class, IDataContainer
		{
			IDataCallback<TResult> externalCallback = assetProcessContainer.ExternCallback;
			if (assetProcessContainer.HasData) {
                Success?.Invoke(this, new DataSuccessEventArgs<IDataContainer>(assetProcessContainer.Result, assetProcessContainer.ID, assetProcessContainer.Source));
				//externalCallback?.OnObtainData(assetProcessContainer.Result);
				return;
			}               
			//externalCallback.onObtainError ();
			assetProcessContainer.SetProcessStepCloud();
			//TODO hier muss die fehlerrückgbe noch umgesetzt werden
			if (assetProcessContainer.Parameter != null) {
				_connectionManager.RequestData(assetProcessContainer.DataDescriptionInstance, assetProcessContainer.Parameter, assetProcessContainer);
			} else {
				_connectionManager.RequestData<TResult, TParameter>(assetProcessContainer.DataDescriptionInstance, assetProcessContainer);
			}
		}

		#endregion

		#region Protected Methods

		/// <summary>
		/// This method will be called after cloud process step (AssetProcessContainer.ProcessSteps) completed.
		/// In this method run case distinction for request direction (read,write)
		/// and performe relevant method.
		/// </summary>
		/// <param name="assetProcesContainer">Asset process container.</param>
		public void PostCloudStep(AssetProcessContainer assetProcessContainer)
		{
			if (assetProcessContainer.HasException()) {
				if (ShouldHandelException(assetProcessContainer.Exception)) {
					PerformErrorHandling(assetProcessContainer.Exception, assetProcessContainer.ExternCallback);
					return;
				}
			}
			switch (assetProcessContainer.CurrentAction) {
				case AssetProcessContainer.ActionType.Read:
					PostGetCloud(assetProcessContainer);
					break;
				case AssetProcessContainer.ActionType.Write:
					PostSetCloud(assetProcessContainer);
					break;
				default:
					PostGetCloud(assetProcessContainer);
					break;
			}
		}

		/// <summary>
		/// This method will be called after cloud process step (AssetProcessContainer.ProcessSteps) completed.
		/// In this method run case distinction for request direction (read,write)
		/// and performe relevant method.
		/// </summary>
		/// <param name="assetProcesContainer">Asset process container.</param>
		public void PostCloudStep<TResult, TParameter>(AssetProcessContainer<TResult, TParameter> assetProcessContainer)
			where TResult : class, IDataContainer
			where TParameter : class, IDataContainer
		{
			if (assetProcessContainer.HasException()) {
				if (ShouldHandelException(assetProcessContainer.Exception)) {
					PerformErrorHandling(assetProcessContainer.Exception, assetProcessContainer.ExternCallback);
					return;
				}
			}
			switch (assetProcessContainer.CurrentAction) {
				case AssetProcessContainer<TResult, TParameter>.ActionType.Read:
					PostGetCloud(assetProcessContainer);
					break;
				case AssetProcessContainer<TResult, TParameter>.ActionType.Write:
					PostSetCloud(assetProcessContainer);
					break;
				default:
					PostGetCloud(assetProcessContainer);
					break;
			}
		}

		/// <summary>
		/// This method will called after cloud process step completed in case of Get Request.
		/// This method analyse cloud process result. (has data or not. No special content validation)
		/// If the request could be fulfilled, cache result in Cache Manager (Fire and forget) and
		/// notify Observer with result and finish the process.
		/// Otherwise notify Observer with error and finish the process.
		/// </summary>
		/// <param name="assetProcesContainer">Asset proces container.</param>
		protected void PostGetCloud(AssetProcessContainer assetProcessContainer)
		{
			IDataCallback externalCallback = assetProcessContainer.ExternCallback;
			if (assetProcessContainer.HasData) {
				IDataContainer receivedData = assetProcessContainer.Result;
				IDataDescription dataDesc = assetProcessContainer.DataDescriptionInstance;
				if (assetProcessContainer.Parameter != null) {
					_cacheManager.SaveData(dataDesc, assetProcessContainer.Parameter, receivedData);
					// TODO prüfung was geschet wenn die Domain einen Dynamischen Part hat können infos aus den Object bezogen werden???
				} else {
					_cacheManager.SaveData(dataDesc, receivedData);
					//Versuch zu cachen der antwort aus dem WS für statische domain
				}
                if(Success!=null)
                    Success?.Invoke(this, new DataSuccessEventArgs<IDataContainer>(receivedData, assetProcessContainer.ID, assetProcessContainer.Source));
                else
				    externalCallback?.OnObtainData(receivedData);
			} else {
				IException ex = assetProcessContainer.Exception;
                if(Fail!=null)
                    Fail?.Invoke(this, new DataFailEventArgs<IException>(ex, assetProcessContainer.ID, assetProcessContainer.Source));
                else
				    externalCallback?.OnObtainError(ex);
				//TODO Integration weitergabe des Fehler 
			}
		}

		/// <summary>
		/// This method will called after cloud process step completed in case of Get Request.
		/// This method analyse cloud process result. (has data or not. No special content validation)
		/// If the request could be fulfilled, cache result in Cache Manager (Fire and forget) and
		/// notify Observer with result and finish the process.
		/// Otherwise notify Observer with error and finish the process.
		/// </summary>
		/// <param name="assetProcesContainer">Asset proces container.</param>
		protected void PostGetCloud<TResult, TParameter>(AssetProcessContainer<TResult, TParameter> assetProcessContainer)
			where TResult : class, IDataContainer
			where TParameter : class, IDataContainer
		{
			IDataCallback<TResult> externalCallback = assetProcessContainer.ExternCallback;
			if (assetProcessContainer.HasData) {
				TResult receivedData = assetProcessContainer.Result;
				IDataDescription dataDesc = assetProcessContainer.DataDescriptionInstance;
				if (assetProcessContainer.Parameter != null) {
					_cacheManager.SaveData(dataDesc, assetProcessContainer.Parameter, receivedData);
					// TODO prüfung was geschet wenn die Domain einen Dynamischen Part hat können infos aus den Object bezogen werden???
				} else {
					_cacheManager.SaveData(dataDesc, receivedData);
					//Versuch zu cachen der antwort aus dem WS für statische domain
				}
                if(Success!=null)
                    Success?.Invoke(this, new DataSuccessEventArgs<IDataContainer>(receivedData, assetProcessContainer.ID, assetProcessContainer.Source));
                else
				    externalCallback?.OnObtainData(receivedData);
			} else {
				IException ex = assetProcessContainer.Exception;
				if(Fail!=null)
                    Fail?.Invoke(this, new DataFailEventArgs<IException>(ex, assetProcessContainer.ID, assetProcessContainer.Source));
                else
				    externalCallback?.OnObtainError(ex);
				//TODO Integration weitergabe des Fehler 
			}
		}

		/// <summary>
		/// This method will called after cloud process step completed in case of Set Request.
		/// This method analyse cache process result. (No special content validation)
		/// If the request could be fulfilled, (if Callback is set) notify Observer with result and finish the process.
		/// Otherwise (if Callback is set) notify Observer with error and finish the process.
		/// </summary>
		/// <param name="assetProcesContainer">Asset process container.</param>
		protected void PostSetCloud(AssetProcessContainer assetProcessContainer)
		{
			IDataCallback externalCallback = assetProcessContainer.ExternCallback;
			if (externalCallback == null) return; //Fire and Forget

			if (assetProcessContainer.HasData) {
				IDataContainer receivedData = assetProcessContainer.Result;
                if(Success!=null)
                    Success?.Invoke(this, new DataSuccessEventArgs<IDataContainer>(receivedData, assetProcessContainer.ID, assetProcessContainer.Source));
                else
				    externalCallback?.OnObtainData(receivedData);
			} else {
				IException ex = assetProcessContainer.Exception;
                if(Fail!=null)
                    Fail?.Invoke(this, new DataFailEventArgs<IException>(ex, assetProcessContainer.ID, assetProcessContainer.Source));
                else
				    externalCallback?.OnObtainError(ex);
				//TODO Integration weitergabe des Fehler 
			}
		}

		/// <summary>
		/// This method will called after cloud process step completed in case of Set Request.
		/// This method analyse cache process result. (No special content validation)
		/// If the request could be fulfilled, (if Callback is set) notify Observer with result and finish the process.
		/// Otherwise (if Callback is set) notify Observer with error and finish the process.
		/// </summary>
		/// <param name="assetProcesContainer">Asset process container.</param>
		protected void PostSetCloud<TResult, TParameter>(AssetProcessContainer<TResult, TParameter> assetProcessContainer)
			where TResult : class, IDataContainer
			where TParameter : class, IDataContainer
		{
			IDataCallback<TResult> externalCallback = assetProcessContainer.ExternCallback;
			if (externalCallback == null) return; //Fire and Forget

			if (assetProcessContainer.HasData) {
				TResult receivedData = assetProcessContainer.Result;
                if(Success!=null)
                    Success?.Invoke(this, new DataSuccessEventArgs<IDataContainer>(receivedData, assetProcessContainer.ID));
                else
				    externalCallback?.OnObtainData(receivedData);
			} else {
				IException ex = assetProcessContainer.Exception;
                if(Fail!=null)
                    Fail?.Invoke(this, new DataFailEventArgs<IException>(ex, assetProcessContainer.ID, assetProcessContainer.Source));
                else
				    externalCallback?.OnObtainError(ex);
				//TODO Integration weitergabe des Fehler 
			}
		}

		#endregion

		protected virtual bool CanHandleException(IException exception)
		{
			bool res = false;
			if (exception is CachingException) {
				res = CanHandleCacheException((exception as CachingException));
			}

			//TODO integration von Cloudexception prüfung
			//else if (exception is CloudException) {
			//   res = canHandleCacheException ( ( exception as CachingException ) );
			//}

			else {
				res = false;
			}

			return res;
		}

		protected virtual bool CanHandleCacheException(CachingException exception)
		{

			switch (exception.CachedType) {
				case CachingException.CachedExceptionType.CantCacheNullValue:
				case CachingException.CachedExceptionType.DataInvalid:
				case CachingException.CachedExceptionType.DataNotFound:
					return true;
				case CachingException.CachedExceptionType.Unexpectedly:
					return false;
				default:
					return false;
			}
		}

		protected virtual bool ShouldHandelException(IException exception)
		{
			bool res = false;
			switch (exception.Criticality) {
				case ExceptionType.Warning:
				case ExceptionType.NoFatal:
					res = false;
					break;
				case ExceptionType.Fatal:
				case ExceptionType.Unexpectedly:
				default:
					res = true;
					break;
			}
			return res;
		}

		// TODO add Cloud Exception prüfung
		//protected virtual bool canHandleCloudException ( CloudException exception )
		//{

		//	switch ( exception.CachedType )
		//	{
		//		case CachingException.CachedExceptionType.CantCacheNullValue:
		//		case CachingException.CachedExceptionType.DataInvalid:
		//		case CachingException.CachedExceptionType.DataNotFound:
		//			return true;
		//		case CachingException.CachedExceptionType.Unexpectedly:
		//			return false;
		//		default:
		//			return false;
		//	}
		//}

		protected virtual void PerformErrorHandling(IException exception, IDataCallback callback)
		{
			if (CanHandleException(exception)) {
				if (exception is CachingException) {
					switch ((exception as CachingException).CachedType) {
						case CachingException.CachedExceptionType.DataNotFound:
							// TODO: Implement logic
							break;
						case CachingException.CachedExceptionType.DataInvalid:
							// TODO: Implement logic
							break;
						default:
							// TODO: Implement logic
							break;
					}
				}

				//TODO fall unterscheidung was gemacht werden soll wenn ein behebare fehler auftritt.
			} else {
				if (exception is ConnectionException) {
					if ((exception as ConnectionException).CurrentCall.HttpStatus == null) {
                        if(Fail != null)
                            Fail?.Invoke(this, new DataFailEventArgs<IException>(exception, callback.ID, callback.Source));
                        else
						    callback?.OnObtainError(exception);
					} else {
						throw new AssetException(exception);
					}
				} else {
                    if(Fail != null)
                        Fail?.Invoke(this, new DataFailEventArgs<IException>(exception, callback.ID, callback.Source));
                    else
					    callback?.OnObtainError(exception);
				}
			}
		}

	}

}