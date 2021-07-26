// ///-----------------------------------------------------------------
// ///   Class:          SettingsManager
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 29.09.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
using System;
using System.Extension;
using Acr.Settings;
using LibDataService.DataModels.Callback;
using LibDataService.DataModels.Container;
using LibDataService.DataModels.Description;
using LibDataService.DataService;
using LibDataService.Exception;
using LibDataService.Helper;

namespace LibDataService.SettingsService
{

	public class SettingsManager : ISettingsManager 
	{
		#region Events

		public event EventHandler<DataSuccessEventArgs<IDataContainer>> Success;
		public event EventHandler<DataFailEventArgs<IException>> Fail;

		#endregion

		#region Attributes

		ISettings Storage;

		#endregion

		#region Constructor

		public SettingsManager()
		{
            Storage = Settings.Current;
		}

		#endregion

		#region Public Methods

		public void RegisterSuccessEvent<TResult>(EventHandler<DataSuccessEventArgs<TResult>> listener) 
			where TResult : class, IDataContainer
		{
			Success += (sender, e) => {
                DataSuccessEventArgs<TResult> dataSuccess = new DataSuccessEventArgs<TResult>(e.Data as TResult, e.ID);
				listener.Invoke(sender, dataSuccess); 
			};
		}

		/// <summary>
		/// This method load data Object from settings for composite identifier (Domain with additional indentifiers from data parameter).
		/// If searched Object don't stored. Return custom default value via settingsCallback member onObtainData(Value data),
		/// otherwise return SettingsErrorTypes.SettingsErrorType.NotFound  via settingsCallback member onObtainError(...), 
		/// </summary>
		/// creation of composite identifier must be ensured by implementation of IDomainConstructionField
		/// <br>
		/// conversion of custom typeparameter must be ensured by implementation of ISettingsComplexData
		/// <br>
		/// delivering of custom default value must be ensured by implementation of ISettingDefaultValue
		/// <param name="dataDescriptionInstance">Data description instance. This Object have to implement 
		/// IDomainConstructionField and ISettingsData</a> </param>
		/// <param name="parameter">Instance of generic typeparameter Value. Contains additional indentifiers.</param>
		/// <param name="settingsCallback">Settings callback.</param>
		/// <typeparam name="Value">Generic typeparameter.</typeparam>
		public void RequestData(IDataDescription dataDescriptionInstance, IDataContainer parameter, IDataCallback settingsCallback)
        {
            string settingKey = DomainHelper.GetDomainAsKey(dataDescriptionInstance, parameter);
			if (!Storage.Contains(settingKey)) {
				if (IsSupported<ISettingDefaultValue<Object>>(dataDescriptionInstance)) {
					(dataDescriptionInstance as ISettingDefaultValue<Object>).GetDefaultValue();
				} else {
					(settingsCallback as ISettingsCallback<IDataContainer>).OnObtainError(SettingsException.ExceptionDataNotFound());
				}
				return;
			}
			if (IsSupported<ISettingsComplexData>(dataDescriptionInstance)) {
                (dataDescriptionInstance as ISettingsComplexData).GetData(settingKey, this, (settingsCallback as IDataCallback<IDataContainer>));
			} else {
                Success?.Invoke(this, new DataSuccessEventArgs<IDataContainer>(default(IDataContainer), dataDescriptionInstance.ID));
				settingsCallback?.OnObtainData(default(IDataContainer));
			}
		}

		public void RequestData<TResult, TParameter>(IDataDescription dataDescriptionInstance, TParameter parameter, IDataCallback<TResult> settingsCallback)
			where TResult : class, IDataContainer
			where TParameter : class, IDataContainer
		{
			string settingKey = DomainHelper.GetDomainAsKey(dataDescriptionInstance, parameter);
			if (!Storage.Contains(settingKey)) {
				if (IsSupported<ISettingDefaultValue<Object>>(dataDescriptionInstance)) {
					(dataDescriptionInstance as ISettingDefaultValue<Object>).GetDefaultValue();
				} else {
					(settingsCallback as ISettingsCallback<TResult>).OnObtainError(SettingsException.ExceptionDataNotFound());
				}
				return;
			}
			if (IsSupported<ISettingsComplexData>(dataDescriptionInstance)) {
				(dataDescriptionInstance as ISettingsComplexData).GetData<TResult, TParameter>(settingKey, this, settingsCallback);
			} else {
                Success?.Invoke(this, new DataSuccessEventArgs<IDataContainer>(default(TResult), dataDescriptionInstance.ID));
				settingsCallback?.OnObtainData(default(TResult));
			}
		}

