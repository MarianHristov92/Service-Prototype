
using System;
using LibDataService.CacheManager;
using LibDataService.DataModels.Description;
using NUnit.Framework;

namespace Test.iOS
{
	public class Cacheable_TestObject
	{
		IDataDescription test_object;

		public Cacheable_TestObject ( IDataDescription t_ob )
		{
			test_object = t_ob;
		}

		public bool IImpl_ICacheData ()
		{
			return ( test_object is ICacheData );
		}
		public double IImpl_ICachData_getValidSpan ()
		{
			if(IImpl_ICacheData())
				return ( test_object as ICacheData ).getValidSpan ();
			return -1;
		}
	}
}
