using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
namespace LibDataService.ConnectionManager
{
	public interface IGenericApi<T, in TKey> where T : class
	{
		[Post("")]
		Task<T> Create([Body] T paylod);

		[Get("")]
		Task<List<T>> ReadAll();

		[Get("/{key}")]
		Task<T> GetOne(TKey key);

		[Get("/{key}")]
		Task<List<T>> GetMultiple(TKey key);

		[Put("/{key}")]
		Task Update(TKey key, [Body]T payload);

		[Delete("/{key}")]
		Task Delete(TKey key);

		[Patch("/{key}")]
		Task Patch(TKey key, [Body]T payload);
	}
}
