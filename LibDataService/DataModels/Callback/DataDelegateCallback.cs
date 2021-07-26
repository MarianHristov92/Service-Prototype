// ///-----------------------------------------------------------------
// ///   Class:          IDataDelegateCallback
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 29.11.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
using System;
using LibDataService.DataModels.Container;
using LibDataService.DataService;
using LibDataService.Exception;

namespace LibDataService.DataModels.Callback
{
	/// <summary>
	/// This class adopted Delegate approach to IDataCallback
	/// </summary>
	public class DataDelegateCallback<TType> : IDataCallback<TType> where TType : class, IDataContainer
	{

		#region Events

        public event EventHandler<DataSuccessEventArgs<TType>> Success;
		public event EventHandler<DataFailEventArgs<IException>> Fail;
		
		#endregion

		#region Attributes

		[Obsolete]
		Delegate _obseleteSuccess; 

		[Obsolete]
		Delegate _obseleteFail;

        #endregion

        #region Properties

        public int ID { get; set; }
        public Type Source { get; set; }

        #endregion

        #region Constructor

        [Obsolete]
		/// <summary>
		/// Constructor with Delegates as Parameter
		/// </summary>
		/// <param name="success">Success.</param>
		/// <param name="fail">Fail.</param>
		public DataDelegateCallback(Delegate success, Delegate fail)
		{
			_obseleteSuccess = success;
			_obseleteFail = fail;
		}

        public DataDelegateCallback(EventHandler<DataSuccessEventArgs<TType>> success, EventHandler<DataFailEventArgs<IException>> fail)
		{
			Success = success;
			Fail = fail;
		}

		#endregion

		#region Event Handler

		/// <summary>
		/// This method is Callback after success operation. 
		/// In case that success Delegate is set, it will be executed
		/// </summary>
		/// <param name="data">Data.</param>
		public void OnObtainData(TType data)
		{
            Success?.Invoke(this, new DataSuccessEventArgs<TType>(data, ID, Source));
			_obseleteSuccess?.DynamicInvoke(data);
		}

		public void OnObtainData(IDataContainer data)
		{
			OnObtainData(data as TType);
		}

		/// <summary>
		/// This method is Callback after not successful operation. 
		/// In case that fail Delegate is set, it will be executed
		/// </summary>
		/// <param name="data">Data.</param>
		public void OnObtainError(IException exception)
		{
            Fail?.Invoke(this, new DataFailEventArgs<IException>(exception, ID, Source));
			_obseleteFail?.DynamicInvoke(exception);
		}

		#endregion

	}
}
