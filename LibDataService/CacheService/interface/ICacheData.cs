// ///-----------------------------------------------------------------
// ///   Class:          ICacheData
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 13.10.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
using System;
namespace LibDataService.CacheService
{
	/// <summary>
	/// This interface make possible to set custom caching preference to record.
	/// </summary>
    public interface ICacheData
    {
		/// <summary>
		/// this method return valid until of record. In millisecound.
		/// </summary>
		/// <returns>The valid until</returns>
		long ValidUntil { get; } 
    }
}
