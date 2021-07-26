// ///-----------------------------------------------------------------
// ///   Class:          CacheDataContainer
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 07.10.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using LibDataService.DataModels.Callback;
using LibDataService.DataModels.Container;
using LibDataService.DataModels.Description;
using LibDataService.DataService;
using LibDataService.Exception;
using LibDataService.Helper;
using LibDataService.Tools;

namespace LibDataService.CacheService
{
	/// <summary>
	/// This Class is basic implementation of ICacheManager interface with Dictionary as record holder.
	/// All record's passed to the instance of this class, will be cached.
	/// Default validspan for all record's is 24h. Unless they support ICacheData.
	/// Where custom period can be specified.
	/// </summary>
	public class CacheManagerDictonary : ICacheManager 
	{
		#region Events

		public event EventHandler<DataSuccessEventArgs<IDataContainer>> Success;
		public event EventHandler<DataFailEventArgs<IException>> Fail;

		#endregion

		#region Constants

		/// <summary>
		/// Default validspan of record is 24h
		/// </summary>
		public static readonly long DefaultCacheTime = 24 * 60 * 60 * 100; //24h

		#endregion

		#region Attributes

		/// <summary>
		/// Internal record holder
		/// </summary>
		Dictionary<string, ICacheDataContainer<IDataContainer>> _cacheDictionary;

		#endregion

		#region Constructor

		public CacheManagerDictonary()
		{
			_cacheDictionary = new Dictionary<string, ICacheDataContainer<IDataContainer>>();
		}

		#endregion

		#region Public Methods

		public void RequestData(IDataDescription dataDescriptionInstance, IDataContainer parameter, IDataCallback callback)
		{
			try {
				string key = DomainHelper.GetDomainAsKey(dataDescriptionInstance, parameter);
				GetDataFromDictionary(key, callback);
			} catch (System.Exception e) {
				BaseException ex = new BaseException(ExceptionInfo.DefaultReason, e);
				callback.OnObtainError(ex);
			}
		}

		public void RequestData<TResult, TParameter>(IDataDescription dataDescriptionInstance, TParameter parameter, IDataCallback<TResult> callback)
			where TResult : class, IDataContainer
			where TParameter : class, IDataContainer
		{
			try {
				string key = DomainHelper.GetDomainAsKey(dataDescriptionInstance, parameter);
				GetDataFromDictionary(key, callback);
			} catch (System.Exception e) {
				BaseException ex = new BaseException(ExceptionInfo.DefaultReason, e);
				callback.OnObtainError(ex);
			}
		}

		public void RequestData(IDataDescription dataDescriptionInstance, IDataCallback callback)
		{
			try {
				string key = DomainHelper.GetDomainAsKey(dataDescriptionInstance);
				GetDataFromDictionary(key, callback);
			} catch (System.Exception e) {
				BaseException ex = new BaseException(ExceptionInfo.DefaultReason, e);
				callback.OnObtainError(ex);
			}
		}

		public void RequestData<TResult, TParameter>(IDataDescription dataDescriptionInstance, IDataCallback<TResult> callback)
			where TResult : class, IDataContainer
			where TParameter : class, IDataContainer
		{
			try {
				string key = DomainHelper.GetDomainAsKey(dataDescriptionInstance);
				GetDataFromDictionary(key, callback);
			} catch (System.Exception e) {
				BaseException ex = new BaseException(ExceptionInfo.DefaultReason, e);
				callback.OnObtainError(ex);
			}
		}

		public void SaveData(IDataDescription dataDescriptionInstance, IDataContainer parameter)
		{
			try {
				string key = DomainHelper.GetDomainAsKey(dataDescriptionInstance);
				Log.AppendToObjectContainer(typeof(CacheManagerDictonary), " will cache Data for Key " + key + " " + parameter);
				SetToDictionary(key, dataDescriptionInstance, parameter, null);
			} catch (System.Exception e) {
				if (e is CachingException)
					throw;
				else {
					BaseException ex = CachingException.ExceptionUnexpectedlye(e);
					throw;
				}
			}
		}

		public void SaveData(IDataDescription dataDescriptionInstance, IDataContainer parameter, IDataCallback callback)
		{
			try {
				string key = DomainHelper.GetDomainAsKey(dataDescriptionInstance, parameter);
				Log.AppendToObjectContainer(typeof(CacheManagerDictonary), " will cache Data for Key " + key + " " + parameter);
				SetToDictionary(key, dataDescriptionInstance, parameter, null);
				// TODO: Is this command useful here ?
				//callback.OnObtainData(parameter);
			} catch (System.Exception e) {
				BaseException ex = CachingException.ExceptionUnexpectedlye(e);
				callback.OnObtainError(ex);
			}
		}

		public void SaveData(IDataDescription dataDescriptionInstance, IDataContainer data, IDataContainer dataToCache, IDataCallback callback)
		{
			try {
				string key = DomainHelper.GetDomainAsKey(dataDescriptionInstance, data);
				Log.AppendToObjectContainer(typeof(CacheManagerDictonary), " will cache Data for Key " + key + " " + dataToCache);
				SetToDictionary(key, dataDescriptionInstance, dataToCache, (callback as ICacheCallback<IDataContainer>));
				// TODO: Is this command useful here ?
				//callback.OnObtainData(dataToCache);
			} catch (System.Exception e) {
				BaseException ex = CachingException.ExceptionUnexpectedlye(e);
				callback.OnObtainError(ex);
				throw;
			}
		}

