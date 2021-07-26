using System;
using LibDataService.DataModels.Callback;
using LibDataService.DataModels.Container;
using LibDataService.DataModels.Description;
using LibDataService.DataService;
using LibDataService.Exception;
using LibDataService.Helper;
using Xamarin.Essentials;

namespace LibDataService.ConnectionService
{
	public class ConnectionManagerFlurl : IConnectionManager 
	{
		#region Events

		public event EventHandler<DataSuccessEventArgs<IDataContainer>> Success;
		public event EventHandler<DataFailEventArgs<IException>> Fail;

		#endregion

		#region Consts

		public const int MaxHttpConnections = 10;

		#endregion

		#region Attributes

		HttpQueue _queue;

		#endregion

		#region Constructor

		public ConnectionManagerFlurl(int maxHttpConnections = MaxHttpConnections)
		{
			_queue = new HttpQueue(maxHttpConnections);
            Connectivity.ConnectivityChanged += ConnectionHelper.OnConnectivityChanged;
		}

		#endregion

		#region Public Methods

		public void RequestData(IDataDescription dataDescriptionInstance, IDataCallback callback)
		{
            ForwardLog("RequestData:" + dataDescriptionInstance.ID);
			RequestData(dataDescriptionInstance, default(IDataContainer), callback);
		}

		public void RequestData<TResult, TParameter>(IDataDescription dataDescriptionInstance, IDataCallback<TResult> callback)
			where TResult : class, IDataContainer
			where TParameter : class, IDataContainer
		{
            ForwardLog("RequestData:" + dataDescriptionInstance.ID);
			RequestData(dataDescriptionInstance, default(TParameter), callback);
		}

		public void RequestData(IDataDescription dataDescriptionInstance, IDataContainer parameter, IDataCallback callback)
		{
            ForwardLog("RequestData:" + dataDescriptionInstance.ID);
			PerformWithQuery(dataDescriptionInstance, parameter, callback);
		}

		public void RequestData<TResult, TParameter>(IDataDescription dataDescriptionInstance, TParameter parameter, IDataCallback<TResult> callback)
			where TResult : class, IDataContainer
			where TParameter : class, IDataContainer
		{
            ForwardLog("RequestData:" + dataDescriptionInstance.ID);
			PerformWithQuery(dataDescriptionInstance, parameter, callback);
		}

		public void SaveData(IDataDescription dataDescriptionInstance, IDataContainer data)
		{
            ForwardLog("SaveData:" + dataDescriptionInstance.ID);
			// TODO: Check this command for inconsistency
			PerformWithQuery(dataDescriptionInstance, data, null);
		}

		public void SaveData(IDataDescription dataDescriptionInstance, IDataContainer data, IDataCallback callback)
		{
            ForwardLog("SaveData:" + dataDescriptionInstance.ID);
			// TODO: Check this command for inconsistency
			PerformWithQuery(dataDescriptionInstance, data, callback);
		}

		#endregion

		#region Private Methods

		private void PerformWithContainer(IDataDescription dataDescriptionInstance, IDataContainer parameter, IDataCallback callback)
		{
            ForwardLog("PerformWithContainer: " + dataDescriptionInstance.ID);
			ConnectionOperationContainer OperationContainer = new ConnectionOperationContainer();
			OperationContainer.Callback = callback;
			OperationContainer.DataDescription = dataDescriptionInstance;
			OperationContainer.Parameter = parameter;
			OperationContainer.PerformRequest();

		}

		private void PerformWithQuery(IDataDescription dataDescriptionInstance, IDataContainer parameter, IDataCallback callback)
		{
            ForwardLog("PerformWithQuery: " + dataDescriptionInstance.ID);
			ConnectionOperationContainer OperationContainer = new ConnectionOperationContainer();
			OperationContainer.Callback = callback;
			OperationContainer.DataDescription = dataDescriptionInstance;
			OperationContainer.Parameter = parameter;
			OperationContainer.RunningQueryRemoved += _queue.RemoveItem;
			bool isPriority = OperationContainer.HasPriorityFlag();
			_queue.AddItem(OperationContainer, isPriority);
		}

		private void PerformWithQuery<TResult, TParameter>(IDataDescription dataDescriptionInstance, TParameter parameter, IDataCallback<TResult> callback)
			where TResult : class, IDataContainer
			where TParameter : class, IDataContainer
		{
            ForwardLog("PerformWithQuery: " + dataDescriptionInstance.ID);
			ConnectionOperationContainer<TResult, TParameter> OperationContainer = new ConnectionOperationContainer<TResult, TParameter>();
			OperationContainer.Callback = callback;
			OperationContainer.DataDescription = dataDescriptionInstance;
			OperationContainer.Parameter = parameter;
			OperationContainer.RunningQueryRemoved += _queue.RemoveItem;
			bool isPriority = OperationContainer.HasPriorityFlag();
			_queue.AddItem(OperationContainer, isPriority);
		}

        private void ForwardLog(string message)
        {
            Tools.Log.ForwardLog?.Invoke(this, new Tools.Log.ForwardEventArgs(this.GetType().Name, message));
        }
		#endregion

	}
}