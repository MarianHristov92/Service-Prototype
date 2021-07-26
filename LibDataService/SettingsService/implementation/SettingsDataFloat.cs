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
	public class SettingsDataFloat : IDataContainer
	{
		public float Data { get; set; }


		public SettingsDataFloat(float data)
		{
			Data = data;
		}

		public Type DataType => Data.GetType();

		public override string ToString()
		{
			return string.Format("[SettingsDataFloat: Data={0}]", Data);
		}

		public bool IsNullValue => false;
	}
}
