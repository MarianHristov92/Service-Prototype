// ///-----------------------------------------------------------------
// ///   Class:          IConnectionPriorityProcessing
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 19.12.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
using LibDataService.DataModels.Container;

namespace LibDataService.ConnectionService
{
	/// <summary>
	/// Interface to add priority flag to request.
	/// it is possible add flag parmenently or at run time
	/// </summary>
	public interface IConnectionPriorityProcessing
	{
		bool HasPriority();
		bool HasPriority(IDataContainer dataIn);
	}
}
