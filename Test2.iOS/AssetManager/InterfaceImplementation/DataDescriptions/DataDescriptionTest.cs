
using System;
using Data.Description.DomainAssociator;
using LibDataService.DataModels.Description;
using NUnit.Framework;

namespace Test.iOS
{
    [TestFixture]
    public class DataDescriptionTest
    {
		IDataDescription test_Object=DomainAssociator.Asset.Example.ComplexDynDomainExampleAssetDomain;
		//IDataDescription test_Object = DomainAssociator.Setting.Example.BasicBoolData;

        [Test]
        public void DomainConsturction ()
        {
			Domain_TestObject domain_test = new Domain_TestObject ( test_Object );
			Assert.True (
				domain_test.IImpl_IDomainConstruction ()
				||
				domain_test.IImpl_IDomainConstructionField () );
        }

		[Test]
		public void Cacheability () 
		{
			Cacheable_TestObject caching_test = new Cacheable_TestObject ( test_Object );
			Assert.True (
				caching_test.IImpl_ICacheData ()
				&&
				(caching_test.IImpl_ICachData_getValidSpan () >-1)
			);
		}

		[Test]
		public void Settingsability ()
		{
			Settingsable_TestObject setting_test = new Settingsable_TestObject ( test_Object );
			Assert.True (setting_test.IImpl_ISettingsData ());
		}

		[Test]
		public void Cloudability ()
		{
			Settingsable_TestObject setting_test = new Settingsable_TestObject ( test_Object );
			Assert.True ( setting_test.IImpl_ISettingsData () );
		}

    }
}