// ///-----------------------------------------------------------------
// ///   Class:          EmptyClass
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 21.11.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
using System;
using Data.Description.DomainAssociator;
using DataServicePrototype.Asset;
using LibDataService;
using LibDataService.AssetManager;
using LibDataService.ConnectionManager;
using LibDataService.DataModels.Callback;
using LibDataService.DataModels.Container;
using LibDataService.DataModels.Description;
using LibDataService.DataModels.Description.Domain;
using LibDataService.SettingsManager;
using NUnit.Framework;
namespace Test.iOS
{
    public class AssertManagerTest
    {
		AssetManager assetMan;

		[SetUp]
		public void RunBeforeAnyTests ()
		{
			assetMan = new AssetManager ();
		}

		[TearDown]
		public void RunAfterAnyTests ()
		{
			assetMan = null;
		}

		/// <summary>
		/// TEST
		/// load data from Cache
		/// </summary>
		//[Test]
		public void loadCachData () 
		{
			//	Boolean operationDone = false;

			//	SettingsDataString expectedValue = LibDataService.ConnectionManager.getExampleJSonResult ();
			//	try
			//	{
			//		assetMan.getData ( DomainAssociator.Asset.Example.ComplexDynDomainExampleAssetDomain, new DTUserDataContainer ( "1170128393" ), new TestAssetCB ( expectedValue,ref operationDone ) );

			//	}
			//	catch ( Exception ex ) 
			//	{
			//		Assert.Fail ();
			//	}
			//do
			//{
			//	Log.i ( "time out " + operationDone );
			//} while ( !operationDone );
			Assert.Ignore();
		}

		/// <summary>
		/// TEST
		/// load data from Cache with invalid Data Description Instance
		/// </summary>
		//[Test]
		public void loadCachDataWithBrokenDaDesc () 
		{
			Assert.Ignore ();
		}

		/// <summary>
		/// TEST
		/// store data in Cache
		/// </summary>
		//[Test]
		public void storeCachData () 
		{ 
			Assert.Ignore ();
		}

		/// <summary>
		/// TEST
		/// store data in Cache with invalide Data Description Instance
		/// </summary>
		//[Test]
		public void storeCacheDataWithBrokenDaDesc () 
		{ 
			Assert.Ignore ();
		}

	}
	public class TestAssetDaDe_defect : IDomainConstruction, IDataDescription
	{
		protected String Key;
		public TestAssetDaDe_defect ( String key )
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
	public class TestAssetDaDe : TestAssetDaDe_defect, ISettingsData
	{
		public TestAssetDaDe ( String key ) : base ( key )
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
	public class TestAssetCB : IDataCallback<IDataContainer>
	{
		public IDataContainer ExpectedValue { get; set; }
		public String expectedError { get; set; }
		private Boolean operationDone;
		public TestAssetCB ( IDataContainer eXpectedValue, ref Boolean operationDone )
		{
			ExpectedValue = eXpectedValue;
			this.operationDone = operationDone;
		}

		public void onObtainData ( IDataContainer data )
		{
			
			Log.i ( "onObtainData" + ExpectedValue + " == " + data );
			Assert.AreEqual ( ExpectedValue.ToString(), data.ToString() );
			operationDone = true;
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
			operationDone=true;
		}
	}
}
