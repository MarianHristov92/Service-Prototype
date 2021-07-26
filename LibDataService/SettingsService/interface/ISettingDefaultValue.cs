// ///-----------------------------------------------------------------
// ///   Class:          ISettingDefaultValue
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 06.10.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
namespace LibDataService.SettingsService
{
	/// <summary>
	/// These interfaces make possible that SettingsData Wrapper can set custom default value.
	///	this Value willbe return in cases if search value are not saved in setting
	/// <typeparam name="TType">Generic typeparameter.</typeparam>
	/// </summary>
	public interface ISettingDefaultValue<TType>
	{
		/// <summary>
		/// This method return custom default value
		/// </summary>
		/// <returns>The default for generic typeparameter value.</returns>
		TType GetDefaultValue();
	}
}

