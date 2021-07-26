// ///-----------------------------------------------------------------
// ///   Class:          IDataCallback
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 06.10.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
using LibDataService.Exception;
using LibDataService.DataModels.Container;
using System;

namespace LibDataService.DataModels.Callback
{
	public interface IDataCallback<TResult> : IDataCallback
	{
		void OnObtainData(TResult data);
	}

	public interface IDataCallback
	{
        int ID { get; set; }
        Type Source { get; set; }
		void OnObtainData(IDataContainer data);
		void OnObtainError(IException exception);
	}

}
