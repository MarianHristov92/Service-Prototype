
using System;
using System.Threading.Tasks;
using LibDataService;
using LibDataService.DataManager;
using LibDataService.DataModels.Callback;
using LibDataService.DataModels.Container;
using LibDataService.DataModels.Description;
using LibDataService.DataModels.Description.Domain;
using LibDataService.SettingsManager;
using NUnit.Framework;

namespace Test.iOS
{
    [TestFixture]
    public class SettingsManagerTest
    {
		IDataManager DataManager;
		SettingsManager SettingsManager;
		[SetUp]
		public void RunBeforeAnyTests ()
		{
			SettingsManager = new SettingsManager ();
			DataManager = SettingsManager;
		}
		[TearDown]
		public void RunAfterAnyTests ()
		{
			SettingsManager = null;
		}

		/*
		 * basic save- load- operation direct with out callbacks and ErrorHandling
		 */
		#region 
		/// <summary>
		/// TEST
		/// basic test direct store and load String value
		/// </summary>
		[Test]
		public void SettingSaveLoadString () 
		{
			String toStoreString = "ich bin ein test String";
			SettingsManager.setData ( "storeString", toStoreString );
			String loadedString = SettingsManager.getData<String> ( "storeString" );
			Assert.AreEqual(loadedString,toStoreString );
		}

		/// <summary>
		/// TEST
		/// basic test direct store and load int value
		/// </summary>
		[Test]
		public void SettingSaveLoadInt ()
		{
			int toStoreInt = 777;
			SettingsManager.setData ( "storeInt", toStoreInt );
			int loadedInt = SettingsManager.getData<int> ( "storeInt" );
			Assert.AreEqual ( toStoreInt, loadedInt );
		}

		/// <summary>
		/// TEST
		/// basic test direct store and load flaot value
		/// </summary>
		[Test]
		public void SettingSaveLoadFloat ()
		{
			float toStoreFloat = 2.2f;
			SettingsManager.setData ( "storeFloat", toStoreFloat );
			float loadedFloat = SettingsManager.getData<float> ( "storeFloat" );
			Assert.AreEqual ( toStoreFloat, loadedFloat );
		}

		/// <summary>
		/// TEST
		/// basic test direct store and load bool value
		/// </summary>
		[Test]
		public void SettingSaveLoadBool ()
		{
			bool toStoreBool = true;
			SettingsManager.setData ( "storeBool", toStoreBool );
			bool loadedBool = SettingsManager.getData<bool> ( "storeBool" );
			Assert.AreEqual ( toStoreBool, loadedBool );
		}

		/// <summary>
		/// TEST
		/// basic test direct store  a null value
		/// </summary>
		[Test]
		public void SettingSaveloadNullValue ()
		{
			String toStoreString = null;
			SettingsManager.setData ( "storeNullValue", toStoreString );
			String loadedString = SettingsManager.getData<String> ( "storeNullValue" );
			Assert.AreEqual ( loadedString, toStoreString );
		}


		/// <summary>
		/// TEST
		/// basic test direct laod not stored value
		/// </summary>
		[Test]
		public void SettingLoadDeletedValue ()
		{
			String toStoreString = "tem";
			SettingsManager.setData ( "toStoreString", toStoreString );
			SettingsManager.removeData ( "toStoreString" );
			String loadedString = SettingsManager.getData<String> ( "toStoreString" );
			Assert.AreEqual ( loadedString, null );
		}
		#endregion

		/*
		 * advanced save-, load- operation with  callbacks and ErrorHandling
		 */
		#region
		/// <summary>
		/// TEST 
		/// try to store Data with DataDescription without Implementation of ISettingsData
		/// </summary>
		[Test]
		public void storeNotSupportedObject ()
		{
			TestSettingsCB testCallback = new TestSettingsCB (null);
			String expectedError=SettingsException.SettingsExceptionType.UnsupportedType.ToString ();
			testCallback.expectedError = expectedError;
			try
			{
				DataManager.setData ( new TestDaDe ( "storeNotSupportedObject" ), new SettingsDataInt ( 25 ) );
			}
			catch (Exception e) 
			{
				if ( e is SettingsException ) 
				{
					String SettingErrorType = ( e as SettingsException ).SettingsType.ToString ();
					Assert.AreEqual ( SettingErrorType, expectedError );
					return;
				}
				Assert.Fail ();
			}
		}

