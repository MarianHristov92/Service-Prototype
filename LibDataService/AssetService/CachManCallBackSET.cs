// ///-----------------------------------------------------------------
// ///   Class:          CachManCallBackSET
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
	public class CachManCallBackSET<T> : CacheManager.ICacheCallback<T>
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
		public CachManCallBackSET ( IDataDescription dataDescriptionInstance, T data, IDataCallback<T> externCallback, IConnectionManager conman )
		{
			ExternCallback = externCallback;
			DataDescriptionInstance = dataDescriptionInstance;
			Data = data;
			ConManInst = conman;
		}

		public void onDataCached ()
		{
			ExternCallback.onObtainData ( Data );
		}

		public void onObtainCacheError ( string error )
		{
			loadDataFromCloud ();
		}

		public void onObtainData ( T data )
		{
			ExternCallback.onObtainData ( data );
		}

		public void onObtainError ()
		{
			loadDataFromCloud ();
		}
		private void loadDataFromCloud ()
		{
			if ( Data == null )
				ConManInst.getData ( DataDescriptionInstance, ExternCallback );
			else
				ConManInst.getData ( DataDescriptionInstance, Data, ExternCallback );
		}
	}
}
