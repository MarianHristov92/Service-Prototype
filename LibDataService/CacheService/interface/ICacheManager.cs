// ///-----------------------------------------------------------------
// ///   Class:          ICacheContainer
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 07.10.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
using LibDataService.DataModels.Callback;
using LibDataService.DataModels.Container;
using LibDataService.DataModels.Description;
using LibDataService.DataService;

namespace LibDataService.CacheService
{
	/// <summary>
	/// This Interface expanded IDataManager with min. capabilities of CacheManager
	/// </summary>
	public interface ICacheManager : IDataOperation 
	{
		/// <summary>
		/// This method cache given record (dataToCache) under the use of Recordendpoint description (dataDescriptionInstance)
		/// with additional spezification in (dataDes). As soon the operation is complete, Observer will be notifyed by
		/// calling relevant method of (callback)
		/// </summary>
		/// <param name="dataDescriptionInstance">Recordendpoint description.</param>
		/// <param name="parameter">record with spezification info's of to cached record </param>
		/// <param name="dataToCache">record to cache</param>
		/// <param name="callback">Callback to notify when operation is complete with result.</param>
		void SaveData(IDataDescription dataDescriptionInstance, IDataContainer parameter, IDataContainer dataToCache, IDataCallback callback);

		/// <summary>
		/// This method cache given record (dataToCache) under the use of Recordendpoint description (dataDescriptionInstance)
		/// with additional spezification in (dataDes). Observer will not be informed. (fire and forget)
		/// </summary>
		/// <param name="dataDescriptionInstance">Recordendpoint description.</param>
		/// <param name="parameter">record with spezification info's of to cached record </param>
		/// <param name="dataToCache">record to cache</param>
		void SaveData(IDataDescription dataDescriptionInstance, IDataContainer parameter, IDataContainer dataToCache);
	}
}