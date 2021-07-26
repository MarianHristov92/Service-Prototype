// ///-----------------------------------------------------------------
// ///   Class:          ISettingsManager
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 29.09.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
using LibDataService.DataService;

namespace LibDataService.SettingsService
{

	public interface ISettingsManager : IDataOperation 
	{

		/// <summary>
		/// This method load data Object from settings for identifier key
		/// </summary>
		/// <returns>Instance of generic typeparameter Value. Containts stored data or null/-1</returns>
		/// <param name="key"><a href="linkURL">String</a> identifier to find stored data in Settings</param>
		/// <typeparam name="Value">Generic typeparameter.</typeparam>
		T GetData<T>(string key);

		/// <summary>
		/// This method store data Object in settings for identifier key.
		/// <br>Only basic Types can be stored.
		/// </summary>
		/// <list type="Types">
		/// <item >String</item>
		/// <item >Int</item>
		/// <item >Float</item>
		/// <item >Boolean</item>
		/// </list>
		/// <param name="Key"><a href="linkURL">String</a> Identifier to save value of data in Settings</param>
		/// <param name="data">Instance of generic typeparameter Value. Contains Data to save in Settings</param>
		/// <typeparam name="Value">Generic typeparameter.</typeparam>
		void SetData<T>(string Key, T data);

		/// <summary>
		/// This method remove stored data from local Storage
		/// </summary>
		/// <param name="key">String Key under which the data record is stored</param>
		void RemoveData(string key);
	}
}