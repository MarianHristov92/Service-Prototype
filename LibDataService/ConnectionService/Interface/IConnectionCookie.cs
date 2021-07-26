// ///-----------------------------------------------------------------
// ///   Class:          IConnectionCookie
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
	public interface IConnectionCookie
    {
		bool HasCookie();
		IList<KeyValuePair<string, string>> GetCookies ();
		bool HasDynamicCookie (IDataContainer dataIn);
		IList<KeyValuePair<string, string>> GetDynamicCookies (IDataContainer dataIn) ;
    }
}
