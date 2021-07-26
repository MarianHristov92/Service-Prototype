// ///-----------------------------------------------------------------
// ///   Class:          ICacheContainer
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 07.10.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
using System;
namespace LibDataService.CacheService
{
	/// <summary>
	/// This interface represent min. capabilities of Wrapper for record which need to be cached
	/// </summary>
	public interface ICacheDataContainer<T>
	{
		/// <summary>
		/// This method compare CreatainTime + ValidSpan(in milliseconds) ticks with  DateTime.now()
		/// </summary>
		/// <returns><c>true</c>CreatainTime + ValidSpan bigger as DateTime.now()  <c>false</c> otherwise.</returns>
		bool IsValid { get; }

		/// <summary>
		/// This method returns the DateTime at which the record becomes invalid.
		/// </summary>
		/// <returns>DateTime at which the record becomes invalid</returns>
		DateTime ValidUntil { get; }

		/// <summary>
		/// This method returns the DateTime at which the record was created (cached).
		/// </summary>
		/// <returns>DateTime at which the record was created (cached).</returns>
		DateTime CreatedAt { get; }

		/// <summary>
		/// This method set Time in milliseconds as long the record is valid.
		/// </summary>
		/// <param name="millisecounds">int Time in milliseconds as long the record is valid</param>
		long ValidUntilMilliSeconds { get; set; }

		/// <summary>
		/// This method return record to cache from memeber
		/// </summary>
		/// <returns>Instance of generic typeparemter, containing record to cache</returns>
		T Data { get; set; }
	}
}
