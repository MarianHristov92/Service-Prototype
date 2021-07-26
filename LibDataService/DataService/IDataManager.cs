// ///-----------------------------------------------------------------
// ///   Class:          ACRSettings
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 29.09.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
using System;
using LibDataService.DataModels.Callback;
using LibDataService.DataModels.Container;
using LibDataService.DataModels.Description;
using LibDataService.Exception;

namespace LibDataService.DataService
{
	/// <summary>
	/// This Interface represent min. capabilities of a DataService 
	/// </summary>
	public interface IDataManager<TResult, TParameter> 
		where TResult : class, IDataContainer
		where TParameter : class, IDataContainer
	{
		#region Events

		event EventHandler<DataSuccessEventArgs<TResult>> Success;
		event EventHandler<DataFailEventArgs<IException>> Fail;

		#endregion

		/// <summary>
		/// This method had to return requestet record.
		/// </summary>
		/// <param name="dataDescriptionInstance">Record description instance {Domain} if applicable WS Endpoint Parameters url, access data and encryption </param>
		/// <param name="data">Dynamic request parameter like Product ID</param>
		/// <param name="callback">Callback</param>
		void RequestData(IDataDescription dataDescriptionInstance, TParameter data, IDataCallback<TResult> callback = null);

		/// <summary>
		/// This method had to return requested record.
		/// </summary>
		/// <param name="dataDescriptionInstance">Record description instance {Domain} if applicable WS Endpoint Parameters url, access data and encryption </param>
		/// <param name="callback">Callback</param>
		void RequestData(IDataDescription dataDescriptionInstance, IDataCallback<TResult> callback = null);

		/// <summary>
		/// This method had to send or to store record.
		/// </summary>
		/// <param name="dataDescriptionInstance">Record description instance {Domain} if applicable WS Endpoint Parameters url, access data and encryption </param>
		/// <param name="data">To send recordDynamic and if applicable Dynamic request parameter like Product ID </param>
		/// <param name="callback">Callback</param>
		void SaveData(IDataDescription dataDescriptionInstance, TParameter data, IDataCallback<TResult> callback);

		/// <summary>
		/// This method had to send or to store record.
		/// </summary>
		/// <param name="dataDescriptionInstance">Record description instance {Domain} if applicable WS Endpoint Parameters url, access data and encryption </param>
		/// <param name="data">To send recordDynamic and if applicable Dynamic request parameter like Product ID </param>
		void SaveData(IDataDescription dataDescriptionInstance, TParameter data);
	}

    public interface IDataManager
    {
        #region Events

        event EventHandler<DataSuccessEventArgs<IDataContainer>> Success;
        event EventHandler<DataFailEventArgs<IException>> Fail;
        event EventHandler<ExceptionEventArgs> ReportError;

        #endregion

        /// <summary>
        /// This method had to return requestet record.
        /// </summary>
        /// <param name="dataDescriptionInstance">Record description instance {Domain} if applicable WS Endpoint Parameters url, access data and encryption </param>
        /// <param name="data">Dynamic request parameter like Product ID</param>
        /// <param name="callback">Callback</param>
		void RequestData(IDataDescription dataDescriptionInstance, IDataContainer data, IDataCallback callback = null);

		/// <summary>
		/// This method had to return requestet record.
		/// </summary>
		/// <param name="dataDescriptionInstance">Record description instance {Domain} if applicable WS Endpoint Parameters url, access data and encryption </param>
		/// <param name="data">Dynamic request parameter like Product ID</param>
		/// <param name="callback">Callback</param>
		void RequestData<TResult, TParameter>(IDataDescription dataDescriptionInstance, TParameter data, IDataCallback<TResult> callback = null)
			where TResult : class, IDataContainer
			where TParameter : class, IDataContainer;

        /// <summary>
        /// This method had to return requested record.
        /// </summary>
        /// <param name="dataDescriptionInstance">Record description instance {Domain} if applicable WS Endpoint Parameters url, access data and encryption </param>
        /// <param name="callback">Callback</param>
		void RequestData(IDataDescription dataDescriptionInstance, IDataCallback callback = null);

		/// <summary>
		/// This method had to return requestet record.
		/// </summary>
		/// <param name="dataDescriptionInstance">Record description instance {Domain} if applicable WS Endpoint Parameters url, access data and encryption </param>
		/// <param name="data">Dynamic request parameter like Product ID</param>
		/// <param name="callback">Callback</param>
		void RequestData<TResult, TParameter>(IDataDescription dataDescriptionInstance, IDataCallback<TResult> callback = null)
			where TResult : class, IDataContainer
			where TParameter : class, IDataContainer;

        /// <summary>
        /// This method had to send or to store record.
        /// </summary>
        /// <param name="dataDescriptionInstance">Record description instance {Domain} if applicable WS Endpoint Parameters url, access data and encryption </param>
        /// <param name="data">To send recordDynamic and if applicable Dynamic request parameter like Product ID </param>
        /// <param name="callback">Callback</param>
		void SaveData(IDataDescription dataDescriptionInstance, IDataContainer data, IDataCallback callback);

        /// <summary>
        /// This method had to send or to store record.
        /// </summary>
        /// <param name="dataDescriptionInstance">Record description instance {Domain} if applicable WS Endpoint Parameters url, access data and encryption </param>
        /// <param name="data">To send recordDynamic and if applicable Dynamic request parameter like Product ID </param>
		void SaveData(IDataDescription dataDescriptionInstance, IDataContainer data);

        /// <summary>
        /// This method will invoke event and pass error to it as eventArguments
        /// </summary>
        /// <param name="sender">Record description instance {Domain} if applicable WS Endpoint Parameters url, access data and encryption </param>
        /// <param name="e">To send error and if applicable result HttpCall </param>
        void OnReportError(object sender,ExceptionEventArgs e);
    }
}