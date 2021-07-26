// ///-----------------------------------------------------------------
// ///   Class:          AssetProcessContainer
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 14.10.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
using System;
using LibDataService.DataModels.Callback;
using LibDataService.DataModels.Container;
using LibDataService.DataModels.Description;
using LibDataService.DataService;
using LibDataService.Exception;

namespace LibDataService.AssetService
{
	/// <summary>
	/// This Class is specialized implementation of ICacheCallback.
	/// It represent internal callback mechanics of AssetManager.
	/// This Class contains all necessary attributes for any Asset operation (get/set and they derivatives)
	/// </summary>
	public class AssetProcessContainer : IDataCallback 
	{
		#region Events

		public event EventHandler<DataSuccessEventArgs<IDataContainer>> Success;
		public event EventHandler<DataFailEventArgs<IException>> Error;

		#endregion

		#region Properties

        public int ID { get; set; }
        public Type Source { get; set; }

		/// <summary>
		/// attribute for external callback outside of Assetmanager
		/// </summary>
		public IDataCallback ExternCallback { get; set; }

		/// <summary>
		/// attribute for Recodendpoint description
		/// </summary>
		public IDataDescription DataDescriptionInstance { get; set; }

		/// <summary>
		/// attribute for opperation specialization or & and send record
		/// </summary>
		public IDataContainer Parameter { get; set; }

		/// <summary>
		/// attribute for opperation result record
		/// </summary>
		public IDataContainer Result { get; set; }

		/// <summary>
		/// attribute for Assetmananger Instance
		/// </summary>
		public IDataOperation AssetManager { get; set; }

		public bool HasData { get; set; } = false;

		public IException Exception { get; set; }

		/// <summary>
		/// Reprepresend operation for the given Instance 
		/// </summary>
		protected ActionType _currentAction;

		public ActionType CurrentAction => _currentAction;

		/// <summary>
		/// Reprepresend operation step at this the moment 
		/// </summary>
		protected ProcessStep _currentStep;

		public ProcessStep CurrentStep => _currentStep;

		#endregion

		#region Constructor

		public AssetProcessContainer()
		{
			SetProcessStepCach();
		}

		#endregion

		#region Enum

		/// <summary>
		/// ActionTypes represend supported operations (record transport direction)
		/// </summary>
		public enum ActionType
		{
			Read,
			Write
		}

		/// <summary>
		/// ProcessSteps represend supported operation steps
		/// </summary>
		public enum ProcessStep
		{
			Cache,
			Cloud
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// specialized setter for operation
		/// </summary>
		public void SetActionRead() =>_currentAction = ActionType.Read;

		/// <summary>
		/// specialized setter for operation
		/// </summary>
		public void SetActionWrite() => _currentAction = ActionType.Write;

		/// <summary>
		/// specialized setter for operation step
		/// </summary>
		public void SetProcessStepCach() => _currentStep = ProcessStep.Cache;

		/// <summary>
		/// specialized setter for operation step
		/// </summary>
		public void SetProcessStepCloud() => _currentStep = ProcessStep.Cloud;

		public bool HasException() => Exception != null;

		#endregion

		#region Event Handler

		/// <summary>
		/// Implematiation of IDataCallback
		/// Represend successful completion of the caching or cloud operation. 
		/// Forward to Assetmanager to trigger post operation process.
		/// </summary>
		public void OnObtainData(IDataContainer data)
		{
			Result = data;
			HasData = true;
			var manager = AssetManager as AssetManager;
			switch (_currentStep) {
				case ProcessStep.Cache:
					manager.PostCachingStep(this);
					break;
				case ProcessStep.Cloud:
					manager.PostCloudStep(this);
					break;
				default:
					// TODO: Add logic for default state
					break;
			}
		}

		/// <summary>
		/// Implematiation of IDataCallback
		/// Represend not successful completion of the caching or cloud operation
		/// Forward to Assetmanager to trigger post operation process.
		/// </summary>
		public void OnObtainError(IException exception)
		{
			// TODO: What is the intnetion here ???
			//if (Exception != null) {
			//	Exception = exception;
			//} else {
			//	Exception = exception;
			//}

			Exception = exception;

			HasData = false;
			var manager = AssetManager as AssetManager;
			switch (_currentStep) {
				case ProcessStep.Cache:
					manager.PostCachingStep(this);
					break;
				case ProcessStep.Cloud:
					manager.PostCloudStep(this);
					break;
				default:
					// TODO: Add logic for default state
					break;

			}
		}

