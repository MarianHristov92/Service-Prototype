// ///-----------------------------------------------------------------
// ///   Class:          ISettingsCallback
// ///   Description:    <These interfaces represent basic version of SettingsCallback>
// ///   Author:         Dimitri Renke                    Date: 29.09.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
using LibDataService.DataModels.Callback;

namespace LibDataService.SettingsService
{
	/// <summary>
	/// These interfaces represent basic version of SettingsCallback
	/// <typeparam name="TType">Generic typeparameter.</typeparam>
	/// </summary>
	public interface ISettingsCallback<TType> : IDataCallback<TType>
	{
	}
}