		/// <summary>
		/// This method load data Object from settings for identifier (Domain only).
		/// /// If searched Object don't stored. Return custom default value via settingsCallback member onObtainData(Value data),
		/// otherwise return SettingsErrorTypes.SettingsErrorType.NotFound  via settingsCallback member onObtainError(...), 
		/// </summary>
		/// delivering of custom default value must be ensured by implementation of ISettingDefaultValue
		/// <param name="dataDescriptionInstance">Data description instance. This Object have to implement 
		/// IDomainConstruction and ISettingsData</param>
		/// <param name="settingsCallback">Settings callback.</param>
		/// <typeparam name="Value">Generic typeparameter.</typeparam>
		public void RequestData(IDataDescription dataDescriptionInstance, IDataCallback settingsCallback)
        {
            string settingKey = DomainHelper.GetDomainAsKey(dataDescriptionInstance);
			/**
			 * Searched data are not stored
			 */
			if (!Storage.Contains(settingKey)) {
				if (IsSupported<ISettingDefaultValue<IDataContainer>>(dataDescriptionInstance)) //check is custom default value defined 
				{
					var customDefaultValue = GetDefaultValue(dataDescriptionInstance); //get default Value
					Success?.Invoke(this, new DataSuccessEventArgs<IDataContainer>(customDefaultValue, dataDescriptionInstance.ID));
					settingsCallback?.OnObtainData(customDefaultValue); //return default Value
				} else {
                    Fail?.Invoke(this, new DataFailEventArgs<IException>(SettingsException.ExceptionDataNotFound(), dataDescriptionInstance.ID));
					settingsCallback?.OnObtainError(SettingsException.ExceptionDataNotFound()); //return error
				}
				return;
			}
			/**
			 *  Searched data found
			 */
			if (IsSupported<ISettingsComplexData>(dataDescriptionInstance)) //check is custom data format
			{
				//get custom data
				(dataDescriptionInstance as ISettingsComplexData).GetData(settingKey, this, settingsCallback);

			} else {
                Fail?.Invoke(this, new DataFailEventArgs<IException>(SettingsException.ExceptionDataNotFound(), dataDescriptionInstance.ID));
				(settingsCallback as ISettingsCallback<IDataContainer>).OnObtainError(SettingsException.ExceptionUnsupportedType()); //return error
			}
		}

		/// <summary>
		/// This method load data Object from settings for identifier (Domain only).
		/// /// If searched Object don't stored. Return custom default value via settingsCallback member onObtainData(Value data),
		/// otherwise return SettingsErrorTypes.SettingsErrorType.NotFound  via settingsCallback member onObtainError(...), 
		/// </summary>
		/// delivering of custom default value must be ensured by implementation of ISettingDefaultValue
		/// <param name="dataDescriptionInstance">Data description instance. This Object have to implement 
		/// IDomainConstruction and ISettingsData</param>
		/// <param name="settingsCallback">Settings callback.</param>
		/// <typeparam name="Value">Generic typeparameter.</typeparam>
		public void RequestData<TResult, TParameter>(IDataDescription dataDescriptionInstance, IDataCallback<TResult> settingsCallback)
			where TResult : class, IDataContainer
			where TParameter : class, IDataContainer
		{
			string settingKey = DomainHelper.GetDomainAsKey(dataDescriptionInstance);
			/**
			 * Searched data are not stored
			 */
			if (!Storage.Contains(settingKey)) {
				if (IsSupported<ISettingDefaultValue<TResult>>(dataDescriptionInstance)) //check is custom default value defined 
				{
					var customDefaultValue = GetDefaultValue<TResult>(dataDescriptionInstance); //get default Value
                    Success?.Invoke(this, new DataSuccessEventArgs<IDataContainer>(customDefaultValue, dataDescriptionInstance.ID));
					settingsCallback?.OnObtainData(customDefaultValue); //return default Value
				} else {
                    Fail?.Invoke(this, new DataFailEventArgs<IException>(SettingsException.ExceptionDataNotFound(), dataDescriptionInstance.ID));
					settingsCallback?.OnObtainError(SettingsException.ExceptionDataNotFound()); //return error
				}
				return;
			}
			/**
			 *  Searched data found
			 */
			if (IsSupported<ISettingsComplexData>(dataDescriptionInstance)) //check is custom data format
			{
				//get custom data
				(dataDescriptionInstance as ISettingsComplexData).GetData<TResult, TParameter>(settingKey, this, settingsCallback);

			} else {
                Fail?.Invoke(this, new DataFailEventArgs<IException>(SettingsException.ExceptionDataNotFound(), dataDescriptionInstance.ID));
				(settingsCallback as ISettingsCallback<IDataContainer>).OnObtainError(SettingsException.ExceptionUnsupportedType()); //return error
			}

		}

