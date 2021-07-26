
using System;
using LibDataService.DataModels.Callback;
using LibDataService.DataModels.Container;
using LibDataService.DataModels.Description;
using LibDataService.Exception;
using System.Linq.Expressions;

namespace LibDataService.DataService
{
	/// <summary>
	/// This Interface represent min. capabilities of a DataManager. Depending on the use of Manager send/store 
	/// & request/laod record from Settings and Cache / Cloud or combination of it
	/// </summary>
	public interface IDataOperation<TResult, TParameter> 
		where TResult : class, IDataContainer
		where TParameter : class, IDataContainer
	{
		#region Events

		event EventHandler<DataSuccessEventArgs<TResult>> Success;
		event EventHandler<DataFailEventArgs<IException>> Fail;

		#endregion

		void RequestData(IDataDescription dataDescriptionInstance, TParameter parameter, IDataCallback<TResult> callback = null);
		void RequestData(IDataDescription dataDescriptionInstance, IDataCallback<TResult> callback = null);
		void SaveData(IDataDescription dataDescriptionInstance, IDataContainer parameter, IDataCallback<TResult> callback);
		void SaveData(IDataDescription dataDescriptionInstance, IDataContainer parameter);
	}

    /// <summary>
	/// This Interface represent min. capabilities of a DataManager. Depending on the use of Manager send/store 
	/// & request/laod record from Settings and Cache / Cloud or combination of it
	/// </summary>
	public interface IDataOperation
    {
        #region Events

        event EventHandler<DataSuccessEventArgs<IDataContainer>> Success;
        event EventHandler<DataFailEventArgs<IException>> Fail;

        #endregion

		void RequestData(IDataDescription dataDescriptionInstance, IDataContainer parameter, IDataCallback callback = null);

		void RequestData<TResult, TParameter>(IDataDescription dataDescriptionInstance, TParameter parameter, IDataCallback<TResult> callback = null)
			where TResult : class, IDataContainer
			where TParameter : class, IDataContainer;

		void RequestData(IDataDescription dataDescriptionInstance, IDataCallback callback = null);

		void RequestData<TResult, TParameter>(IDataDescription dataDescriptionInstance, IDataCallback<TResult> callback = null)
			where TResult : class, IDataContainer
			where TParameter : class, IDataContainer;

		void SaveData(IDataDescription dataDescriptionInstance, IDataContainer parameter, IDataCallback callback);

        void SaveData(IDataDescription dataDescriptionInstance, IDataContainer parameter);
    }
}