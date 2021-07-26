// ///-----------------------------------------------------------------
// ///   Class:          EmptyClass
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 30.09.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
using System;
using LibDataService.DataModels.Container;

namespace LibDataService.SettingsService
{
	/// <summary>
	/// This Class is SettingsData Wrapper for basic Parametertypes
	/// </summary>
	public class SettingsDataBool : IDataContainer
	{
		public bool Data { get; set; }

		public SettingsDataBool(bool data)
		{
			Data = data;
		}

		public Type DataType => Data.GetType();

		public override string ToString()
		{
			return string.Format("[SettingsDataBool: Data={0}]", Data);
		}

		public bool IsNullValue => false;

	}
}