		#endregion
	}

	/// <summary>
	/// This Class is specialized implementation of ICacheCallback.
	/// It represent internal callback mechanics of AssetManager.
	/// This Class contains all necessary attributes for any Asset operation (get/set and they derivatives)
	/// </summary>
	public class AssetProcessContainer<TResult, TParameter> : IDataCallback<TResult>
		where TResult : class, IDataContainer
		where TParameter : class, IDataContainer
	{
		#region Events

		public event EventHandler<DataSuccessEventArgs<TResult>> Success;
		public event EventHandler<DataFailEventArgs<IException>> Error;

		#endregion

		#region Properties

        public int ID { get; set; }
        public Type Source { get; set; }

		/// <summary>
		/// attribute for external callback outside of Assetmanager
		/// </summary>
		public IDataCallback<TResult> ExternCallback { get; set; }

		/// <summary>
		/// attribute for Recodendpoint description
		/// </summary>
		public IDataDescription DataDescriptionInstance { get; set; }

		/// <summary>
		/// attribute for opperation specialization or & and send record
		/// </summary>
		public TParameter Parameter { get; set; }

		/// <summary>
		/// attribute for opperation result record
		/// </summary>
		public TResult Result { get; set; }

		/// <summary>
		/// attribute for Assetmananger Instance
		/// </summary>
		public IDataOperation AssetManager { get; set; }

		public bool HasData { get; set; } = false;

		public IException Exception { get; set; }

		/// <summary>
		/// Reprepresend operation for the given Instance 
		/// </summary>
		protected ActionType _currentAction;

		public ActionType CurrentAction => _currentAction;

		/// <summary>
		/// Reprepresend operation step at this the moment 
		/// </summary>
		protected ProcessStep _currentStep;

		public ProcessStep CurrentStep => _currentStep;

		#endregion

		#region Constructor

		public AssetProcessContainer()
		{
			SetProcessStepCach();
		}

		#endregion

		#region Enum

		/// <summary>
		/// ActionTypes represend supported operations (record transport direction)
		/// </summary>
		public enum ActionType
		{
			Read,
			Write
		}

		/// <summary>
		/// ProcessSteps represend supported operation steps
		/// </summary>
		public enum ProcessStep
		{
			Cache,
			Cloud
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// specialized setter for operation
		/// </summary>
		public void SetActionRead() =>_currentAction = ActionType.Read;

		/// <summary>
		/// specialized setter for operation
		/// </summary>
		public void SetActionWrite() => _currentAction = ActionType.Write;

		/// <summary>
		/// specialized setter for operation step
		/// </summary>
		public void SetProcessStepCach() => _currentStep = ProcessStep.Cache;

		/// <summary>
		/// specialized setter for operation step
		/// </summary>
		public void SetProcessStepCloud() => _currentStep = ProcessStep.Cloud;

		public bool HasException() => Exception != null;

		#endregion

		#region Event Handler

		/// <summary>
		/// Implematiation of IDataCallback
		/// Represend successful completion of the caching or cloud operation. 
		/// Forward to Assetmanager to trigger post operation process.
		/// </summary>
		public void OnObtainData(TResult data)
		{
			Result = data;
			HasData = true;
			var manager = AssetManager as AssetManager;
			switch (_currentStep) {
				case ProcessStep.Cache:
					manager.PostCachingStep(this);
					break;
				case ProcessStep.Cloud:
					manager.PostCloudStep(this);
					break;
				default:
					// TODO: Add logic for default state
					break;
			}
		}

		public void OnObtainData(IDataContainer data)
		{
			OnObtainData(data as TResult);
		}

		/// <summary>
		/// Implematiation of IDataCallback
		/// Represend not successful completion of the caching or cloud operation
		/// Forward to Assetmanager to trigger post operation process.
		/// </summary>
		public void OnObtainError(IException exception)
		{
			// TODO: What is the intnetion here ???
			//if (Exception != null) {
			//	Exception = exception;
			//} else {
			//	Exception = exception;
			//}

			Exception = exception;

			HasData = false;
			var manager = AssetManager as AssetManager;
			switch (_currentStep) {
				case ProcessStep.Cache:
					manager.PostCachingStep(this);
					break;
				case ProcessStep.Cloud:
					manager.PostCloudStep(this);
					break;
				default:
					// TODO: Add logic for default state
					break;
			}
		}

		#endregion
	}
}