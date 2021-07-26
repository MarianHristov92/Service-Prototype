// ///-----------------------------------------------------------------
// ///   Class:          DataServiceTest
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 21.11.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
using System;
using Data.Description.Domain;
using Data.Description.Domain.Asset;
using Data.Description.Domain.Settings;
using Data.Description.DomainAssociator;
using LibDataService;
using LibDataService.DataModels.Callback;
using LibDataService.DataModels.Container;
using LibDataService.DataModels.Description;
using LibDataService.DataModels.Description.Domain;
using LibDataService.DataService;
using LibDataService.SettingsManager;
using NUnit.Framework;

namespace Test.iOS
{
	[TestFixture]
    public class DataServiceTest
    {

		IDataService DataManager;
		[SetUp]
		public void RunBeforeAnyTests ()
		{
			DataManager = new DataService();
		}
		[TearDown]
		public void RunAfterAnyTests ()
		{
			DataManager = null;
		}
		/// <summary>
		/// TEST
		/// soter and load data immediately to/from settings
		/// </summary>
		[Test]
		public void loadSettingsData ()
		{
			SettingsDataInt intValueToStore = new SettingsDataInt ( 25 );
			DataManager.setData ( DomainAssociator.Setting.Example.BasicIntData, intValueToStore );
			DataManager.getData ( DomainAssociator.Setting.Example.BasicIntData, new TestDataServiceCB ( intValueToStore ) );
		}
		/// <summary>
		/// TEST
		/// load Data from Setting that is'n stored
		/// </summary>
		[Test]
		public void loadSettingsNotStoredData () 
		{
			SettingsDataFloat intValueToStore = new SettingsDataFloat ( 25 );
			TestDataServiceCB callback = new TestDataServiceCB ( intValueToStore );
			callback.expectedError= SettingsException.SettingsExceptionType.DataNotFound.ToString();

			DataManager.getData ( DomainAssociator.Setting.Example.BasicFloatData,callback);
		}

		/// <summary>
		/// TEST
		/// store data in settings with invalid Data Description Instance
		/// </summary>
		[Test]
		public void storeSettingsDataWithBrokenDaDesc () 
		{
			TestDataServiceCB testCallback = new TestDataServiceCB ( null );
			String expectedError = SettingsException.SettingsExceptionType.UnsupportedType.ToString ();
			testCallback.expectedError = expectedError;
			try
			{
				DataManager.setData ( new TestDataServiceDaDe ( "storeNotSupportedObject",3 ), new SettingsDataInt ( 25 ) );
			}
			catch ( Exception e )
			{
				if ( e.Message.Equals("Given instance don't implement ISettingsComplexData / IConnectionDescription"))
				{
					Assert.Pass ();
					return;
				}
				Assert.Fail ();
			}
		}

		/// <summary>
		/// TEST
		/// load data from settings with invalid Data Description Instance
		/// </summary>

		public void loadSettingsDataWithBrokenDaDesc () 
		{ 
			TestDataServiceCB testCallback = new TestDataServiceCB ( null );
			String expectedError = SettingsException.SettingsExceptionType.UnsupportedType.ToString ();
			testCallback.expectedError = expectedError;
			try
			{
				DataManager.getData ( new TestDataServiceDaDe ( "storeNotSupportedObject", 3 ), testCallback);
			}
			catch ( Exception e )
			{
				if ( e.Message.Equals ( "Given instance don't implement ISettingsComplexData / IConnectionDescription" ) )
				{
					Assert.Pass ();
					return;
				}
				Assert.Fail ();
			}
		}

		/// <summary>
		/// TEST
		/// store (Null)data in settings
		/// </summary>
		[Test]
		public void storeSettingsNullData () 
		{
			TestDataServiceCB testCallback = new TestDataServiceCB ( new SettingsDataString ( null ) );
			String expectedError = SettingsException.SettingsExceptionType.UnsupportedType.ToString ();
			testCallback.expectedError = expectedError;
			DataManager.setData ( new TestDataServiceDaDeSettings ( "storeNotSupportedObject", 3 ), new SettingsDataString (null ),testCallback );
		}

    }

	public class TestDataServiceDaDe : IDomainConstruction, IDataDescription
	{
		protected String Key;
		protected int DomainPart;

		/// <summary>
		/// param name="part" { User = 1, Assets = 2,Settings = 3}
		/// </summary>
		public TestDataServiceDaDe ( String key, int part)
		{
			Key = key;
			DomainPart = part;
		}
		public string getDomain ()
		{
			switch ( DomainPart ) 
			{
				case ( int ) RootDomain.DataTypes.Assets:
					return AssetDomain.Endpoints.Example + RootDomain.Separator + Key;
				case ( int ) RootDomain.DataTypes.Settings:
					return SettingsDomain.Endpoints.Example + RootDomain.Separator + Key;
				case (int)RootDomain.DataTypes.User:
					return SettingsDomain.Endpoints.Example + RootDomain.Separator + Key;
				default:
					return "usupport Domain Area";
			}
		}

		public double getValidSpan ()
		{
			return 100;
		}
	}
	public class TestDataServiceDaDeSettings : TestDataServiceDaDe, ISettingsData
	{
		public TestDataServiceDaDeSettings ( String key, int part ) : base ( key, part)
		{} 
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

	public class TestDataServiceCB : ISettingsCallback<IDataContainer>
	{
		public IDataContainer ExpectedValue { get; set; }
		public String expectedError { get; set; }
		public TestDataServiceCB ( IDataContainer eXpectedValue )
		{
			ExpectedValue = eXpectedValue;
		}

		public void onObtainData ( IDataContainer data )
		{

			Log.i ( "onObtainData" + ExpectedValue + " == " + data );
			Assert.AreEqual ( ExpectedValue.ToString (), data.ToString () );
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
