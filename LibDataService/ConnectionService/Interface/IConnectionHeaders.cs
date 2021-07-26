// ///-----------------------------------------------------------------
// ///   Class:          IConnectionHeaders
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 19.12.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
using System.Collections.Generic;
using LibDataService.DataModels.Container;

namespace LibDataService.ConnectionService
{
	/// <summary>
	/// Interface to add static or dynamic Headers
	/// </summary>
	public interface IConnectionHeaders
	{
		bool HasHeader { get; }
		List<KeyValuePair<string, string>> Headers { get; }

		bool HasDynamicHeader(IDataContainer dataIn);

		List<KeyValuePair<string, string>> GetDynamicHeaders(IDataContainer dataIn);
	}
}

