// ///-----------------------------------------------------------------
// ///   Class:          ISettingsData
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 29.09.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------

namespace LibDataService.SettingsService
{
	/// <summary>
	/// These interfaces represent slightly version of SettingsData Wrapper
	/// Only basic Types can be set / get.
	/// <list type="Types">
	/// <item >String</item>
	/// <item >Int</item>
	/// <item >Float</item>
	/// <item >Boolean</item>
	/// <item >...</item>
	/// </list>
	/// <typeparam name="TType">Generic typeparameter.</typeparam>
	/// </summary>
	public interface ISettingsData<TType>
	{
		/// <summary>
		/// This method get data Object from SettingsData Wrapper
		/// </summary>
		/// <returns>Instace of generic typeParameter</returns>
		TType GetData();

		/// <summary>
		/// This method set data Object to SettingsData Wrapper
		/// </summary>
		/// <param name="data">Instance of generic typeprameter, contain Value.</param>
		/// 
		void SetData(TType data);
	}
}
