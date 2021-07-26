// ///-----------------------------------------------------------------
// ///   Class:          IDataContainerSimpleType
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 26.10.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
using System;

namespace LibDataService.DataModels.Container
{

	public interface IDataContainer
	{
		Type DataType { get; }
		bool IsNullValue { get; }
	}
}