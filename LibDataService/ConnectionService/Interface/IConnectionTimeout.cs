// ///-----------------------------------------------------------------
// ///   Class:          IConnectionTimeout
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 19.12.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
namespace LibDataService.ConnectionService
{
	/// <summary>
	/// Intereface for addition custom Timeout for request
	/// </summary>
	public interface IConnectionTimeout
	{
		int GetTimeout();
	}
}
