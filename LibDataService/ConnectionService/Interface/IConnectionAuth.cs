// ///-----------------------------------------------------------------
// ///   Class:          IConnectionAuth
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 05.12.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
using LibDataService.DataModels.Container;

namespace LibDataService.ConnectionService
{
	public interface IConnectionAuth
	{
		bool HasAuthentification { get; }

		AuthentificationType AuthentificationType { get; } 

		string GetBearerToken(IDataContainer data);

		BasicAuthentification GetBasicAuthentification(IDataContainer data);
	}
}