		/// <summary>
		/// This method store basic or custom data Object in settings for normal identifier or composite identifier
		/// dependent on interface implementaion of dataDescriptionInstance.
		/// In case that data is equal system default value or custom default Value. This method will remove stored data from local Storage.
		/// </summary>
		/// for custom data Object, dataDescriptionInstance must be ensure by implementation of ISettingsComplexData,
		/// <br>
		/// for composite identifier, dataDescriptionInstance must be ensure by implementation of IDomainConstructionField
		/// <br>
		/// for custom default value, dataDescriptionInstance must be ensured by implementation of ISettingDefaultValue
		/// <param name="dataDescriptionInstance">Data description instance. This Object have to implement 
		/// IDomainConstruction and ISettingsData</param>
		/// <param name="result">Instance of generic typeparameter Value. Contains data to save in settings.
		/// In case of using composite identifier contains additional indentifiers.</param>
		/// <typeparam name="Value">Generic typeparameter.</typeparam>
		public void SaveData(IDataDescription dataDescriptionInstance, IDataContainer result)
		{
			string settingKey = DomainHelper.GetDomainAsKey(dataDescriptionInstance, result);

			if (NeedToRemove(result, dataDescriptionInstance)) // given data is null or -1(by simple types) 
			{
				RemoveData(settingKey);// remove data from storage
				return;
			}

			if (IsSupported<ISettingsComplexData>(dataDescriptionInstance)) //given data is complex
			{
				(dataDescriptionInstance as ISettingsComplexData).SetData(settingKey, this, result);
			} else {
				throw (SettingsException.ExceptionUnsupportedType() as System.Exception);
			}

		}

		public void SaveData(IDataDescription dataDescriptionInstance, IDataContainer parameter, IDataCallback callback)
		{

            SaveData(dataDescriptionInstance, parameter);
            Success?.Invoke(this, new DataSuccessEventArgs<IDataContainer>(parameter, dataDescriptionInstance.ID));
			callback?.OnObtainData(parameter);
		}

		public T GetData<T>(string Key)
		{
			return Storage.Get<T>(Key);
		}

		public Int16 GetData(string Key)
		{
			return Storage.Get<Int16>(Key);

		}

		public void SetData<T>(string Key, T data)
		{
			Storage.Set(Key, data);
		}

		public void RemoveData(string key)
		{
			Storage.Remove(key);
		}

		#endregion

		#region Private Methods


		/// <summary>
		/// This method compare given data Value with system default value. In case Instance of IDataDescription
		/// implement ISettingDefaultValue Interface: Given data Value will be compared with custom default Value 
		/// </summary>
		/// <returns><c>true</c>, if given value is equal with system/ custom default value, <c>false</c> otherwise.</returns>
		/// <param name="parameter">Instance of generic parameterType Data. Containing Value.</param>
		/// <param name="dataDescription">instance of IDataDescription.</param>
		/// <typeparam name="Value">Generic parameterType</typeparam>
		private bool NeedToRemove(IDataContainer parameter, IDataDescription dataDescription)
		{
			if (parameter.IsNullValue) {
				return true;
			}
			IDataContainer defaultValue = typeof(IDataContainer).Default<IDataContainer>(); //get system default value
			if (IsSupported<ISettingDefaultValue<IDataContainer>>(dataDescription)) //check support custom default value
			{
				defaultValue = GetDefaultValue(dataDescription);
			}
			return parameter.Equals(defaultValue);
		}

		/// <summary>
		/// This mehtod validate do Instance of IDataDescription implement Interface given as 
		/// generic parametertype
		/// </summary>
		/// <returns><c>true</c>, if implement, <c>false</c> otherwise.</returns>
		/// <param name="targetObject"> instance of IDataDescription that will be validated</param>
		/// <typeparam name="InterFaceType">Generic paramterType </typeparam>
		private bool IsSupported<InterFaceType>(IDataDescription targetObject)
		{
			var isSupported = targetObject is InterFaceType;
			return isSupported;
		}

		/// <summary>
		/// This method cast targetObject to ISettingDefaultValue-Value- interface
		/// and performe member getDefaultValue()
		/// </summary>
		/// <returns>The dafault value.</returns>
		/// <param name="targetObject"> Instance of IDataDescription Interface</param>
		/// <typeparam name="Value">return costum defined defautl Value.</typeparam>
		IDataContainer GetDefaultValue(IDataDescription targetObject)
		{
			return (targetObject as ISettingDefaultValue<IDataContainer>).GetDefaultValue();
		}

		/// <summary>
		/// This method cast targetObject to ISettingDefaultValue-Value- interface
		/// and performe member getDefaultValue()
		/// </summary>
		/// <returns>The dafault value.</returns>
		/// <param name="targetObject"> Instance of IDataDescription Interface</param>
		/// <typeparam name="Value">return costum defined defautl Value.</typeparam>
		TResult GetDefaultValue<TResult>(IDataDescription targetObject)
			where TResult : class, IDataContainer
		{
			return (targetObject as ISettingDefaultValue<TResult>).GetDefaultValue();
		}

		#endregion

	}
}


