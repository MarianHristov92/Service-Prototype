// ///-----------------------------------------------------------------
// ///   Class:          IDataContainerSimpleType
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 26.10.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
using System;
using LibDataService.DataModels.Container;

namespace LibDataService
{
	public class DataContainerSimpleType : IDataContainer
	{

		#region Attributes

		Type _containingDataType;

		#endregion

		#region Properties

		object _data;
		public object Data => _data;

		#endregion

		#region Constructor

		#endregion

		#region Public Methods

		public void SetData(Object data)
		{
			_data = data;
		}

		public void SetDataType(Type toContainDataType)
		{
			_containingDataType = toContainDataType;
		}

		public Type DataType => _containingDataType;

		public bool IsNullValue => _data == null;

		#endregion

	}
}
