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
	public class SettingsDataInt : IDataContainer
	{
		public int Data { get; set; }
		public SettingsDataInt()
		{
		}

		public SettingsDataInt(int data)
		{
			Data = data;
		}

		public Type DataType => Data.GetType();

		public override string ToString()
		{
			return string.Format("[SettingsDataInt: Data={0}]", Data);
		}

		public bool IsNullValue => false;
	}
}
