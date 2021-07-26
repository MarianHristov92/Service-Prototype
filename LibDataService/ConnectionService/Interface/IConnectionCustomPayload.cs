// ///-----------------------------------------------------------------
// ///   Class:          IConnectionCustomPayload
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 05.01.2017
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
using System.Net.Http;
using LibDataService.DataModels.Container;

namespace LibDataService.ConnectionService
{
	public interface IConnectionCustomPayload  : IDataContainer
	{
		HttpContent GetCustomPayload(IDataContainer data);
	}
}