		public void SaveData(IDataDescription dataDescriptionInstance, IDataContainer data, IDataContainer dataToCache)
		{
			try {
				string key = DomainHelper.GetDomainAsKey(dataDescriptionInstance, data);
				Log.AppendToObjectContainer(typeof(CacheManagerDictonary), " will cache Data for Key " + key + " " + dataToCache);
				SetToDictionary(key, dataDescriptionInstance, dataToCache, null);
			} catch (System.Exception e) {
				BaseException ex = CachingException.ExceptionUnexpectedlye(e);
				throw;
			}
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// This mehtod search cached record in Dictionary. Check is this record still valid (time).
		/// Send record to observer via callback. 
		/// In case record was not found notify obeserver "DataNotFound"
		/// In case record is invalid notify obeserver "DataInvalid" and delete invalid record 
		/// </summary>
		/// <param name="key">final identifier of record </param>
		/// <param name="callback">Callback to notify the Observer</param>
		/// <typeparam name="IDataContainer">The 1st type parameter.</typeparam>
		private void GetDataFromDictionary(string key, IDataCallback callback)
		{
			if (!_cacheDictionary.ContainsKey(key))  // is stored
			{
				//Log.AppendToObjectContainer(typeof(CacheManagerDictonary<TResult, TParameter>), "No Data Cached for Key " + key);
				IException ex = CachingException.ExceptionDataNotFound();
				callback.OnObtainError(ex);
				return;
			}
			var cachedDataContainer = _cacheDictionary[key];
			if (!cachedDataContainer.IsValid) // time validation
			{
				Log.AppendToObjectContainer(typeof(CacheManagerDictonary), "Cached Data for Key " + key + " is invalid ");
				IException ex = CachingException.ExceptionDataInvalid();
				callback.OnObtainError(ex);
				RemoveFromDictionary(key); // remove invalid record

				return;
			}
			var cachedData = cachedDataContainer.Data;
			Log.AppendToObjectContainer(typeof(CacheManagerDictonary), "Cached Data for Key " + key + " found " + cachedData);
			callback.OnObtainData(cachedData); // data found and return
		}

		/// <summary>
		/// This method 
		/// - try to get custom validspan of record. By checking is ICacheData implemeted.
		/// - Create new CacheWrapper (CacheDataContainer) and pass record with validspan to it.
		/// - Delete old cached record if they exist.
		/// - Cache wrapped record.
		/// - Notify Obserser (if is set) opereation complete.
		/// In case: record is null
		/// - Delete old cached record if they exist. 
		/// - Notify Obserser (if is set) "CantCacheNullValue"
		/// </summary>
		/// <param name="key">Key.</param>
		/// <param name="dataDescriptionInstance">Data description instance.</param>
		/// <param name="data">Data.</param>
		/// <param name="callback">Callback.</param>
		/// <typeparam name="IDataContainer">The 1st type parameter.</typeparam>
		private void SetToDictionary(string key, IDataDescription dataDescriptionInstance, IDataContainer data, ICacheCallback<IDataContainer> callback)
		{
			long validUntil = DefaultCacheTime; // set default span
			if (dataDescriptionInstance is ICacheData) {
				validUntil = (dataDescriptionInstance as ICacheData).ValidUntil; //get Custom span
			}

			ICacheDataContainer<IDataContainer> container = new CacheDataContainer<IDataContainer>();
			container.ValidUntilMilliSeconds = validUntil; //set span
			container.Data = data; //set data
			Log.AppendToObjectContainer(typeof(CacheManagerDictonary), " cache Data for Key " + key + " " + data + " valid until " + container.ValidUntil.ToString());

			/// TODO: Improve the logic
			/// Ist es schlau, einen vorhandenen Eintrag immer zu löschen, auch
			/// wenn ein Neueintrag zur Zeit nicht möglich ist? Der alte Eintrag
			/// hätte seine Berechtigung, wenn er sich noch innerhalb seiner
			/// Lebensdauer befindet.

			RemoveFromDictionary(key); // remove old data if exists

			if (data != null) // cach data
			{
				_cacheDictionary.Add(key, container);
				if (callback != null)
					callback.OnDataCached();
			} else {
				IException ex = CachingException.ExceptionCantCacheNullValue();
				if (callback != null) {
					callback.OnObtainError(ex);
				} else {
					throw (ex as System.Exception);
				}
			}
		}

		/// <summary>
		/// This method remove Wrapped Record for identifier (key) if they 
		/// </summary>
		/// <param name="key">Key.</param>
		private void RemoveFromDictionary(String key)
		{
			if (_cacheDictionary.ContainsKey(key)) // remove old data if it exists
			{
				_cacheDictionary.Remove(key);
			}
		}

		#endregion
	}

}