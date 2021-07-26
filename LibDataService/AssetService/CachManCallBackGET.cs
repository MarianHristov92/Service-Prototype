// ///-----------------------------------------------------------------
// ///   Class:          asd
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
	public class CachManCallBackGET<T> : CacheManager.ICacheCallback<T>
	{
		IDataCallback<T> ExternCallback { get; set; }
		IDataDescription DataDescriptionInstance { get; set; }
		T Data { get; set; }
		public enum ActionType
		{
			read,
			write
		}
		ActionType ActionDirection { get; set; }
		public CachManCallBackGET ( IDataDescription dataDescriptionInstance, T data, IDataCallback<T> externCallback, IConnectionManager conman )
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
