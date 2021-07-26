// ///-----------------------------------------------------------------
// ///   Class:          ACRSettings
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 29.09.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
using System;
using LibDataService.SettingsService;
using LibDataService.DataModels.Callback;
using LibDataService.DataModels.Description;
using LibDataService.DataModels.Container;
using LibDataService.DataModels.Description.Domain;
using LibDataService.Exception;
using LibDataService.CacheService;

namespace LibDataService.DataService
{
	public class DataManager : IDataManager 
	{
		#region Events

		public event EventHandler<DataSuccessEventArgs<IDataContainer>> Success;
        public event EventHandler<DataFailEventArgs<IException>> Fail;
        public event EventHandler<ExceptionEventArgs> ReportError;


        #endregion

        #region Attributes

        protected IDataOperation _assetDataManager;
		protected IDataOperation _settingsManager;
		protected IDataOperation _userDataManager;

		#endregion

		#region Properties

		static IDataManager _instance;
		public static IDataManager Instance => _instance ?? (_instance = new DataManager());

        #endregion

        #region Public static methods

        public static void ResetInstance()
        {
            _instance = null;
        }

        #endregion

        #region Constructor

        private DataManager()
		{
			Init();
		}


        #endregion

        #region Private Methods

        private void Init()
		{
			_assetDataManager = new AssetService.AssetManager();
			_settingsManager = new SettingsService.SettingsManager();
			_userDataManager = new UserService.UserManager();

			_assetDataManager.Success += OnAssetSuccess;
			_assetDataManager.Fail += OnAssetFail;

			_settingsManager.Success += OnSettingsSuccess;
			_settingsManager.Fail += OnSettingsFail;

			_userDataManager.Success += OnUserSuccess;
			_userDataManager.Fail += OnUserFail;

		}

		#endregion

		#region Protected Methods

		protected void PerformErrorHandling(IException e, IDataCallback callback)
		{
            Tools.Log.ForwardLog?.Invoke(this, new Tools.Log.ForwardEventArgs(this.GetType().Name, "PerformErrorHandling"));
            Fail?.Invoke(this, new DataFailEventArgs<IException>(e, callback.ID, callback.Source));
			callback?.OnObtainError(e);
		}

		protected void PerformErrorHandling<TResult>(IException e, IDataCallback<TResult> callback)
			where TResult : class, IDataContainer
		{
            Tools.Log.ForwardLog?.Invoke(this, new Tools.Log.ForwardEventArgs(this.GetType().Name, "PerformErrorHandling<TResult>"));
            Fail?.Invoke(this, new DataFailEventArgs<IException>(e, callback.ID, callback.Source));
			callback?.OnObtainError(e);
		}

		protected IDataOperation SelectManager(IDataDescription dataDescriptionInstance)
		{
			String domain = (dataDescriptionInstance as IDomainConstruction).GetDomain();
			IDataOperation selectedManager = default(IDataOperation);
			if (dataDescriptionInstance is ISettingsComplexData) {
				selectedManager = _settingsManager;
			} else if (dataDescriptionInstance is ICacheData) {
                Tools.Log.ForwardLog?.Invoke(this, new Tools.Log.ForwardEventArgs(this.GetType().Name, "SelectManager: AssetManager"));
				selectedManager = _assetDataManager;
				//TODO Add Connection Maganger Datadescription Interface 
            } else if (domain.Contains("User")) {
                Tools.Log.ForwardLog?.Invoke(this, new Tools.Log.ForwardEventArgs(this.GetType().Name, "SelectManager: UserDataManager"));
				selectedManager = _userDataManager;
			} else {
				throw new NotImplementedException("Given instance don't implement ISettingsComplexData / IConnectionDescription");
			}
			return selectedManager;
		}

        protected DataDelegateCallback<IDataContainer> CreateEventHandler(IDataDescription desc) {
            return new DataDelegateCallback<IDataContainer>(Success, Fail) { ID = desc.ID, Source = desc.Source };
        }

        protected DataDelegateCallback<TType> CreateEventHandler<TType>(IDataDescription desc) where TType : class, IDataContainer
        {
            return new DataDelegateCallback<TType>(Success, Fail) { ID = desc.ID, Source = desc.Source };

        }

		#endregion

		#region Public Methods