		/// <summary>
		/// TEST 
		/// try to load Data with DataDescription without Implementation of ISettingsData
		/// </summary>
		[Test]
		public void loadNotSupportedObject ()
		{
			TestSettingsCB testCallback = new TestSettingsCB ( null );
			String expectedError = SettingsException.SettingsExceptionType.UnsupportedType.ToString ();
			testCallback.expectedError = expectedError;
			DataManager.setData( new TestSettingDaDe ( "loadNotSupportedObject" ),new SettingsDataString ( "asd" ) );
			try
			{
				DataManager.getData ( new TestDaDe ( "loadNotSupportedObject" ), testCallback );
			}
			catch ( Exception e )
			{
				if ( e is SettingsException )
				{
					String SettingErrorType = ( e as SettingsException ).SettingsType.ToString ();
					Assert.AreEqual ( SettingErrorType, expectedError );
					return;
				}
				Assert.Fail ();
			}
		}

		/// <summary>
		/// TEST 
		/// try to load  not stored Data 
		/// </summary>
		[Test]
		public void loadNotStoredObject ()
		{
			TestSettingsCB testCallback = new TestSettingsCB ( null );
			String expectedError = SettingsException.SettingsExceptionType.DataNotFound.ToString ();
			testCallback.expectedError = expectedError;
			try
			{
				DataManager.getData ( new TestSettingDaDe ( "loadNotStoredObject" ), testCallback );
			}
			catch ( Exception e )
			{
				if ( e is SettingsException )
				{
					String SettingErrorType = ( e as SettingsException ).SettingsType.ToString();
					Assert.AreEqual ( SettingErrorType, expectedError );
					return;
				}
				Assert.Fail ();
			}
		}
		#endregion


    }
	public class TestDaDe : IDomainConstruction, IDataDescription
	{
		protected String Key;
		public TestDaDe ( String key )
		{
			Key = key;
		}
		public string getDomain ()
		{
			return "test_string_" + Key;
		}

		public double getValidSpan ()
		{
			return 100;
		}
	}
	public class TestSettingDaDe : TestDaDe, ISettingsData
	{
		public TestSettingDaDe ( String key ) : base ( key ) 
		{
		}

		public void getData ( string settingKey, ISettingsManager manager, IDataCallback<IDataContainer> settingCallback )
		{
			String readedValue = manager.getData<String> ( settingKey );
			SettingsDataString cont = new SettingsDataString ( readedValue );
			settingCallback.onObtainData ( cont );

		}
		public void setData ( string settingKey, ISettingsManager manager, IDataContainer data )
		{
			String toWriteValue = ( data as SettingsDataString ).Data;
			manager.setData<String> ( settingKey, toWriteValue );
		}
	}
	public class TestSettingsCB : ISettingsCallback<IDataContainer>
	{
		public IDataContainer ExpectedValue { get; set; }
		public String expectedError { get; set; }
		public TestSettingsCB ( IDataContainer eXpectedValue )
		{
			ExpectedValue = eXpectedValue;
		}
		public void onObtainData ( IDataContainer data )
		{
			Assert.AreEqual ( ExpectedValue, data );
			Log.i ( "onObtainData" + ExpectedValue + " == " + data );
		}

		public void onObtainError ( IException exception )
		{
			if ( exception is SettingsException )
			{
				String exeptiontype = ( ( exception as SettingsException ).SettingsType.ToString () );
				Assert.AreEqual ( expectedError, exeptiontype );
				return;
			}
			Assert.Fail ();
		}
	}
}
