// ///-----------------------------------------------------------------
// ///   Class:          HttpQueue
// ///   Description:    <Description>
// ///   Author:         Christian Haseloff                    Date: 
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:DIR           Date:19.12.16        Description: proted from another branch

// ///-----------------------------------------------------------------
using System.Collections.Generic;
using Flurl.Http;
using LibDataService.DataModels.Container;

namespace LibDataService.ConnectionService
{
	public delegate void QueryRemovedEventHandler(string id, IFlurlClient client);

	public class HttpQueue
	{

		#region Attributes

		private readonly int MaxHttpConnections;

		private List<IConnectionOperationContainer> _queueItems;
		private List<IConnectionOperationContainer> _queueItemsPriority;
		private Dictionary<string, IConnectionOperationContainer> _queueRunning;

		#endregion

		#region Constructor

		public HttpQueue(int pMHttpConnections)
		{
			_queueItems = new List<IConnectionOperationContainer>();
			_queueItemsPriority = new List<IConnectionOperationContainer>();
			_queueRunning = new Dictionary<string, IConnectionOperationContainer>();
			MaxHttpConnections = pMHttpConnections;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// this method add given request represend by instance of ConnectionOperationContainer
		/// to priority or normal Query depend on isPriorityItem flag.
		/// And try to run next request in query by performing tryRunItem();
		/// </summary>
		/// <param name="item">request</param>
		/// <param name="isPriorityItem">If set to <c>true</c> is priority item.</param>
		public void AddItem(IConnectionOperationContainer item, bool isPriorityItem = false)
		{
			if (!isPriorityItem) {
				_queueItems.Add(item);
			}
			else {
				_queueItemsPriority.Add(item);
			}
			ExecuteNextRunItem();
		}

		/// <summary>
		/// this method remove completed request given by Domain
		/// </summary>
		/// <param name="id">Domain of Request.</param>
		public void RemoveItem(string id, IFlurlClient client)
		{
			_queueRunning.Remove(id);
			ExecuteNextRunItem();
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// this method
		/// select next reqeust to run. first from priority query
		/// or from normal query. Add it to RunQuery and start request
		/// Domain will be used As identifier of running request,
		/// If selected request allready in RunQuery. This Request
		/// will be rejected from all query with out notification.
		/// </summary>
		private void ExecuteNextRunItem()
		{
			bool isNormalItemAvailable = (_queueItems.Count > 0);
			bool isPriorityItemAvailable = (_queueItemsPriority.Count > 0);
			if (_queueRunning.Count < MaxHttpConnections && (isNormalItemAvailable || isPriorityItemAvailable)) {
				IConnectionOperationContainer toRun;
				if (isPriorityItemAvailable) {
					toRun = _queueItemsPriority[0];
					_queueItemsPriority.RemoveAt(0);
				} else {
					toRun = _queueItems[0];
					_queueItems.RemoveAt(0);
				}
				string id = toRun.Id;
				if (!_queueRunning.ContainsKey(id)) {
					_queueRunning.Add(id, toRun);
					toRun.PerformRequest();
				}
			}
		}

		#endregion

	}
}