		public void RequestData(IDataDescription dataDescriptionInstance, IDataCallback callback = null)
        {
            Tools.Log.ForwardLog?.Invoke(this, new Tools.Log.ForwardEventArgs(this.GetType().Name, "RequestData: " + dataDescriptionInstance.ID));
            try {
                if (callback == null) callback = CreateEventHandler(dataDescriptionInstance);
				SelectManager(dataDescriptionInstance).RequestData(dataDescriptionInstance, callback);
			} catch (System.Exception e) {
				IException ex = new BaseException(ExceptionInfo.DefaultReason, e);
				PerformErrorHandling(ex, callback);
			}
		}

		public void RequestData<TResult, TParameter>(IDataDescription dataDescriptionInstance, IDataCallback<TResult> callback = null)
			where TResult : class, IDataContainer
			where TParameter : class, IDataContainer
		{
            Tools.Log.ForwardLog?.Invoke(this, new Tools.Log.ForwardEventArgs(this.GetType().Name, "RequestData: " + dataDescriptionInstance.ID));
			try {
                if (callback == null) callback = CreateEventHandler<TResult>(dataDescriptionInstance);
				SelectManager(dataDescriptionInstance).RequestData<TResult, TParameter>(dataDescriptionInstance, callback);
			} catch (System.Exception e) {
				IException ex = new BaseException(ExceptionInfo.DefaultReason, e);
				PerformErrorHandling(ex, callback);
			}
		}

		public void RequestData(IDataDescription dataDescriptionInstance, IDataContainer data, IDataCallback callback = null)
        {
            Tools.Log.ForwardLog?.Invoke(this, new Tools.Log.ForwardEventArgs(this.GetType().Name, "RequestData: " + dataDescriptionInstance.ID));
			try {
                if (callback == null) callback = CreateEventHandler(dataDescriptionInstance);
				SelectManager(dataDescriptionInstance).RequestData(dataDescriptionInstance, data, callback);
			} catch (System.Exception e) {
				IException ex = new BaseException(ExceptionInfo.DefaultReason, e);
				PerformErrorHandling(ex, callback);
			}
		}

		public void RequestData<TResult, TParameter>(IDataDescription dataDescriptionInstance, TParameter data, IDataCallback<TResult> callback = null)
			where TResult : class, IDataContainer
			where TParameter : class, IDataContainer
		{
            Tools.Log.ForwardLog?.Invoke(this, new Tools.Log.ForwardEventArgs(this.GetType().Name, "RequestData: " + dataDescriptionInstance.ID));
			try {
                if (callback == null) callback = CreateEventHandler<TResult>(dataDescriptionInstance);
				SelectManager(dataDescriptionInstance).RequestData(dataDescriptionInstance, data, callback);
			} catch (System.Exception e) {
				IException ex = new BaseException(ExceptionInfo.DefaultReason, e);
				PerformErrorHandling(ex, callback);
			}
		}

		public void SaveData(IDataDescription dataDescriptionInstance, IDataContainer data)
        {
			SelectManager(dataDescriptionInstance).SaveData(dataDescriptionInstance, data);
		}

		public void SaveData(IDataDescription dataDescriptionInstance, IDataContainer data, IDataCallback callback)
        {
			try {
				SelectManager(dataDescriptionInstance).SaveData(dataDescriptionInstance, data, callback);
			} catch (System.Exception e) {
				IException ex = new BaseException(ExceptionInfo.DefaultReason, e);
				PerformErrorHandling(ex, callback);
			}
		}

        public void OnReportError(object sender, ExceptionEventArgs e) => ReportError?.Invoke(sender, e);

		#endregion

		#region Event Handler

		protected void OnAssetSuccess(object sender, DataSuccessEventArgs<IDataContainer> e) => Success?.Invoke(sender, e);

		protected void OnAssetFail(object sender, DataFailEventArgs<IException> e) => Fail?.Invoke(sender, e);

		protected void OnSettingsSuccess(object sender, DataSuccessEventArgs<IDataContainer> e) => Success?.Invoke(sender, e);

		protected void OnSettingsFail(object sender, DataFailEventArgs<IException> e) => Fail?.Invoke(sender, e);

		protected void OnUserSuccess(object sender, DataSuccessEventArgs<IDataContainer> e) => Success?.Invoke(sender, e);

		protected void OnUserFail(object sender, DataFailEventArgs<IException> e) => Fail?.Invoke(sender, e);

        #endregion

    }
}