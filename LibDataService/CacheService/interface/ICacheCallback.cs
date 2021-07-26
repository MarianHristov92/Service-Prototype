using LibDataService.DataModels.Callback;

namespace LibDataService.CacheService
{
	public interface ICacheCallback<T> : IDataCallback<T>
	{
		void OnDataCached();
		void OnObtainCacheError(string error);
	}
}
