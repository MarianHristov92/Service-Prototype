// ///-----------------------------------------------------------------
// ///   Class:          ISettingsComplexData
// ///   Description:    These interfaces represent version of SettingsData Wrapper for custom parametertypes
// ///   Author:         Dimitri Renke                    Date: 30.09.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
using System;
using LibDataService.DataModels.Callback;
using LibDataService.DataModels.Container;

namespace LibDataService.SettingsService
{
	/// <summary>
	/// These interfaces represent version of SettingsData Wrapper for custom parametertypes
	/// <typeparam name="Value">Generic typeparameter.</typeparam>
	/// </summary>
	public interface ISettingsComplexData<TResult, TParameter>
		where TResult : class, IDataContainer
		where TParameter : class, IDataContainer
	{
		/// <summary>
		/// Gets the data.
		/// </summary>
		/// <param name="settingKey">String identifier in settings</param>
		/// <param name="manager">ISettingsManager abstracted object to store and load persistent information in local storage</param>
		/// <param name="settingCallback">ISettingsCallback Callback</param>
		void GetData(String settingKey, ISettingsManager manager, IDataCallback<TResult> settingCallback);

		/// <summary>
		/// Sets the data.
		/// </summary>
		/// <param name="settingKey">String identifier in settings</param>
		/// <param name="manager">ISettingsManager abstracted object to store and load persistent information in local storage</param>
		/// <param name="data">Instance of generic typeprameter, contain Value to store.</param>
		void SetData(String settingKey, ISettingsManager manager, IDataContainer data);
	}

    /// <summary>
	/// These interfaces represent version of SettingsData Wrapper for custom parametertypes
	/// <typeparam name="Value">Generic typeparameter.</typeparam>
	/// </summary>
	public interface ISettingsComplexData
    {
        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <param name="settingKey">String identifier in settings</param>
        /// <param name="manager">ISettingsManager abstracted object to store and load persistent information in local storage</param>
        /// <param name="settingCallback">ISettingsCallback Callback</param>
		void GetData(String settingKey, ISettingsManager manager, IDataCallback settingCallback);

		/// <summary>
		/// Gets the data.
		/// </summary>
		/// <param name="settingKey">String identifier in settings</param>
		/// <param name="manager">ISettingsManager abstracted object to store and load persistent information in local storage</param>
		/// <param name="settingCallback">ISettingsCallback Callback</param>
		void GetData<TResult, TParameter>(String settingKey, ISettingsManager manager, IDataCallback<TResult> settingCallback)
			where TResult : class, IDataContainer
			where TParameter : class, IDataContainer;

        /// <summary>
        /// Sets the data.
        /// </summary>
        /// <param name="settingKey">String identifier in settings</param>
        /// <param name="manager">ISettingsManager abstracted object to store and load persistent information in local storage</param>
        /// <param name="data">Instance of generic typeprameter, contain Value to store.</param>
        void SetData(String settingKey, ISettingsManager manager, IDataContainer data);
    }
}
