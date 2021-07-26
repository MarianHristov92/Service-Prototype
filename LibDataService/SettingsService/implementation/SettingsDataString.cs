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
using LibDataService.DataModels.Description.Domain;

namespace LibDataService.SettingsService
{
	/// <summary>
	/// This Class is SettingsData Wrapper for basic Parametertypes
	/// </summary>
	public class SettingsDataString : IDataContainer
	{
		public String Data { get; set; }
		public SettingsDataString()
		{
		}

		public SettingsDataString(String data)
		{
			Data = data;
		}

		public Type DataType => Data.GetType();

		public override string ToString()
		{
			if (Data == null) {
				return "[SettingsDataString: Data=null]";
			}
			return string.Format("[SettingsDataString: Data={0}]", Data);
		}

		public bool IsNullValue => Data == null;
	}
}
