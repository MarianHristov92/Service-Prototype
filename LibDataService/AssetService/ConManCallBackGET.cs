// ///-----------------------------------------------------------------
// ///   Class:          ConManCallBackGET
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 13.10.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
using System;
using LibDataService.ConnectionManager;
using LibDataService.DataModels.Callback;
using LibDataService.DataModels.Description;

namespace LibDataService.AssetManager
{
	/// Callback for Interaction with Connection manager
	/// </summary>
	public class ConManCallBackGET<T> : CacheManager.ICacheCallback<T>
	{
		IDataCallback<T> ExternCallback;
		IDataDescription DataDescriptionInstance;
		IConnectionManager ConManInst;
		T Data;
		public enum ActionType
		{
			read,
			write
		}
		public ConManCallBackGET ( IDataDescription dataDescriptionInstance, T data, IDataCallback<T> externCallback )
		{
			ExternCallback = externCallback;
			DataDescriptionInstance = dataDescriptionInstance;
			Data = data;
		}

		public void onDataCached ()
		{
			ExternCallback.onObtainData ( Data );
		}

		public void onObtainCacheError ( string error )
		{
			ExternCallback.onObtainError ();
		}

		public void onObtainData ( T data )
		{
			ExternCallback.onObtainData ( data );
		}

		public void onObtainError ()
		{
			ExternCallback.onObtainError ();
		}
	}
}
