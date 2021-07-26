// ///-----------------------------------------------------------------
// ///   Class:          TSettingManagerAsync
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 15.11.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
using System;
using System.Threading.Tasks;
using Data.Description.DomainAssociator;
using LibDataService;
using LibDataService.CacheManager;
using LibDataService.DataModels.Callback;
using LibDataService.DataModels.Container;
using LibDataService.DataModels.Description;
using LibDataService.DataModels.Description.Domain;
using LibDataService.DataService;
using LibDataService.SettingsManager;
using NUnit.Framework;

namespace Test.iOS
{
    public class CacheManagerTest
    {
		CacheManagerDictonary cacheMan;

		[SetUp]
		public void RunBeforeAnyTests ()
		{
			cacheMan=new CacheManagerDictonary ();
		}
		[TearDown]
		public void RunAfterAnyTests ()
		{
			cacheMan = null;
		}
      	/// <summary>
      	/// TEST
		/// cache and load data immediately
      	/// </summary>
		[Test]
		public void StoreAndLoadImmediately ()
		{
			SettingsDataString dataContainer = new SettingsDataString ( "hier mein text" );
			TestCallback testCallback = new TestCallback ( dataContainer );
			cacheMan.setData ( DomainAssociator.Asset.Example.StringExampleAssetDomain, dataContainer );
			cacheMan.getData ( DomainAssociator.Asset.Example.StringExampleAssetDomain, testCallback );
		}

		/// <summary>
		/// TEST
		/// loading Data from cach if data is not cached
		/// </summary>
		[Test]
		public void LoadNotCachendItem ()
		{
			TestCallback testCallback = new TestCallback ( null );
			testCallback.expectedError = CachingException.CachedExceptionType.DataNotFound.ToString ();
			cacheMan.getData ( new TestDataDescription ( "LoadNotCachendItem" ), testCallback );
		}

		/// <summary>
		/// TEST
		/// Laod cached data whitch valid pariod is exipred
		/// </summary>
		[Test]
		public async void LoadInvalidetItem ()
		{
				TestCallback testCallback = new TestCallback ( null );
				testCallback.expectedError = CachingException.CachedExceptionType.DataInvalid.ToString ();
				TestDataDescription testDataDesc = new TestDataDescription ( "LoadInvalidetItem" );
				SettingsDataString dataContainer = new SettingsDataString ( "hier mein text" );
				cacheMan.setData ( testDataDesc, dataContainer );
				await Task.Delay ( 100 );
				cacheMan.getData ( testDataDesc, testCallback );
		}

		/// <summary>
		/// TEST
		/// try to cache null value
		/// </summary>
		[Test]
		public void StoreNullItem ()
		{
			TestDataDescription testDataDesc = new TestDataDescription ( "StoreNullItem" );
			try
			{
				cacheMan.setData ( testDataDesc, null );
			}
			catch ( Exception e )
			{
				if ( e is CachingException )
				{
					Log.i ( "StoreNullItem" + ( e as CachingException ).CachedType );
					if ( ( e as CachingException ).CachedType == CachingException.CachedExceptionType.CantCacheNullValue )
					{
						Assert.Pass ();
						return;
					}
				}
				Log.i ( "StoreNullItem " + " is not Cach Exeption" );

				Assert.Fail ();

			}
		}

    }


	public class TestDataDescription : IDomainConstruction, IDataDescription, ICacheData
	{
		String Key;
		public TestDataDescription ( String key ) 
		{
			Key = key;
		}

		public string getDomain ()
		{
			return "test_string_"+Key;
		}

		public double getValidSpan ()
		{
			return 100;
		}
	}
	public class TestCallback:IDataCallback<IDataContainer>
	{
		public IDataContainer ExpectedValue { get; set;}
		public String expectedError { get; set;}
		public TestCallback ( IDataContainer eXpectedValue ) 
		{
			ExpectedValue = eXpectedValue;
		}
		public void onObtainData ( IDataContainer data )
		{
			Assert.AreEqual ( ExpectedValue, data );
			Log.i ( "onObtainData"+ExpectedValue+" == "+data );
		}

		public void onObtainError ( IException exception )
		{
			if ( exception is CachingException )
			{
				String exeptiontype = ( ( exception as CachingException ).CachedType.ToString () );
				Assert.AreEqual ( expectedError, exeptiontype );
				return;
			} 
			Assert.Fail ();
		}
	}
	
}
